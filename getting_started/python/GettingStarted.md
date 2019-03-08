
# Getting started with hcOS Search and Document Retrieval

## Introduction

In this Getting Started guide you will learn how to:

* [Load hcOS API configurations](#LoadhcOSAPIconfigurations) for:
  * [hcOS Search API Configuration](#hcOSSearchAPIConfiguration)
  * [hcOS Document API Configuration](#hcOSDocumentAPIConfiguration)
* [Create hcOS API requests](#CreatehcOSAPIrequests) to:
  * [Create, Sign and Issue hcOS API Requests](#CreateSignhandIssuehOSAPIRequests):
  * [Process hcOS Search Results](#ProcesshcOSSearchResults)
  * [Retrieve hcOS Documents](#RetrievehcOSDocuments)

The hcOS Search APIs are extremely powerful and support a wide variety [search use cases](../../data/Searches.json). You can view the completed [GettingStarted.py](GettingStarted.py) if you'd like to skip ahead to the end.

## Load hcOS API configurations

The hcOS Search and Document APIs are secured behind a cryptography standard known as [HMAC](https://en.wikipedia.org/wiki/HMAC). We're also adding support for [Oauth2 Client Credentials](https://www.oauth.com/oauth2-servers/access-tokens/client-credentials/) , but this guide will focus exclusively on signing requests with HMAC. *Note: You will need to obtain configuration information for your specific application and tenant*

In order to access hcOS APIs your application needs 2 critical pieces of information:

1. Application Credentials
2. Tenant Credentials

**Application Credentials** identify your application uniquely across all members of an hcOS tenant. Whereas **Tenant Credentials** limit the scope of your application access to a specific dataset within hcOS. The hcOS platform is a [multitenant cloud](https://searchcloudcomputing.techtarget.com/definition/multi-tenant-cloud) platform. Tenancy is usually determined by a business associates agreement between the data owner (e.g. provider or payor organization) and the data consumer (e.g. the applications running on the tenant).

The way in which you sign hcOS API requests between the hcOS Search and hcOS Document APIs is slightly different with the main difference being the hcOS Document API uses an additional ```tenantSecret``` parameter. Both configurations and signing methods will be explained and fully functional sample code for signing API requests exist in this repository. The configuration files for [hcOS Search API](#hcOSSearchAPIConfiguration) and the [hcOS Document API](#hcOSDocumentAPIConfiguration) are shown next.

### hcOS Search API Configuration

```JSON
{
    "name" : "Configuration name",
    "baseUrl" : "https://hostname",
    "appId":"An application id",
    "appSecret":"Base64 encoded application secret",
    "tenantId" : "A tenant id"
}
```

### hcOS Document API Configuration

```JSON
{
    "name" : "Configuration name",
    "baseUrl" : "https://hostname",
    "appId":"An application id",
    "appSecret":"Base64 encoded application secret",
    "tenantId" : "A tenant id",
    "tenantSecret": "Base64 encoded application secret"
}
```

In general, your application pseudo will look something like:

```python
# load api configuration
# create hcOS API https request
# issue hcOS API request
# process hcOS API results
```

### Loading hcOS Configurations

We typically store the API configurations in files (or environment variables) so we can automate deployments across environments. Here, we load both hcOS Search API and hcOS Document API configurations because our [app](GettingStarted.py) will both search for documents and retrieve the documents which are part of the result set. Since our configuration is stored in json files, loading the configuration is as simple as:

```python
# load search configuration information
with open('../../configurations/Configuration.Searching.json') as f:
    search_config = json.load(f)

with open('../../configurations/Configuration.Documents.json') as f:
    document_config = json.load(f)
```

Once loaded, we perform some decoding of the configuration elements because the *secrets* are stored as **Strings** in the json file, but the HMAC algorithm requires **utf-8** encoded byte arrays.

For the hcOS Search API we convert the ```appSecret``` String object to a **utf-8** byte array using the builtin ```bytes``` method.

```python
search_app_id = search_config['appId']
search_app_secret = bytes(search_config['appSecret'], 'utf-8')
search_tenant_id = search_config['tenantId']
```

For the hcOS Document API we use a slightly different method of conversion where we  **base64 decode** the string in the configuration file.

```python
import base64

document_app_id = document_config['appId']
document_app_secret = base64.b64decode(document_config['appSecret'])
document_tenant_id = document_config['tenantId']
document_tenant_secret = base64.b64decode(document_config['tenantSecret'])
```

*Note: Here we import the base64 package and include the ```tenantSecret```*

### Create hcOS API requests

hcOS APIs are web services which can be consumed using any standard http library. Just like any http request you'll need to setup the http headers, url parameters, and message body accordingly.

Every request begins with a url which we'll call a ```resource``` the resource is a url that provides a specific set of functionality. We'll interact with two resources in this demo.

The search resource we'll use is:

```python
search_resource = f'{search_config["baseUrl"]}/api/v1/{search_tenant_id}/document/search'
```

where the ```baseUrl``` represents the hcOS environment you're using (PRD or STG) and the ```tenant_id``` represents the specific tenant that your application is allowed to access.

The document resource we'll use is:

```python
document_resource = f'{document_config["baseUrl"]}/api/v1/patient_document/{document_root}/{document_extension}'
```

where the ```baseUrl``` represents the hcOS environment you're using (PRD or STG) and the pair```document_root/document_extension``` represent a unique identifier to retrieve a single document.

#### Create, Sign and Issue hcOS API Requests

Now that we have the resources for the APIs we're going to consume established, we'll build the https request, sign the request with the appropriate HMAC algorithm, and issue the request.

Since we're issuing a search with complex criteria, the hcOS Search API is a http POST request requiring both request headers and a request body. The request headers specify that the POST body will contain json data representing the hcOS Search criteria.

```python
# create search http request headers
search_headers = {
    'Content-Type': 'application/json',
    'user_root': 'UserRoot',
    'user_extension': 'UserExtension'
}
```

The hcOS Search API supports a wide range of free text and structured query parameters. Some examples can be found [here](../../data/Searches.json). Here, we'll take a single query from the collection to use as an example. This query searches over 200 million clinical documents for the literal text 'ulcerative colitis'.

```python
query = { "criterion": "literal='ulcerative colitis'" }
```

we can then formulate and issue the request using the ```requests``` library

```python
import json
import requests
from searching import Authenticator as SearchAuthenticator

query = { "criterion": "literal='ulcerative colitis'" }
search_request = requests.Request('POST',
    search_resource,
    data=json.dumps(query),
    headers=search_headers,
    auth=SearchAuthenticator.Authenticator(search_app_id, search_app_secret))
```

Note that we're converting the query to a json object using ```json.dumps(query)``` and specifying a custom authentication method ```auth=SearchAuthenticator.Authenticator(search_app_id, search_app_secret))``` that will sign the request with the appropriate [hcOS Search API HMAC algorithm](searching/Authenticator.py).

Issuing the response and checking for a valid status is as easy as:

```python
with requests.Session() as search_session:
    search_response = search_session.send(search_request.prepare())

if search_response.status_code == 200:
    # Process hcOS Search Results
else:
    # print error
```

### Process hcOS Search Results

The hcOS Search API returns results contained within a json object. The field ```hits``` contains an array of the results. In python we can ```enumerate``` over these results getting an ```index``` and ```search_entry``` object that contains each item in the array.

```python
# iterate over the results and retrieve documents
for index, search_entry in enumerate(search_response_data['hits']):
    print(f"{index} {search_entry['document_root']}-{search_entry['document_extension']}-{search_entry['document_type_extension']}")

    # Retrieve hcOS Documents
```

### Retrieve hcOS Documents

The final step of this demo is to parse out the ```document_root``` and ```document_extension``` from the ```search_entry``` result item and use that information to retrieve the document.

The ```document_root``` and ```document_entry``` uniquely identify a document inside of hcOS. These values become part of the resource url when making the https request.

```python
# Documents tenant configuration info
document_root = search_entry['document_root']
document_extension = search_entry['document_extension']

# Getting current document for given document root&extension
document_resource = f'{document_config["baseUrl"]}/api/v1/patient_document/{document_root}/{document_extension}'
```

Once the resource url has been established, we can issue a http GET request. Since this is a GET request the ```document_params``` are passed into the ```params``` parameter. The[hcOS Document API HMAC algorithm](documents/Authenticator.py) uses a different algorithm than the [hcOS Search API HMAC algorithm](searching/Authenticator.py).

```python
document_params = {
    'user[root]': 'UserRoot',
    'user[extension]': 'UserExtension',
    'tid': document_config['tenantId']
}

# Create and execute request with HMAC
document_request = requests.Request('GET',
    document_resource,
    params=document_params,
    auth=DocumentAuthenticator.Authenticator(document_app_id, document_app_secret, document_tenant_secret))

with requests.Session() as document_session:
    document_response = document_session.send(document_request.prepare())
```

Finally, we can process the results pulling out key information from the ```document_response_data``` which is a decoded json payload.

```python
if document_response.status_code == 200:
    document_response_data = document_response.json()
    print(f'\tdocument_root: {document_response_data["document_root"]}')
    print(f'\tdocument_extension: {document_response_data["document_extension"]}')
    print(f'\tdocument_type-description: {document_response_data["document_type_description"]}')
else:
    print(f'\tstatus: {document_response_data.status_code} - {document_response_data.reason}')
```

We hope you enjoyed this tutorial. You can request API keys from hcOS and use the [working example](GettingStarted.py) to get started innovating.
