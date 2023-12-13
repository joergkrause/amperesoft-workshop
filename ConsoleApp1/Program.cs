// See https://aka.ms/new-console-template for more information
using Azure.Core;
using Azure.Identity;
using ConsoleApp1;
using Microsoft.Graph;

Console.WriteLine("Hello, World!");
string[] scopes = [".default"];

var credentials = new ClientSecretCredential(
     "192923d6-b12b-43a6-b61b-10ae42e61795",
     "0dcc1ca3-6e4f-414a-83e5-7916897b8fcb",
     "1Ll8Q~7f65RFrxGNmW2ecuuCI2Iu412iR5utlarx",
     new TokenCredentialOptions
     {
       AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
       Retry =
       {
               Delay= TimeSpan.FromSeconds(2),
               MaxDelay = TimeSpan.FromSeconds(16),
               MaxRetries = 5,
               Mode = RetryMode.Exponential
       }
     }
);

GraphServiceClient graphClient = new(credentials, scopes);

var user = await graphClient.Users["1a41da2d-682c-4909-807e-dba0fce591d0"].GetAsync();

Console.WriteLine(user.DisplayName);
Console.WriteLine(user.UserPrincipalName);

Console.ReadLine();