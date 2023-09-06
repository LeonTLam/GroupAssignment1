namespace GroupAssignment1.Models
{
    public class Housing
    {
        public int HousingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Rent { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }   
    }
}
