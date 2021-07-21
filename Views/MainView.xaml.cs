using EZCounter.ViewModels;
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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private MainViewModel vm;
        private ListView lWindow;


        public MainView()
        {
            vm = new MainViewModel();
            Properties.Settings.Default.listOpen = true;

            InitializeComponent();

            ((App)Application.Current).WindowPlace.Register(this);
            DataContext = vm;

            lWindow = new ListView(vm);
            lWindow.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveListView();
        }

        public void MoveListView()
        {
            lWindow.Top = GetTop();
            lWindow.Left = TehWindow.Left;
            if(Properties.Settings.Default.anchorListTop)
            {
                toggleListButton.Margin = new Thickness(108, 1, 108, 1);
                if(Properties.Settings.Default.listOpen)
                {
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/downArrow.png"));
                } else
                {
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/upArrow.png"));
                }
            } else
            {
                toggleListButton.Margin = new Thickness(108, 52, 108, 52);
                if (Properties.Settings.Default.listOpen)
                {
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/upArrow.png"));
                }
                else
                {
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/downArrow.png"));
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void CounterChanged(object sender, TextChangedEventArgs e)
        {
            vm.db.SaveChanges();
        }

        private void ExitEZC(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ContextMenu contextMenu = button.ContextMenu;
            contextMenu.PlacementTarget = button;
            contextMenu.IsOpen = true;
        }

        private void Moving(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.listOpen && lWindow != null)
            {
                lWindow.Top = GetTop();
                lWindow.Left = TehWindow.Left;
            }
        }

        private double GetTop()
        {
            if(!Properties.Settings.Default.anchorListTop)
            {
                return TehWindow.Top + TehWindow.Height;
            } else
            {
                return TehWindow.Top - lWindow.Height;
            }
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsView((MainView)TehWindow);
            settings.ShowDialog();
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Helpers.IsWindowOpen<ListView>())
            {
                vm.IsExiting = true;
                lWindow.Close();
            }
        }

        private void ToggleListView(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.anchorListTop)
            {
                if(Properties.Settings.Default.listOpen)
                {
                    Properties.Settings.Default.listOpen = false;
                    lWindow.Visibility = Visibility.Collapsed;
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/upArrow.png"));
                } else
                {
                    
                    Properties.Settings.Default.listOpen = true;
                    lWindow.Visibility = Visibility.Visible;
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/downArrow.png"));
                }
            } else
            {
                if(Properties.Settings.Default.listOpen)
                {
                    Properties.Settings.Default.listOpen = false;
                    lWindow.Visibility = Visibility.Collapsed;
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/downArrow.png"));
                } else
                {
                    Properties.Settings.Default.listOpen = true;
                    lWindow.Visibility = Visibility.Visible;
                    toggleListImage.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/upArrow.png"));
                }
            }        
        }
    }
}
