using LuckyCurrencyTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Threading;
using FancyCandles;
using IO.Swagger.Model;
using LuckyCurrencyTest;
using LuckyCurrencyTest.Models;
using LuckyCurrencyTest.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using LuckyCurrencyTest.Infrastructure.Commands;

namespace LuckyCurrencyTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Торговая пара
        private ComboBoxItem _selectedPair;
        public ComboBoxItem SelectedPair
        {
            get => _selectedPair;
            set => Set(ref _selectedPair, value);
        }
        #endregion

        #region Таймфрейм
        private ComboBoxItem _selectedTimeframe;
        public ComboBoxItem SelectedTimeframe
        {
            get => _selectedTimeframe;
            set => Set(ref _selectedTimeframe, value);
        }
        #endregion

        #region Свечи
        private ObservableCollection<ICandle> _candles;
        public ObservableCollection<ICandle> Candles
        {
            get => _candles;
            set => Set(ref _candles, value);
        }
        #endregion

        #region Команды
        public ICommand SelectedTabItemCommand { get; }
        private bool CanSelectedTabItemCommandExecute(object p) => true;
        private void OnSelectedTabItemCommandExecuted(object p)
        {
            Candles = Bybit.GetCandles(SelectedPair.Content.ToString(), SelectedTimeframe.Content.ToString());
        }
        #endregion
        public MainWindowViewModel()
        {
            #region Команды

            SelectedTabItemCommand = new LambdaCommand(OnSelectedTabItemCommandExecuted, CanSelectedTabItemCommandExecute);

            #endregion

            Candles = Bybit.GetCandles("BTCUSDT", "1");
        }
    }
}
