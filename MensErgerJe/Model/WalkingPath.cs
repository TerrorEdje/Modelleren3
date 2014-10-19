using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MensErgerJe.Model
{
    class WalkingPath : Field
    {
        public WalkingPath(string setDirection, string setDotted) 
        {
            direction = setDirection;
            dotted = setDotted;
        }
    }
}
