using System.ComponentModel.DataAnnotations;

namespace OnlineBankingApp.ViewModels
{
    public class WithdrawViewModel
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        [Display(Name = "Withdrawal Amount")]
        public decimal Amount { get; set; }
    }
}
