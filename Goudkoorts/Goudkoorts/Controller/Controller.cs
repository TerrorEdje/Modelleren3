 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goudkoorts.Controller
{
    public class Controller
    {
        public Loods LoodsA{set; get;} 
        public Loods LoodsB{set; get;} 
        public Loods LoodsC{set; get;}

        public Controller() 
        {
        }

        public void Run() 
        {
            
        }

        public string show()
        {
            return LoodsA.getInfoAll("");
        }

        public void simpleVersion()
        {
            LoodsA = new Loods();
            LoodsA.addBaan(new Baan());
            Baan newBaan = new Baan();
            LoodsA.addBaan(newBaan);
            newBaan.addHaven(new Haven());
            LoodsA.addBaan(new Baan());
            WisselSplit wisselsplit = new WisselSplit();
            wisselsplit.Baan = new Baan();
            wisselsplit.Stance = 1;
            LoodsA.addBaan(wisselsplit);
            LoodsA.addBaan(new Baan());
            Wissel wissel = new Wissel();

            Baan wissel0 = new Baan();
            Baan wissel1 = new Baan();
            LoodsA.addBaan(wissel0);
            wisselsplit.Baan.addBaan(wissel1);
            wissel.previous0 = wissel0;
            wissel.previous1 = wissel1;
            wissel0.next = wissel;
            wissel1.next = wissel;
            wissel.Stance = 1;
            wissel.addBaan(new Baan());
            
            LoodsA.CreateCar();

            LoodsA.MoveCar();
            LoodsA.MoveCar();
            LoodsA.MoveCar();
            LoodsA.MoveCar();
            LoodsA.MoveCar();
            LoodsA.MoveCar();
            LoodsA.MoveCar();

        }

    }
}
