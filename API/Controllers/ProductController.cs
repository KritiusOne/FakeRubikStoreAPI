using API.Response;
using Aplication.CustomEntities;
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
            MetaData metaData = new MetaData()
            {
                CurrentPage = Products.CurrentPage,
                HasNextPage = Products.hasNextPage,
                HasPreviousPage = Products.hasPreviousPage,
                PageSize = Products.PageSize,
                TotalCount = Products.PageCount,
                TotalPage = Products.TotalPages
            };
            var response = new ResponsePagination<IEnumerable<ProductDTO>>(productsDTO, 
                "This is all products",
                200, 
                metaData);
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
