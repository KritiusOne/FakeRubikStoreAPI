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
            return Ok(Products);
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
