using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace Support.Models;

public class Turn
{
    public int Id { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; } // Required foreign key property
    public User User { get; set; } = null!; // Required reference navigation to principal
    
}