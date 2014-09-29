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

        public bool load()
        {
            if (Schip != null)
            {
                Schip.AddLoad();
                return true;
            }
            return false;
        }
    }
}
