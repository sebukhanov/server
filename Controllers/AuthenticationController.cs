using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chat.Common;
using Chat.Models;
using System.Security.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ChatDBContext dbContext;

        public AuthenticationController(ChatDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("GetToken")]
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody]User user)
        {
            User _user = await dbContext.Users.FirstOrDefaultAsync(x => x.Name == user.Name && x.Password == user.Password);
            if (_user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: DateTime.Now,
                        claims: claimsIdentity.Claims,
                        expires: DateTime.Now.AddSeconds(AuthOptions.LIFETIME),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt
                };
                return new JsonResult(response);
            }
            else
            {
                return BadRequest("Invalid username or password.");
            }
        }
    }
}