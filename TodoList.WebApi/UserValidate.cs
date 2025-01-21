using ToDoLIstAPi.DbContext;

namespace ToDoLIstAPi;

public static class UserValidate
{
    
    public static string HashPassword(string password)
    {
        // Use a secure hashing algorithm like BCrypt
        return BCrypt.Net.BCrypt.HashPassword(password);
    } 

}
