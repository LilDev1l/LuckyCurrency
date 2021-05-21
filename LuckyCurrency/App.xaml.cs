using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LuckyCurrency
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Application.Current.Shutdown();
            MessageBox.Show(e.Exception.InnerException.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            
            //e.Handled = true;
        }
    }
}
