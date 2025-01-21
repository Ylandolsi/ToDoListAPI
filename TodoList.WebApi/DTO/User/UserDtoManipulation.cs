using System.ComponentModel.DataAnnotations;

namespace ToDoLIstAPi.DTO.User;

public record UserDtoManipulation
{
    [Required(ErrorMessage = "Name Required ") , MaxLength(50 , ErrorMessage = "Name is Too long ") , MinLength(3  , ErrorMessage = "Name is too shrot ") ]
    public string Name { get; set; }
    [Required(ErrorMessage = "Position Required")]
    public string position { get; set; }
}
