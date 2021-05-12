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

namespace LuckyCurrency
{
    public partial class MainWindow : Window
    {
        public MainWindow(API_Key api_key)
        {
            DataContext = new MainWindowViewModel(api_key);

            InitializeComponent();
        }
    }
}

