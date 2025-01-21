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
    private readonly ILogger<TaskController> _logger;

    public TaskController(ITaskService taskService , 
        ILogger<TaskController> logger)
    {
        _logger = logger;
        _taskService = taskService;
    }


    private IActionResult AuthorizeUser(Tasks task ) 
    {
        var userId = HttpContext.User.Claims.FirstOrDefault( u => u.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        if (userId is null)
            return BadRequest("User dosent Have an Id");
        var role = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Role))?.Value;
        if ( role != "Admin" && task.UserId != int.Parse(userId))
        {
            return Unauthorized("You are not allowed to Modify this task");
        }

        return null; 
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
        var adminName = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name))?.Value;
        _logger.LogInformation($"admin {adminName} is requesting task with id {id}");
        var taskResponse = await _taskService.GetTaskAsync(id);
        return Ok(taskResponse);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("collection")]
    public async Task<IActionResult> GetTasks()
    {
        var adminName = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name))?.Value;
        _logger.LogInformation($"admin {adminName} is requesting all the tasks");
        var tasks = await _taskService.GetAllTaskAsync();
        return Ok(tasks);
    }

    [Authorize]
    [HttpPost]
    // user create a task for himself
    public async Task<IActionResult> CreateTaskForTheUser([FromBody] TaskAddForUserDto inputTask)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity("Invalid input");
        }
        var userId = HttpContext.User.Claims.FirstOrDefault( u => u.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        if (userId is null)
            return BadRequest("User dosent Have an Id"); 
        inputTask.UserId = int.Parse(userId);
        await _taskService.CreateTaskAsync(inputTask);
        return NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("admin")]
    // admin create a task for a user
    public async Task<IActionResult> CreateTaskByAdmin([FromBody] TaskAddForUserDto inputTask)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity("Invalid input");
        }
        var adminName = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name))?.Value;
        _logger.LogInformation($"admin {adminName} is adding  a task for user with id {inputTask.UserId}");
        await _taskService.CreateTaskAsync(inputTask);
        return NoContent();
    }
    
    
    
    [Authorize]
    [HttpPut("{id}")]
    // (Admin && user who is assigned to the task) can update the task
    public async Task<IActionResult> UpdateTaskbyId([FromBody]  Tasks inputTask)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity("Invalid input");
        }
        _logger.LogInformation("Checking if user is allowed to update the task");

        var authResult = AuthorizeUser(inputTask);
        if ( authResult is not null)
        {
            return authResult;
        }
        await _taskService.UpdateTaskAsync(inputTask);
        return NoContent();
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
        _logger.LogInformation("Checking if user is allowed to delete the task");
        var task = await _taskService.GetTaskAsync(id);
        
        if ( task is null)
        {
            return BadRequest("Task not found");
        }
        var authResult = AuthorizeUser(task);
        if ( authResult is not null)
        {
            return authResult;
        }
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
    [Authorize]

    [HttpPut("finish/{id}")]
    public async Task<IActionResult> FinishTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
        _logger.LogInformation("Checking if user is allowed to finish the task");
        var task = await _taskService.GetTaskAsync(id);
        if ( task is null)
        {
            return BadRequest("Task not found");
        }
    
        var authResult = AuthorizeUser(task);
        if ( authResult is not null)
        {
            return authResult;
        }
        await _taskService.FinishTaskAsync(id);
        return NoContent();
    }
    
}
