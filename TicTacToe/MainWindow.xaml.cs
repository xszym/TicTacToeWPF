using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        List<Button> plansza;
        bool isXPlayerMove;
        bool isOPlayerMove;
        int moveCounter;

        public MainWindow()
        {
            InitializeComponent();

            InitPlansza();
            moveCounter = 0;

            isXPlayerMove = true;
            isOPlayerMove = false;

            InitGameOverScreen();
        }

        private void InitPlansza()
        {
            plansza = new List<Button>();

            plansza.Add(LeftUp);
            plansza.Add(MiddleUp);
            plansza.Add(RightUp);
            plansza.Add(LeftMiddle);
            plansza.Add(MiddleMiddle);
            plansza.Add(RightMiddle);
            plansza.Add(LeftDown);
            plansza.Add(MiddleDown);
            plansza.Add(RightDown);
            foreach (Button b in plansza)
            {
                b.Click += delegate
                {
                    fillButton(b);
                };
            }
        }

        private void InitGameOverScreen()
        {
            GameOverScreen.MouseDown += delegate
            {
                GameOverScreen.Visibility = Visibility.Hidden;
            };
        }

        private void fillButton(Button b)
        {
            if (isXPlayerMove && b.Content.ToString() == "")
            {
                b.Content = "X";
                isOPlayerMove = true;
                isXPlayerMove = false;
                moveCounter++;
            }
            else if (isOPlayerMove && b.Content.ToString() == "")
            {
                b.Content = "O";
                isOPlayerMove = false;
                isXPlayerMove = true;
                moveCounter++;
            }
            gameStatus();
        }

        private void gameStatus()
        {
            if(isPlayerWin("X"))
            {
                StanGry.Content = "X Wygrał";
                GameOverScreen.Visibility = Visibility.Visible;
                refresh();
            }
            else if(isPlayerWin("O"))
            {
                StanGry.Content = "O Wygrał";
                GameOverScreen.Visibility = Visibility.Visible;
                refresh();
            }
            else if (moveCounter >= 9)
            {
                GameOverScreen.Visibility = Visibility.Visible;
                StanGry.Content = "Remis";
                refresh();
            }
            else if(isXPlayerMove)
            {
                StanGry.Content = "Ruch gracza X";
            }
            else if(isOPlayerMove)
            {
                StanGry.Content = "Ruch gracza O";
            }
            
        }

        private bool isPlayerWin(String player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (isPlayerThere(i * 3, player) &&
                   isPlayerThere(i * 3 + 1, player) &&
                   isPlayerThere(i * 3 + 2, player))
                {
                    return true;
                }
                if (isPlayerThere(i, player) &&
                   isPlayerThere(i + 3, player) &&
                   isPlayerThere(i + 6, player))
                {
                    return true;
                }
            }
            if ((isPlayerThere(2, player) && isPlayerThere(4, player) && isPlayerThere(6, player)) ||
                (isPlayerThere(0, player) && isPlayerThere(4, player) && isPlayerThere(8, player)))
            {
                return true;
            }
            return false;
        }

        private bool isPlayerThere(int i, String player)
        {
            return plansza[i].Content.ToString() == player;
        }

        private void refresh()
        {
            foreach (Button b in plansza)
                b.Content = "";
            moveCounter = 0;
        }
    }
}