using GroupAssignment1.Models;

namespace GroupAssignment1.ViewModels
{
    public class HousingListViewModel
    {
        public IEnumerable<Housing> Housings;
        public string? CurrentViewName;

        public HousingListViewModel(IEnumerable<Housing> housings, string? currentViewName)
        {
            Housings = housings;
            CurrentViewName = currentViewName;
        }
    }
}
