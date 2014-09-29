using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Haven
    {

        public Schip Schip{set; get; }

        public Haven() 
        {
            Schip = new Schip();
        }
    }
}
