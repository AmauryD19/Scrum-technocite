using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    class DisplayPuissance4
    {
        public static int PlayerChoice(char[,] board, PlayerPuissance4 activePlayer)
        {
            int choice;

            Console.WriteLine("\n" + activePlayer.Name + " à vous ! ");
            Console.WriteLine("Numéro de 1 à 7 : ");
            choice = int.Parse(Console.ReadLine());
            while (choice < 1 || choice > 7)
            {
                Console.WriteLine("Erreur, numéro de 1 à 7 :");
                choice = int.Parse(Console.ReadLine());
            }
            while (board[1, choice] == 'X' || board[1, choice] == 'O')
            {
                Console.WriteLine("Ligne pleine. Numéro de 1 à 7 : ");
                choice = int.Parse(Console.ReadLine());
            }
            return choice;
        }

        public static PlayerPuissance4 Player1()
        {
            Console.WriteLine("Bienvenue sur le puissance 4.\n");

            Console.WriteLine("Nom du joueur 1 : ");
            String name1 = Console.ReadLine();
            char ID1 = 'X';

            PlayerPuissance4 player1 = new PlayerPuissance4(name1, ID1);
            return player1;
        }

        public static PlayerPuissance4 Player2()
        {
            Console.WriteLine("\nNom du joueur 2 : ");
            String name2 = Console.ReadLine();
            char ID2 = 'O';

            PlayerPuissance4 player2 = new PlayerPuissance4(name2, ID2);
            return player2;
        }


        public static void PlayerWin(PlayerPuissance4 actualPlayer)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n" + actualPlayer.Name + " a gagné.");
        }

        public static void DisplayBoard(char[,] board)
        {
            int i, ix;
            int memRow = 6;
            int memCol = 7;

            for (i = 1; i <= memRow; i++)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("|");
                for (ix = 1; ix <= memCol; ix++)
                {
                    if (board[i, ix] != 'X' && board[i, ix] != 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        //board[i, ix] = '■';
                    }
                    if (board[i, ix] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    if (board[i, ix] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(board[i, ix]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("| \n");
            }
            Console.Write(" 1234567");

        }
    }
}
