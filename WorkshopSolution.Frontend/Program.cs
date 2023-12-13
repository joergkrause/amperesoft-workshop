using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WorkshopSolution.Frontend;
using WorkshopSolution.Frontend.Security;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;


var builder = WebApplication.CreateBuilder(args);

// v1
//builder.Services.AddHttpClient("RackServiceClient", client =>
//{
//  client.BaseAddress = new Uri("https://localhost:7111");
//});

builder.Services.AddSingleton<RackServiceClient>(sp =>
{
  // v2
  var url = builder.Configuration["BackendServices:RackService:Url"];
  var authDelegate = new BasicAuthDelegate(builder.Configuration);
  var httpClient = new HttpClient(authDelegate)
  {
    BaseAddress = new Uri(url),        
  };
  var client = new RackServiceClient(httpClient);
  client.BaseUrl = url;
  return client;
});

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
  .AddMicrosoftIdentityWebApp(options =>
  {
    builder.Configuration.Bind("AzureAD", options);
    options.Prompt = "login";

    options.TokenValidationParameters.ValidateIssuer = false;
    options.TokenValidationParameters.ValidateIssuerSigningKey = false;

    options.Events.OnTokenValidated = async context =>
    {
      await Task.CompletedTask;
    };

    options.SaveTokens = true;
  });

builder.Services
  .AddControllersWithViews()
  .AddMicrosoftIdentityUI();

builder.Services.AddBlazorise(options =>
{
  // https://blazorise.com/docs/components/memo
  options.Immediate = false;
  options.Debounce = true;
  options.DebounceInterval = 300;
})
  .AddBootstrap5Providers()  
  .AddFontAwesomeIcons()
  ;

// builder.Services.AddAuthentication()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
