using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _1640WebDevUMC.Models
{
    public class ContributionWithFile : Contribution
    {
        public string ContributionID { get; set; }
        public string Title { get; set; }
        public List<string> FilePaths { get; set; }
    }

}
