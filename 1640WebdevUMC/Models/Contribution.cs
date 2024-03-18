using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public partial class Contribution
    {
        public int ContributionId { get; set; }

        public int? UserId { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public string AcademicYearId { get; set; } = string.Empty;

        public AcademicYear AcademicYear { get; set; }

        public DateTime? UploadDate { get; set; }

        public DateTime? ClosureDate { get; set; }

        public DateTime? FinalClosureDate { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public string? Status { get; set; }

        public string? Comment { get; set; }

        public bool? SelectedForPublication { get; set; }

        public ICollection<DownloadHistory> DownloadHistories { get; set; } = new List<DownloadHistory>();

        public ICollection<File> Files { get; set; } = new List<File>();

        public ICollection<Image> Images { get; set; } = new List<Image>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ApplicationUser? User { get; set; }
    }
}
