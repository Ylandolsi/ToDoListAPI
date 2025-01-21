using Models.Entities;

namespace ToDoLIstAPi;

public interface IAuthenticationService
{
        Task<User> Authenticate(string username, string password);
        bool VerifyPasswordHash(string password, string storedHash); 

}
