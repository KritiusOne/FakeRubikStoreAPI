using API.Response;
using Aplication.DTOs.Cards;
using Aplication.Entities;
using Aplication.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardServices _cardServices;
        private readonly IMapper _map;
        public CardController(ICardServices _cardServices, IMapper _map)
        {
            this._cardServices = _cardServices;
            this._map = _map;
        }
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetCardById(int id)
        {
            Card card = _cardServices.GetCardById(id);
            CardDTO dto = _map.Map<CardDTO>(card);
            if(dto == null)
            {
                return NotFound();
            }
            ResponseBase<CardDTO> response = new ResponseBase<CardDTO>(dto, "This is your card");
            return Ok(response);
        }
        [HttpPost("/create")]
        [Authorize]
        public async Task<IActionResult> CreateCard(CreateCardDTO dto)
        {
            var IdUser = HttpContext.User.Claims.FirstOrDefault(token => token.Type == "Id")?.Value;
            if(IdUser == null || IdUser == "" || !Int32.TryParse(IdUser, out int userIdInt))
            {
                return StatusCode(406, "The Id Claim it's invalid");
            }

            var Card = _map.Map<Card>(dto);
            int state = await _cardServices.CreateCard(Card, userIdInt);

            if(state == -1)
            {
                return StatusCode(502, "We can't create your card");
            }
            return Created($"Your card was created", state);
        }
        [HttpPut("/update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCard(CardInfoDTO dto)
        {
            var IdUser = HttpContext.User.Claims.FirstOrDefault(token => token.Type == "Id")?.Value;
            if (IdUser == null || IdUser == "" || !Int32.TryParse(IdUser, out int userIdInt))
            {
                return StatusCode(406, "The Id Claim it's invalid");
            }
            Card card = _map.Map<Card>(dto);
            int status = await _cardServices.Updatecard(card, userIdInt);

            if(status == -1) return StatusCode(502, "We can't Update your card");
            return Ok($"Your card was updated {status}");
        }
    }
}
