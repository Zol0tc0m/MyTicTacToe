using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private Button[] buttons;
        private string currentPlayer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            buttons = new Button[9];
            for (int i = 0; i < 9; i++)
            {
                buttons[i] = new Button();
                buttons[i].Click += Button_Click;
                GameBoard.Items.Add(buttons[i]);
            }
            currentPlayer = "X";
            WinnerText.Text = "Игра еще не началась.";
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Button button in buttons)
            {
                button.Content = "";
                button.IsEnabled = true;
            }
            currentPlayer = "X";
            WinnerText.Text = "Игра еще не завершена.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Content = currentPlayer;
            clickedButton.IsEnabled = false;

            if (CheckWinner())
            {
                WinnerText.Text = $"Победил {currentPlayer}!";
                DisableButtons();
            }
            else if (CheckDraw())
            {
                WinnerText.Text = "Опа, ничья!";
                DisableButtons();
            }
            else
            {
                currentPlayer = currentPlayer == "X" ? "O" : "X";
                if (currentPlayer == "O")
                {
                    MakeRobotMove();
                }
            }
        }

        private void MakeRobotMove()
        {
            var emptyButtons = buttons.Where(b => string.IsNullOrEmpty(b.Content.ToString())).ToList();
            if (emptyButtons.Any())
            {
                Random random = new Random();
                int index = random.Next(emptyButtons.Count);
                Button button = emptyButtons[index];
                button.Content = currentPlayer;
                button.IsEnabled = false;

                if (CheckWinner())
                {
                    WinnerText.Text = $"Победил {currentPlayer}!";
                    DisableButtons();
                }
                else if (CheckDraw())
                {
                    WinnerText.Text = "Опа, ничья!";
                    DisableButtons();
                }
                else
                {
                    currentPlayer = currentPlayer == "X" ? "O" : "X";
                }
            }
        }

        private bool CheckWinner()
        {
            int[,] winConditions = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8},
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8},
                {0, 4, 8}, {2, 4, 6}
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                if (buttons[winConditions[i, 0]].Content.ToString() == currentPlayer &&
                    buttons[winConditions[i, 1]].Content.ToString() == currentPlayer &&
                    buttons[winConditions[i, 2]].Content.ToString() == currentPlayer)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckDraw()
        {
            return buttons.All(b => !string.IsNullOrEmpty(b.Content.ToString()));
        }

        private void DisableButtons()
        {
            foreach (Button button in buttons)
            {
                button.IsEnabled = false;
            }
        }
    }
}