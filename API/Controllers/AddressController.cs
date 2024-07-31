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
        [Authorize(Policy = "OnlyAdmins")]
        public IActionResult GetAll()
        {
            var AllAddress = _AddressService.GetAll();
            var AllAddressWithUsersDTO = _mapper.Map<IEnumerable<AddressWithUserDTO>>(AllAddress);
            var response = new ResponseBase<IEnumerable<AddressWithUserDTO>>(AllAddressWithUsersDTO, "This are all directions with users");
            return Ok(response);
        }
        [HttpGet("id")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var Address = _AddressService.GetById(id);
            var AddressDTO = _mapper.Map<AddressWithUserDTO>(Address);
            return Ok(AddressDTO);
        }
        [HttpPut("id")]
        [Authorize]
        public async Task<IActionResult> Update(int id, AddressDTO dto)
        {
            var AddressToUpdate = _mapper.Map<UserDirection>(dto);
            var directionResponse = await _AddressService.Update(id, AddressToUpdate);
            var directionResponseDTO = _mapper.Map<AddressWithUserDTO>(directionResponse);
            var response = new ResponseBase<AddressWithUserDTO>(directionResponseDTO, "the direction update was success");
            return Ok(response);
        }
    }
}
