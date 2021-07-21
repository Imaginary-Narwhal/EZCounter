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
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Window
    {
        private MainViewModel vm;

        public ListView(MainViewModel _vm)
        {
            vm = _vm;
            InitializeComponent();

            DataContext = vm;
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!vm.IsExiting)
            {
                e.Cancel = true;
            }
        }
    }
}
