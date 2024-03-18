using System;
using System.Collections.Generic;

namespace _1640WebDevUMC.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }

        public int? ContributionId { get; set; }

        public byte[]? Image1 { get; set; }

        public string? Description { get; set; }

        public virtual Contribution? Contribution { get; set; }
    }
}
