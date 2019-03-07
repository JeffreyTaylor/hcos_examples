import base64
import json
import requests
import traceback
from requests_toolbelt.utils import dump
import Authenticator as SearchAuthenticator

def demo():
    print('Search demo begins...')

    # load search configuration information
    with open('../../configurations/Configuration.Searching.json') as f:
        config = json.load(f)

    app_id = config['appId']
    app_secret = bytes(config['appSecret'], 'utf-8')
    tenant_id = config['tenantId']
    resource = f'{config["baseUrl"]}/api/v1/{tenant_id}/document/search'

    # create http request headers
    headers = {
        'Content-Type': 'application/json',
        'user_root': 'UserRoot',
        'user_extension': 'UserExtension'
    }


    # load searches
    with open('../../data/Searches.json') as f:
        search_examples = json.load(f)

    for example in search_examples:
        print('----------------------------------------------------------------------')
        print(example["description"])

        try:
            # Create and execute request signed with HMAC
            request = requests.Request('POST',
                                       resource,
                                       data=json.dumps(example['query']),
                                       headers=headers,
                                       auth=SearchAuthenticator.Authenticator(app_id, app_secret))

            with requests.Session() as session:
                response = session.send(request.prepare())
                # data = dump.dump_all(response)
                # print(data.decode('utf-8'))

            # Process hcOS result sets
            if response.status_code == 200:
                response_data = response.json()
                print(f'offset: {response_data["offset"]}')
                print(f'record_count: {response_data["record_count"]}')
                print(f'total_record_count: {response_data["total_record_count"]}')
                for index, entry in enumerate(response_data['hits']):
                    print(f"{index} {entry['document_root']}-{entry['document_extension']}-{entry['document_type_extension']}")
                    # print(f'\t{entry}')
            else:
                print(f'status: {response.status_code} - {response.reason}')
        except Exception as e:
            print(traceback.print_exc())

    print("Search demo ends...")

if __name__ == '__main__':
    demo()
