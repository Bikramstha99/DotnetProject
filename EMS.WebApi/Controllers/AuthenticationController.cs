using EmployeeManagementWebApi.Controllers;
using EMS.Entities.Dtos;
using EMSBusiness.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppSettings _appSettings;
        public AuthenticationController(IAuthenticationService authenticationService, IHttpContextAccessor contextAccessor)
        {
            _authenticationService = authenticationService;
            _contextAccessor = contextAccessor;
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                var response = await _authenticationService.Authenticate(model);

                if (response == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                response.Token = "";
                return Ok(new { data = response });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
