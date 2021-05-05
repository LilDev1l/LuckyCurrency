using LuckyCurrency.Infrastructure.Commands.Base;
using System;

namespace LuckyCurrency.Infrastructure.Commands
{
    class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public override void Execute(object parameter) => _execute(parameter);
    }
}
