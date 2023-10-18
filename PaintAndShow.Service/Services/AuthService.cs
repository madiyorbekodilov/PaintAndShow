using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaintAndShow.Data.IRepasitories;
using PaintAndShow.Domain.Entities;
using PaintAndShow.Service.Exceptions;
using PaintAndShow.Service.Helpers;
using PaintAndShow.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaintAndShow.Service.Services;

public class AuthService : IAutService
{
    private readonly IConfiguration configuration;
    private readonly IRepasitory<User> userRepository;
    public AuthService(IRepasitory<User> userRepository, IConfiguration configuration)
    {
        this.configuration = configuration;
        this.userRepository = userRepository;
    }

    public async ValueTask<string> GenerateTokenAsync(string phone, string originalPassword)
    {
        var user = await this.userRepository.SelectAsync(u => u.UserName.Equals(phone));
        if (user is null)
            throw new NotFoundException("This user is not found");

        bool verifiedPassword = PasswordHash.Verify(user.Password, originalPassword);
        if (!verifiedPassword)
            throw new CustomException(400, "Phone or password is invalid");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("UserName", user.UserName),
                 new Claim("Id", user.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
