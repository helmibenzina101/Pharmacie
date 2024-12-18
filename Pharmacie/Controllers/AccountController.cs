using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pharmacie.DTO;
using Pharmacie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pharmacie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] NewUserDTO newUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var appUser = new ApplicationUser
            {
                UserName = newUserDTO.Email,
                FullName = newUserDTO.FullName,
                Email = newUserDTO.Email,
                PhoneNumber = newUserDTO.PhoneNumber,
                Role = newUserDTO.Role
            };

            var result = await _userManager.CreateAsync(appUser, newUserDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, newUserDTO.Role);
                return Ok(new { message = "Utilisateur créé avec succès." });
            }

            return BadRequest(new { errors = result.Errors });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginDTO.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                {
                    var secretKey = _configuration["Jwt:Key"];
                    if (string.IsNullOrEmpty(secretKey))
                    {
                        throw new InvalidOperationException("JWT SecretKey is not configured in appsettings.json.");
                    }

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role)
            };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: creds
                    );

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        username = loginDTO.Username,
                        Role = user.Role
                    });
                }
                return Unauthorized("Nom d'utilisateur ou mot de passe incorrect.");
            }
            return BadRequest(ModelState);
        }

    }
}
