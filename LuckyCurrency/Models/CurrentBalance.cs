using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models
{
    class Balance
    {
        public double WalletBalance { get; set; }
        public double AvailableBalance { get; set; }

        public Balance(double walletBalance, double availableBalance)
        {
            WalletBalance = walletBalance;
            AvailableBalance = availableBalance;
        }
    }
}
