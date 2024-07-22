using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs.Orders;
using Aplication.Entities;
using Aplication.Interfaces;
using Aplication.QueryFilters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IMapper _map;
        private readonly IUriService _uriService;
        public OrderController(IOrderService service, IMapper map, IUriService uriService)
        {
            _map = map;
            this._service = service;
            _uriService = uriService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] OrderQueryFilters filters)
        {
            var pagePrev = filters.PageNumber != 1 ? filters.PageNumber - 1 : 1;
            var pageNext = filters.PageNumber + 1;
            var AllOrders = _service.GetAll(filters);
            var AllOrdersDTO = _map.Map<IEnumerable<OrderBasicDTO>>(AllOrders);
            Dictionary<string, string> queryParams = new Dictionary<string, string>
            {
                {"Date", filters.Date?.ToShortDateString()},
                {"MinPrice", filters.MinPrice.ToString()},
                {"MaxPrice", filters.MaxPrice.ToString() },
                {"MaxDate", filters.MaxDate?.ToShortDateString() },
                {"MinDate", filters.MinDate?.ToShortDateString()},
                {"PageSize", filters.PageSize == 0 ? "1" : filters.PageSize.ToString() }
            };
            var previousQueryParams = queryParams;
            previousQueryParams["PageNumber"] = AllOrders.hasPreviousPage == false ? "false" : pagePrev.ToString();
            var previousParamsURL = QueryHelpers.AddQueryString("https://apifakerubikstore.azurewebsites.net/api/Order", previousQueryParams);

            var nextQueryParams = queryParams;
            nextQueryParams["PageNumber"] = AllOrders.hasNextPage == true ? pageNext.ToString() : "false";
            var nextParamsURL = QueryHelpers.AddQueryString("https://apifakerubikstore.azurewebsites.net/api/Order", nextQueryParams);
            MetaData metaData = new MetaData()
            {
                CurrentPage = AllOrders.CurrentPage,
                HasNextPage = AllOrders.hasNextPage,
                HasPreviousPage = AllOrders.hasPreviousPage,
                PageSize = AllOrders.PageSize,
                TotalCount = AllOrders.PageCount,
                TotalPage = AllOrders.TotalPages,
                NextPageURL = _uriService.GetPaginationOrder(filters, nextParamsURL).ToString(),
                PreviousPageURL = _uriService.GetPaginationOrder(filters, previousParamsURL).ToString()
            };
            var response = new ResponsePagination<IEnumerable<OrderBasicDTO>>(AllOrdersDTO,
                "this all Orders filtered",
                200, metaData);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDTO dto)
        {
            var newOrder = _map.Map<Order>(dto);
            var CreatedOrder = await _service.CreateOrder(newOrder);
            var DTO = _map.Map<OrderBasicDTO>(CreatedOrder);
            return Ok(DTO);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var Searched = await _service.GetById(id);
            var DTO = _map.Map<OrderCompleteInfoDTO>(Searched);
            return Ok(DTO);
        }
    }
}
