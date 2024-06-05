using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using TesteTecnicoDiscord.Components;
using TesteTecnicoDiscord.DependencyInjection;
using TesteTecnicoDiscord.Hubs;
using TesteTecnicoDiscord.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();

builder.Services.AddMudServices();

builder.Services.AddServiceCollection();

builder.Services.AddControllers();

// Adicione o contexto do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.MapControllers();

app.MapHub<ChannelHub>("/channelhub");

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TesteTecnicoDiscord.Client._Imports).Assembly);

app.Run();