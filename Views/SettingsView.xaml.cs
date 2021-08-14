using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private App app = Application.Current as App;
        public TimerSounds Sounds { get; set; }
         
        public SettingsView(MainView tehWindow)
        {
            TehWindow = tehWindow;
            InitializeComponent();

            Sounds = TehWindow.vm.Sounds;
            DataContext = Sounds;
        }

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            TehWindow.MoveListView();
        }

        private void ChangedSound(object sender, SelectionChangedEventArgs e)
        {
            Properties.Settings.Default.timerSound = Sounds.SelectedSound;
            Properties.Settings.Default.Save();
        }

        private void OpenSounds(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", Storage.Sounds);
        }
    }
}
