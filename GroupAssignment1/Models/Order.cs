using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public decimal? TotalPrice {  get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = default!;

    [ForeignKey("Housing")]
    public int HousingId { get; set; }
    public virtual Housing Housing { get; set; } = default!;
}