using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace AccountService.Domain.Entities
{
    public class Account
    {
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than zero.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Account number is required.")]
        public string AccountNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than zero.")]
        public int UserId { get; set; }

    }

}
