using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
    public decimal? TotalPrice {  get; set; }

    [ForeignKey("ApplicationUser")]
    public int CustomerId { get; set; }
    
    [ForeignKey("Housing")]
    public int HousingId { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDate >= StartDate)
        {
            yield return new ValidationResult(
                $"EndDate must be larger than StartDate.",
                new[] { nameof(StartDate) });
        }
    }
}