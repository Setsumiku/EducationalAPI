using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;
using System.Web.Http.Cors;

namespace EducationalAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "get")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IGenericRepository<EduMatNavpoint> _eduMatNavpointRepository;
        public AuthorsController(IMapper mapper, IGenericRepository<Author> authorRepository, IGenericRepository<EduMatNavpoint> navpointRepository)
        {
            _mapper = mapper;
            _authorRepository = authorRepository;
            _eduMatNavpointRepository = navpointRepository;
        }

        /// <summary>
        /// Use to receive all Authors
        /// </summary>
        /// <returns>Authors</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/authors
        [HttpGet("authors")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var authors = _mapper.Map<IEnumerable<AuthorReadDTO>>(await _authorRepository.GetAllAsync(new[] { "EduMatNavpoints" }));
            return Ok(authors);
        }
        /// <summary>
        /// Use to receive materials with a higher than 5 average score
        /// </summary>
        /// <returns>Authors</returns>
        /// <param name="id">Id for Author</param>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/authors/{id}/GetHighAverageMaterials
        [HttpGet("authors/{id}/GetHighAverageMaterials")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetHighAverage(int id)
        {

            var navpointsWithAuthor = await _eduMatNavpointRepository.GetMultipleByConditionAsync(p => p.EduMatAuthor.AuthorId == id, new string[] { "EduMatAuthor", "EduMatReviews" });
            if (navpointsWithAuthor is null) return NotFound();
            List<EduMatNavpoint> navpointsWithHighAverage = new();
            foreach(var navpoint in navpointsWithAuthor)
            {
                if(navpoint.CalculateAverageReviewScore()>5) navpointsWithHighAverage.Add(navpoint);
            }
            var mappedNavpoints = _mapper.Map<IEnumerable<EduMatNavpointReadDTO>>(navpointsWithHighAverage);
            return Ok(mappedNavpoints);
        }
        /// <summary>
        /// Use to receive most productive author
        /// </summary>
        /// <returns>most productive author</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/authors/GetMostProductiveAuthor
        [HttpGet("authors/GetMostProductiveAuthor")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetMostProductiveAuthor()
        {

            var authors = await _authorRepository.GetAllAsync(Array.Empty<string>());
            if (authors is null) return NotFound();
            var mostProductiveAuthor = authors.MaxBy(a => a.AmountOfMaterials);
            return Ok(mostProductiveAuthor);
        }
    }
}
