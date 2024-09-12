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
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class RoleController : ControllerBase
    {
        private IRoleBusiness _roleBusiness;
        public RoleController(IRoleBusiness roleBusiness)
        {
            _roleBusiness = roleBusiness;
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(RoleVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Get(int id)
        {
            var role = _roleBusiness.FindByID(id);
            if (role == null) return NotFound();

            return Ok(role);
        }
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<RoleVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult Get()
        {
            return Ok(_roleBusiness.FindAll());
        }
    }
}
