using GroupAssignment1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupAssignment1.DAL
{
    public class HousingUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; } = string.Empty;
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = string.Empty;

        public virtual List<Housing>? Housings { get; set; }
        public virtual List<Order>? Orders { get; set; }

        internal object Select(Func<object, SelectListItem> value)
        {
            throw new NotImplementedException();
        }
    }
}
