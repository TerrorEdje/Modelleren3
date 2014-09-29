 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goudkoorts.Controller
{
    class Controller
    {
        MainWindow window;
        public Loods LoodsA{set; get;} 
        public Loods LoodsB{set; get;} 
        public Loods LoodsC{set; get;}

        public Controller(MainWindow inputWindow) 
        {
            window = inputWindow;
            simpleVersion();
        }

        public void Run() 
        {
            
        }

        public void simpleVersion()
        {
            LoodsA = new Loods();
            LoodsA.addBaan(new Baan());
            Baan newBaan = new Baan();
            LoodsA.addBaan(newBaan);
            newBaan.addHaven(new Haven());
            LoodsA.addBaan(new Baan());
            window.add(LoodsA.getInfoAll(""));
            LoodsA.CreateCar();
            window.add(LoodsA.getInfoAll(""));
            LoodsA.MoveCar(new List<Kar>());
            window.add(LoodsA.getInfoAll(""));

        }

    }
}
