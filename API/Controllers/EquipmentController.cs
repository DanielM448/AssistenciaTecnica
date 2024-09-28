using API.Business;
using API.Data.VO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Authorize(Policy = "AdminOrTechnician")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class EquipmentController : ControllerBase
    {
        private readonly ILogger<EquipmentController> _logger;
        private readonly IEquipmentBusiness _equipmentBusiness;
        public EquipmentController(ILogger<EquipmentController> logger, IEquipmentBusiness equipmentBusiness)
        {
            _logger = logger;
            _equipmentBusiness = equipmentBusiness;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<EquipmentVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_equipmentBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(EquipmentVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int id)
        {
            var equipment = _equipmentBusiness.FindByID(id);
            if (equipment == null) return NotFound();

            return Ok(equipment);            
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(EquipmentVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] EquipmentVO equipment)
        {
            if (equipment == null) return BadRequest();
            return Ok(_equipmentBusiness.Create(equipment));
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(EquipmentVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] EquipmentVO equipment)
        {
            if(equipment == null) return BadRequest();  

            return Ok(_equipmentBusiness.Update(equipment));
        }
    }
}
