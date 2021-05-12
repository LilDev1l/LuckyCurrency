using LuckyCurrency.Hasher;
using LuckyCurrency.Infrastructure.Commands;
using LuckyCurrency.Models.DB;
using LuckyCurrency.Services;
using LuckyCurrency.ViewModels.Base;
using LuckyCurrency.Views.Autorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LuckyCurrency.ViewModels.Autorization
{
    class AutorizationViewModel : ViewModel
    {
        private UnitOfWork dbWorker;

        #region Login
        private string _login;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }
        #endregion

        #region Password
        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion

        #region InfoMessage
        private string _infoMessage = "";
        public string InfoMessage
        {
            get => _infoMessage;
            set => Set(ref _infoMessage, value);
        }
        #endregion

        #region Команды

        #region LoginCommand
        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object p) => true;
        private void OnLoginCommandExecuted(object p)
        {
            var passBox = p as PasswordBox;
            Password = passBox.Password;
            LoginFunc();
        }
        #endregion

        #region SwitchToRegistrationCommand
        public ICommand SwitchToRegistrationCommand { get; }
        private bool CanSwitchToRegistrationCommandExecute(object p) => true;
        private void OnSwitchToRegistrationCommandExecuted(object p)
        {
            //SwitchTo(new Registration());
        }
        #endregion

        #endregion

        public AutorizationViewModel()
        {
            #region Команды
            LoginCommand = new LambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
            SwitchToRegistrationCommand = new LambdaCommand(OnSwitchToRegistrationCommandExecuted, CanSwitchToRegistrationCommandExecute);
            #endregion

            dbWorker = new UnitOfWork();
        }

        private void LoginFunc()
        {
            if (IsFieldsNotEmpty())
            {
                string passwordHash = PasswordHasher.GetHash(Password);
                var user = dbWorker.Users.GetAll().FirstOrDefault(s => s.Login == Login && s.Password == passwordHash);
                if (user != null)
                {
                    SwitchTo(GetMainWindow(dbWorker.API_Keys.Get(user.Id)));
                }
                else
                {
                    InfoMessage = "Данные не верны!!!";
                }
            }
            else
            {
                InfoMessage = "Поля не заполнены!!!";
            }
        }

        private bool IsFieldsNotEmpty()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Login);
        }

        //factory methods for window
        private MainWindow GetMainWindow(API_Key apy_key)
        {
            MainWindow clientMainWindow = new MainWindow();
            //MainWindowViewModel clientMainViewModel = new MainWindowViewModel(apy_key);
            //clientMainWindow.DataContext = clientMainViewModel;
            return clientMainWindow;
        }
    }
}
