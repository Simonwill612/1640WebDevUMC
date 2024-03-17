using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _1640WebDevUMC.Models
{
    public class Accounts
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        [NotMapped] // Add this attribute to ignore the property during database mapping
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}
