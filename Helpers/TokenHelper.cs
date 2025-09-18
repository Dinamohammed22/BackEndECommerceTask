using KOG.ECommerce.Common;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KOG.ECommerce.Helpers;

public static class TokenGenerator
{
    public static string Generate(string id, string mobile, Role roleID)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("ID", id),
                new Claim("RoleID", roleID.ToString()),
                new Claim(ClaimTypes.MobilePhone, mobile)
            }),
            Expires = DateTime.Now.AddDays(7),
            Issuer = "KOG.ECommerce",
            Audience = "KOG.ECommerce-Users",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
