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
using System.Windows.Input;

namespace LuckyCurrency.ViewModels.Autorization
{
    class RegistrationViewModel : ViewModel
    {
        private const string DUPLICATE_USER_DATA = "Пользаватель с таким именем или логином уже существует!!!";
        private const string PASSWORDS_NOT_EQUEL = "Пароли не совпадают!!!";
        private const string FIELDS_EMPTY = "Заполнены не все поля!!!";

        private UnitOfWork _dbWorker;

        #region Models

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

        #region RepeatPassword
        private string _repeatPassword;
        public string RepeatPassword
        {
            get => _repeatPassword;
            set => Set(ref _repeatPassword, value);
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
                if (IsRepeatPasswordEquals())
                {
                    if (DuplicateCheck())
                    {
                        User user = new User()
                        {
                            Login = this.Login,
                            Password = this.Password
                        };
                        _dbWorker.Users.Create(user);
                        _dbWorker.Save();
                        SwitchTo(new Login());
                    }
                    else
                    {
                        InfoMessage = DUPLICATE_USER_DATA;
                    }
                }
                else
                {
                    InfoMessage = PASSWORDS_NOT_EQUEL;
                }
            }
            else
            {
                InfoMessage = FIELDS_EMPTY;
            }
        }

        private bool IsFieldNotEmpty()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password)
                   && !string.IsNullOrEmpty(RepeatPassword);
        }

        private bool IsRepeatPasswordEquals()
        {
            return Password == RepeatPassword;
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
