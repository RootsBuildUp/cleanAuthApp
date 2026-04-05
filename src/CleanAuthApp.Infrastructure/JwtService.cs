
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanAuthApp.Application.Command;
using Microsoft.IdentityModel.Tokens;

namespace CleanAuthApp.Infrastructure;

public class JwtService : IJwtService
{
    private readonly string _key = "THIS_IS_A_VERY_SECURE_SECRET_KEY_FOR_JWT_1234567890";

    public string GenerateToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}