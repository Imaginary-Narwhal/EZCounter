using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Media;
using System.Windows.Media;
using ImaginaryNarwhal;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace EZCounter
{
    public class CommandHandler : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A bolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }

    public static class Helpers
    {
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }

    public class BoolInverterConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        #endregion
    }

    public class TimerSounds : ICustomPropertyNotify
    {
        private MediaPlayer player = new MediaPlayer();
        private List<Uri> _soundFiles;
        private List<string> _allSounds;
        private string _selectedSound;

        public List<Uri> SoundFiles
        {
            get => _soundFiles;
            set => SetProperty(ref _soundFiles, value);
        }

        public List<string> AllSounds
        {
            get => _allSounds;
            set => SetProperty(ref _allSounds, value);
        }

        public string SelectedSound
        {
            get => _selectedSound;
            set 
            { 
                SetProperty(ref _selectedSound, value); 
                if(!SelectedSound.Contains("System:"))
                {
                    player.Close();
                    player.Open(GetSoundFileFromString(value));
                }
            }
        }

        public TimerSounds()
        {
            SoundFiles = new List<Uri>();
            GetSoundFiles();

            AllSounds = new List<string>();
            AllSounds.Add("None");
            AllSounds.Add("System: Asterisk");
            AllSounds.Add("System: Beep");
            AllSounds.Add("System: Exclamation");
            AllSounds.Add("System: Hand");
            AllSounds.Add("System: Question");
            foreach(var file in SoundFiles)
            {
                AllSounds.Add(Path.GetFileName(file.LocalPath));
            }

            SelectedSound = Properties.Settings.Default.timerSound;
        }

        public void GetSoundFiles()
        {
            SoundFiles.Clear();

            var ext = new List<string> { "wav", "mp3", "wma" };

            var files = Directory.EnumerateFiles(Storage.Sounds, "*.*").Where(f => ext.Contains(Path.GetExtension(f).TrimStart('.').ToLowerInvariant())).ToList();
            foreach(var file in files)
            {
                SoundFiles.Add(new Uri(Path.Combine(Storage.Sounds, file)));
            }
        }

        public Uri GetSoundFileFromString(string sound)
        {
            return SoundFiles.Where(x => Path.GetFileName(x.LocalPath) == sound).FirstOrDefault();
        }

        public void PlaySound()
        {
            switch(SelectedSound)
            {
                case "None": break;
                case "System: Asterisk": SystemSounds.Asterisk.Play(); break;
                case "System: Beep": SystemSounds.Beep.Play(); break;
                case "System: Exclamation": SystemSounds.Exclamation.Play(); break;
                case "System: Hand": SystemSounds.Hand.Play(); break;
                case "System: Question": SystemSounds.Question.Play(); break;
                default:
                    player.Play();
                    break;
            }
        }
    }

    public static class Storage
    { 
        public static string Dir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "EZCounter");
        public static string Sounds => Path.Combine(Dir, "Sounds");
    }
}
