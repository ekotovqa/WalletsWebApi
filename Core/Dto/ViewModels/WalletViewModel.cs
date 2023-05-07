using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.ViewModels
{
    public class WalletViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
