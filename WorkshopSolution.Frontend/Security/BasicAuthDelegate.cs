using System.Net.Http.Headers;
using System.Text;

namespace WorkshopSolution.Frontend.Security
{
  public class BasicAuthDelegate : DelegatingHandler
  {
    private readonly IConfiguration _configuration;    

    public BasicAuthDelegate(IConfiguration configuration)
    {
      _configuration = configuration;
      InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var username = _configuration["BasicAuth:Username"];
      var password = _configuration["BasicAuth:Password"];
      var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
      request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
      request.Headers.Add("X-UserName", "admin");
      return await base.SendAsync(request, cancellationToken);
    }
  }
}
