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
    public class CategoryController : ControllerBase
    {
        private readonly IBasicEndpointService<Category> _basicEndpoint;
        private readonly IMapper _mapper;
        public CategoryController(IMapper mapper, IBasicEndpointService<Category> basicEndpointService)
        {
            _mapper = mapper;
            _basicEndpoint = basicEndpointService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var Categories = _basicEndpoint.GetAll();
            return Ok(Categories);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CategoryDTO categoryDTO)
        {
            var newCategory = _mapper.Map<Category>(categoryDTO);
            await _basicEndpoint.Create(newCategory);
            return Ok();
        }
    }
}
