using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministrationApp.Views
{
    public class AllDictionaryViewBase : Window
    {
        static AllDictionaryViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AllDictionaryViewBase), new FrameworkPropertyMetadata(typeof(AllDictionaryViewBase)));
        }
    }
}
