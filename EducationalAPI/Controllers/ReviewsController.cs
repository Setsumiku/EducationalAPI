using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;

namespace EducationalAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Review> _reviewRepository;
        public ReviewsController(IMapper mapper, IGenericRepository<Review> reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }
        /// <summary>
        /// Use to receive all Reviews
        /// </summary>
        /// <returns>Reviews</returns>
        /// <response code="200">Ok</response>
        /// <response code="404">Not found</response>
        // GET: api/<EducationalAPI>/reviews
        [HttpGet("reviews")]
        [Authorize(Roles = "Admin,User")]
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
        [HttpDelete("reviews/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewToDelete = await _reviewRepository.GetSingleByConditionAsync(r=>r.ReviewId==id,Array.Empty<string>());
            if (reviewToDelete is null) return NotFound();
            _ = await _reviewRepository.DeleteAsync(reviewToDelete);
            return NoContent();
        }
    }
}
