using API.Response;
using Aplication.DTOs.Products;
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
        private readonly ICategoryService _service;
        public CategoryController(IMapper mapper, IBasicEndpointService<Category> basicEndpointService, ICategoryService service)
        {
            _mapper = mapper;
            _basicEndpoint = basicEndpointService;
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var Categories = _basicEndpoint.GetAll();
            var categoriesDTO = _mapper.Map<IEnumerable<CategoryDTO>>(Categories);
            var response = new ResponseBase<IEnumerable<CategoryDTO>>(categoriesDTO, "This is the all categories");
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateCategoryDTO categoryDTO)
        {
            var newCategory = _mapper.Map<Category>(categoryDTO);
            await _service.CreateCategory(newCategory);
            return Ok();
        }
        [HttpPost("/addPC")]
        public async Task<IActionResult> NewCategoryToProducts(CategoryManyProductsDTO dto)
        {
            var newsCategoriesProducts = _mapper.Map<ICollection<ProductCategory>>(dto.categoryProductDTOs);
            await _service.CreateManyProductsCategories(newsCategoriesProducts);
            return Ok("Se realizo correctamente");
        }
    }
}
