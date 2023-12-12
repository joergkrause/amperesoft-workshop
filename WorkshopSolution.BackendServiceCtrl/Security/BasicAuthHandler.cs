using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using WorkshopSolution.Persistence;

internal class BasicAuthHandler : AuthenticationHandler<BasicAuthOptions>
{

  private readonly IConfiguration _configuration;
  private readonly IUserContext _userContext;

  public BasicAuthHandler(
    IOptionsMonitor<BasicAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, IConfiguration configuration, IUserContext userContext
    ) : base(options, logger, encoder)
  {
    _configuration = configuration;
    _userContext = userContext;
  }

  protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
  {
    if (!Request.Headers.ContainsKey("Authorization"))
    {
      return AuthenticateResult.Fail("Missing Authorization Header");
    }
    if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
    {
      return AuthenticateResult.Fail("Invalid Authorization Header");
    }
    if (!"Basic".Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
    {
      return AuthenticateResult.NoResult();
    }
    // USERNAME:PASSWORD
    var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(headerValue.Parameter ?? string.Empty)).Split(':', 2);
    if (credentials.Length != 2)
    {
      return AuthenticateResult.Fail("Invalid Authorization Header");
    }
    var username = credentials[0];
    var password = credentials[1];
    // v1 Statisch (Dienst)
    if (username != _configuration["BasicAuth:Username"] || password != _configuration["BasicAuth:Password"])
    {
      return AuthenticateResult.Fail("Invalid Username or Password");
    }
    // v2 Dynamisch (User)
    //if (!Options.Credentials.TryGetValue(username, out var expectedPassword))
    //{
    //  return AuthenticateResult.Fail("Invalid Username or Password");
    //}
    var userName = Request.Headers["X-UserName"];

    var claims = new[]
    {
      new Claim(ClaimTypes.NameIdentifier, userName),
      new Claim(ClaimTypes.Name, userName),
    };
    var identity = new ClaimsIdentity(claims, Scheme.Name);
    var principal = new ClaimsPrincipal(identity);
    var ticket = new AuthenticationTicket(principal, Scheme.Name);

    _userContext.User = principal;

    return AuthenticateResult.Success(ticket);

  }

  /// <summary>
  /// Nur für Benutzer! Dienste können das nicht!
  /// </summary>
  /// <param name="properties"></param>
  /// <returns></returns>
  protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
  {
    Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Options.Realm}\" charset=\"UTF-8\"";
    await base.HandleChallengeAsync(properties);
  }

}