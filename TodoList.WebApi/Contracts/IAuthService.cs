using Models.Entities;

namespace ToDoLIstAPi.Contracts;

public interface IAuthService

{
        Task<User> Authenticate(string username, string password);
        bool VerifyPasswordHash(string password, string storedHash); 

}
