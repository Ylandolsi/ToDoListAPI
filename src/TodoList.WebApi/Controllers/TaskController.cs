using System.Security.Claims;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ToDoLIstAPi.Contracts;
using ToDoLIstAPi.DTO.Tasks;

namespace ToDoLIstAPi.Controllers;

[Route("api/Tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
    
        var taskResponse = await _taskService.GetTaskAsync(id);
        return Ok(taskResponse);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("collection")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTaskAsync();
        return Ok(tasks);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTaskbyId([FromBody] TaskAddForUserDto inputTask)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity("Invalid input");
        }
        var userId = HttpContext.User.Claims.FirstOrDefault( u => u.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        if (userId is null)
            return BadRequest("User dosent Have an Id"); 
        await _taskService.CreateTaskAsync(int.Parse(userId), inputTask);
        return NoContent();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateTaskbyId([FromBody] Tasks inputTask)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity("Invalid input");
        }
        await _taskService.UpdateTaskAsync(inputTask);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
    [HttpPut("finish/{id}")]
    public async Task<IActionResult> FinishTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
        await _taskService.FinishTaskAsync(id);
        return NoContent();
    }
    
}
