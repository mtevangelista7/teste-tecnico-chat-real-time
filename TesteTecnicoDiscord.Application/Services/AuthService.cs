using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TesteTecnicoDiscord.Application.Dtos;
using TesteTecnicoDiscord.Application.Interfaces.Services;
using TesteTecnicoDiscord.Domain.Entities;
using TesteTecnicoDiscord.Infra.Interfaces;

namespace TesteTecnicoDiscord.Application.Services;

public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
{
    public async Task<string> Register(CreateUserDto request)
    {
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
        string token = GenerateAccessToken(createdUser, password);

        if (String.IsNullOrWhiteSpace(token))
            return string.Empty;

        return token;
    }

    public async Task<string> Login(LoginUserDto request)
    {
        var user = await userRepository.GetByUsername(request.Username);

        if (user is null)
            return string.Empty;

        var validToken = GenerateAccessToken(user, request.Password);

        if (string.IsNullOrWhiteSpace(validToken))
            return string.Empty;

        return validToken;
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
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        ];

        // get the secrete key
        var tokenKey = configuration.GetSection("AppSettings:Token").Value;

        if (string.IsNullOrWhiteSpace(tokenKey))
            throw new ArgumentNullException("");

        var keySecretEncrypted = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey));
        var creds = new SigningCredentials(keySecretEncrypted, SecurityAlgorithms.HmacSha256);

        var tokenProperties = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds,
            Issuer = "",
            IssuedAt = DateTime.Now,
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
        if (!CheckPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return string.Empty;
        }

        // Create token
        return CreateToken(user);
    }
}