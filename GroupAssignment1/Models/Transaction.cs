using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public decimal Amount { get; set; }

        public String? TransactionDate { get; set; }

        [ForeignKey("ApplicationUser")]
        public string? CustomerId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
    }
}
