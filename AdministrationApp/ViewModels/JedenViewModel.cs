using AdministrationApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AdministrationApp.ViewModels
{
    //
    public class JedenViewModel<T> : WorkspaceViewModel
    {

        #region Command
        private BaseCommand _SaveAndCloseCommand;
        //ta komenda bedzie podpieta pod przycisk Zapisz i Zamknik
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                    //kom....
                    _SaveAndCloseCommand = new BaseCommand(SaveAndClose);
                return _SaveAndCloseCommand;
            }
        }
        #endregion

        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            DisplayName = displayName;
        }
        #endregion

        #region Pomocniczy
        private void SaveAndClose()
        {
                //dodać wysyłanie do api jsona         
                OnRequestClose();
         
         }
        #endregion

        
    }
}
