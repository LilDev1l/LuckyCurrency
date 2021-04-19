using LuckyCurrency.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Заголовок окна
        private string _title = "LuckyCurrency";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

    }
}
