using BlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectShared;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddRefitClient<IDataAccess>().ConfigureHttpClient(c =>
{
    //c.BaseAddress = new Uri("http://localhost:7071/api");
    c.BaseAddress = new Uri("https://blazormongodbapi.azurewebsites.net/api");
});

await builder.Build().RunAsync();
