using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.PositionClosePnl
{
    public class PositionClosePnlPage
    {
        public int current_page { get; set; }
        public List<PositionClosePnlData> data { get; set; }
    }
}
