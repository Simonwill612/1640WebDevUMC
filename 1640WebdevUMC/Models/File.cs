﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public partial class File
    {
        [Key]
        public string FileID { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FileType { get; set; }

        public int FileSize { get; set; }

        [Required]
        public DateTime UploadDate { get; set; }

        public byte[] FileContent { get; set; }

        [ForeignKey("ContributionItem")]
        public string ContributionItemID { get; set; }

        public virtual ContributionItem ContributionItem { get; set; }
    }
}
