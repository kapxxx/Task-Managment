using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersAndTasks([FromQuery] string filterText = null,
            [FromQuery] string sortColumn = "UserName",
            [FromQuery] string sortOrder = "ASC",
            [FromQuery] int? taskStatus = null,
            [FromQuery] int? taskPriority = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var filterDto = new UserTaskFilterDTO
            {
                FilterText = filterText,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                TaskStatus = taskStatus,
                TaskPriority = taskPriority,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Ok(await _userManager.GetAllUserTasks(filterDto));
        }
    }
}
