using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public partial class Contribution
    {
        [Key]
        public int ContributionID { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        [Required]
        public DateTime ClosureDate { get; set; }

        [Required]
        public DateTime FinalClosureDate { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        [Required]
        public bool SelectedForPublication { get; set; }

        [ForeignKey("AcademicYear")]
        public int YearID { get; set; }
        public virtual AcademicYear AcademicYear { get; set; }

        public virtual ICollection<File> Files { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<DownloadHistory> DownloadHistories { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
