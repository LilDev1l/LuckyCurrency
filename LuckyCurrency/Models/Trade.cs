using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models
{
    class Trade
    {
        public double Price { get; set; }
        public double Size { get; set; }
        public DateTime Timestamp { get; set; }
        public string Tick_direction { get; set; }
        public string Side { get; set; }

        public Trade(double price, double size, DateTime timestamp, string tick_direction, string side)
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
