using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.API.Data;
using TaskManager.API.DTOS;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        

        public AuthController(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ResgisterDto registuser)
        {
            var user = new User
            {
                UserName = registuser.UserName,
                Email = registuser.Email,
            };
            var result = await _userManager.CreateAsync(user, registuser.Password);
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDtomodel)
        {
            var user = await _userManager.FindByNameAsync(loginDtomodel.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDtomodel.Password))
                return Unauthorized();
            var token = GenerateJwtToken(user);
            return Ok(new { token });

        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id), // GUID الفعلي
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_config["JWT:DurationInDays"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
