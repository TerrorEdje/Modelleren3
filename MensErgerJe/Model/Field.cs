using MensErgerJe.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MensErgerJe
{
    public abstract class Field : UserControl, INotifyPropertyChanged
    {
        //public string FieldName = "Field";
        /*private double opacityButton = 1;
        public double OpacityButton 
        {
            set
            {
                if (value != opacityButton)
                {
                    opacityButton = value;
                    NotifyPropertyChanged("OpacityButton"); 
                }
            }
            get
            {
                return opacityButton;
            }
        }*/
        /*private string color = "Gray";
        public string Color 
        {
            set
            {
                if (value != color)
                {
                    color = value;
                    setButtonColor();
                }
            }
            get
            {
                return color;
            }
        }*/
        /*private Brush brushColor;
        public Brush BrushColor 
        {
            set 
            {
                if (value != brushColor)
                {
                    brushColor = value;
                    NotifyPropertyChanged("BrushColor"); 
                }
            }
            get
            {
                return brushColor;
            }
        }*/
        /*private Image image;
        public Image Image
        {
            set
            {
                if (value != image)
                {
                    NotifyPropertyChanged("Image");
                    image = value;
                }
            }
            get 
            {
                return image; 
            }
        }*/
        public Player Player { get; set;}
        private Pin pin;
        public Pin Pin
        {
            set
            {
                NotifyPropertyChanged("Pin");
                pin = value;
            }
            get
            {
                return pin;
            }
        }

        //private RoundButton button;

        private Field next;
        public Field Next { set { next = value; } get { return next; } }
        public string direction;
        public string dotted;

        public virtual void AddPin(Pin newPin)
        {
            pin = newPin;
            pin.Field = this;
        }

        public virtual void SetNext(Field NextField)
        {
            Next = NextField;
        }

        public virtual void RemovePin()
        {
            pin = null;
            //Color = "Gray";
        }

        public virtual bool checkFinish()
        {
            return false;
        }

        public virtual Field CheckMove(Pin p, int steps)
        {
            if (steps == 0 && Pin.Player != p.Player)
                return this;
            else if (steps == 0 && Pin.Player == p.Player)
                return null;
            else
                return Next.CheckMove(p, steps - 1);
        }

        /*public void SetButton(RoundButton newButton) 
        {
            button = newButton;
            button.DataContext = this;
            setButtonColor();
        }*/

        /*public void setButtonColor() 
        {
            if (Color != null && button != null)
            {
                SolidColorBrush colr = (SolidColorBrush)new BrushConverter().ConvertFromString(Color);
                NotifyPropertyChanged("Button");
                BrushColor = colr;
            }
        }*/

        /*public void EnableButton(bool ableBut) 
        {
            button.IsEnabled = ableBut;
        }*/

        /*public void SetImage(Image newImage)
        {
            Image = newImage;
            string ImagesPath = "pack://application:,,/MensErgerJe;component/Image/" + dotted + direction + ".png";
            Uri uri = new Uri(ImagesPath, UriKind.RelativeOrAbsolute);
            BitmapImage bitmap = new BitmapImage(uri);
            Image.Source = bitmap;
        }*/

        public bool IsEmpty()
        {
            return (Pin == null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
