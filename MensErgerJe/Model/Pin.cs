using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MensErgerJe
{
    public class Pin
    {
        public Player Player {get;set;}
        public Field Field { get; set; }
        public Field WaitingField {get;set;}

        public Pin(Player player, Field waitingField)
        {
            Player = player;
            WaitingField = WaitingField;
        }

        public bool Move(int dice)
        {
            Field temp = Field.CheckMove(this,dice);
            
            if (temp == null)
                return false;            
            if (temp.Pin != null)
                    temp.Pin.WaitingField.AddPin(temp.Pin);

            Field.RemovePin();
            temp.AddPin(this);
            temp.Opacity = 1;

            return true;
        }
    }
}
