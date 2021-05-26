using LuckyCurrency.Infrastructure;
using LuckyCurrency.ViewModels.Authorization;
using System;
using System.Windows;

namespace LuckyCurrency.Views.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            if (!InternetAvailability.IsInternetAvailable())
                throw new Exception("No internet connection");

            InitializeComponent();

            LoginViewModel vm = new LoginViewModel();
            DataContext = vm;
            vm.CurrentWindow = this;
        }
    }
}
