using Models.Entities;
using ToDoLIstAPi.DTO.Tasks;
using ToDoLIstAPi.DTO.User;

namespace ToDoLIstAPi.Contracts;

public interface IUserService
{
    Task CreateUserAsync(Userinput user);
    Task UpdateUserAsync(int id , Userinput user);
    Task DeleteUserAsync(int id);
    Task<UserDtoOutput> GetUserAsync(int id);
    Task<IEnumerable<UserDtoOutput>> GetAllUsersAsync();
    Task<IEnumerable<TaskOutputDto>> GetAllTasksAsync(int userId);
    Task AddTaskToUserAsync(int userId, TaskAddForUserDto task);
}
