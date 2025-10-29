using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBankingApp.Models
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer
    }

    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FromAccountId { get; set; }

        [ForeignKey("FromAccountId")]
        public Account FromAccount { get; set; }

        public int? ToAccountId { get; set; }

        [ForeignKey("ToAccountId")]
        public Account ToAccount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public string Description { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Completed"; // Completed, Pending, Failed
    }
}
