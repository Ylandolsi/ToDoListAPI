using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ToDoLIstAPi.DTO.Tasks;

namespace ToDoLIstAPi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("collection")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserAsync(id);
        return Ok(user);
    }
    [HttpGet("{id}/tasks")]
    public async Task<IActionResult> GetTasksOfUser(int id)
    {
        var user = await _userService.GetAllTasksAsync(id);
        return Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User object is null");
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _userService.CreateUserAsync(user);
        return Ok();
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User object is null");
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _userService.UpdateUserAsync(user);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
    [HttpPost("{userId}/tasks")]
    public async Task<IActionResult> AddTaskToUser(int userId, [FromBody] TaskAddForUserDto task)
    {
        if (task == null)
        {
            return BadRequest("Task object is null");
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _userService.AddTaskToUserAsync(userId, task);
        return Ok();
    }

}
