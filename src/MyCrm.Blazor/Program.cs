using MyCrm.Shared.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddBlazorServices(builder.HostEnvironment.BaseAddress);
builder.Services.AddBlazorServices("https://localhost:44367/");
builder.Services.AddBrowserStorageService();

await builder.Build().RunAsync();
