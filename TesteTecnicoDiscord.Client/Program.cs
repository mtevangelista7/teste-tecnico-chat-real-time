using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using TesteTecnicoDiscord.Client.HttpClientHandler;
using TesteTecnicoDiscord.Client.RefitInterfaces;
using TesteTecnicoDiscord.Client.States;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

// Http handler
builder.Services.AddScoped<AuthenticatedHttpClientHandler>();

// Refit
builder.Services.AddRefitClient<IAuthEndpoints>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7290"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddRefitClient<IGuildsEndpoints>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7290"))
    .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Blazored
builder.Services.AddBlazoredLocalStorage();

// CustomAuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


await builder.Build().RunAsync();
