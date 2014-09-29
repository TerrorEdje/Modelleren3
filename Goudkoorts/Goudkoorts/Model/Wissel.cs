using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goudkoorts
{
    public class Wissel : Baan
    {
        // 0 is up and 1 is down
        public int Stance { get; set; }

        public Baan previous0 { get; set; }
        public Baan previous1 { get; set; }

        public override bool canMove(Baan previous)
        {
            if (Stance == 1)
            {
                return (previous == previous1);
            }
            if (Stance == 0)
            {
                return (previous == previous0);
            }
            return false;
        }
    }
}
