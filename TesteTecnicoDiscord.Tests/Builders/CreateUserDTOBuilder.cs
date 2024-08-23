using Bogus;
using TesteTecnicoDiscord.Application.Dtos;

namespace TesteTecnicoDiscord.Tests.Builders;

public class CreateUserDtoBuilder
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime? BirthDate { get; set; }

    public CreateUserDtoBuilder()
    {
        var faker = new Faker("pt_BR");

        Name = faker.Name.FullName().Clamp(3, 100);
        Username = faker.Internet.UserName().Clamp(3, 50);
        Password = faker.Internet.Password(8, false).Clamp(8, 50);
        BirthDate = faker.Date.Past(30, DateTime.Now.AddYears(-18)); // Gera uma data de nascimento fictícia
    }

    public CreateUserDtoBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public CreateUserDtoBuilder WithUsername(string username)
    {
        Username = username;
        return this;
    }

    public CreateUserDtoBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public CreateUserDtoBuilder WithBirthDate(DateTime? birthDate)
    {
        BirthDate = birthDate;
        return this;
    }

    public CreateUserDto Build()
    {
        return new CreateUserDto
        {
            Name = Name,
            Username = Username,
            Password = Password,
            BirthDate = BirthDate
        };
    }
}

// Método de extensão para garantir que as strings estejam dentro dos limites especificados
public static class StringExtensions
{
    public static string Clamp(this string value, int min, int max)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        if (value.Length > max)
            return value.Substring(0, max);

        if (value.Length < min)
            return value.PadRight(min, ' ');

        return value;
    }
}