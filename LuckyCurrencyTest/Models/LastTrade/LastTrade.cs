using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrencyTest.Models
{
    class LastTrade
    {
        public int Price { get; set; }
        public int Size { get; set; }
        public DateTime Timestamp { get; set; }
        public TickDirection Tick_direction { get; set; }
        public Side Side { get; set; }

        public LastTrade(int price, int size, DateTime timestamp, TickDirection tick_direction, Side side)
        {
            Price = price;
            Size = size;
            Timestamp = timestamp;
            Tick_direction = tick_direction;
            Side = side;
        }
    }
    enum Side
    { 
        Buy,
        Sell
    }
    enum TickDirection
    {
        PlusTick,
        ZeroPlusTick,
        MinusTick,
        ZeroMinusTick
    }
}
