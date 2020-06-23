using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    class PuissanceFour
    {

        public static void Play()
        {
            Console.Clear();
            PlayerPuissance4 player1 = DisplayPuissance4.Player1();
            PlayerPuissance4 player2 = DisplayPuissance4.Player2();
            BoardPuissance4 board = new BoardPuissance4(player1, player2);

        }
    }
}
