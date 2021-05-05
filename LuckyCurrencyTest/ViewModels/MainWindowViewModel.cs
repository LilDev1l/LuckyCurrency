using LuckyCurrencyTest.ViewModels.Base;
using LuckyCurrencyTest.Services;
using System.Windows.Input;
using LuckyCurrencyTest.Infrastructure.Commands;

namespace LuckyCurrencyTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Команды

        #region RunWebSocketCommand
        public ICommand RunWebSocketCommand { get; }
        private bool CanRunWebSocketCommandExecute(object p) => true;
        private void OnRunWebSocketCommandExecuted(object p)
        {
            Bybit.NewMessage += OnGetNewMessage;
            Bybit.RunBybitWebSocket();
        }
        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region Команды
            RunWebSocketCommand = new LambdaCommand(OnRunWebSocketCommandExecuted, CanRunWebSocketCommandExecute);
            #endregion
        }

        #region NewMessage
        private void OnGetNewMessage(string message)
        {

        }
        #endregion
    }
}
