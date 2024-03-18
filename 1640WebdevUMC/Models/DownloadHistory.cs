using System;
using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public partial class DownloadHistory
    {
        [Key] // Define DownloadId as the primary key
        public int DownloadId { get; set; }

        public int? MarketingManagerId { get; set; }

        public int? ContributionId { get; set; }

        public DateTime? DownloadDate { get; set; }

        public virtual Contribution? Contribution { get; set; }

    }
}
