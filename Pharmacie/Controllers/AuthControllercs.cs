using Microsoft.AspNetCore.Mvc;
using Pharmacie.Utils;


namespace PharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Remplace ceci par une validation réelle des utilisateurs
            if (loginModel.Username == "pharmacist" && loginModel.Password == "password")
            {
                var token = _jwtTokenGenerator.GenerateToken("123", "Pharmacist");
                return Ok(new { Token = token });
            }


            return Unauthorized();
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
