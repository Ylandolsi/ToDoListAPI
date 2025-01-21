namespace ToDoLIstAPi.DTO.User;
using Models.Entities;

public record Userinput : UserDtoManipulation
{
    public ICollection<Tasks> Tasks { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 
    public string Role { get; set; } 

}
