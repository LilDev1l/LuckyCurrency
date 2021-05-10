using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Services.Models.PositionClosePnl
{
    class PositionClosePnlPage
    {
        public int current_page { get; set; }
        public List<PositionClosePnlData> data { get; set; }
    }
}
