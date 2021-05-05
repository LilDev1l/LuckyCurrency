using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Models
{
    class CurrentBalance
    {
        public double WalletBalance { get; set; }
        public double AvailableBalance { get; set; }

        public CurrentBalance(double walletBalance, double availableBalance)
        {
            WalletBalance = walletBalance;
            AvailableBalance = availableBalance;
        }
    }
}
