using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1640WebDevUMC.Models
{
    public class Comment
    {
        [Key]
        public string CommentID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [ForeignKey("ContributionItem")]
        public string ContributionItemID { get; set; }
        public virtual ContributionItem? ContributionItem { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}
