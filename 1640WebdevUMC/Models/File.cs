using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public class File
    {
        [Key]
        public string ContributionID { get; set; } // This property associates the file with a contribution

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }
}
