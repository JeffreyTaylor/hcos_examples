const request = require('request');
const authenticator = require('./authenticator');

// load config
const config = require('../../configurations/Configuration.Documents.json');

// app credentials
const appId = config.appId,
  appSecret = new Buffer(config.appSecret).toString('base64'),
  tenantId = config.tenantId,
  tenantSecret = new Buffer(config['tenantSecret']).toString('base64');

// example document information
const documentRoot = 'MIMIC.DOCUMENT.OID',
  documentExtension = '1';

// example document params
const params = {
  'user[root]': 'UserRoot',
  'user[extension]': 'UserExtension',
  'tid': config.tenantId
};

function demo() {

  console.log('Getting Started demo begins...');

  request.get({
    url: `${config.baseUrl}/api/v1/patient_document/${documentRoot}/${documentExtension}`,
    qs: params,
    headers: authenticator(appId, appSecret, tenantId)
  })
  .on('response', function (response) {

    if (response.statusCode === 200) {
      console.log(`document_root: ${response_data.document_root}`);
      console.log(`document_extension: ${response_data.document_extension}`);
      console.log(`document_type-description: ${response_data.document_type_description}`)
    } else {
      console.log('error');
      console.log(response.statusCode);
      console.log(response.reason);
    }
  })
  .on('error', function (error) {
    console.log('error');
    console.log(error);
  });
}

demo();