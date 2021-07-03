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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EZCounter.ViewModels;

namespace EZCounter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private Storyboard hideData;
        private Storyboard showData;
        private Storyboard openRow;
        private Storyboard closeRow;
        private bool IsSliderOpen = true;
        private MainViewModel vm;


        public MainView()
        {
            vm = new MainViewModel();
            InitializeComponent();
            ((App)Application.Current).WindowPlace.Register(this);
            DataContext = vm;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            showData = (Storyboard)this.FindResource("showData");
            hideData = (Storyboard)this.FindResource("hideData");
            openRow = (Storyboard)tehGrid.FindResource("openRow");
            closeRow = (Storyboard)tehGrid.FindResource("closeRow");
        }

        private void ToggleSlider(object sender, RoutedEventArgs e)
        {
            if (IsSliderOpen)
            {
                BeginStoryboard(hideData);
                BeginStoryboard(closeRow);
                openArrow.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/downArrow.png"));

                IsSliderOpen = false;
            }
            else
            {
                BeginStoryboard(showData);
                BeginStoryboard(openRow);
                openArrow.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/upArrow.png"));
                IsSliderOpen = true;
            }
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CounterChanged(object sender, TextChangedEventArgs e)
        {
            vm.db.SaveChanges();
        }
    }
}
