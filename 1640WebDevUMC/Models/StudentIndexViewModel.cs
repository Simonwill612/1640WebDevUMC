using _1640WebDevUMC.Models;

namespace _1640WebDevUMC.ViewModels
{
    public class StudentIndexViewModel
    {
        public IEnumerable<Contribution> Contributions { get; set; }
        public List<string> UserFaculties { get; set; }
    }
}
