using AdministrationApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdministrationApp.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        #region Pola i właściwosci
        public string DisplayName { get; set; }
        private BaseCommand _CloseCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(OnRequestClose);
                return _CloseCommand;
            }
        }
        #endregion
        #region RequestClose [event]
        public event EventHandler RequestClose;
        protected void OnRequestClose()
        {
            EventHandler handler = RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        #endregion
    }
}
