using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class WisselSplit : Baan
    {
        //The down stand
        public Baan Baan { get; set; }
        // 0 is up and 1 is down
        public int Stance { get; set; }

        public override string getInfoAll(String tekst)
        {
            tekst += "" + getInfo();
            if (next != null)
            {
                tekst = next.getInfoAll(tekst);
            }
            if (Baan != null)
            {
                tekst = tekst +  "seperated track";
                tekst = Baan.getInfoAll(tekst);
            }
            tekst += "";
            return tekst;
        }

        public override void MoveCar()
        {
            if (next != null)
            {
                next.MoveCar();
            }
            if (Baan != null)
            {
                Baan.MoveCar();
            }

            if (Kar != null)
            {
                if (Stance == 0)
                {
                    next.Kar = Kar;
                    this.Kar = null;
                }
                if (Stance == 1)
                {
                    Baan.Kar = Kar;
                    this.Kar = null;
                }
            }
        }
    }
}
