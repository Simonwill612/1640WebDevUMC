using _1640WebDevUMC.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    [Key]
    public string CommentID { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime CommentDate { get; set; }

    // Foreign key to ApplicationUser (assuming it's the identity user)
    [ForeignKey("ApplicationUser")]
    public string Email { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }

    // Foreign key to ContributionItem
    [ForeignKey("Contribution")]
    public string ContributionID { get; set; }
    public virtual Contribution? Contribution { get; set; }
}
