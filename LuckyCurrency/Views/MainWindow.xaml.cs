using System.Windows;
using System.Collections.ObjectModel;
using FancyCandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json.Linq;
using IO.Swagger.Model;
using IO.Swagger.Api;
using LuckyCurrency.Models;
using LuckyCurrency.Models.DB;
using System.Linq;
using LuckyCurrency.Hasher;
using LuckyCurrency.ViewModels;
using MahApps.Metro.Controls;

namespace LuckyCurrency
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

/*            MainWindowViewModel vm = new MainWindowViewModel(api_key);
            DataContext = vm;
            vm.Close = new Action(this.Close);*/
        }
    }
}

