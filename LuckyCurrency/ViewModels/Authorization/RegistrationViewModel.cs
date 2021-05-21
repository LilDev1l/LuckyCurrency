using LuckyCurrency.Data;
using LuckyCurrency.Hasher;
using LuckyCurrency.Infrastructure.Commands;
using LuckyCurrency.Models.DB;
using LuckyCurrency.Services;
using LuckyCurrency.ViewModels.Base;
using LuckyCurrency.Views.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LuckyCurrency.ViewModels.Authorization
{
    class RegistrationViewModel : ViewModel
    {
        private const string DUPLICATE_USER_DATA = "A user with this login already exists";
        private const string FIELDS_EMPTY = "Fields are not filled";

        private UnitOfWork _dbWorker;

        #region Models

        #region Title
        private string _title = "LuckyCurrency";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

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

        #region PublicAPI_Key
        private string _publicAPI_Key;
        public string PublicAPI_Key
        {
            get => _publicAPI_Key;
            set => Set(ref _publicAPI_Key, value);
        }
        #endregion

        #region PrivateAPI_Key
        private string _privateAPI_Key;
        public string PrivateAPI_Key
        {
            get => _privateAPI_Key;
            set => Set(ref _privateAPI_Key, value);
        }
        #endregion

        #region InfoMessage
        private string _infoMessage;
        public string InfoMessage
        {
            get => _infoMessage;
            set => Set(ref _infoMessage, value);
        }
        #endregion

        #endregion

        #region Команды

        #region RegistrationCommand
        public ICommand RegistrationCommand { get; }
        private bool CanRegistrationCommandExecute(object p) => true;
        private void OnRegistrationCommandExecuted(object p)
        {
            var values = (object[])p;
            var password = values[0] as PasswordBox;
            var privateKey = values[1] as PasswordBox;
            Password = password.Password;
            PrivateAPI_Key = privateKey.Password;
            Registration();
        }
        #endregion

        #region SwitchToLoginCommand
        public ICommand SwitchToLoginCommand { get; }
        private bool CanSwitchToLoginCommandExecute(object p) => true;
        private void OnSwitchToLoginCommandExecuted(object p)
        {
            SwitchTo(new Login());
        }
        #endregion

        #endregion

        public RegistrationViewModel()
        {
            #region Команды
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
            SwitchToLoginCommand = new LambdaCommand(OnSwitchToLoginCommandExecuted, CanSwitchToLoginCommandExecute);
            #endregion

            _dbWorker = new UnitOfWork();
        }

        private void Registration()
        {
            if (IsFieldNotEmpty())
            {
                if (DuplicateCheck())
                {
                    User user = new User()
                    {
                        Login = this.Login,
                        Password = PasswordHasher.GetHash(this.Password)
                    };
                    _dbWorker.Users.Create(user);
                    _dbWorker.Save();

                    Account account = new Account()
                    {
                        Id = user.Id,
                        User = user
                    };
                    _dbWorker.Accounts.Create(account);
                    _dbWorker.Save();

                    API_Key apiKey = new API_Key()
                    {
                        Id = account.Id,
                        PublicKey = this.PublicAPI_Key,
                        SecretKey = this.PrivateAPI_Key,
                        Account = account
                    };
                    _dbWorker.API_Keys.Create(apiKey);
                    _dbWorker.Save();

                    SwitchTo(new Login());
                    MessageBox.Show("Add Account");
                }
                else
                {
                    InfoMessage = DUPLICATE_USER_DATA;
                }
            }
            else
            {
                InfoMessage = FIELDS_EMPTY;
            }
        }

        private bool IsFieldNotEmpty()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PublicAPI_Key) && !string.IsNullOrEmpty(PrivateAPI_Key);
        }

        private bool DuplicateCheck()
        {
            IEnumerable<User> users = _dbWorker.Users.GetAll().Where(s => s.Login == Login);
            if (users.Count() > 0)
            {
                return false;
            }
            return true;
        }
    }
}
