using Models.Entities;
using ToDoLIstAPi.DTO.Tasks;

namespace ToDoLIstAPi.Contracts;

public interface ITaskService
{
    Task<IEnumerable<Tasks>> GetAllTaskAsync();
    Task<Tasks> GetTaskAsync(int id);
    Task CreateTaskAsync( TaskAddForUserDto task); 

    Task  UpdateTaskAsync(Tasks task);
    Task  DeleteTaskAsync(int id);
    Task  FinishTaskAsync(int id);

}
