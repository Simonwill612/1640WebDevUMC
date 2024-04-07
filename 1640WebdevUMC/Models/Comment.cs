using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _1640WebDevUMC.Models;

public class Comment
{
    [Key]
    public string CommentID { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Content { get; set; }

    [Required]
    public DateTime SubmissionTime { get; set; }

    [Required]
    public string Email { get; set; }

    [ForeignKey("Contribution")]
    public string ContributionID { get; set; }
    public virtual Contribution Contribution { get; set; }
}
