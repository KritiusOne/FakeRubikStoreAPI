using API.Response;
using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _review;
        private readonly IMapper _mapper;
        public ReviewController(IMapper map, IReviewServices reviewServices)
        {
            this._review = reviewServices;
            this._mapper = map;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var AllReviews = _review.GetReviews();
            var AllReviewsDTO = _mapper.Map<IEnumerable<ReviewDTO>>(AllReviews);
            var response = new ResponseBase<IEnumerable<ReviewDTO>>(AllReviewsDTO, "This is the all reviews");
            return Ok(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var AllReviewsById = await _review.GetReviewById(id);
            var AllReviews = _mapper.Map<ReviewDTO>(AllReviewsById);
            var response = new ResponseBase<ReviewDTO>(AllReviews, "This is the reviews from the this ID");
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ReviewDTO review)
        {
            var newReview = _mapper.Map<Review>(review);
            await _review.CreateReview(newReview);
            return Ok("The review was created success");
        }
    }
}
