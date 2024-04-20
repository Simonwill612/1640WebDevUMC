using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace _1640WebDevUMC.Models.ViewModels
{
    public class FileUploadViewModel
    {
        [Required(ErrorMessage = "Contribution ID is required")]
        public string ContributionID { get; set; }

        [Required(ErrorMessage = "Upload title is required")]
        public string UploadTitle { get; set; }

        [Required(ErrorMessage = "Student email is required")]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Please select file")]
        public IFormFile FormFile { get; set; }

        [Required(ErrorMessage = "Please select file type")]
        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        File,
        Image
    }
}
