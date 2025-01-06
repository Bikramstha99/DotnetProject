using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpPost("CreateUser")]
        [HasPermission(PermissionConstant.UserModuleCreateUser)]
        public async Task<IActionResult> CreateUser([FromBody] UserApiModel model)
        {
            try
            {
                if (await _userService.CheckIfUserNameExists(model.UserName))
                    throw new InvalidDataException("This UserName already exists");

                return Ok(new { data = await _userService.CreateUser(model) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UserController while creating user: {ex.Message}");
                throw;
            }
        }
    }
}
