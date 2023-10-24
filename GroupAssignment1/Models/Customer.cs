using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email {  get; set; } = string.Empty;
    public string Phone {  get; set; } = string.Empty;
    public virtual List<Housing>? OwnedHousings { get; set; }
    // Navigation property
    public virtual List<Order>? Orders { get; set; }
}