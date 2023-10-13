
using MedicalOffice.Server.Context;
using MedicalOffice.Shared.Entities;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MedicalOffice.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController:ControllerBase
{
    #region Constructor
    private readonly IConfiguration _config;
    private readonly AppDbContext _db;
    private readonly ProtectPassword _protectPassword;
    public AuthController(IConfiguration config, AppDbContext db,
        ProtectPassword protect)
    {
        _config = config;
        _db = db;
        _protectPassword = protect;
    }
    #endregion

    #region Methods

    [HttpPost("Login")]
    public async Task<ActionResult<TokenData>> Login([FromBody] UserData userData)
    {
        var query = _db.Users.Where(p => p.Mobile == userData.Mobile);
        if (query != null && query.Count() > 0)
        {
            User user = query.Include(p => p.Role).FirstOrDefault();

            if (user != null && _protectPassword.ValidatePassword(userData.Password, user.Password))
            {
                return await GenerateToken(user);
            }
            else
            {
                return new TokenData
                {
                    Token = null,
                    Expiration = null,
                    Status = false,
                    Message = "نام کاربری یا کلمه عبور اشتباه است"
                };
            }
        }
        else
        {
            return new TokenData
            {
                Token = null,
                Expiration = null,
                Status = false,
                Message = "نام کاربری یا کلمه عبور اشتباه است"
            };
        }
    }
    private async Task<TokenData> GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name , user.Mobile),
            new Claim(ClaimTypes.MobilePhone , user.Mobile),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role , user.Role.EnCaption)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:key"]));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.Now.AddHours(1);
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: cred
        );
        return await Task.FromResult(new TokenData
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Status = true,
            Message = "Success"
        });
    }
    #endregion
}
