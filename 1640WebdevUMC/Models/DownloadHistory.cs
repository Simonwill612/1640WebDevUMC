using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public partial class DownloadHistory
    {
        [Key]
        public int DownloadID { get; set; }

        [Required]
        public int MarketingManagerID { get; set; }

        [ForeignKey("Contribution")]
        public int ContributionID { get; set; }

        [Required]
        public DateTime DownloadDate { get; set; }

        public virtual Contribution Contribution { get; set; }
    }

}
