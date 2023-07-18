using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Support.Models;

public class User
{
    public int Id { get; set; }
    
    [StringLength(60, MinimumLength = 3)]
    [Required]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
    public string FullName { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

    public string Phone { get; set; }
    [Required]
    public Role Role { get; set; }
    
    public ICollection<Turn> Turns { get; } = new List<Turn>(); // Collection navigation containing dependents

    
    // public Turn? Turn { get; set; } // Reference navigation to dependent
}

public enum Role
{
    Support,
    OPs
}

