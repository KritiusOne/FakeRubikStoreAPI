using API.CustomClass;
using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs.Products;
using Aplication.Entities;
using Aplication.Interfaces;
using Aplication.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IUriService _uriService;
        private readonly IConfiguration _config;
        public ProductController(IMapper map, IProductService productService, IUriService uriService, IConfiguration config)
        {
            this._productService = productService;
            _mapper = map;
            this._uriService = uriService;
            _config = config;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] ProductQueryFilter filters)
        {
            var numberPrevious = filters.PageNumber - 1;
            var numberNext = filters.PageNumber + 1;
            var Products = _productService.GetAllProducts(filters);
            var productsDTO = _mapper.Map<IEnumerable<ProductBasicInfoDTO>>(Products);
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                {"ProductID", filters.ProductID.ToString() },
                {"MinPrice", filters.MinPrice.ToString()},
                {"MaxPrice", filters.MaxPrice.ToString() },
                {"NameProduct", filters.NameProduct },
                {"DescriptionProduct", filters.DescriptionProduct },
                {"PageSize", filters.PageSize == 0 ? "1" : filters.PageSize.ToString() }
            };
            var previousQueryParams = queryParams;
            previousQueryParams["PageNumber"] = Products.hasPreviousPage == false ? "false" : numberPrevious.ToString();
            var previousParamsURL = QueryHelpers.AddQueryString("https://apifakerubikstore.azurewebsites.net/api/Product", previousQueryParams);

            var nextQueryParams = queryParams;
            nextQueryParams["PageNumber"] = Products.hasNextPage == true ? numberNext.ToString() : "false";
            var nextParamsURL = QueryHelpers.AddQueryString("https://apifakerubikstore.azurewebsites.net/api/Product", nextQueryParams);

            MetaData metaData = new MetaData()
            {
                CurrentPage = Products.CurrentPage,
                HasNextPage = Products.hasNextPage,
                HasPreviousPage = Products.hasPreviousPage,
                PageSize = Products.PageSize,
                TotalCount = Products.PageCount,
                TotalPage = Products.TotalPages,
                NextPageURL = _uriService.GetPostPaginationUri(filters, nextParamsURL).ToString(),
                PreviousPageURL = _uriService.GetPostPaginationUri(filters, previousParamsURL).ToString()
            };
            var response = new ResponsePagination<IEnumerable<ProductBasicInfoDTO>>(productsDTO, 
                "This is all products",
                200, 
                metaData);
            return Ok(response);
        }
        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var ProductSelected = _productService.GetById(id);
            var productDTO = _mapper.Map<ProductWithAllDataDTO>(ProductSelected);
            var response = new ResponseBase<ProductWithAllDataDTO>(productDTO, "This is the product selected");
            return Ok(response);
        }
        [HttpPost]
        [Authorize(Policy = "OnlyAdmins")]
        public async Task<IActionResult> Post([FromForm] CreateProductWithImgDTO AllInfoProduct)
        {
            var product = _mapper.Map<Product>(AllInfoProduct.InfoProduct);
            using(Stream thumbnail = AllInfoProduct.ThumbnailImage.OpenReadStream())
            {
                using(Stream productImg = AllInfoProduct.ProductImage.OpenReadStream())
                {
                    await _productService.AddProduct(product, thumbnail, productImg, _config["BlobStorage:ConnectionString"]);
                }
            }
            return Ok();
        }
        [HttpPut("update")]
        [Authorize(Policy = "OnlyAdmins")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductInfoUpdateDTO InfoProduct)
        {
            string key = _config["BlobStorage:ConnectionString"];
            using(Stream thumbnailImg = InfoProduct.ThumbnailImage.OpenReadStream())
            {
                using(Stream productImg = InfoProduct.ProductImage.OpenReadStream())
                {
                    thumbnailImg.Position = 0;
                    productImg.Position = 0;
                    var product = _mapper.Map<Product>(InfoProduct.InfoProduct);
                    await _productService.UpdateProduct(thumbnailImg, productImg, product, key, product.Id);
                }
            }
            return Ok("El registro a sido actualizado con exito");
        }
    }
}
