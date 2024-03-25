using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }

        [Required]
        public string FacultyName { get; set; }

        public virtual ICollection<AcademicYear> AcademicYears { get; set; }
    }

}
