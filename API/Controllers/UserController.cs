using API.Business;
using API.Data.VO;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class UserController : ControllerBase
    {
        private IUserBusiness _userBusiness;
        private IRoleBusiness _roleBusiness;

        public UserController(IUserBusiness userBusiness, IRoleBusiness roleBusiness)
        {
            _userBusiness = userBusiness;
            _roleBusiness = roleBusiness;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult AddUser([FromBody] RegisterUserVO user)
        {
            var validateFiels = _userBusiness.IsvalidRequirementsRegisterUserVO(user);
            if (validateFiels != null) return BadRequest(validateFiels);

            try
            {
                _userBusiness.RegisterUser(user);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize("Bearer")]
        [Authorize("AdminOrTechnician")]
        //[Authorize("Tecnichian")]
        public IActionResult Get(string? email)
        {
            if (email == null)
                return Ok(_userBusiness.GetUsers());

            var user = _userBusiness.GetUserByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }
        
        [HttpPost()]
        [Route("addRole")]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize("Bearer")]
        [Authorize("Admin")]
        public IActionResult AddRole([FromQuery] string email, [FromBody]  RoleVO role)
        {
            if(string.IsNullOrEmpty(email) || role == null) return BadRequest();

            role = _roleBusiness.FindByID(role.Id);
            if (role == null) return BadRequest("Role not found");

            var user = _userBusiness.AddRole(email, role);
            if (user == null) return BadRequest("User not found");
            
            return Ok(user);
        }
        [HttpPost()]
        [Route("removeRole")]
        [ProducesResponseType((200), Type = typeof(UserVO))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize("Bearer")]
        [Authorize("Admin")]
        public IActionResult RemoveRole([FromQuery] string email, [FromBody] RoleVO role)
        {
            if (string.IsNullOrEmpty(email) || role == null) return BadRequest();

            role = _roleBusiness.FindByID(role.Id);
            if (role == null) return BadRequest("Role not found");

            var user = _userBusiness.RemoveRole(email, role);
            if (user == null) return BadRequest("User not found");

            return Ok(user);

        }
    }
}
