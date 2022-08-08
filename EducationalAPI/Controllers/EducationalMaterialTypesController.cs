using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;

namespace EducationalAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalMaterialTypesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<EduMatType> _eduMatTypeRepository;
        public EducationalMaterialTypesController(IMapper mapper, IGenericRepository<EduMatType> eduMatTypeRepository)
        {
            _mapper = mapper;
            _eduMatTypeRepository = eduMatTypeRepository;
        }
        /// <summary>
        /// Use to receive all Educational Material Types
        /// </summary>
        /// <returns>Types</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/educationalmaterialtypes
        [HttpGet("educationalmaterialtypes")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var eduMatTypes = _mapper.Map<IEnumerable<EduMatTypeReadDTO>>(await _eduMatTypeRepository.GetAllAsync(Array.Empty<string>()));
            if (eduMatTypes is null) return NotFound();
            return Ok(eduMatTypes);
        }
    }
}
