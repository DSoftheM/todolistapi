using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoList.Domain.Entity;
using TodoList.Service.AuthService;

namespace TodoList.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthService authService)
{
    [Route("register/{username}")]
    public string Register(string username)
    {
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

    [Route("login")]
    [HttpPost]
    public Task<string?> Login(UserSiteDto userSiteDto)
    {
        return authService.LoginAndGetToken(userSiteDto);
    }
}