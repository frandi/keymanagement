using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace keymanagement
{
    public class KeyManager
    {
        private static string ClientId => ConfigurationManager.AppSettings["ClientId"];
        private static string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
        private static string KeyVaultUri => ConfigurationManager.AppSettings["KeyVaultUri"];

        public static string Add(string keyName, string keyValue)
        {
            var client = new KeyVaultClient(GetToken);
            var result = client.SetSecretAsync(KeyVaultUri, keyName, keyValue).Result;

            return result.Id;
        }

        public static string Get(string keyName)
        {
            var client = new KeyVaultClient(GetToken);
            var sec = client.GetSecretAsync(KeyVaultUri, keyName).Result;

            return sec.Value;
        }

        public static string Delete(string keyName)
        {
            var client = new KeyVaultClient(GetToken);
            var result = client.DeleteSecretAsync(KeyVaultUri, keyName).Result;
            
            return result.RecoveryId;
        }

        private static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(ClientId, ClientSecret);
            var result = await authContext.AcquireTokenAsync(resource, clientCred);
            if (result == null)
                throw new InvalidOperationException("Failed to obtain JWT token");

            return result.AccessToken;
        }
    }
}
