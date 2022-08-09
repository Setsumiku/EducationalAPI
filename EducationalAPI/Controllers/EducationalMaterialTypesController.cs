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
        private readonly IGenericRepository<EduMatNavpoint> _navpointRepository;
        public EducationalMaterialTypesController(IMapper mapper, IGenericRepository<EduMatType> eduMatTypeRepository, IGenericRepository<EduMatNavpoint> eduMatNavpointRepository)
        {
            _mapper = mapper;
            _eduMatTypeRepository = eduMatTypeRepository;
            _navpointRepository = eduMatNavpointRepository;
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
        /// <summary>
        /// Use to receive all Educational Navpoints by Material Type
        /// </summary>
        /// <returns>Navpoints</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        /// <param name="id">ID for Mat Type</param>
        // GET: api/<EducationalAPI>/educationalmaterialtypes/getallnavpointsbymaterialtype
        [HttpGet("educationalmaterialtypes/getallnavpointsbymaterialtype")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllNavpointsByMaterialType(int id)
        {
            var navpoints = await _navpointRepository.GetMultipleByConditionAsync(p => p.EduMatType.EduMatTypeId == id, new string[] { "EduMatType", "EduMatAuthor","EduMatReviews" });
            if (navpoints is null) return NotFound();
            var mappedNavpoints = _mapper.Map<IEnumerable<EduMatNavpointReadDTO>>(navpoints);
            return Ok(mappedNavpoints);
        }
    }
}
