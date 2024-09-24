using API.Business;
using API.Data.VO;
using API.Libraries.Validations;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientBusiness _clientBusiness;
        public ClientController(ILogger<ClientController> logger, IClientBusiness clientBusiness)
        {
            _logger = logger;
            _clientBusiness = clientBusiness;
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(ClientVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post( [FromBody] RegisterClientVO clientBsic)
        {
            if (clientBsic == null) return BadRequest();
            if (!EmailValidate.IsValidEmail(clientBsic.Email)) return BadRequest("Invalid Email");
            ClientVO client = new ClientVO
            {
                Name = clientBsic.Name,
                CellNumber = clientBsic.CellNumber,
                CellNumberAlternative = clientBsic.CellNumberAlternative,
                Email = clientBsic.Email,
            };

            var clientExist = _clientBusiness.FindByEmail(client.Email);
            if (clientExist != null) return BadRequest($"Client Already exists by email '{clientExist.Email}'");

            return Ok(_clientBusiness.Create(client));
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<ClientVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Policy = "AdminOrTechnician")]
        public IActionResult Get()
        {
            return Ok(_clientBusiness.FindAll());
        }

        [HttpGet("{email}")]
        [ProducesResponseType((200), Type = typeof(ClientVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(string email)
        {
            var client = _clientBusiness.FindByEmail(email);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPatch]
        [ProducesResponseType((200), Type = typeof(ClientVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Patch(ClientVO client)
        {
            if (client == null) return BadRequest();
            return Ok(_clientBusiness.Update(client));
        }


        [HttpPost]
        [Route("address/add")]
        [ProducesResponseType((200), Type = typeof(ClientVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromQuery] string email, [FromBody] AddressVO address)
        {
            if (!EmailValidate.IsValidEmail(email)) return BadRequest("Invalid Email");
            if (address == null) return BadRequest();

            var client = _clientBusiness.FindByEmail(email);
            if (client == null) return BadRequest("Client not found!");

            var result = _clientBusiness.AddAddress(client, address);
            return Ok(result);
        }

        [HttpPost]
        [Route("address/remove")]
        [ProducesResponseType((200), Type = typeof(ClientVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromQuery] string email, int addressID)
        {
            if (!EmailValidate.IsValidEmail(email)) return BadRequest("Invalid Email");
            var client = _clientBusiness.FindByEmail(email);
            if (client == null) return BadRequest("Client not found!");
            
            var result = _clientBusiness.RemoveAddress(client, addressID);
            if (result == null) return BadRequest("Address not found");

            return Ok(result);
        }

        [HttpPatch]
        [Route("address/update")]
        [ProducesResponseType((200), Type = typeof(ClientVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Patch([FromQuery] string email, [FromBody] AddressVO address)
        {
            if (!EmailValidate.IsValidEmail(email)) return BadRequest("Invalid Email");
            if (address == null) return BadRequest();

            var result = _clientBusiness.UpdateAddress(_clientBusiness.FindByEmail(email), address);
            if (result == null) return BadRequest();

            return Ok(result);
        }
    }
}
