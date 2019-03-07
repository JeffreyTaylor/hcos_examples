using RestSharp;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using RestSharp.Authenticators;
namespace hcos.getting_started.searching
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
        /// Tenant Id
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Contructor initialization using appName and secret.
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="secret"></param>
        public Authenticator(string appName, string appSecret, string tenantId)
        {
            AppId = appName;
            AppSecret = appSecret;
            TenantId = tenantId;
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
            Uri finalUri = client.BuildUri(request);
            string algorithm = "hmac-sha1";

            string signingBase = $"path: {finalUri.PathAndQuery}\ndate: {requestDT.ToString("R")}";

            byte[] secretBytes = Encoding.ASCII.GetBytes(AppSecret);

            string signature;

            using (var hmac = new HMACSHA1(secretBytes))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(signingBase));
                signature = Convert.ToBase64String(hash);
            }

            request.AddOrUpdateParameter("Date", requestDT.ToString("R"), ParameterType.HttpHeader);
            request.AddOrUpdateParameter("Path", finalUri.PathAndQuery, ParameterType.HttpHeader);
            request.AddOrUpdateParameter("Authorization", $"hmac username=\"{AppId}\",algorithm=\"{algorithm}\",headers=\"path date\",signature=\"{signature}\"", ParameterType.HttpHeader);
        }
        #endregion
    }
}