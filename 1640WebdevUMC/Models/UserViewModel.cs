namespace _1640WebDevUMC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> AllRoles { get; set; } // New property for all roles

        public UserViewModel()
        {
            Roles = new List<string>();
            AllRoles = new List<string>();
        }
    }

}
