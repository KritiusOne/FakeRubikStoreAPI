using API.Response;
using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("{productId}/{userId}")]
        public IActionResult GetById(int productId, int userId)
        {
            var AllReviewsById = _review.GetReviewById(productId, userId);
            if(AllReviewsById == null)
            {
                return NotFound("No existe dicha Review");
            }
            var AllReviews = _mapper.Map<ReviewDTO>(AllReviewsById);
            var response = new ResponseBase<ReviewDTO>(AllReviews, "This is the reviews from the this ID");
            return Ok(response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(ReviewDTO review)
        {
            var newReview = _mapper.Map<Review>(review);
            await _review.CreateReview(newReview);
            return Ok("The review was created success");
        }
        [HttpPut("{productId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> Update(int productId, int userId, ReviewDTO review)
        {
            var toUpdated = _mapper.Map<Review>(review);
            await _review.UpdateReview(productId, userId, toUpdated);
            return Ok("The review was update success");

        }
    }
}
