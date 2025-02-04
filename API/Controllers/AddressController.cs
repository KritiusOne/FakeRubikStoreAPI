using API.CustomClass.External;
using API.Response;
using Aplication.CustomEntities;
using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost("/CreateCities")]
        [Authorize(Policy = "OnlyAdmins")]
        public async Task<IActionResult> CreateCities()
        {
            string json = await _AddressService.GetExternalCities();
            if(json == "Error")
            {
                return BadRequest("Error al llamar a las ciudades");
            }
            var jsonResponse = JsonConvert.DeserializeObject<FactusResponseBase<ExternalMunicipalities>>(json);
            await _AddressService.CreateExternalCitiesAndDepartament(jsonResponse.Data);
            return Ok(jsonResponse);
        }
    }
}
