using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class Contribution
    {
        [Key]
        public string ContributionID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; }

        [ForeignKey("ApplicationUser")]
        public string Email { get; set; } = string.Empty;
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [ForeignKey("AcademicYear")]
        public string AcademicYearID { get; set; } = string.Empty;
        public virtual AcademicYear? AcademicYear { get; set; }

        public string FilePath { get; set; } = string.Empty;

    }
}
