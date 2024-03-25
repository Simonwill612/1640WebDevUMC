using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class AcademicYear
    {
        [Key]
        public int YearID { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public DateTime ClosureDate { get; set; }

        [Required]
        public DateTime FinalClosureDate { get; set; }

        [ForeignKey("Faculty")]
        public int FacultyID { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<Contribution> Contributions { get; set; }
    }
}
