using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Entities;
using Models.Error;
using ToDoLIstAPi.DbContext;

namespace Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(ApplicationDbContext context, ILogger<UserService> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Result<Tasks>> GetTaskAsync(int id)
    {
        _logger.LogInformation("Getting task by id ");
        var task = await _context.Set<Tasks>().FindAsync(id);
        if (task is null)
        {
            _logger.LogInformation("Task not found");
            return Result<Tasks>.Failure(new Error(400, "Task not found"));
        }

        return Result<Tasks>.Success(task);
    }

    public async Task<Result<IEnumerable<Tasks>>> GetAllTaskAsync()
    {
        _logger.LogInformation("Getting all tasks");
        return Result<IEnumerable<Tasks>>.Success(await _context.Set<Tasks>().ToListAsync());
    }

    public async Task<Result<Tasks>> CreateTaskAsync(Tasks task)
    {
        _logger.LogInformation("Creating task");
        _context.Set<Tasks>().Add(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task created with Success");
        return Result<Tasks>.Success(task);
    }

    public async Task<Result<Tasks>> UpdateTaskAsync(Tasks task)
    {
        _logger.LogInformation("Updating task");
        _context.Set<Tasks>().Update(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Update with Success");
        return Result<Tasks>.Success(task);
    }

    public async Task<Result<Tasks>> FinishTaskAsync(int id)
    {
        _logger.LogInformation("Marking Finish task");
        var task = await _context.Set<Tasks>().FindAsync(id);
        if (task is null)
        {
            _logger.LogInformation("Task not found");
            return Result<Tasks>.Failure(new Error(400, "Task not found"));
        }

        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Marked Finished with Success");
        return Result<Tasks>.Success(task);
    }

    public async Task<Result<Tasks>> DeleteTaskAsync(int id)
    {
        _logger.LogInformation("Deleting task");
        var task = await _context.Set<Tasks>().FindAsync(id);
        if (task is null)
        {
            _logger.LogInformation("Task not found");
            return Result<Tasks>.Failure(new Error(400, "Task not found"));
        }

        _context.Set<Tasks>().Remove(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task deleted");
        return Result<Tasks>.Success(task);
    }
}
