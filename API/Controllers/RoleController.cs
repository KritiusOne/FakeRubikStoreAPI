using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IBasicEndpointService<Role> _roleService;
        private readonly IMapper _mapper;
        public RoleController(IBasicEndpointService<Role> role, IMapper mapper)
        {
            _roleService = role;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }
        [HttpPost]
        public async  Task<IActionResult> Post(RoleDTO roleDTO)
        {
            var role = _mapper.Map<Role>(roleDTO);
            await _roleService.Create(role);
            return Ok();
        }
    }
}
