using System.Security.Cryptography;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class AuthService(IUserRepository userRepository) : IAuthService
{
    public async Task<string> Register(CreateUserDto request)
    {
        // TODO: validate request

        string password = request.Password;

        // create hash and salt
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        // create user
        var user = new User
        {
            Email = request.Email,
            Name = request.Name,
            Username = request.Username,
            BirthDate = (DateTime)request.BirthDate!,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        // add user to database
        var createdUser = await userRepository.Add(user);

        if (createdUser is null)
        {
            return string.Empty;
        }

        // generate token for user
        string token = await GenerateAccessToken(createdUser, password);

        return null;
    }

    public async Task<string> Login(LoginUserDto request)
    {
        throw new NotImplementedException();
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool CheckPasswordHash(string password, IReadOnlyList<byte> passwordHash, byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new(passwordSalt);
        byte[] computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
    }

    private string CreateToken(User user)
    {
        // TODO: implement token creation
        return string.Empty;
    }

    private async Task<string> GenerateAccessToken(User user, string password)
    {
        // If the password is incorrect, return an empty string
        if (!CheckPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return string.Empty;
        }

        // Create token
        string validToken = CreateToken(user);
        return validToken;
    }
}