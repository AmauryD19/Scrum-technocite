using System;

namespace ScrumLearning
{
    internal class Timer
    {
        public static void TimerMethod()
        {
            Console.Clear();
            Console.WriteLine("Veuillez entrer le nombre de secondes à patienter");
            int timer = 0;

            do
            {
                try
                {
                    timer = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Les caractères entrés ne correspondent pas à entier positif.");
                }
            }
            while (timer <= 0);

            var cursorTop = Console.CursorTop;
            for (int a = timer; a >= 0; a--)
            {
                Console.SetCursorPosition(0, cursorTop);
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Temps restant avant la fin du timer : {0} secondes", a);
                System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Timer terminé.");
            Console.WriteLine("------------------------------------");
            Console.Beep();
            Console.WriteLine("Appuyez sur une touche pour retourner au menu principal.");
            Console.ReadKey();
        }
    }
}