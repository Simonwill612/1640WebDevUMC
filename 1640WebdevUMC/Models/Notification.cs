using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class Notification
    {
        [Key]
        public string NotificationID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ContributionID { get; set; }

        [ForeignKey("ContributionID")]
        public virtual Contribution Contribution { get; set; }

        [Required]
        public string Message { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
