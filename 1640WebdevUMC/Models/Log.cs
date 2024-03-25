using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public class Log
    {
        [Key]
        public int LogID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
