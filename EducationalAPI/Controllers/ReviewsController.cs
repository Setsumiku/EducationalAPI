using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace EducationalAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IGenericRepository<EduMatNavpoint> _navpointRepository;
        public ReviewsController(IMapper mapper, IGenericRepository<Review> reviewRepository, IGenericRepository<EduMatNavpoint> navpointRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _navpointRepository = navpointRepository;
        }
        /// <summary>
        /// Use to receive all Reviews
        /// </summary>
        /// <returns>Reviews</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/reviews
        [HttpGet("reviews")]
        //[Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var reviews = _mapper.Map<IEnumerable<ReviewReadDTO>>(await _reviewRepository.GetAllAsync(new [] { "EduMatNavpoint" }));
            if (reviews is null) return NotFound();
            return Ok(reviews);
        }

        /// <summary>
        /// Use to delete a Review
        /// </summary>
        /// <param name="id">Id of the review</param>
        /// <response code="404">Not found</response>
        /// <response code="204">No content</response>
        // DELETE: api/<EducationalAPI>/reviews/id
        [HttpDelete("reviews/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewToDelete = await _reviewRepository.GetSingleByConditionAsync(r=>r.ReviewId==id,Array.Empty<string>());
            if (reviewToDelete is null) return NotFound();
            _ = await _reviewRepository.DeleteAsync(reviewToDelete);
            return NoContent();
        }
        /// <summary>
        /// Use to Edit a Review
        /// </summary>
        /// <param name="id">Id of the review</param>
        /// <response code="404">Not found</response>
        /// <response code="204">No content</response>
        // PATCH: api/<EducationalAPI>/reviews/id
        [HttpPatch("reviews/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<ReviewWriteDTO> reviewWriteDTO)
        {
            var reviewToUpdate = await _reviewRepository.GetSingleByConditionAsync(r=> r.ReviewId == id, Array.Empty<string>());
            if (reviewToUpdate is null) return BadRequest();
            var reviewToPatch = _mapper.Map<ReviewWriteDTO>(reviewToUpdate);
            reviewWriteDTO.ApplyTo(reviewToPatch, ModelState);
            if (TryValidateModel(reviewToUpdate))
            {
                _mapper.Map(reviewToPatch, reviewToUpdate);
                _ = await _reviewRepository.UpdateAsync(reviewToUpdate);
                return NoContent();
            }
            else return NotFound();
        }
        /// <summary>
        /// Use to Create Navpoint
        /// </summary>
        /// <returns>No Content</returns>
        // POST api/<EducationalAPI>/reviews
        [HttpPost("reviews")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int id, ReviewWriteDTO review)
        {
            Review reviewToAdd = _mapper.Map<Review>(review);
            var navpointToInsert = await _navpointRepository.GetSingleByConditionAsync(p => p.EduMatNavpointId == id, Array.Empty<string>());
            reviewToAdd.EduMatNavpoint = navpointToInsert;
            _ = await _reviewRepository.CreateAsync(reviewToAdd);
            return NoContent();
        }
    }
}
