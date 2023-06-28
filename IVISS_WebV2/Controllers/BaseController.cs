using IVISS_WebV2.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators.Digest;
using RestSharp.Serializers.Utf8Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace IVISS_WebV2.Controllers
{
    public class BaseController: Controller
    {
        public string BaseUrl = "http://localhost:4000/";

        public string AlprUrl = "http://localhost:8080/myapp/";

        public static string RefreshTokeCookie = "";
        public static string PublicKeyXml = "";

      
        public string RefreshToken()
        {


           


            var client = new RestClient(BaseUrl + "users/refresh-token");
            RestRequest request = new RestRequest(Method.POST);

            request.AddHeader("Cookie", "refreshToken=" + BaseController.RefreshTokeCookie + "");

            IRestResponse<List<string>> response = client.Execute<List<string>>(request);

            if (response != null)
            {
                if (response.Content != null)
                {
                    if (response.Content.Contains("message"))
                    {
                        return "";
                    }
                    else
                    {
                        var loginResponse = JsonSerializer.Deserialize<LoginResponseModel>(response.Content);
                        if (loginResponse != null)
                        {
                            BaseController.RefreshTokeCookie = response.Cookies[0].Value;
                            return loginResponse.jwtToken;
                        }
                        else
                            return "";
                    }
                }
                else

                {
                    return "";
                }

            }
            else
            {
                return "";
            }
        }

        public static string Encryption(string strText, string publicKey)
        {
            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, false);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
