using System;
using System.Collections.Generic;

namespace ScrumLearning
{
    internal class BoardPuissance4
    {
        private char[,] board = new char[8, 10];
        private int choice, win;
        private int myRow, turn = 0;

        public BoardPuissance4(PlayerPuissance4 player1, PlayerPuissance4 player2)
        {
            Console.Clear();
            Console.WriteLine("La partie commence !\n");
            Menu men = new Menu(new List<string> { Constants.NEW_OPINION_ITEM, Constants.CONSULT_DELETE_OPINION_ITEM, Constants.METEO_BXL_ITEM, Constants.CALCULATOR_ITEM, Constants.SAVE_QUIT_ITEM, Admin.MenuAdmin(), Constants.PUISSANCE_4_ITEM, Constants.TIMER_ITEM }, MenuType.MainMenu);
            men.RefreshItemsMainMenu();
            DisplayPuissance4.DisplayBoard(board);
            while (turn < 42)
            {
                choice = DisplayPuissance4.PlayerChoice(board, player1);
                myRow = BoardPos(player1, choice);
                turn += 1;
                Console.Clear();
                win = BoardWin(player1, myRow, choice);
                DisplayPuissance4.DisplayBoard(board);
                if (win == 1)
                {
                    DisplayPuissance4.PlayerWin(player1);
                    Console.ReadKey();
                    men.ShowMenu(MenuType.MainMenu, true, 0);
                }

                choice = DisplayPuissance4.PlayerChoice(board, player2);
                myRow = BoardPos(player2, choice);
                win = BoardWin(player2, myRow, choice);
                turn += 1;
                Console.Clear();
                DisplayPuissance4.DisplayBoard(board);
                if (win == 1)
                {
                    DisplayPuissance4.PlayerWin(player2);
                    men.ShowMenu(MenuType.MainMenu, true, 0);
                }
            }
            Console.WriteLine("Egalité !!");
            men.ShowMenu(MenuType.MainMenu, true, 0);
        }

        public int BoardPos(PlayerPuissance4 player, int choice)
        {
            int memRow = 6;

            while (true)
            {
                if (board[memRow, choice] != 'X' && board[memRow, choice] != 'O')
                {
                    board[memRow, choice] = player.ID;
                    break;
                }
                else
                {
                    memRow = memRow - 1;
                }
            }
            return memRow;
        }

        public int BoardWin(PlayerPuissance4 player, int row, int choice)
        {
            int memLig = row;
            int memCol = choice;
            int colWin = 0;
            int ligWin = 0;
            int dia1Win = 0;
            int dia2Win = 0;

            /** Victoire d'un joueur sur une colonne */
            while (board[memLig + 1, choice] == player.ID)
            {
                colWin++;
                memLig++;
            }
            memLig = row;
            while (board[memLig - 1, choice] == player.ID)
            {
                memLig--;
                colWin++;
            }
            memLig = row;
            if (colWin + 1 >= 4)
            {
                return 1;
            }

            /** Victoire d'un joueur sur une ligne */
            while (board[row, memCol + 1] == player.ID)
            {
                ligWin++;
                memCol++;
            }
            memCol = choice;
            while (board[row, memCol - 1] == player.ID)
            {
                memCol--;
                ligWin++;
            }
            memCol = choice;
            if (ligWin + 1 >= 4)
            {
                return 1;
            }

            /** Victoire d'un joueur sur une diagonale */
            while (board[memLig + 1, memCol + 1] == player.ID)
            {
                dia1Win++;
                memLig++;
                memCol++;
            }
            memLig = row;
            memCol = choice;
            while (board[memLig - 1, memCol - 1] == player.ID)
            {
                memCol--;
                memLig--;
                dia1Win++;
            }
            memLig = row;
            memCol = choice;
            if (dia1Win + 1 >= 4)
            {
                return 1;
            }

            while (board[memLig - 1, memCol + 1] == player.ID)
            {
                memCol++;
                memLig--;
                dia2Win++;
            }
            memLig = row;
            memCol = choice;
            while (board[memLig + 1, memCol - 1] == player.ID)
            {
                memCol++;
                memLig--;
                dia2Win++;
            }
            memLig = row;
            memCol = choice;
            if (dia2Win + 1 >= 4)
            {
                return 1;
            }
            return 0;
        }
    }
}