using EmployeeManagementWebApi.Controllers;
using EMS.Business.Interface;
using EMS.Entities.Dtos.Employee;
using Microsoft.AspNetCore.Mvc;

namespace EMS.WebApi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class EmployeeController : BaseController
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //[Authorize]
        [HttpGet("GetAllEmployeeInformation")]

        public async Task<IActionResult> GetAllEmployeeInformation()
        {
            var booksDto = await _employeeService.GetAllEmployeeInformation();
            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        //[Authorize]
        [HttpPost("AddUpdateEmployee")]
        public async Task<IActionResult> AddUpdateEmployee([FromForm] EmployeeAddUpdateApiModel employeeAddUpdateApiModel)
        {
            await _employeeService.AddUpdateEmployee(employeeAddUpdateApiModel);
            return Ok();
        }


        //[HttpGet("GetById")]
        //public async Task<IActionResult> GetMovieById(int id)
        //{
        //    var movieDto = await _bookService.GetBookAsync(id);
        //    return Ok(movieDto);
        //}

        //[HttpDelete("Delete")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _bookService.DeleteBookAsync(id);
        //    return Ok();
        //}
    }
}
