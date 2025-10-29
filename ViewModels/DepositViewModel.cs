using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.ViewModels
{
    public class DepositViewModel
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Deposit Amount")]
        public decimal Amount { get; set; }
    }
}
