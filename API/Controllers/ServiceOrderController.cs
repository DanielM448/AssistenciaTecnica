using API.Business;
using API.Data.VO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Enums;

namespace API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Authorize(Policy = "AdminOrTechnician")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class ServiceOrderController : ControllerBase
    {
        private readonly ILogger<ServiceOrderController> _logger;
        private readonly IServiceOrderBusiness _serviceOrderBusiness;
        public ServiceOrderController(ILogger<ServiceOrderController> logger, IServiceOrderBusiness serviceOrderBusiness)
        {
            _logger = logger;
            _serviceOrderBusiness = serviceOrderBusiness;
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] EditServiceOrderVO serviceOrder)
        {
            if (serviceOrder == null) return BadRequest();
            return Ok(_serviceOrderBusiness.Create(serviceOrder));
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            return Ok(_serviceOrderBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int id)
        {
            var serviceOrder = _serviceOrderBusiness.FindById(id);

            if (serviceOrder == null) return NotFound();

            return Ok(serviceOrder);
        }

        [HttpGet]
        [Route("status/{status}")]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(ServiceOrderStatus status)
        {
            var serviceOrder = _serviceOrderBusiness.FindByStatus(status);

            if (serviceOrder == null) return NotFound();

            return Ok(serviceOrder);
        }

        [HttpPut]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put(EditServiceOrderVO serviceOrder)
        {
            if (serviceOrder == null) return BadRequest();
            bool existsStatus = Enum.IsDefined(typeof(ServiceOrderStatus), serviceOrder.Status);

            if (!existsStatus) return BadRequest("Status Not Found");

            var result = _serviceOrderBusiness.Update(serviceOrder);           

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        [Route("parts/{serviceOrderId}")]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post(int serviceOrderId, [FromBody] List<PartVO> parts)
        {
            var serviceOrder = _serviceOrderBusiness.FindById(serviceOrderId);
            if (serviceOrder == null) return NotFound("Service Order Not Found");

            if (parts == null || parts.Count == 0) BadRequest();

            return Ok( _serviceOrderBusiness.AddParts(serviceOrderId, parts));
        }

        [HttpPut]
        [Route("parts/{serviceOrderId}")]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put(int serviceOrderId, [FromBody] List<PartVO> parts)
        {
            var serviceOrder = _serviceOrderBusiness.FindById(serviceOrderId);
            if (serviceOrder == null) return NotFound("Service Order Not Found");

            if (parts == null || parts.Count == 0) BadRequest();

            return Ok(_serviceOrderBusiness.UpdateParts(serviceOrderId, parts));
        }

        [HttpDelete]
        [Route("parts/{serviceOrderId}")]
        [ProducesResponseType((200), Type = typeof(ServiceOrderVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int serviceOrderId, [FromQuery] int partId)
        {
            var serviceOrder = _serviceOrderBusiness.FindById(serviceOrderId);
            if (serviceOrder == null) return NotFound("Service Order Not Found");

            if (!serviceOrder.Parts.Exists(p => p.Id == partId)) return NotFound("Part Not Found");

            return Ok(_serviceOrderBusiness.DeleteParts(serviceOrderId, partId));
        }
    }
}
