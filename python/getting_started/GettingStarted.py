import base64
import json
import requests
import traceback
from requests_toolbelt.utils import dump
from searching import Authenticator as SearchAuthenticator
from documents import Authenticator as DocumentAuthenticator

def demo():
    print('Getting Started demo begins...')

    # load search configuration information
    with open('../../configurations/Configuration.Searching.json') as f:
        search_config = json.load(f)

    search_app_id = search_config['appId']
    search_app_secret = bytes(search_config['appSecret'], 'utf-8')
    search_tenant_id = search_config['tenantId']
    search_resource = f'{search_config["baseUrl"]}/api/v1/{search_tenant_id}/document/search'

    # Load document configuration information
    with open('../../configurations/Configuration.Documents.json') as f:
        document_config = json.load(f)

    document_app_id = document_config['appId']
    document_app_secret = base64.b64decode(document_config['appSecret'])
    document_tenant_id = document_config['tenantId']
    document_tenant_secret = base64.b64decode(document_config['tenantSecret'])

    # create search http request headers
    search_headers = {
        'Content-Type': 'application/json',
        'user_root': 'UserRoot',
        'user_extension': 'UserExtension'
    }

    # create standard document url params
    document_params = {
        'user[root]': 'UserRoot',
        'user[extension]': 'UserExtension',
        'tid': document_config['tenantId']
    }

    try:
        # load searches
        with open('../../data/Searches.json') as f:
            search_examples = json.load(f)

        search_example = search_examples[0]
        print(search_example["description"])
        # Create and execute request signed with HMAC
        search_request = requests.Request('POST',
                                    search_resource,
                                    data=json.dumps(search_example['query']),
                                    headers=search_headers,
                                    auth=SearchAuthenticator.Authenticator(search_app_id, search_app_secret))

        with requests.Session() as search_session:
            search_response = search_session.send(search_request.prepare())
            # data = dump.dump_all(search_response)
            # print(data.decode('utf-8'))

        # Process hcOS result sets
        if search_response.status_code == 200:
            search_response_data = search_response.json()
            print(f'offset: {search_response_data["offset"]}')
            print(f'record_count: {search_response_data["record_count"]}')
            print(f'total_record_count: {search_response_data["total_record_count"]}')

            # iterate over the results and retrieve documents
            for index, search_entry in enumerate(search_response_data['hits']):
                print(f"{index} {search_entry['document_root']}-{search_entry['document_extension']}-{search_entry['document_type_extension']}")

                # Documents tenant configuration info
                document_root = search_entry['document_root']
                document_extension = search_entry['document_extension']

                # Getting current document for given document root&extension
                document_resource = f'{document_config["baseUrl"]}/api/v1/patient_document/{document_root}/{document_extension}'
                
                # Create and execute request with HMAC
                document_request = requests.Request('GET',
                    document_resource,
                    params=document_params,
                    auth=DocumentAuthenticator.Authenticator(document_app_id, document_app_secret, document_tenant_secret))

                with requests.Session() as document_session:
                    document_response = document_session.send(document_request.prepare())
                    # document_data = dump.dump_all(document_response)
                    # print(document_data.decode('utf-8'))

                if document_response.status_code == 200:
                    document_response_data = document_response.json()
                    print(f'\tdocument_root: {document_response_data["document_root"]}')
                    print(f'\tdocument_extension: {document_response_data["document_extension"]}')
                    print(f'\tdocument_type-description: {document_response_data["document_type_description"]}')
                else:
                    print(f'\tstatus: {document_response_data.status_code} - {document_response_data.reason}')
        else:
            print(f'status: {search_response.status_code} - {search_response.reason}')
    except Exception as e:
        print(traceback.print_exc())

    print("Getting Started demo ends...")

if __name__ == '__main__':
    demo()
    