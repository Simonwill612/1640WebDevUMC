using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;

namespace _1640WebDevUMC.Models;

// Add profile data for application users by adding properties to the UMCPJ1640_WEND_User class
public class ApplicationUser : IdentityUser
{
    [ForeignKey("Faculty")]
    public string? FacultyID { get; set; }
    public Faculty? Faculty { get; set; }
    public DateTime CreatedTime { get; set; }
    public List<Notification>? Notifications { get; set; }
    public List<Contribution>? Contributions { get; set; }
    public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

}

