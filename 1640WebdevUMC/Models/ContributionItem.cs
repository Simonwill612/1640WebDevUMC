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

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        // Properties for file upload
        [NotMapped]
        public IFormFile FileUpload { get; set; }

        [NotMapped]
        public IFormFile ImageUpload { get; set; }

        // Navigation properties
        public string FileName { get; set; }
        public string ImageName { get; set; }

        [ForeignKey("FileName")]
        public virtual File File { get; set; }

        [ForeignKey("ImageName")]
        public virtual Image Image { get; set; }
    }
}
