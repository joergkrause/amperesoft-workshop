// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.WriteLine("Hello, World!");

var url = builder.Configuration["BackendServices:RackService:Url"];
var authDelegate = new BasicAuthDelegate(builder.Configuration);
var httpClient = new HttpClient(authDelegate)
{
  BaseAddress = new Uri(url),
};
var client = new Proxy();
client.BaseUrl = url;
var model = await client.GetRackAsync(1);
