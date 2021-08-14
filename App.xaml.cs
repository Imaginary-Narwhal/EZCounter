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
            string Storage = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EZCounter");

            string placementFile = Path.Combine(Storage, "placement.config");

            if(!Directory.Exists(Path.Combine(Storage, "Sounds")))
            {
                Directory.CreateDirectory(Path.Combine(Storage, "Sounds"));
            }

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
