namespace _1640WebDevUMC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> AllRoles { get; set; }
        public IList<string> Faculties { get; set; }
        public IList<string> AllFaculties { get; set; } // New property for all faculties
        public int NumberOfContributions { get; set; }
        public string Faculty { get; set; }

        public UserViewModel()
        {
            Roles = new List<string>();
            AllRoles = new List<string>();
            Faculties = new List<string>();
            AllFaculties = new List<string>(); // Initialize the AllFaculties list
        }
    }
}
