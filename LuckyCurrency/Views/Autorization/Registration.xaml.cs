using LuckyCurrency.ViewModels.Autorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((RegistrationViewModel)this.DataContext).Password = PasswordBox.Password;
            }
        }

        private void RepeatPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((RegistrationViewModel)this.DataContext).RepeatPassword = RepeatPasswordBox.Password;
            }
        }
    }
}
