using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Loods : Baan
    {

        public Loods() : base()
        {
        }

        public void CreateCar() 
        {
            this.Kar = new Kar();
        }
    }
}
