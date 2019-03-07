import base64
import hmac
import hashlib
from datetime import datetime
from time import mktime
from wsgiref.handlers import format_date_time

class Authenticator(object):

    def __init__(self, app_id, app_secret, tenant_secret, digestmod=hashlib.sha1):
        self.app_id = app_id
        self.digestmod = digestmod

        combined = app_secret + tenant_secret        
        combined = hashlib.sha512(combined).digest()
        self.combined_secret = combined


    def __call__(self, request):
        request_dt = mktime(datetime.now().timetuple())
        request_dt = format_date_time(request_dt)

        if request.method == 'POST':
            token = self.getAuthToken(request.headers['Content-Type'], request.body, request.path_url, request_dt)
            # if request.body is not None:
            #     Create http request headers
            #     request.headers['Content-Type'] = 'application/json'
            #     pass
        else:
            token = self.getAuthToken('', '', request.path_url, request_dt)

        request.headers['Date'] = request_dt
        request.headers['Authorization'] = f'APIAuth {token}'

        return request

    def getAuthToken(self, contentType, bodyMD5, url, date):
        # 1. Build canonical string
        if bodyMD5 is None:
            bodyMD5 = ''
        canonicalString = f'{contentType},{bodyMD5},{url},{date}'
        
        # 2. Encrypt canonical string
        encryptedCanonicalString = hmac.new(
                self.combined_secret,
                canonicalString.encode('utf-8'),
                digestmod=self.digestmod).digest()

        token = self.app_id+':'+base64.b64encode(encryptedCanonicalString).decode('utf-8')
        return token