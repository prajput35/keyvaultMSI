using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keyvaultMSI
{
    class Program
    {
        static string keyvaultURI = "https://vault204.vault.azure.net/secrets/dbpassword";
        

        static void Main(string[] args)
        {
           GetSecret().GetAwaiter().GetResult();
        }

        static public async Task GetSecret()
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();

            var keyVaultClient = new KeyVaultClient(
          new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));

            var secret = await keyVaultClient.GetSecretAsync(keyvaultURI)
                .ConfigureAwait(false);

            Console.WriteLine(secret.Value);
            Console.ReadKey();
        }
    }
}
