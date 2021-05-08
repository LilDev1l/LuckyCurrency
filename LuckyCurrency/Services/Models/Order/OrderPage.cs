using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.Order
{
    class OrderPage
    {
        public int current_page { get; set; }
        public int last_page { get; set; }
        public List<OrderData> data { get; set; }
    }
}
