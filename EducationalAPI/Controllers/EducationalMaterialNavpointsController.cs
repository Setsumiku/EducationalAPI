using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;

namespace EducationalAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalMaterialNavpointsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EduMatNavpoint> _eduMatNavpointRepository;
        public EducationalMaterialNavpointsController(IMapper mapper, IGenericRepository<EduMatNavpoint> eduMatNavpointRepository)
        {
            _mapper = mapper;
            _eduMatNavpointRepository = eduMatNavpointRepository;
        }
        /// <summary>
        /// Use to receive all Educational Material Navpoints
        /// </summary>
        /// <returns>Navpoints</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/educationalmaterialnavpoints
        [HttpGet("educationalmaterialnavpoints")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var eduMatNavpoints = _mapper.Map<IEnumerable<EduMatTypeReadDTO>>(await _eduMatNavpointRepository.GetAllAsync(Array.Empty<string>()));
            if (eduMatNavpoints is null) return NotFound();
            return Ok(eduMatNavpoints);
        }
    }
}
