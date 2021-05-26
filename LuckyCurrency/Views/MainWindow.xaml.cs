using System.Windows;
using System.Collections.ObjectModel;
using FancyCandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json.Linq;
using Bybit.Model;
using Bybit.Api;
using LuckyCurrency.Models;
using System.Linq;
using LuckyCurrency.Hasher;
using LuckyCurrency.ViewModels;
using MahApps.Metro.Controls;
using LuckyCurrency.Data.Model;

namespace LuckyCurrency
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(API_Key api_key)
        {
            InitializeComponent();

            MainWindowViewModel vm = new MainWindowViewModel(api_key);
            DataContext = vm;
            vm.CurrentWindow = this;
        }
    }
}

