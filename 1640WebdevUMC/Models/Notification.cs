using System;
using System.Collections.Generic;

namespace _1640WebDevUMC.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }

        public int? ContributionId { get; set; }

        public int? RecipientUserId { get; set; }

        public string? NotificationType { get; set; }

        public DateTime? Timestamp { get; set; }

        public virtual Contribution? Contribution { get; set; }

        public virtual ApplicationUser? RecipientUser { get; set; }
    }
}
