using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImaginaryNarwhal;
using EZCounter.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Timers;
using System.Diagnostics;

namespace EZCounter.ViewModels
{
    public class MainViewModel : ICustomPropertyNotify
    {
        public TimerSounds Sounds { get; set; }

        public readonly dbContext db;
        private Timer cTimer;
        private decimal currentTime = 0;

        private Counter current;
        private ObservableCollection<Counter> records;
        private Counter selectedItem;
        private decimal timerCounter;

        public decimal TimerCounter { get=> timerCounter; set => SetProperty(ref timerCounter, value); }
        public Counter Current { get => current; set => SetProperty(ref current, value); }
        public ObservableCollection<Counter> Records { get => records; set => SetProperty(ref records, value); }
        public Counter SelectedItem {
            get
            {
                return selectedItem;
            } set 
            { 
                SetProperty(ref selectedItem, value);
                Current = value;
            }
        }
        public bool IsExiting { get; set; }

        private ICommand increment;
        private ICommand decrement;
        private ICommand newRecord;


        public MainViewModel()
        {
            db = new dbContext();
            Records = db.Counters.ToObservableCollection();
            Current = null;
            cTimer = new Timer();
            cTimer.Elapsed += new ElapsedEventHandler(OnTimerEVent);
            cTimer.Interval = 1000;
            TimerCounter = 0;
            Sounds = new TimerSounds();
        }

        private void OnTimerEVent(object sender, ElapsedEventArgs e)
        {
            cTimer.Stop();
            decimal maxTime = Properties.Settings.Default.timerMinutes * 60 + Properties.Settings.Default.timerSeconds;
            currentTime++;
            TimerCounter = currentTime / maxTime * 100;

            if (currentTime == maxTime)
            {
                var sounds = new TimerSounds();
                TimerCounter = 100;
                cTimer.Stop();
                sounds.PlaySound();
            }
            else
            {
                cTimer.Start();
            }
        }

        public ICommand Increment
            => increment ?? (
            increment = new CommandHandler(
                () => AlterCounter(),
                () => CanExecuteInc));

        public ICommand Decrement
            => decrement ?? (
            decrement = new CommandHandler(
                () => AlterCounter(-1), 
                () => CanExecuteDec));

        public ICommand NewRecord 
            => newRecord ?? (
            newRecord = new CommandHandler(
                () => CreateNewRecord(), 
                () => CanExecute));

        private void AlterCounter(int amt = 1)
        {
            if (Properties.Settings.Default.timerActive)
            {
                if (amt == 1)
                {
                    TimerCounter = 0;
                    currentTime = 0;
                    cTimer.Start();
                }
            } else
            {
                TimerCounter = 0;
            }

            Current.Count = Current.Count + amt;
            db.SaveChanges();
        }

        private void CreateNewRecord()
        {
            var newCounter = new Counter { Count = 0, Date = DateTime.Now };
            db.Counters.Add(newCounter);
            db.SaveChanges();
            Records = db.Counters.ToObservableCollection();
            SelectedItem = newCounter;
            Current = newCounter;
            if (Properties.Settings.Default.timerActive)
            {
                TimerCounter = 0;
                currentTime = 0;
                cTimer.Start();
            }
        }

        public static bool CanExecute => true;

        public bool CanExecuteInc => Current != null;

        public bool CanExecuteDec => Current != null ? Current.Count != 0 : false;
    }
}
