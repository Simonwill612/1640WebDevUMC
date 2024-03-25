using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public partial class Notification
    {
        [Key]
        public int NotificationID { get; set; }

        [ForeignKey("Contribution")]
        public int ContributionID { get; set; }

        [Required]
        public int RecipientUserID { get; set; }

        [Required]
        public string NotificationType { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public virtual Contribution Contribution { get; set; }
    }
}
