using System.Security.Cryptography;
using System.Text;

namespace PortfolioTracker.Common.Helpers
{
    public static class PlatformRequestSigner
    {
        /// <summary>
        /// Creates API-Sign that can be used to authenticate HTTP requests on Kraken platform
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="nonce"></param>
        /// <param name="uriPath"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static string GetKrakenSignature(string privateKey, long nonce, string uriPath, Dictionary<string, string> data)
        {
            //HMAC-SHA512 of (URI path + SHA256(nonce + POST data)) and base64 decoded secret API key
            byte[] decodedPrivateKey = Convert.FromBase64String(privateKey);
            string postData = string.Join("&", data.Select(i => i.Key + "=" + i.Value));
            byte[] binData = Encoding.UTF8.GetBytes(nonce + postData);
            SHA256 sha256 = SHA256.Create();
            byte[] shaHashValue = sha256.ComputeHash(binData);

            byte[] msg = Encoding.UTF8.GetBytes(uriPath).Concat(shaHashValue).ToArray();
            HMACSHA512 hmac = new HMACSHA512(decodedPrivateKey);
            byte[] result = hmac.ComputeHash(msg);

            return Convert.ToBase64String(result);
        }

    }
}
