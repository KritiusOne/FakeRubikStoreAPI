using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _delivery;
        private readonly IMapper _map;
        public DeliveryController(IDeliveryService delivery, IMapper map)
        {
            _delivery = delivery;
            _map = map;
        }
        [HttpPut("id")]
        [Authorize]
        public async Task<IActionResult> UpdateState(int Id, int newState)
        {
            await _delivery.UpdateState(newState, Id);
            return Ok($"El nuevo estado del envío {Id} es {newState}");
        }
    }
}
