﻿using API.Response;
using Aplication.DTOs;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IBasicEndpointService<State> _basicEndpoint;
        private readonly IMapper _mapper;
        public StateController(IBasicEndpointService<State> basicEndpointService, IMapper mapper)
        {
            _basicEndpoint = basicEndpointService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var States = _basicEndpoint.GetAll();
            var statesDTO = _mapper.Map<IEnumerable<StateDTO>>(States);
            var response = new ResponseBase<IEnumerable<StateDTO>>(statesDTO, "This is the all states of deliveries");
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(StateDTO stateDTO)
        {
            var state = _mapper.Map<State>(stateDTO);
            await _basicEndpoint.Create(state);
            return Ok();
        }
    }
}
