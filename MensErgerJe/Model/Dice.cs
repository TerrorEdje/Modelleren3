using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace MensErgerJe
{
    public class Dice : INotifyPropertyChanged
    {
        private static Random random;
        public static int Number { get; set; }
        private string numberImage;
        public string NumberImage
        {
            get
            {
                return numberImage;
            }
            set
            {
                if (value != numberImage)
                {
                    numberImage = value;
                    NotifyPropertyChanged("NumberImage");
                }
            }
        }

        public int getNumber()
        {
            return Number;
        }
        private static Timer timer;

        public Dice()
        {
            random = new Random();
            timer = new Timer(500);

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            NumberImage = "/MensErgerje;component/Image/Dice/4.png";
        }

        public int TrowDice() 
        {
            Number = random.Next(6) + 1;
            NumberImage = "/MensErgerje;component/Image/Dice/" + Number + ".png";
            return Number;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Number = random.Next(6) + 1;
            NumberImage = "/MensErgerje;component/Image/Dice/" + Number + ".png";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
