using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletServiceAPI.Entities
{
    public enum TransactionType
    {
        Credit,
        Debit,
        Transfer
    }

    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; } = Guid.NewGuid();
        public Guid WalletId { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
