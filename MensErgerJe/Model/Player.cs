using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MensErgerJe
{
    public enum PlayerColor {Red, Blue, Green, Yellow }; // indexen op 0,1,2,3

    public class Player
    {
        private static Dice dice;
        private Field startField;
        public Field StartField { set { startField = value; } get { return startField; } }
        public Pin[] Pins;

        public Player(Dice newDice)
        {
            dice = newDice;
            Pins = new Pin[4];
        }

        public void AddPin(Pin newPin) 
        {
            for (int i = 0; i < Pins.Length; i++) 
            {
                if (Pins[i] == null)
                {
                    Pins[i] = newPin;
                    return;
                }
            }
        }

        public bool checkWin()
        {
            foreach (Pin p in Pins)
                if (!p.Field.checkFinish())
                    return false;
            return true;
        }

        public bool checkWait()
        {
            foreach (Pin p in Pins)
                if (p.Field != p.WaitingField)
                    return false;
            return true;
        }

        public int ThrowDice() 
        {
            return dice.TrowDice();
        }

        
    }
}
