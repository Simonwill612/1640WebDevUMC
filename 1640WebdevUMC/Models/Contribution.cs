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
            public string? Id { get; set; }
            public virtual ApplicationUser ApplicationUser { get; set; }

            [Required]
            public string? Title { get; set; }

            public string? Content { get; set; }

            [ForeignKey("AcademicYear")]
            public string AcademicYearID { get; set; }=string.Empty;
        public virtual AcademicYear AcademicYear { get; set; }

            public DateTime UploadDate
            {
                get { return AcademicYear.UploadDate; }
            }

            public DateTime ClosureDate
            {
                get { return AcademicYear.ClosureDate; }
            }

            public DateTime FinalClosureDate
            {
                get { return AcademicYear.FinalClosureDate; }
            }

            public string Status { get; set; }

            public string Comment { get; set; }

            [Required]
            public bool SelectedForPublication { get; set; }

            public virtual ICollection<File> Files { get; set; }

            public virtual ICollection<Image> Images { get; set; }

            public virtual ICollection<DownloadHistory> DownloadHistories { get; set; }

            public virtual ICollection<Notification> Notifications { get; set; }
        

    }
}
