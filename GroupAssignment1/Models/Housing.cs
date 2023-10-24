using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.Models;

public class Housing
{
    [Key]
    public int HousingId { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Rent { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public string? ImageUrl { get; set; }

    public int? OwnerId { get; set; }
    public virtual Customer Owner { get; set; } = default!;

    // Navigation property
    public virtual List<Order>? Orders { get; set; }
}

