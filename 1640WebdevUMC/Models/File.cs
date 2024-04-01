using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public class File
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string ContributionID { get; set; } // This property associates the file with a contribution
    }
}
