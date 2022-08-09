using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;
using EducationalAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationalAPI.Controllers
{
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepository;

        public TokenController(IConfiguration config, IMapper mapper, IGenericRepository<User> userRepository)
        {
            _configuration = config;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserReadDTO _userData)
        {
            if (_userData?.UserLogin != null && _userData.UserPassword != null)
            {
                var hashedLogin = Hashing.Hash(_userData.UserLogin);
                var hashedPassword = Hashing.Hash(_userData.UserPassword);
                var user = await GetUser(_userData.UserName, _userData.Password);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("UserLogin", user.DisplayName),
                        new Claim("UserPassword", user.UserName),
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
