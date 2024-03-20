using API.Response;
using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public ProductController(IMapper map, IProductService productService)
        {
            this._productService = productService;
            _mapper = map;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var Products = _productService.GetAllProducts();
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var response = new ResponseBase<IEnumerable<ProductDTO>>(productsDTO, "This is all products");
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await _productService.AddProduct(product);
            return Ok();
        }
    }
}
