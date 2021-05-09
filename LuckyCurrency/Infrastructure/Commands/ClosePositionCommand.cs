using LuckyCurrency.Infrastructure.Commands.Base;
using LuckyCurrency.Models;
using LuckyCurrency.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Infrastructure.Commands
{
    class ClosePositionCommand : Command
    {
        public override bool CanExecute(object parameter)
        {
            if (parameter is Position position)
            {
                return position.Size != 0;
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Position position)
            {
                Bybit.CreateMarketOrder(position.Side == "Buy" ? "Sell" : "Buy", position.Symbol, position.Size, "GoodTillCancel", true, true);
            }
        }
    }
}
