using EducationalAPI.Data.DAL;
using EducationalAPI.Data.Models;
using EducationalAPI.Links;
using Microsoft.AspNetCore.JsonPatch;

namespace EducationalAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "get,delete,patch,post")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Review> _reviewRepository;
        private readonly IGenericRepository<EduMatNavpoint> _navpointRepository;
        private readonly LinkGenerator _linkGenerator;
        public ReviewsController(IMapper mapper, IGenericRepository<Review> reviewRepository, IGenericRepository<EduMatNavpoint> navpointRepository, LinkGenerator linkGenerator)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _navpointRepository = navpointRepository;
            _linkGenerator = linkGenerator;
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
            var reviews = _mapper.Map<IEnumerable<ReviewReadDTO>>(await _reviewRepository.GetAllAsync(new[] { "EduMatNavpoint" }));
            for (var index = 0; index < reviews.Count(); index++)
            {
                //reviews.ElementAt(index).Add("Contents", new { reviews.ElementAt(index).ReviewContents });
                //reviews.ElementAt(index).Add("Score", new { reviews.ElementAt(index).ReviewScore });
                //reviews.ElementAt(index).Add("Navpoint", new { reviews.ElementAt(index).EduMatNavpoint });
                var reviewLinks = CreateLinks(reviews.ElementAt(index).ReviewId);
                reviews.ElementAt(index).Add("Links", reviewLinks);
            }
            var reviewsWrapper = new LinkWrapper<ReviewReadDTO>(reviews);
            return Ok(CreateLinksWrapped(reviewsWrapper));
            //return Ok(reviews);
        }

        /// <summary>
        /// Use to delete a Review
        /// </summary>
        /// <param name="id">Id of the review</param>
        /// <response code="404">Not found</response>
        /// <response code="204">No content</response>
        // DELETE: api/<EducationalAPI>/reviews/id
        [HttpDelete("reviews/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewToDelete = await _reviewRepository.GetSingleByConditionAsync(r => r.ReviewId == id, Array.Empty<string>());
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
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Update(int id, JsonPatchDocument<ReviewWriteDTO> reviewWriteDTO)
        {
            var reviewToUpdate = await _reviewRepository.GetSingleByConditionAsync(r => r.ReviewId == id, Array.Empty<string>());
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
        /// Use to Create Review
        /// </summary>
        /// <param name="id">Id of navpoint to review</param>
        /// <returns>Created</returns>
        /// <response code="201">Created</response>
        // POST api/<EducationalAPI>/reviews
        [HttpPost("reviews")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create(int id, ReviewWriteDTO review)
        {
            Review reviewToAdd = _mapper.Map<Review>(review);
            var navpointToInsert = await _navpointRepository.GetSingleByConditionAsync(p => p.EduMatNavpointId == id, Array.Empty<string>());
            reviewToAdd.EduMatNavpoint = navpointToInsert;
            var createdReview = await _reviewRepository.CreateAsync(reviewToAdd);
            return Created(string.Empty, createdReview.ReviewId);
        }

        private IEnumerable<Link> CreateLinks(int id)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get), values: new { id }),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Delete), values: new { id }),
                "delete_review",
                "DELETE"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Update), values: new { id }),
                "edit_review",
                "PUT")
            };
            return links;
        }

        private LinkWrapper<ReviewReadDTO> CreateLinksWrapped(LinkWrapper<ReviewReadDTO> reviewsWrapper)
        {
            reviewsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get)),
                    "self",
                    "GET"));
            reviewsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Create)),
                    "self",
                    "CREATE"));
            return reviewsWrapper;
        }
    }
}
