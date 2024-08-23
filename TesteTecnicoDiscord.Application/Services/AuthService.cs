using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using FluentResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Domain.Util;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<Result<string>> Register(CreateUserDto request)
    {
        var password = request.Password;
        CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            Name = request.Name,
            Username = request.Username,
            BirthDate = (DateTime)request.BirthDate!,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        var resultUserCreated = await userRepository.Add(user);

        if (resultUserCreated is null)
            return Result.Fail<string>(Messages.Errors.UserCreationFailed);

        var token = GenerateAccessToken(resultUserCreated, password);

        return string.IsNullOrWhiteSpace(token)
            ? Result.Fail(string.Format(Messages.Errors.UngeneratedToken, resultUserCreated.Username))
            : Result.Ok(token);
    }

    public async Task<Result<string>> Login(LoginUserDto request)
    {
        var resultUser = await userRepository.GetByUsername(request.Username);

        if (resultUser.IsFailed || resultUser.Value is null)
            return Result.Fail(string.Format(Messages.Errors.UserNotFound, request.Username));

        var validToken = GenerateAccessToken(resultUser.Value, request.Password);
        return string.IsNullOrWhiteSpace(validToken) ? string.Empty : validToken;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool CheckPasswordHash(string password, IReadOnlyList<byte> passwordHash, byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
    }

    private string CreateToken(User user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        ];

        // get the secrete key
        var tokenKey = configuration.GetSection("AppSettings:Token").Value;

        if (string.IsNullOrWhiteSpace(tokenKey))
            throw new ArgumentNullException("");

        var keySecretEncrypted = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey));
        var creds = new SigningCredentials(keySecretEncrypted, SecurityAlgorithms.HmacSha256);

        var tokenProperties = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds,
            Issuer = "",
            IssuedAt = DateTime.Now
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var validToken = tokenHandler.CreateToken(tokenProperties);

        if (validToken is null)
            throw new ArgumentException();

        return tokenHandler.WriteToken(validToken);
    }

    private string GenerateAccessToken(User user, string password)
    {
        // If the password is incorrect, return an empty string
        if (!CheckPasswordHash(password, user.PasswordHash, user.PasswordSalt)) return string.Empty;

        // Create token
        return CreateToken(user);
    }
}