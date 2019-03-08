import base64
import hmac
import hashlib
from datetime import datetime
from time import mktime
from wsgiref.handlers import format_date_time

class Authenticator(object):

    def __init__(self, app_id, app_secret, algorithm='hmac-sha1', digestmod=hashlib.sha1):
        self.app_id = app_id
        self.app_secret = app_secret
        self.algorithm = algorithm
        self.digestmod = digestmod

    def __call__(self, request):
        # body = request.body
        # if body and not isinstance(body, bytes):   # Python 3
        #     body = body.encode('utf-8')  # standard encoding for HTTP

        request_dt = mktime(datetime.now().timetuple())
        request_dt = format_date_time(request_dt)
        signing_base = f'path: {request.path_url}\ndate: {request_dt}'

        signature = hmac.new(
            self.app_secret,
            signing_base.encode('utf-8'),
            digestmod=self.digestmod).digest()
        signature = base64.b64encode(signature).decode('utf-8')

        request.headers['Date'] = request_dt
        request.headers['Path'] = request.path_url
        request.headers['Authorization'] = f'hmac username="{self.app_id}",algorithm="{self.algorithm}",headers="path date",signature="{signature}"'

        return request