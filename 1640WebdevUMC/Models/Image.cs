using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public partial class Image
    {
        [Key]
        public string ImageID { get; set; }

        public byte[] ImageData { get; set; }

        public string Description { get; set; }

        [ForeignKey("ContributionItem")]
        public string ContributionItemID { get; set; }

        public virtual ContributionItem ContributionItem { get; set; }
    }
}
