using GroupAssignment1.Areas.Identity.Data;
using GroupAssignment1.Models;

namespace GroupAssignment1.ViewModels
{
    public class TransactionListViewModel
    {
        public IEnumerable<Transaction>? Transactions { get; set; }

        public string? CurrentViewName;

        public TransactionListViewModel(IEnumerable<Transaction>? transactions, string? currentViewName)
        {
            Transactions = transactions;
            CurrentViewName = currentViewName;
        }
    }
}
