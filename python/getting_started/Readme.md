# hcOS Python Examples

hcOS Examples is a collection of code examples and documentation to help you get started using the hcOS Platform quickly.

## Development environment

These examples are written in python3 using virtualenv. You can install the required dependencies in your python3 environment by running

```bash
$ pip install -r requirements.txt
```

## Search Example

The [search_example.py](search_example.py) demonstrates how to issue queries to hcOS's search engine. The search engine API provides a powerful tool to build applications on top of unstructured document text with Natural Language Processed (NLP) annotation or DICOM imaging data stored in hcOS.

In the [search_example.py](search_example.py) you will learn how to:

* Load configuration information from [configuration_searching.json](#configuration_searchingjson)
* Create http request headers
* Issue various **hcOS search examples**
* Sign http requests with HMAC
* Process hcOS result sets

You wil need to obtain configuration information and save it to project directory.

### configuration_searching.json

```JSON
{
    "name" : "Configuration name",
    "baseUrl" : "https://hostname",
    "appId":"An application id",
    "appSecret":"Base64 encoded application secret",
    "tenantId" : "A tenant id"
}
```

*Note: You wil need to obtain configuration information and save it to project directory.*

## Document Retrieval Example

The [document_example.py](document_example.py) demonstrates how to retrieve documents from hcOS's Natural Language Processed (NLP) document repository. The document API provides a powerful tool to build applications on top of unstructured document text with Natural Language Processed (NLP) annotation.

In the [document_example.py](document_example.py) you will learn how to:

* Load configuration information from [configuration_documents.json](#configuration_documentsjson)
* Create http request headers
* Retrieve a document from hcOS
* Sign http requests with HMAC
* Process hcOS result sets

### configuration_documents.json

```JSON
{
    "name" : "Configuration name",
    "baseUrl" : "https://hostname",
    "appId":"An application id",
    "appSecret":"Base64 encoded application secret",
    "tenantId" : "A tenant id",
    "tenantSecret": "Base64 encoded tenant secret"
}
```

*Note: You wil need to obtain configuration information and save it to project directory.*
