using LuckyCurrency.ViewModels.Autorization;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LuckyCurrency.Views.Autorization
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
            vm.Close = new Action(this.Close);
        }
    }
}
