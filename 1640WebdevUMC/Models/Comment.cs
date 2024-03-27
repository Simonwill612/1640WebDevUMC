/*using _1640WebDevUMC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Comment
{
    [Key]
    public string CommentID { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Content { get; set; }

    [Required]
    [ForeignKey("ApplicationUser")]
    public string Email { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }

    [Required]
    public string ContributionItemID { get; set; }
    [ForeignKey("ContributionItemID")]
    public virtual ContributionItem ContributionItem { get; set; }

    public DateTime CommentDate { get; set; } = DateTime.Now;
}
*/