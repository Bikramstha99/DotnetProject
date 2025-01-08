using EMS.Business.Interface;
using EMS.Common.Constants;
using EMS.Entities.Dtos.User;
using EMS.Entities.Models;
using EMS.WebApi.Authorizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace EMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

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
                throw;
            }
        }
        [HttpGet("GetUser")]
        [HasPermission(PermissionConstant.UserModuleCreateUser)]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                return Ok(new { data = await _userService.GetUser() });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                return Ok(new { data = await _userService.GetAllRoles() });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("GetPermissionsOfRole")]
        public async Task<IActionResult> GetPermissionsOfRole(string roleId)
        {
            try
            {
                return Ok(new { data = await _userService.GetAllPermissionsByRoleId(roleId) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("GetUserRoleMapping")]

        public async Task<IActionResult> GetUserRoleMapping(string userId)
        {
            try
            {
                return Ok(new { data = await _userService.GetUserRoleMapping(userId) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("AddUserRoleMapping")]
        public async Task<IActionResult> AddUserRoleMapping([FromBody] List<UserRoleMappingApiModel> model)
        {
            try
            {
                return Ok(new { data = await _userService.AddUserRoleMapping(model) });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
