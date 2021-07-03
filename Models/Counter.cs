using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImaginaryNarwhal;

namespace EZCounter.Models
{
    public class Counter : ICustomPropertyNotify
    {
        private int id;
        private DateTime date;
        private int count;

        [Key]
        public int ID { get => id; set => SetProperty(ref id, value); }

        public DateTime Date { get => date; set => SetProperty(ref date, value); }
        public int Count { get => count; set => SetProperty(ref count, value); }
    }
}
