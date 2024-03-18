using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class AcademicYear
    {
        [Key]
        public string Id { get; set; }=Guid.NewGuid().ToString();

        [ForeignKey("Faculty")]
        public string FacultyId { get; set; }=string.Empty;
        public Faculty? Faculty { get; set; }    


        public DateTime FinalDate { get; set; }
        public DateTime ClosureDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
