using System.ComponentModel.DataAnnotations;

namespace GroupAssignment1.Models
{
    public class Housing
    {
        public int HousingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Rent { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? ImageUrl { get; set; }   
    }
}
