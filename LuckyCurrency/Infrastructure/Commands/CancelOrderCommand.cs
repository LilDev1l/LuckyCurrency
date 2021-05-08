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
    class CancelOrderCommand : Command
    {
        public override bool CanExecute(object parameter) => true;  

        public override void Execute(object parameter)
        {
            if(parameter is Order order)
            {
                Bybit.CancelOrder(order.Symbol, order.Order_id);
            }
        }
    }
}
