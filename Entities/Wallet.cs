using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletServiceAPI.Entities
{
    public class Wallet
    {
        [Key]
        public Guid WalletId { get; set; } = Guid.NewGuid();
        public string WalletAddress { get; set; } = Guid.NewGuid().ToString();
        public decimal Balance { get; set; } = 0;
        public string Currency { get; set; } = "NGN";

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
