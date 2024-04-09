using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class File 
    {
        [Key]
        public string FileID { get; set; } = Guid.NewGuid().ToString(); // Unique ID for each file

        [ForeignKey("Contribution")]
        public string ContributionID { get; set; } // This property associates the file with a contribution
        public virtual Contribution Contribution { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string StudentEmail { get; set; } // This property stores the email of the uploader
    }
}
