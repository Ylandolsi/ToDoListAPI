using Models.Entities;

namespace Contracts;

public interface ITaskService
{
    Task<List<Tasks>> GetAllTaskAsync();
    Task<Tasks> GetTaskAsync(int id);
    Task CreateTaskAsync(Tasks task);
    Task  UpdateTaskAsync(Tasks task);
    Task  DeleteTaskAsync(int id);
    Task  FinishTaskAsync(int id);

}
