using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Kar
    {
        public bool lading;

        public Kar() 
        {
            lading = true;
        }

        public void Drop()
        {
            lading = false;
        }

       
    }
}
