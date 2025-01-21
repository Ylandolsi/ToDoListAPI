using System.ComponentModel.DataAnnotations;

namespace ToDoLIstAPi.DTO.Tasks;

public record TaskManipulationDto
{
    [Required(ErrorMessage = "Title Required")]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required(ErrorMessage = "Due Date Required")]
    public DateTime DueDate { get; set; }

    public bool IsCompleted { get; set; } = false; 
    public int? UserId { get; set; }
}
