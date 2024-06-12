using Microsoft.VisualStudio.Services.Account;

namespace AccountService.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<Account>? Accounts { get; set; }
    }

}
