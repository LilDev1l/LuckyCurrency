using LuckyCurrency.ViewModels.Authorization;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LuckyCurrency.Views.Authorization
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();

            RegistrationViewModel vm = new RegistrationViewModel();
            DataContext = vm;
            vm.CurrentWindow = this;
        }
    }
}
