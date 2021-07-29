using BlazorBlog.Shared.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBlog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public UsersController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {


            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(userDTO.Username, userDTO.Password, false, false);
            if (result.Succeeded)
            {
                IdentityUser user = await _userManager.FindByNameAsync(userDTO.Username);
                string token = await GenerateJsonWebToken(user);

                return Ok(new UserDTO { Token = token, Id = user.Id, Username = user.UserName });
            }
            return Unauthorized();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            string id = Request.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest(new { message = "User not found" });
            }

            return Ok(new UserDTO { Id = id, Username = user.UserName });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] UserDTO userDTO)
        {

            await _signInManager.SignOutAsync();
            return Ok();
        }
        private async Task<string> GenerateJsonWebToken(IdentityUser user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            IList<string> roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));
            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                null,
                DateTime.Now.AddHours(1),
                credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
