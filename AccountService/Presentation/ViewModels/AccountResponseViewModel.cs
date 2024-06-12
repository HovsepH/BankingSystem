using System.ComponentModel.DataAnnotations;

namespace AccountService.Presentation.ViewModels
{
    public class AccountResponseViewModel
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public int UserId { get; set; }
    }
}
