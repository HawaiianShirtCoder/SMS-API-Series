using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMS.Shared.BLL;

public static class AuthServices
{
    public static string? Login(string username, string password)
    {
        if (username.ToLower() == "superadmin" && password.ToLower() == "hainesjason")
        {
            return GenerateJwtToken(username, "SuperAdmin");
        }

        if (username.ToLower() == "admin" && password.ToLower() == "selectiongroup")
        {
            return GenerateJwtToken(username, "Admin");
        }

        return null;
    }
    private static string GenerateJwtToken(string username, string role)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKey"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, username),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim(ClaimTypes.Role, role)

            };

        var token = new JwtSecurityToken(
            issuer: "StanwayRonnies",
            audience: "StanwayRonnies",
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
