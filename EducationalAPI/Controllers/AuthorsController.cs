using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;

namespace EducationalAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Author> _authorRepository;
        public AuthorsController(IMapper mapper, IGenericRepository<Author> authorRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// Use to receive all Authors
        /// </summary>
        /// <returns>Authors</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/authors
        [HttpGet("authors")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var authors = _mapper.Map<IEnumerable<AuthorReadDTO>>(await _authorRepository.GetAllAsync(new[] { "EduMatNavpoints" }));
            if (authors is null) return NotFound();
            return Ok(authors);
        }
    }
}
