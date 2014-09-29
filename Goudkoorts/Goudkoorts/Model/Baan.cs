using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Baan
    {
        public Haven Haven { set; get; }
        public Kar Kar{set; get;}
        public Baan next;

        public Baan()
        {
        }

        public void addBaan(Baan nextBaan) 
        {
            if (next == null)
            {
                next = nextBaan;
            }
            else
            {
                next.addBaan(nextBaan);
            }
        }

        public void addHaven(Haven inputHaven)
        {
            Haven = inputHaven;
        }

        public String getInfo()
        {
            string temp;
            temp = "|";
            temp += this.ToString();
            if (Kar == null)
                //temp += "geen kar";
                temp += "";
            else
                //temp += "wel kar";
                temp += "K";
            if (Haven == null)
                //temp += "geen haven";
                temp += "";
            else
                //temp += "wel haven";
                temp += "H";
            temp += "|";
            return temp;
        }

        public string getInfoAll(String tekst)
        {
            tekst += ""+getInfo();
            if (next != null) 
            {
                tekst = next.getInfoAll(tekst);
            }
            tekst += "\r\n";
            return tekst;
        }

        public void MoveCar(List<Kar> temp)
        {
            if (Kar != null) 
            {
                temp.Add(Kar);                
            }
            if (next != null)
            {
                next.MoveCar(temp);
            }
            else
            {
                foreach (Kar k in temp)
                {
                    k.Move();
                }
            }
        }


    }
}
