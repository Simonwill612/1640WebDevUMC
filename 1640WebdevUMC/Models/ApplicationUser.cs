using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using _1640WebDevUMC.Models;
using Microsoft.AspNetCore.Identity;

namespace _1640WebDevUMC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Faculty")]
        public int? FacultyID { get; set; }
        public virtual Faculty Faculty { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Contribution> Contributions { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
    }
}
