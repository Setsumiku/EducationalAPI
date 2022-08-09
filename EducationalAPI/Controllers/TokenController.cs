using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;
using EducationalAPI.DTO;
using EducationalAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationalAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "post")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IGenericRepository<User> _userRepository;

        public TokenController(IConfiguration config, IGenericRepository<User> userRepository)
        {
            _configuration = config;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Log in with your credentials
        /// </summary>
        /// <param name="_userData"></param>
        /// <response code="400">Bad Request</response>
        /// <response code="200">Ok</response>
        /// <returns>JWT Token if successful</returns>
        [HttpPost]
        public async Task<IActionResult> Post(UserLoginDTO _userData)
        {
            if (_userData?.UserLogin != null && _userData.UserPassword != null)
            {
                var hashedLogin = Hashing.Hash(_userData.UserLogin);
                var hashedPassword = Hashing.Hash(_userData.UserPassword);
                var user = await GetUser(hashedLogin,hashedPassword);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("UserLogin", user.UserLogin),
                        new Claim("UserPassword", user.UserPassword),
                        new Claim(ClaimTypes.Role, user.UserRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
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

        private async Task<User> GetUser(string userLogin, string userPassword)
        {
            return await _userRepository.GetSingleByConditionAsync(u => u.UserLogin == userLogin && u.UserPassword == userPassword, Array.Empty<string>());
        }
    }
}
