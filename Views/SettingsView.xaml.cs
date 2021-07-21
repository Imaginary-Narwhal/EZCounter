using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EZCounter.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>

    public partial class SettingsView : Window
    {
        private MainView TehWindow;
         
        public SettingsView(MainView tehWindow)
        {
            TehWindow = tehWindow;
            InitializeComponent();
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            TehWindow.MoveListView();
        }
    }
}
