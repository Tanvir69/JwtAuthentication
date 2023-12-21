using JWTAUthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

    namespace JWTAUthentication.Controllers;

    [Route("api/token")]
    [ApiController]
    public class TokenControllers : ControllerBase
    {
        private readonly IConfiguration _config;
        public  DatabaseContext _context;


        public TokenControllers(IConfiguration config, DatabaseContext context)
            {
            _config = config; _context=context;
            }

         
  
        [HttpPost("Admins")]
       // [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Post(UserInfo _userData)
            {
                if (_userData != null && _userData.Email != null && _userData.Password != null)
                {
                    var user = await GetUser(_userData.Email, _userData.Password);

                    if (user != null)
                    {
                        //create claims details based on the user information
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("UserId",user.UserId.ToString()),
                            new Claim("DisplayName", user.DisplayName),
                            new Claim("UserName", user.UserName),
                            new Claim("Email", user.Email)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _config["Jwt:Issuer"],
                            _config["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    }
                    else
                    {
                        return BadRequest("Invalid credentials");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }

            private async Task<UserInfo> GetUser(string email, string password)
            {
                return await _context.UserInfos.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            }
        }
    