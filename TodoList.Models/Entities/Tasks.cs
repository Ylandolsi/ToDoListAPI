using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities;

public class Tasks
{
    [Key ]
    public int Id { get; set; }
    [Required(ErrorMessage = "Title Required")]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required(ErrorMessage = "Due Date Required")]
    public DateTime DueDate { get; set; }
    
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId) ) ]
    public User? User { get; set; }
    
    
}
