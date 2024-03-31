using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _1640WebDevUMC.Models;

public class Comment
{
    [Key]
    public int CommentID { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime CommentDate { get; set; }

    // Foreign key to ApplicationUser (assuming it's the identity user)
    [ForeignKey("ApplicationUser")]
    public string Email { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    // Foreign key to ContributionItem
    [ForeignKey("ContributionItem")]
    public string ContributionItemID { get; set; }
    public virtual ContributionItems ContributionItem { get; set; }
}
