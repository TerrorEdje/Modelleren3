using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MensErgerJe
{
    class ExitField : Field
    {
        public Field Exit { get; set; }

        public ExitField(Player player) 
        {
            Player = player;
        }
    }
}
