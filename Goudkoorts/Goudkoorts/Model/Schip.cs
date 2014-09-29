using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Schip
    {
        private int ladingen;

        public Schip()
        {
            ladingen = 0;
        }
        public void AddLoad()
        {
            ladingen =+ 1;
        }
    }
}
