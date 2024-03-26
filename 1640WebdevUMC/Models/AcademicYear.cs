using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class AcademicYear
    {
        [Key]
        public string AcademicYearID { get; set; }=Guid.NewGuid().ToString();

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public DateTime ClosureDate { get; set; }

        [Required]
        public DateTime FinalClosureDate { get; set; }

        [ForeignKey("Faculty")]
        public string FacultyID { get; set; }=string.Empty;

        public virtual Faculty? Faculty { get; set; }

        public virtual ICollection<Contribution> Contributions { get; set; }
    }
}
