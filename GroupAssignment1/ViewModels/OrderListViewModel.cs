using GroupAssignment1.Models;

namespace GroupAssignment1.ViewModels
{
    public class OrderListViewModel
    {
        public IEnumerable<Order> Orders;
        public string? CurrentViewName;

        public OrderListViewModel(IEnumerable<Order> orders, string? currentViewName)
        {
            Orders = orders;
            CurrentViewName = currentViewName;
        }
    }
}
