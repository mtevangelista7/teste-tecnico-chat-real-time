using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TesteTecnicoDiscord.Infra.Data.Context;

namespace TesteTecnicoDiscord.Tests.Integration.Features;

public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    protected readonly AppDbContext _dbContext;
    private readonly WebApplicationFactory<Program> _factory;

    protected readonly HttpClient _httpClient;

    public BaseIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateDefaultClient();

        // cria um escopo de serviços já registrados para utilzar nos testes e só nos testes
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        // Garante que o banco de dados seja criado e as migrações sejam aplicadas
        // _dbContext.Database.EnsureDeleted();
        // _dbContext.Database.EnsureCreated();
        // _dbContext.Database.Migrate();
    }

    public HttpClient GetHttpClient()
    {
        return _httpClient;
    }

    public AppDbContext GetDbContext()
    {
        return _dbContext;
    }
}