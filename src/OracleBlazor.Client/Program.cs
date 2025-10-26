using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

using OracleBlazor.Client;
using OracleBlazor.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<AuthHeaderHandler>();
builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5233/api/"); // <- değiştir
}).AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

// Basit bir AuthService (login/logout)
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AssetsService>();

await builder.Build().RunAsync();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
