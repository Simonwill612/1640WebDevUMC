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

        public string StudentEmail { get; set; } = string.Empty;

        [ForeignKey("AcademicYear")]
        public string AcademicYearID { get; set; } = string.Empty;
        public virtual AcademicYear? AcademicYear { get; set; }

        public virtual ICollection<File> Files { get; set; } = new List<File>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
