using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "Upload title is required")]
        public string UploadTitle { get; set; } // Title for the upload

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string StudentEmail { get; set; } // This property stores the email of the uploader

        public DateTime UploadTime { get; set; } // Upload time

        // Add a field to determine the type of file (image or file)
        public FileType FileType { get; set; }

        public bool IsPublic { get; set; } // Indicates if the file is public

        // Add a field to store comments related to the file
        public virtual ICollection<Comment> Comments { get; set; }

        // Constructor để khởi tạo Comments
        public File()
        {
            Comments = new List<Comment>();
        }

        // Set file to public based on StudentEmail
        public void SetPublic(string studentEmail)
        {
            if (studentEmail == StudentEmail)
            {
                IsPublic = true;
                foreach (var comment in Comments)
                {
                    comment.IsPublic = true;
                }
            }
        }
    }

    // Enum to define the type of file
    public enum FileType
    {
        File,
        Image
    }
}
