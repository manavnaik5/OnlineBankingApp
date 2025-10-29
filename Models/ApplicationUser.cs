using Microsoft.AspNetCore.Identity;

namespace OnlineBankingApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; } = false;

        // Navigation property
        public ICollection<Account> Accounts { get; set; }
    }
}
