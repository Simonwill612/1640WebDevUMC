using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public partial class ContributionItem
    {
        [Key]
        public string ContributionItemID { get; set; }

        [Required]
        [ForeignKey("Contribution")]
        public string ContributionID { get; set; }
        public virtual Contribution? Contribution { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        public byte[] FileData { get; set; }

        public byte[] ImageData { get; set; }

        public ContributionItem()
        {
            UploadDate = DateTime.Now;
        }

        public void UploadFile(string filePath)
        {
            // Assume you have a method to handle the file upload and convert it to a byte array
            this.FileData = System.IO.File.ReadAllBytes(filePath);
        }

        public void UploadImage(string imagePath)
        {
            // Assume you have a method to handle the image upload and convert it to a byte array
            this.ImageData = System.IO.File.ReadAllBytes(imagePath);
        }
    }
}
