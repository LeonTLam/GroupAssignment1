using GroupAssignment1.Models;

namespace GroupAssignment1.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Housing> Housings { get; set; }
        public Housing housing { get; set; }
        public Order order { get; set; }
    }
}
