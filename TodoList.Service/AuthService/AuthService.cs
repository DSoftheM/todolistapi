using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Todolist.DAL;
using TodoList.Domain.Entity;

namespace TodoList.Service.AuthService;

public class AuthService(AppDbContext dbContext)
{
    public async Task<string?> LoginAndGetToken(UserSiteDto userSiteDto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x=>x.Name == userSiteDto.Name);
        if (user == null) return null;
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: DateTime.Now,
            claims: [new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())],
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}