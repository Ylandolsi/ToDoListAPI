using Models.Entities;

namespace Contracts;

public interface IUserService{
    Task<Result<IEnumerable<Tasks>>> GetAllTaskAsync();
    Task<Result<Tasks>> GetTaskAsync(int id);
    Task<Result<Tasks>> CreateTaskAsync(Tasks task);
    Task<Result<Tasks>> UpdateTaskAsync(Tasks task);
    Task<Result<Tasks>> DeleteTaskAsync(int id);
    Task<Result<Tasks>> FinishTaskAsync(int id);
 
}
