using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public partial class Image
    {
        [Key]
        public int ImageID { get; set; }

        [ForeignKey("Contribution")]
        public string ContributionID { get; set; }

        public byte[] ImageData { get; set; }

        public string Description { get; set; }

        public virtual Contribution Contribution { get; set; }
    }
}
