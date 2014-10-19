using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace MensErgerJe.View_Model
{
    public class BoardView_Model
    {
        public int currentPlayer = 0;
        private int amountOfPlayers;
        private Board board;
        public Board Board 
        {
            get 
            {
                return board;
            }
        }
        private Field field;
        public Field Field {
            get
            {
                return field;
            }
            set
            {
                field = value;
                PinSelected();
            }
        }
        private int trownNumber;

        /// <summary>
        /// Een board aanmaken die gaat fungeren als spel
        /// </summary>
        public BoardView_Model(Grid viewGrid, int amountOfPlayers) 
        {
            board = new Board(viewGrid, this);
            this.amountOfPlayers = amountOfPlayers;
        }

        public void throwDice()
        {
            trownNumber = Board.players[currentPlayer].ThrowDice();
            PlayerColor kleur = ((PlayerColor)currentPlayer);
            string stringkleur = kleur.ToString();
            Board.WelcomPlayerTurn = stringkleur;
            if (Board.getCurrentPlayer(currentPlayer).checkWait())
                if (trownNumber == 6)
                    Board.DisableButton = false;
                else
                {
                    Board.DisableButton = true;
                    SetNextTurn();
                }
            else
                Board.DisableButton = false;
        }

        public void PinSelected()
        {
            if (Field != null)
            {
                if (!Field.IsEmpty())
                {
                    int value = currentPlayer;
                    PlayerColor kleur = ((PlayerColor)value);
                    string stringkleur = kleur.ToString();
                    Console.WriteLine("enum:" + stringkleur + ":");
                    if (stringkleur == Field.Pin.Color)
                    {
                        if (field.FieldName == "WaitingField")
                        {
                            if (trownNumber == 6)
                            {
                                if (field.Next.IsEmpty() || field.Next.Pin.Color != stringkleur)
                                {
                                    field.Pin.Move(1);
                                    trownNumber = 0;
                                    Board.DisableButton = true;
                                    SetNextTurn();
                                }
                            }
                        }
                        else
                        {

                            field.Pin.Move(trownNumber);
                            if (board.getCurrentPlayer(currentPlayer).checkWin()) 
                            {
                                MessageBox.Show("Congratulations!!!", "The game is won by " + stringkleur,
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                            trownNumber = 0;
                            Board.DisableButton = true;
                            SetNextTurn();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("op dit veld staat helaas niks");
                }
            }
        }

        private void SetNextTurn()
        {
            if (currentPlayer < amountOfPlayers-1)
            {
                currentPlayer++;
            }
            else
            {
                currentPlayer = 0;
            }
        }
    }
}
