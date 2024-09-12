using API.Business;
using API.Data.VO;
using API.Libraries.Validations;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] LoginVO user)
        { 
            if (user == null) return BadRequest("Invalid client request");

            if (!EmailValidate.IsValidEmail(user.Email)) return BadRequest("Invalid Email");

            if (!PasswordValidateRequirements.IsValid(user.Password)) return BadRequest(PasswordValidateRequirements.RequirementsString());

            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null) return Unauthorized("Invalid email or password");
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo is null) return BadRequest("Ivalid client request");
            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null) return BadRequest("Ivalid client request");
            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);
            if (!result) return BadRequest("Ivalid client request");
            return NoContent();
        }
    }
}
