using AutoMapper;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Exceptions;
using ToDoLIstAPi.DbContext;
using ToDoLIstAPi.DTO.Tasks;
using ToDoLIstAPi.DTO.User;

namespace ToDoLIstAPi.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly ITaskService _taskService;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper; 
    public UserService(ApplicationDbContext context, ITaskService taskService, ILogger<UserService> logger, IMapper mapper)
    {
        _context = context;
        _taskService = taskService;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task CreateUserAsync(Userinput user)
    {
        _logger.LogInformation("Creating user");
        var usser = _mapper.Map<User>(user);
        _logger.LogInformation("Hashing password");
        usser.PasswordHash = UserValidate.HashPassword(user.Password);
        
        await _context.Set<User>().AddAsync(usser);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User created with Success");
    }

    public async Task UpdateUserAsync(int id , Userinput user)
    {
        _logger.LogInformation("Updating user");
        _logger.LogInformation("Mapping user");
        var usser = _mapper.Map<User>(user);
        _logger.LogInformation("Hashing password");
        usser.PasswordHash = UserValidate.HashPassword(user.Password);
        usser.Id = id; 
        _context.Set<User>().Update(usser);
        
        await _context.SaveChangesAsync();
        _logger.LogInformation("Update with Success");
    }

    public async Task DeleteUserAsync(int id)
    {
        _logger.LogInformation("Deleting user");
        var user = await _context.Set<User>().FindAsync(id);
        if (user is null)
        {
            _logger.LogInformation("User not found");
            throw new BadRequestException("Id is invalid");
        }

        _context.Set<User>().Remove(user);
        await _context.SaveChangesAsync();
        _logger.LogInformation("User deleted with Success");
    }

    public async Task<UserDtoOutput> GetUserAsync(int id)
    {
        _logger.LogInformation("Getting user by id ");
        var user = await _context.Set<User>().FindAsync(id);
        if (user is null)
        {
            _logger.LogInformation("User not found");
            throw new BadRequestException("Id is invalid");
        }

        _logger.LogInformation("User found");
        return _mapper.Map<UserDtoOutput>(user);
    }

    public async Task<IEnumerable<UserDtoOutput>> GetAllUsersAsync()
    {
        _logger.LogInformation("Getting all users"); 
        var users = await _context.Set<User>().ToListAsync();
        var usersDtos = _mapper.Map<IEnumerable<UserDtoOutput>>(users);
        return usersDtos;
    }

    public async Task<IEnumerable<TaskOutputDto>> GetAllTasksAsync(int userId)
    {
        _logger.LogInformation("Getting all Active tasks by user id");
        var tasks = await _context.Set<Tasks>().Where(t => t.UserId == userId ).ToListAsync();
        var tasksDtos = _mapper.Map<IEnumerable<TaskOutputDto>>(tasks);
        return tasksDtos;
    }

    public async Task AddTaskToUserAsync(int userId, TaskAddForUserDto taskDto)
    {
        _logger.LogInformation("Adding task to user");
        var user = await _context.Set<User>().FindAsync(userId);
        if (user is null)
        {
            _logger.LogInformation("User not found");
            throw new BadRequestException("Id is invalid");
        }

        var task = _mapper.Map<Tasks>(taskDto);
        task.UserId = userId;
        await _taskService.CreateTaskAsync(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task added to user with Success");
    }
}
