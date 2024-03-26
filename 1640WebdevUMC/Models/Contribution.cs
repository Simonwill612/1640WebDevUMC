using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace _1640WebDevUMC.Models
{
    public class Contribution
    {
        [Key]
        public string ContributionID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string File { get; set; }

        public string Image { get; set; }

        [ForeignKey("AcademicYear")]
        public string AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Email { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
