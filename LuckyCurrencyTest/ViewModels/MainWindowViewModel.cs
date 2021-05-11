using LuckyCurrencyTest.ViewModels.Base;
using LuckyCurrencyTest.Services;
using System.Windows.Input;
using LuckyCurrencyTest.Infrastructure.Commands;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System;

namespace LuckyCurrencyTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: App.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = App.Current.Dispatcher;
        });

        #region Команды 

        #region RunWebSocketCommand
        public ICommand RunWebSocketCommand { get; }
        private bool CanRunWebSocketCommandExecute(object p) => true;
        private void OnRunWebSocketCommandExecuted(object p)
        {
            Bybit.NewMessage += OnGetNewMessage;
            Bybit.RunBybitWebSocket();
            notifier.ShowInformation("Inf");
            notifier.ShowSuccess("Success");
            notifier.ShowWarning("Warning");
            notifier.ShowError("Error");
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
