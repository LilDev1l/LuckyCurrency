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
            InitializeComponent();

            LoginViewModel vm = new LoginViewModel();
            DataContext = vm;
            vm.Close = new Action(this.Close);
        }
    }
}
