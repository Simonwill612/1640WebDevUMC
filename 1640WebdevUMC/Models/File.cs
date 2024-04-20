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

        public DateTime UploadTime { get; set; } // Thời gian tải lên

        // Thêm một trường để xác định loại của tệp (image hoặc file)
        public FileType FileType { get; set; }

        public bool IsPublic { get; set; } // Indicates if the file is public

        // Thêm một trường để lưu trữ các bình luận liên quan đến tệp
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

    // Enum để định nghĩa loại của tệp
    public enum FileType
    {
        File,
        Image
    }
}
