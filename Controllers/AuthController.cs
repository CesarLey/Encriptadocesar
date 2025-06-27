using CaesarApi.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaesarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("ip")]
        public IActionResult GetIp()
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var forwardedFor = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            var realIp = HttpContext.Request.Headers["X-Real-IP"].ToString();
            
            return Ok(new { 
                RemoteIp = remoteIp,
                ForwardedFor = forwardedFor,
                RealIp = realIp,
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            if (usuario.Username == "admin" && usuario.Password == "1234")
            {
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, usuario.Username) }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            return Unauthorized();
        }
    }
} 