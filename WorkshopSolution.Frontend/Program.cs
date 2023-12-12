using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WorkshopSolution.Frontend;
using WorkshopSolution.Frontend.Security;


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
// builder.Services.AddTransient()

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

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
