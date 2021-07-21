using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RestoreWindowPlace;

namespace EZCounter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        public WindowPlace WindowPlace { get; set; }

        public App()
        {
            string placementFile = Path.Combine(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EZCounter"),
                "placement.config");

            WindowPlace = new WindowPlace(placementFile);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            WindowPlace.Save();
            EZCounter.Properties.Settings.Default.Save();
        }
    }
}
