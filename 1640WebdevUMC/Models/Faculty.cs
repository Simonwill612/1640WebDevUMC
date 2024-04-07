using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace _1640WebDevUMC.Models
{
    public class Faculty
    {
        [Key]
        [StringLength(450)]
        [Required(ErrorMessage = "Faculty can not be null")]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "ID can not be null")]
        public string FacultyID { get; set; } = string.Empty;
        [Required(ErrorMessage = "Faculty can not be null")]
        [StringLength(255)]
        public string FacultyName { get; set; } = string.Empty;


        [StringLength(255)]
        public string FacultyGuest { get; set; } = string.Empty;



        public List<ApplicationUser>? Users { get; set; }
        public List<AcademicYear>? AcademicYears { get; set; }
    }

}