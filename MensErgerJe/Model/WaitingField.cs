using System;
using System.ComponentModel;

namespace MensErgerJe
{
    public class WaitingField : Field
    {
        Player Player { get; set; }
        public WaitingField(Player player)
        {
            Player = player;
        }
    }
}
