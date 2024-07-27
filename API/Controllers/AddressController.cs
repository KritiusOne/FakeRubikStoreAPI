using API.Response;
using Aplication.DTOs;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDirectionService _AddressService;
        public AddressController(IMapper map, IDirectionService address)
        {
            this._mapper = map;
            this._AddressService = address;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var AllAddress = _AddressService.GetAll();
            var AllAddressWithUsersDTO = _mapper.Map<IEnumerable<AddressWithUserDTO>>(AllAddress);
            var response = new ResponseBase<IEnumerable<AddressWithUserDTO>>(AllAddressWithUsersDTO, "This are all directions with users");
            return Ok(response);
        }
    }
}
