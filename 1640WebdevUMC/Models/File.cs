// File.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class File
    {
        [Key]
        public string FileID { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public byte[] FileData { get; set; } // Thêm thuộc tính FileData

        // Other properties and relationships
    }
}
