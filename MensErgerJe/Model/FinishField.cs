using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MensErgerJe
{
    public class FinishField : Field
    {

        public Field Previous { get; set; }

        public FinishField(Player player) 
        {
            Player = player;
        }

        public override bool checkFinish()
        {
            return true;
        }

        public override void SetNext(Field NextField)
        {
            
            base.SetNext(NextField);
        }
    }
}
