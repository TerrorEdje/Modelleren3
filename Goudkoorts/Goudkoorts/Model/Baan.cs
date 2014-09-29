using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Baan
    {
        public Haven Haven { set; get; }
        public Kar Kar { set; get; }
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
                temp += "+KAR-Lading:" + Kar.lading;
            if (Haven == null)
                //temp += "geen haven";
                temp += "";
            else
                //temp += "wel haven";
                temp += "+HAVEN-Lading:" + Haven.Schip.ladingen;
            temp += "|";
            return temp;
        }

        public virtual string getInfoAll(String tekst)
        {
            tekst += "" + getInfo();
            if (next != null)
            {
                tekst = next.getInfoAll(tekst);
            }
            tekst += "\r\n";
            return tekst;
        }

        public virtual void MoveCar()
        {
            if (next != null)
            {
                next.MoveCar();

                if (Kar != null)
                {
                    if (next.canMove(this))
                    {
                        next.Kar = Kar;
                        this.Kar = null;
                        if (next.Haven != null)
                        {
                            if (next.Haven.load())
                            {
                                next.Kar.Drop();
                            }
                        }
                    }
                }
            }
        }
        public virtual bool canMove(Baan previous)
        {
            return true;
        }
    }
}
