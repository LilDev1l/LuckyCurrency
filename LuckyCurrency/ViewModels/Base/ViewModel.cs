using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace LuckyCurrency.ViewModels.Base
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Action Close { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

            var handlers = PropertyChanged;
            if (handlers == null) return;

            var invocation_list = handlers.GetInvocationList();

            var arg = new PropertyChangedEventArgs(PropertyName);
            foreach (var action in invocation_list)
            {
                if (action.Target is DispatcherObject disp_object)
                    disp_object.Dispatcher.Invoke(action, this, arg);
                else
                    action.DynamicInvoke(this, arg);
            }
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        protected void SwitchTo(Window window)
        {
            window.Show();
            Close?.Invoke();
        }
    }
}
