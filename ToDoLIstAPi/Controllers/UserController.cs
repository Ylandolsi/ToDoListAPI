using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using ToDoLIstAPi.DTO.Tasks;
using ToDoLIstAPi.DTO.User;

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
    [Authorize(Roles = "Admin")]
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
    
    public async Task<IActionResult> CreateUser([FromBody] Userinput user)
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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id , [FromBody] Userinput user)
    {
        if (id <= 0 || user == null)
        {
            return BadRequest("User object is null");
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        await _userService.UpdateUserAsync(id , user);
        return Ok();
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }
    [Authorize(Roles = "Admin")]
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
