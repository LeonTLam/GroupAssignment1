using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.Models;

public class Housing
{
    [Key]
    public int HousingId { get; set; }

    [RegularExpression(@"[0-9a-zA-ZæøåÆØÅ. \-]{2,20}", ErrorMessage = "The Address must be numbers or letters and between 2 to 20 characters.")]
    [Display(Name = "Housing Address")]
    public string Name { get; set; } = string.Empty;
    [Range(0.01, double.MaxValue, ErrorMessage = "The Rent must be greater than 0.")]
    public int Rent { get; set; }
    [Required]
    [StringLength(200)]
    public string? Description { get; set; }
    [Required]
    public string? ImageUrl { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }

    [ForeignKey("Customer")]
    public int? CustomerId { get; set; }
    public virtual Customer? Owner { get; set; } = default!;

    // Navigation property
    public virtual List<Order>? Orders { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDate <= StartDate)
        {
            yield return new ValidationResult("End date must be greater than the start date.", new[] { "EndDate" });
        }
    }
}

