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
        private readonly IGenericRepository<EduMatType> _eduMatTypeRepository;
        private readonly IGenericRepository<Author> _authorRepository;
        public EducationalMaterialNavpointsController(IMapper mapper, IGenericRepository<EduMatNavpoint> eduMatNavpointRepository, IGenericRepository<EduMatType> eduMatTypeRepository, IGenericRepository<Author> authorRepository)
        {
            _mapper = mapper;
            _eduMatNavpointRepository = eduMatNavpointRepository;
            _eduMatTypeRepository = eduMatTypeRepository;
            _authorRepository = authorRepository;
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
            var eduMatNavpoints = _mapper.Map<IEnumerable<EduMatNavpointReadDTO>>(await _eduMatNavpointRepository.GetAllAsync(new string[] { "EduMatReviews", "EduMatType", "EduMatAuthor" }));
            if (eduMatNavpoints is null) return NotFound();
            return Ok(eduMatNavpoints);
        }
        /// <summary>
        /// Use to Edit Navpoint
        /// </summary>
        /// <param name="id">int for ID of Navpoint</param>
        /// <param name="title">string for title of Navpoint</param>
        /// <param name="location">string for location of Navpoint</param>
        /// <returns>No Content</returns>
        // PUT api/<EducationalAPI>/educationalmaterialnavpoints/{id}
        [HttpPut("educationalmaterialnavpoints/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, string title, string location)
        {
            var navpointToUpdate = await _eduMatNavpointRepository.GetSingleByConditionAsync(navpoint => navpoint.EduMatNavpointId == id, Array.Empty<string>());
            if (navpointToUpdate is null) return BadRequest();
            navpointToUpdate.EduMatTitle = title;
            navpointToUpdate.EduMatLocation = location;
            _ = await _eduMatNavpointRepository.UpdateAsync(navpointToUpdate);
            return NoContent();
        }
        /// <summary>
        /// Use to Create Navpoint
        /// </summary>
        /// <returns>No Content</returns>
        /// <response code="204">No content</response>
        // POST api/<EducationalAPI>/educationalmaterialnavpoints
        [HttpPost("educationalmaterialnavpoints")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(EduMatNavpointWriteDTO navpoint)
        {
            EduMatNavpoint navpointToAdd = _mapper.Map<EduMatNavpoint>(navpoint);
            navpointToAdd.EduMatType = await _eduMatTypeRepository.GetSingleByConditionAsync(s => s.EduMatTypeId == navpoint.EduMatTypeEduMatTypeId, Array.Empty<string>());
            navpointToAdd.EduMatAuthor = await _authorRepository.GetSingleByConditionAsync(s => s.AuthorId == navpoint.AuthorAuthorId, Array.Empty<string>());
            _ = await _eduMatNavpointRepository.CreateAsync(navpointToAdd);
            return NoContent();
        }
        /// <summary>
        /// Use to Delete Navpoint
        /// </summary>
        /// <returns>No Content</returns>
        /// <param name="id">Id of navpoint to delete</param>
        /// <response code="204">No content</response>
        /// <response code="404">Not found</response>
        // DELETE api/<EducationalAPI>/educationalmaterialnavpoints
        [HttpDelete("educationalmaterialnavpoints/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var navpointToDelete = await _eduMatNavpointRepository.GetSingleByConditionAsync(s => s.EduMatNavpointId == id, Array.Empty<string>());
            if (navpointToDelete is null) return NotFound();
            _ = await _eduMatNavpointRepository.DeleteAsync(navpointToDelete);
            return NoContent();
        }
    }
}
