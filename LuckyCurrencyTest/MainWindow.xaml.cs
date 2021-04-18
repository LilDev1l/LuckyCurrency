using System.Windows;
using System.Collections.ObjectModel;
using FancyCandles;
using System;

namespace LuckyCurrencyTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            DateTime t0 = new DateTime(2019, 6, 11, 23, 40, 0);
            for (int i = 0; i < 100; i++)
            {
                double p0 = Math.Round(Math.Sin(0.3 * i) + 0.1 * i, 3);
                double p1 = Math.Round(Math.Sin(0.3 * i + 1) + 0.1 * i, 3);
                candles.Add(new Candle(t0.AddMinutes(i * 5),
                            100 + p0, 101 + p0, 99 + p0, 100 + p1, i));
            }

            DataContext = candles;
        }
    }
}
