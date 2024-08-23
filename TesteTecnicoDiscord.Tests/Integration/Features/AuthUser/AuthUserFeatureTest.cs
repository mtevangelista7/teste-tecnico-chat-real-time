using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Tests.Builders;
using Xunit.Gherkin.Quick;

namespace TesteTecnicoDiscord.Tests.Integration.Features.AuthUser;

[FeatureFile(
    @"C:\Users\p-mesantos\matheus-apagar-study\teste-tecnico-chat-real-time\TesteTecnicoDiscord.Tests\Integration\Features\AuthUser\AuthUser.feature")]
public class AuthUserFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private LoginUserDto _loginUser = null!;

    public AuthUserFeatureTest()
    {
        var factory = new WebApplicationFactory<Program>();
        _base = new BaseIntegrationTests(factory);
    }

    [Given(@"an existing user with access to the login endpoint")]
    public static void GivenAnExistingUserWhoHasAccessToTheLogInEndpoint()
    {
    }

    [When(@"they provide a username ""(.+)"" and password ""(.+)""")]
    public async Task WhenTheyProvideAnUserNameAndPassword(string username, string password)
    {
        var userDto = new CreateUserDtoBuilder().WithUsername(username).WithPassword(password).Build();
        var result = await _base.GetHttpClient().PostAsJsonAsync("/register", userDto);
        
        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        _loginUser = new LoginUserDto
        {
            Username = username, Password = password
        };
    }

    [Then(@"the API should authenticate the user successfully")]
    public async Task ThenTheAPIShouldAuthenticateTheUserSuccessfully()
    {
        // realiza post com o objeto de usuário criado
        var result = await _base.GetHttpClient().PostAsJsonAsync("/login", _loginUser);

        var dbContext = _base.GetDbContext();
        var user = dbContext.Users.SingleOrDefault(u => u.Username == _loginUser.Username);

        // remove o mesmo
        if (user is not null)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var obtainedToken = await result.Content.ReadAsStringAsync();
        obtainedToken.Should().NotBeNullOrEmpty();
    }
}