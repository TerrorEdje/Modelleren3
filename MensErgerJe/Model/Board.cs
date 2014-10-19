using MensErgerJe.Commands;
using MensErgerJe.Model;
using MensErgerJe.View;
using MensErgerJe.View_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;

namespace MensErgerJe
{
    public class Board : INotifyPropertyChanged
    {
        public LinkedList<Field> veld = new LinkedList<Field>();
        private XmlDocument xmldoc;
        public Player[] players;
        private int columns, rows;
        private Grid FieldGrid;
        public Dice Dice;

        private string boardTitel;
        public string BoardTitel { get { return "Mens erger je (" + boardTitel + ")"; } }
        private int playerAmount;
        public string PlayerAmount { get { return "Aantal spelers: " + playerAmount; } }
        private bool disableButton = true;
        public bool DisableButton { get { return disableButton; } set { disableButton = value; NotifyPropertyChanged("DisableButton"); } }
        private Brush brushColor = Brushes.Gray;
        public Brush BrushColor { get { return brushColor; } set { brushColor = value;} }
        private double opacityButton = 1;
        public double OpacityButton { get { return opacityButton; } set { opacityButton = value; } }
        public BoardView_Model vm { get; set; }
        private string welcomPlayerTurn = "Blue";
        public string WelcomPlayerTurn { get { return "It is the " + welcomPlayerTurn + " players turn"; } set { welcomPlayerTurn = value; NotifyPropertyChanged("WelcomPlayerTurn"); } }

        /// <summary>
        /// Maken van het spel
        /// </summary>
        public Board(Grid viewGrid, View_Model.BoardView_Model newvm)
        {
            xmldoc = new XmlDocument();
            FieldGrid = viewGrid;
            Dice = new Dice();
            vm = newvm;

            //gooien afhandelen
            //ThrowCommand = new ThrowDice();

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            /// <summary>
            /// laden van de map
            /// </summary>
            //xmldoc.Load(GetResourceTextFile("normal.xaml"));
            
            Console.WriteLine(GetPath());
            xmldoc.Load(GetPath() + "Map\\normal.xml");

            boardTitel = xmldoc.SelectSingleNode("spel/informatie/title").InnerText;
            //NotifyPropertyChanged("boardTitel");
            playerAmount = Convert.ToInt32(xmldoc.SelectSingleNode("spel/informatie/aantal_mensen").InnerText);
            //NotifyPropertyChanged("PlayerAmount");
           
            //spelers aanmaken
            MakePlayers(playerAmount);

            //hoogte van de map ophalen
            columns = Convert.ToInt32(xmldoc.SelectSingleNode("spel/informatie/columns").InnerText);
            rows = Convert.ToInt32(xmldoc.SelectSingleNode("spel/informatie/rows").InnerText);

            //maken van het grid
            CreateGrid();
            
        }

        private void CreateGrid()
        {
            //xml ontcijferen
            Field[,] Fields = new Field[columns, rows];
            int countrow = 0, countcolumn = 0;

            foreach (XmlNode node in xmldoc.SelectNodes("spel/map/row")) 
            {

                string tempRow = node.SelectSingleNode("short").InnerText;
                
                string[] tempFields = tempRow.Split(';');
                foreach (string tempField in tempFields)
                {
                    Field temp = null;
                    switch (tempField) 
                    {
                        case " ":
                            temp = null;
                            break;
                        case "F":
                            temp = new NormalField();
                            break;
                        case "W-r":
                            temp = new WaitingField(players[(int)PlayerColor.Red]);
                            temp.AddPin(new Pin(players[(int)PlayerColor.Red], temp));
                            players[(int)PlayerColor.Red].AddPin(temp.Pin);
                            break;
                        case "W-b":
                            temp = new WaitingField(players[(int)PlayerColor.Blue]);
                            temp.AddPin(new Pin(players[(int)PlayerColor.Blue],temp));
                            players[(int)PlayerColor.Blue].AddPin(temp.Pin);
                            break;
                        case "W-g":
                            temp = new WaitingField(players[(int)PlayerColor.Green]);
                            temp.AddPin(new Pin(players[(int)PlayerColor.Green], temp));
                            players[(int)PlayerColor.Green].AddPin(temp.Pin);
                            break;
                        case "W-e":
                            temp = new WaitingField(players[(int)PlayerColor.Yellow]);
                            temp.AddPin(new Pin(players[(int)PlayerColor.Yellow], temp));
                            players[(int)PlayerColor.Yellow].AddPin(temp.Pin);
                            break;
                        case "F-r":
                            temp = new FinishField(players[(int)PlayerColor.Red]);
                            break;
                        case "F-b":
                            temp = new FinishField(players[(int)PlayerColor.Blue]);
                            break;
                        case "F-g":
                            temp = new FinishField(players[(int)PlayerColor.Green]);
                            break;
                        case "F-e":
                            temp = new FinishField(players[(int)PlayerColor.Yellow]);
                            break;
                        case "F-F-b":
                            temp = new ExitField(players[(int)PlayerColor.Blue]);
                            break;
                        case "F-F-g":
                            temp = new ExitField(players[(int)PlayerColor.Green]);
                            break;
                        case "F-F-r":
                            temp = new ExitField(players[(int)PlayerColor.Red]);
                            break;
                        case "F-F-e":
                            temp = new ExitField(players[(int)PlayerColor.Yellow]);
                            break;
                        case "F-S-b":
                            temp = new StartField(players[(int)PlayerColor.Blue]);
                            players[(int)PlayerColor.Blue].StartField = temp;
                            break;
                        case "F-S-g":
                            temp = new StartField(players[(int)PlayerColor.Yellow]);
                            players[(int)PlayerColor.Green].StartField = temp;
                            break;
                        case "F-S-r":
                            temp = new StartField(players[(int)PlayerColor.Red]);
                            players[(int)PlayerColor.Red].StartField = temp;
                            break;
                        case "F-S-e":
                            temp = new StartField(players[(int)PlayerColor.Yellow]);
                            players[(int)PlayerColor.Yellow].StartField = temp;
                            break;
                        case "|":
                            temp = new WalkingPath("Vertical", "");
                            break;
                        case "-":
                            temp = new WalkingPath("Horizontal", "");
                            break;
                        case "\\":
                            temp = new WalkingPath("Vertical", "Block");
                            break;
                        case "/":
                            temp = new WalkingPath("Horizontal", "Block");
                            break;

                    }
                    if (Fields != null) Fields[countrow, countcolumn] = temp;
                    countcolumn++;
                }
                countcolumn = 0;
                countrow++;
            }

            //koppelen van de velden.
            Field firstField;
            Field lastField;
            string position = "";
            string dotted = "";

            //eerste veld opsporen
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    if (Fields[x, y] != null)
                    {
                        if (Fields[x, y].GetType() == typeof(WalkingPath))
                        {
                            switch (Fields[x, y].direction)
                            {
                                case "Vertical":
                                    firstField = Fields[x - 1, y];
                                    lastField = Fields[x + 1, y];
                                    position = "Vertical";
                                    dotted = Fields[x, y].dotted;
                                    break;
                                case "Horizontal":
                                    firstField = Fields[x, y - 1];
                                    lastField = Fields[x, y + 1];
                                    position = "Horizontal";
                                    dotted = Fields[x, y].dotted;
                                    break;
                            }

                            if (position.Equals("Vertical"))
                            {
                                if ((columns) / 2 < y)
                                {
                                    if (dotted.Equals("Block")) // finish field, moet heen terug
                                    {
                                        //firstField.SetNext(lastField);
                                        //((FinishField)lastField).SetPrevious(firstField);
                                    }
                                    else
                                    {
                                        //firstField.SetNext(lastField);
                                    }
                                }
                                else
                                {
                                    if (dotted.Equals("Block")) // finish field, moet heen terug
                                    {
                                        //firstField.SetNext(lastField);
                                        //((FinishField)lastField).SetPrevious(firstField);
                                    }
                                    /*else if (firstField.FieldName == "ExitField" && lastField.FieldName == "FinishField")
                                    {
                                        ((ExitField)firstField).Exit = lastField;
                                    }
                                    else if (firstField.FieldName == "FinishField" && lastField.FieldName == "ExitField")
                                    {
                                        ((ExitField)lastField).Exit = firstField;
                                    }*/
                                    else
                                    {
                                        //lastField.SetNext(firstField);
                                    }
                                }
                            }
                            else
                            {
                                if ((rows) / 2 < x)
                                {
                                    if (dotted.Equals("Block")) // finish field, moet heen terug
                                    {
                                        //firstField.SetNext(lastField);
                                        //((FinishField)lastField).SetPrevious(firstField);
                                    }
                                    else
                                    {
                                        //lastField.SetNext(firstField);
                                    }
                                }
                                else
                                {
                                    if (dotted.Equals("Block")) // finish field
                                    {
                                        //firstField.SetNext(lastField);
                                        //((FinishField)lastField).SetPrevious(firstField);
                                    }
                                    /*else if (lastField.FieldName == "ExitField" && firstField.FieldName == "FinishField")
                                    {
                                        ((ExitField)lastField).Exit = firstField;
                                    }
                                    else if (firstField.FieldName == "ExitField" && lastField.FieldName == "FinishField")
                                    {
                                        ((ExitField)firstField).Exit = lastField;
                                    }*/
                                    else
                                    {
                                        //firstField.SetNext(lastField);
                                    }
                                }
                            }
                        }
                        else if (Fields[x, y].GetType() == typeof(WaitingField))
                        {
                            foreach (Player p in players)
                            {
                                if (Fields[x,y].Player == p)
                                {
                                    Fields[x, y].SetNext(p.StartField);
                                }
                            }

                        }
                    }
                }
            }

            //grid tekenen
            int cellBig = 40, cellSmall = 28;
            GridLength CellSizeBig = new GridLength(cellBig);
            GridLength CellSizeSmall = new GridLength(cellSmall);

            for (int x = 0; x < rows; x++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                if (x % 2 == 0)
                {
                    colDef.Width = CellSizeBig;
                }
                else
                {
                    colDef.Width = CellSizeSmall;
                }
                FieldGrid.ColumnDefinitions.Add(colDef);
            }

            for (int y = 0; y < columns; y++)
            {
                RowDefinition rowDef = new RowDefinition();
                if (y % 2 == 0)
                {
                    rowDef.Height = CellSizeBig;
                }
                else
                {
                    rowDef.Height = CellSizeSmall;
                }
                FieldGrid.RowDefinitions.Add(rowDef);
            }

            //Grid koppelen
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    if (Fields[y, x] != null)
                    {
                        if(Fields[y, x].GetType() == typeof(WalkingPath))
                        {
                            Image smallpic = new Image();

                            smallpic.Height = cellSmall;
                            smallpic.Width = cellSmall;

                            smallpic.SetValue(Grid.RowProperty, y);
                            smallpic.SetValue(Grid.ColumnProperty, x);

                            //Fields[y, x].SetImage(smallpic);
                            FieldGrid.Children.Add(smallpic);
                        }
                        else
                        {
                            RoundButton button = new RoundButton(Fields[y, x], vm);

                            button.SetValue(Grid.RowProperty, y);
                            button.SetValue(Grid.ColumnProperty, x);

                            //Fields[y, x].SetButton(button);
                            FieldGrid.Children.Add(button);
                        }
                     }
                }
            }

            // grid kleuren
            foreach(Field fieldd in Fields)
            {
                //if(fieldd != null) fieldd.setButtonColor();
            }
        }

        private void MakePlayers(int amount) 
        {
            players = new Player[amount];
            for (int i = 0; i < amount; i++)
            {
                players[i] = new Player(Dice);
            }
        }

        public Player getCurrentPlayer(int i)
        {
            return players[i];
        }

        private string GetPath() 
        {
            string path = Directory.GetCurrentDirectory();
            string correctPath = "";
            int slashes = 0, endIndex = 0;
            for (int i = path.Length - 1; slashes != 2; i--) 
            {
                if(path[i] == '\\')
                {
                    slashes++;
                    if (slashes == 2) 
                    {
                        endIndex = i;
                    }
                }
            }
            for (int i = 0; i <= endIndex; i++) 
            {
                if (path[i] == '\\')
                {
                    correctPath += "\\";
                }
                else 
                { 
                    correctPath += path[i];
                }
            }
            return correctPath;
        }

        public ICommand ThrowCommand
        {
            get;
            private set;
        }
    
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
