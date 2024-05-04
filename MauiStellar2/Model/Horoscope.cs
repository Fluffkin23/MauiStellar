using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStellar2.Model
{
    public class Horoscope
    {
        public bool Status { get; set; }
        public string Prediction { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public string Mantra { get; set; }
        public string Remedy { get; set; }
    }
}
