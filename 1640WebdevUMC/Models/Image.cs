// Image.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class Image
    {
        [Key]
        public string ImageID { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public byte[] ImageData { get; set; } // Thêm thuộc tính ImageData

        // Other properties and relationships
    }
}
