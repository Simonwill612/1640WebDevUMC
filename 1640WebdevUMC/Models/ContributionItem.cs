using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class ContributionItem
    {
        [Key]
        public string ContributionItemID { get; set; }

        [Required]
        [ForeignKey("Contribution")]
        public string ContributionID { get; set; }
        public virtual Contribution Contribution { get; set; }

        public DateTime UploadDate { get; set; }

        // Properties for file upload
        [NotMapped]
        public IFormFile FileUpload { get; set; }

        [NotMapped]
        public IFormFile ImageUpload { get; set; }

        // Navigation properties
        [ForeignKey("File")]
        public string FileID { get; set; }
        public virtual File? File { get; set; }

        [ForeignKey("Image")]
        public string ImageID { get; set; }
        public virtual Image? Image { get; set; }
    }
}
