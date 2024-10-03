using DTOs.RequestDTO;
using DTOs.ResponseDTO;
using Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Task_managment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManager _taskManager;

        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        // POST: api/task
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateDTO taskCreateDTO)
        {
            if (taskCreateDTO == null)
            {
                return BadRequest("Task data is null.");
            }

            var createdTask = await _taskManager.CreateAsync(taskCreateDTO);
            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            try
            {
                var task = await _taskManager.GetAsync(id);
                return Ok(task);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found.");
            }
        }

        // PUT: api/task
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id,[FromBody] TaskUpdateDTO taskUpdateDTO)
        {
            if (taskUpdateDTO == null)
            {
                return BadRequest("Task data is null.");
            }

            try
            {
                var updatedTask = await _taskManager.UpdateAsync(id,taskUpdateDTO);
                return Ok(updatedTask);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found.");
            }
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                await _taskManager.DeleteAsync(id);
                return NoContent(); // Successful deletion
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Task not found.");
            }
        }
        [HttpGet("GetUserTasks")]
        public async Task<ActionResult<List<TaskDTO>>> GetUserTasks(
        [FromQuery] Guid? userId, // User ID to filter tasks by
        [FromQuery] string taskName = null,
        [FromQuery] string description = null,
        [FromQuery] string sortColumn = "Name", // Default sort column
        [FromQuery] string sortOrder = "ASC",   // Default sort order
        [FromQuery] int? taskStatus = null,
        [FromQuery] int? taskPriority = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            // Create filter DTO
            var filterDto = new GetTasksByUserIdDto
            {
                TaskName = taskName,
                Description = description,
                TaskStatus = taskStatus,
                TaskPriority = taskPriority,
                StartDate = startDate,
                EndDate = endDate,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Call service to get tasks
            var tasks = await _taskManager.GetAllByUserIdAsync(userId, filterDto);

            // Check if tasks are found
           

            return Ok(tasks); // Return the list of tasks with 200 OK
        }
    }
}
