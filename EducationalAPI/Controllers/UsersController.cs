using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;
using EducationalAPI.Utils;

namespace EducationalAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "post")]
    [Authorize(AuthenticationSchemes = "Bearer" , Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepository;
        public UsersController(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Use to Create a new User/Admin
        /// </summary>
        /// <param name="userRole">String for user's role: "User" or "Admin"</param>
        /// <returns>Created User ID</returns>
        /// <response code="201">Created</response>
        // POST api/<EducationalAPI>/users
        [HttpPost("users")]
        public async Task<IActionResult> Add([FromBody] string userRole, string userLogin, string userPassword)
        {
            var savedUser = await _userRepository.CreateAsync(new User() { UserRole = userRole, UserLogin = Hashing.Hash(userLogin), UserPassword = Hashing.Hash(userPassword) });
            return Created(string.Empty , savedUser.UserId);
        }
    }
}
