using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Exceptions;
using ToDoLIstAPi.Contracts;
using ToDoLIstAPi.DbContext;

namespace ToDoLIstAPi.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ApplicationDbContext context, ILogger<TaskService> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Tasks> GetTaskAsync(int id)
    {
        _logger.LogInformation("Getting task by id ");
        var task = await _context.Set<Tasks>().Include(u => u.User) // Eager load the User 
            .FirstOrDefaultAsync(u => u.Id == id );
        if (task is null)
        {
            _logger.LogInformation("Task not found");
            throw new BadRequestException("Id is invalid");
        }

        _logger.LogInformation("Task found");
        return task;
    }

    public async Task<IEnumerable<Tasks>> GetAllTaskAsync()
    {
        _logger.LogInformation("Getting all tasks");
        return  await _context.Set<Tasks>().Include(t => t.User).ToListAsync();
         
    }

    public async Task CreateTaskAsync(Tasks task)
    {
        _logger.LogInformation("Creating task");
        await _context.Set<Tasks>().AddAsync(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task created with Success");
    }


    public async Task UpdateTaskAsync(Tasks task)
    {
        _logger.LogInformation("Updating task");
        _context.Set<Tasks>().Update(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Update with Success");
    }

    public async Task FinishTaskAsync(int id)
    {
        _logger.LogInformation("Marking Finish task");
        var task = await _context.Set<Tasks>().FindAsync(id);
        if (task is null)
        {
            _logger.LogInformation("Task not found");
            throw new BadRequestException("Id is invalid");
        }

        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        _logger.LogInformation("Marked Finished with Success");
    }

    public async Task DeleteTaskAsync(int id)
    {
        _logger.LogInformation("Deleting task");
        var task = await _context.Set<Tasks>().FindAsync(id);
        if (task is null)
        {
            _logger.LogInformation("Task not found");
            throw new BadRequestException("Id is invalid");
        }
        _context.Set<Tasks>().Remove(task);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Task deleted");
    }
}
