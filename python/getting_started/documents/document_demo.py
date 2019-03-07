import base64
import json
import requests
import traceback
from requests_toolbelt.utils import dump
import Authenticator as DocumentAuthenticator

def demo():
    print('Documents demo begins...')

    # Load configuration information
    with open('../../configurations/Configuration.Documents.json') as f:
        config = json.load(f)

    app_id = config['appId']
    app_secret = base64.b64decode(config['appSecret'])
    tenant_id = config['tenantId']
    tenant_secret = base64.b64decode(config['tenantSecret'])

    # Documents tenant configuration info
    document_root = 'MIMIC.DOCUMENT.OID'
    document_extension = '1'

    # Getting current document for given document root&extension
    resource = f'{config["baseUrl"]}/api/v1/patient_document/{document_root}/{document_extension}'

    params = {
        'user[root]': 'UserRoot',
        'user[extension]': 'UserExtension',
        'tid': config['tenantId']
    }

    try:
        # Create and execute request with HMAC
        request = requests.Request('GET',
                                resource,
                                params=params,
                                auth=DocumentAuthenticator.Authenticator(app_id, app_secret, tenant_secret))

        with requests.Session() as session:
            response = session.send(request.prepare())
            # data = dump.dump_all(response)
            # print(data.decode('utf-8'))

        if response.status_code == 200:
            response_data = response.json()
            print(f'document_root: {response_data["document_root"]}')
            print(f'document_extension: {response_data["document_extension"]}')
            print(f'document_type-description: {response_data["document_type_description"]}')
        else:
            print(f'status: {response.status_code} - {response.reason}')
    except Exception as e: 
        print(e)

    print('Documents demo ends...')

if __name__ == '__main__':
    demo()
