using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using TrendLens.Client.ApiHandles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);



// Register HttpClient for API requests
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register ApiManager as a scoped service
builder.Services.AddScoped<ApiManager>();

await builder.Build().RunAsync();
