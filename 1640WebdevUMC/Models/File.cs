using System;
using System.Collections.Generic;

namespace _1640WebDevUMC.Models
{
    public partial class File
    {
        public int FileId { get; set; }

        public string FileName { get; set; } = null!;

        public string FileType { get; set; } = null!;

        public int FileSize { get; set; }

        public DateTime? UploadDate { get; set; }

        public byte[]? FileContent { get; set; }

        public int? ContributionId { get; set; }

        public virtual Contribution? Contribution { get; set; }
    }
}
