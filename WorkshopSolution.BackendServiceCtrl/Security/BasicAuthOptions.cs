using Microsoft.AspNetCore.Authentication;

internal class BasicAuthOptions : AuthenticationSchemeOptions
{
    public string Realm { get; set; }

  // username / Password
}