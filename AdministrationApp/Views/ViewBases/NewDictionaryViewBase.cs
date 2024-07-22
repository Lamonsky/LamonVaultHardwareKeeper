using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministrationApp.Views
{
    public class NewDictionaryViewBase : Window
    {
        static NewDictionaryViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NewDictionaryViewBase), new FrameworkPropertyMetadata(typeof(NewDictionaryViewBase)));
        }
    }
}
