using System.ComponentModel.DataAnnotations;

namespace AccountService.Presentation.ViewModels
{
    public class AccountRequestViewModel
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Account number is required.")]
        public string AccountNumber { get; set; }


        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than zero.")]
        public int UserId { get; set; }
    }
}
