using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Kar
    {
        public Baan baan;
        public bool lading;

        public Kar(Baan input) 
        {
            baan = input;
            lading = true;
        }

        public void Drop()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            if (baan.next == null)
            {
                baan.Kar = null;
            }
            else
            {
                baan.next.Kar = this;
                baan.Kar = null;
                baan = baan.next;
                if (baan.Haven != null)
                {
                    baan.Haven.Schip.AddLoad();
                    lading = false;
                }
            }
        }
    }
}
