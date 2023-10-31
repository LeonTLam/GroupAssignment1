using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GroupAssignment1.Models;
using Microsoft.AspNetCore.Identity;

namespace GroupAssignment1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;

    public virtual List<Order>? Orders { get; set; }
}
    public class ApplicationRole : IdentityRole
{

}