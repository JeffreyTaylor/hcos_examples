using RestSharp;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using RestSharp.Authenticators;

namespace hcos.getting_started.documents
{
    /// <summary>
    /// HMAC implementation of Restsharp IAuthenticator.
    /// </summary>
    public class Authenticator : IAuthenticator
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Application secret.
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// Application secret.
        /// </summary>
        public string TenantSecret { get; set; }

        /// <summary>
        /// Combined secret.
        /// </summary>
        private string CombinedSecret{get;set;}
        /// <summary>
        /// Default constructor. For deserialization. 
        /// </summary>
        public Authenticator()
        {
            // to be used by deserialization and subclasses that need new constructors. 
        }

        /// <summary>
        /// Contructor initialization using appName and secret.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        public Authenticator(string appId, string appSecret, string tenantSecret)
        {
            AppId = appId;
            AppSecret = appSecret;
            TenantSecret = tenantSecret;
            CombinedSecret = CalculateSecretForTenant(AppSecret,TenantSecret);
        }
        #region RestSharp.IAuthenticator
        /// <summary>
        /// Method implementing point of entry for RestSharp authentication.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        public void Authenticate(IRestClient client, IRestRequest request)
        {
            DateTime requestDT = DateTime.Now.ToUniversalTime();
            string authHeader = null;
            request.AddOrUpdateParameter("Date", requestDT.ToString("R"), ParameterType.HttpHeader);
            Uri finalUri = client.BuildUri(request);

            if (request.Method == Method.POST)
            {
                MD5 md5 = MD5.Create();
                string postData = request.Parameters.Single(p => p.Name == "application/json").Value.ToString();
                string contentMD5 = postData == null ? "" : GetMd5(md5, postData);
                if (contentMD5 != "") { request.AddOrUpdateParameter("Content-MD5", contentMD5, ParameterType.HttpHeader); }

                authHeader = GetAuthHeader("application/json", contentMD5, finalUri.PathAndQuery, requestDT);
            }
            else
            {
                authHeader = GetAuthHeader("", "", finalUri.PathAndQuery, requestDT);
            }
            //HMAC Header
            request.AddOrUpdateParameter("Authorization", string.Format("APIAuth {0}", authHeader),ParameterType.HttpHeader);
        }
        #endregion

        private string CalculateSecretForTenant(string appSecret, string tenantSecret)
        {
            string newSecret = null;
            SHA512 shah512 = SHA512.Create();
            byte[] appSecretBytes = Convert.FromBase64String(appSecret);
            byte[] tenantSecretBytes = Convert.FromBase64String(tenantSecret);

            if ((tenantSecretBytes != null) && (tenantSecretBytes.Length != 0))
            {
                byte[] newSecretBytes = new byte[appSecretBytes.Length + tenantSecretBytes.Length];
                Buffer.BlockCopy(appSecretBytes, 0, newSecretBytes, 0, appSecretBytes.Length);
                Buffer.BlockCopy(tenantSecretBytes, 0, newSecretBytes, appSecretBytes.Length, tenantSecretBytes.Length);
                byte[] newSecretSha = shah512.ComputeHash(newSecretBytes);
                newSecret = Convert.ToBase64String(newSecretSha);
            }
            return newSecret;
        }
        /// <summary>
        /// Builds canonical string used by this IAuthenticator implementation.
        /// </summary>
        /// <param name="contentType">Post/Update content type. Null if transaction is not Post/Update.</param>
        /// <param name="bodyMd5">MD5 of Post/Update body. Null if transaction is not Post/Update.</param>
        /// <param name="url">Endpoit url without the hostname.</param>
        /// <param name="timestamp">TimeStamp of transaction.</param>
        /// <returns>Canonical string</returns>
        private string GetCanonicalString(string contentType, string bodyMd5, string url, DateTime timestamp)
        {
            var timestampFormatted = string.Format("{0}", timestamp.ToString("R")); //.ToString("ddd, dd MMM yyyy hh:mm:ss")
            return string.Format("{0},{1},{2},{3}", contentType, bodyMd5, url, timestampFormatted);
        }
        /// <summary>
        /// Concatenates appName an HMACSignature into Authentication header value.
        /// </summary>
        /// <param name="appName">HMC application name.</param>
        /// <param name="hMacSignature">HMAC signature</param>
        /// <returns>formated header.</returns>
        private string GetHeader(string appName, string hMacSignature)
        {
            if (String.IsNullOrEmpty(appName)) throw new ArgumentException("Missing required parameter: appName");
            if (String.IsNullOrEmpty(hMacSignature)) throw new ArgumentException("Missing required parameter: hMacSignature");

            return string.Format("{0}:{1}", appName, hMacSignature);
        }
        /// <summary>
        /// Calculates HMAC for a given string value.
        /// </summary>
        /// <param name="value">string value to calculate HMAC on.</param>
        /// <returns></returns>
        private string GetHMAC(string value)
        {
            return GetHMAC(Encoding.UTF8.GetBytes(value));
        }
        /// <summary>
        /// Calculates HMAC for a bytes array.
        /// </summary>
        /// <param name="valueBytes">array of bytes to calculate HMAC on.</param>
        /// <returns>Base64 encoded signature</returns>
        private string GetHMAC(byte[] valueBytes)
        {
            if (string.IsNullOrEmpty(CombinedSecret)) throw new ArgumentException("Expected private key not found!");
            if (valueBytes == null) throw new ArgumentException("Expected value to encrypt not found!");

            var secretBytes = System.Convert.FromBase64String(CombinedSecret);//Encoding.UTF8.GetBytes(Secret);
            string signature;

            using (var hmac = new HMACSHA1(secretBytes))
            {
                var hash = hmac.ComputeHash(valueBytes);
                signature = Convert.ToBase64String(hash);
            }

            return signature.Replace("\n", "\\n");
        }
        /// <summary>
        /// Process information into Authentication header value.
        /// </summary>
        /// <param name="requestContentType"></param>
        /// <param name="requestBodyMd5"></param>
        /// <param name="requestUrl"></param>
        /// <param name="requestTs"></param>
        /// <returns></returns>
        private string GetAuthHeader(string requestContentType, string requestBodyMd5, string requestUrl, DateTime requestTs)
        {
            string finalizedAuthHeader = "";

            //1. Build canonical string
            var canonicalString = GetCanonicalString(requestContentType, requestBodyMd5,
                                                                             requestUrl, requestTs);
            //Console.WriteLine($"canonicalString: {canonicalString}");

            //2. Encrypt canonical string
            var encryptedCanonicalString = GetHMAC(canonicalString);
            //Console.WriteLine($"encryptedCanonicalString: {encryptedCanonicalString}");

            //3. Create Auth Header
            finalizedAuthHeader = GetHeader(AppId, encryptedCanonicalString);
            //Console.WriteLine($"finalizedAuthHeader: {finalizedAuthHeader}");

            return finalizedAuthHeader;
        }
        /// <summary>
        /// Return MD5 has for a given string.
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMd5(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash. 
            byte[] hash = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}