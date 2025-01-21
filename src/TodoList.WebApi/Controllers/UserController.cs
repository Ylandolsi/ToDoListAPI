using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoLIstAPi.Contracts;
using ToDoLIstAPi.DTO.Tasks;
using ToDoLIstAPi.DTO.User;

namespace ToDoLIstAPi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    private IActionResult AuthorizeUser(int resourceId)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        if (userId is null)
            return BadRequest("User doesn't have an Id");

        var role = HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Role))?.Value;
        if (role != "Admin" && resourceId != int.Parse(userId))
        {
            return Unauthorized($"You are not allowed to access the resource with ID {resourceId}");
        }

        return null;
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("collection")]
    public async Task<IActionResult> GetUsers()
    {
        var userNameOfUserWhoSentRequest =
            HttpContext.User.Claims.FirstOrDefault(u => u.Type.Equals(ClaimTypes.Name))?.Value;
        _logger.LogInformation($"User {userNameOfUserWhoSentRequest} is requesting all users");
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        if (id <= 0)
            return BadRequest("Id is invalid");

        var authResult = AuthorizeUser(id);
        if (authResult is not null)
            return authResult;
        var user = await _userService.GetUserAsync(id);
        return Ok(user);
    }

    [Authorize]
    [HttpGet("{id}/tasks")]
    public async Task<IActionResult> GetTasksOfUser(int id)
    {
        var authResult = AuthorizeUser(id);
        if (authResult is not null)
            return authResult;
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

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] Userinput user)
    {
        if (id <= 0 || user == null)
        {
            return BadRequest("User object is null");
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var authResult = AuthorizeUser(id);

        if (authResult is not null)
            return authResult;
        await _userService.UpdateUserAsync(id, user);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var authResult = AuthorizeUser(id);

        if (authResult is not null)
            return authResult;
        await _userService.DeleteUserAsync(id);
        return Ok();
    }

    [Authorize]
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

        var authResult = AuthorizeUser(userId);

        if (authResult is not null)
            return authResult;

        await _userService.AddTaskToUserAsync(userId, task);
        return Ok();
    }
}
