using LuckyCurrency.ViewModels.Autorization;
using System;
using System.Windows;

namespace LuckyCurrency.Views.Autorization
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
