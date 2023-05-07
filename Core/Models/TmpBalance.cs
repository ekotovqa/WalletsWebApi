using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class TmpBalance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "decimal(40,20)")]
        public decimal? Balance { get; set; }
        public DateTime Updated_At { get; set; }
        public Wallet Wallets { get; set; }
        public int WalletId { get; set; }
    }
}
