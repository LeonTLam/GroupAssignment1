using GroupAssignment1.Areas.Identity.Data;
using GroupAssignment1.Models;

namespace GroupAssignment1.ViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<ApplicationUser> Users;
        public string? CurrentViewName;

        public UserListViewModel(IEnumerable<ApplicationUser> users, string? currentViewName)
        {
            Users = users;
            CurrentViewName = currentViewName;
        }
    }
}
