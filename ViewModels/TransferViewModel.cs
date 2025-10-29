using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.ViewModels
{
    public class TransferViewModel
    {
        [Required]
        [Display(Name = "Recipient Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string RecipientEmail { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Transfer Amount")]
        public decimal Amount { get; set; }
    }
}
