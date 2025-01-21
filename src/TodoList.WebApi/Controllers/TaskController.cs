using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ToDoLIstAPi.Contracts;

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
    public async Task<IActionResult> GetTaskbyId(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id is invalid");
        }
    
        var taskResponse = await _taskService.GetTaskAsync(id);
        return Ok(taskResponse);
    }
    [HttpGet("collection")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetAllTaskAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskbyId([FromBody] Tasks inputTask)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity("Invalid input");
        }
        await _taskService.CreateTaskAsync(inputTask);
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
