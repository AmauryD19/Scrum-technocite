using System;
using System.Collections.Generic;
using System.IO;

public enum MenuType { MainMenu, ConsultOpinion }

namespace ScrumLearning
{
    internal class Menu
    {
        public List<string> Items { get; set; }

        public Menu(List<string> items, MenuType menutype)
        {
            Items = items;
            if (menutype == MenuType.MainMenu)
            {
                RefreshItemsMainMenu();
            }
        }

        public void RefreshItemsMainMenu()
        {
            Items = new List<string> { Constants.NEW_OPINION_ITEM, Constants.CONSULT_DELETE_OPINION_ITEM, Constants.METEO_BXL_ITEM, Constants.CALCULATOR_ITEM, Constants.SAVE_QUIT_ITEM, Admin.MenuAdmin(), Constants.PUISSANCE_4_ITEM, Constants.TIMER_ITEM };
            if (Admin.isConnected)
            {
                Items.Add(Constants.CHANGE_COLORBG_ITEM);
                Items.Add(Constants.CHANGE_COLORTXT_ITEM);
            }
        }

        public void ShowMenu(MenuType menuType, bool clearConsole, int max)
        {
            if (clearConsole) Console.Clear();
            if (Admin.isConnected)
            {
                Console.BackgroundColor = Admin.Color;
                Console.ForegroundColor = Admin.Colortxt;
                Console.Clear();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }
            foreach (var item in Items)
            {
                Console.WriteLine($"{Items.IndexOf(item) + 1}. {item}");
            }
            int action = 0;
            do
            {
                try
                {
                    Console.WriteLine("----------------------------------");
                    action = int.Parse(Console.ReadLine());
                    GetKey(action, menuType, max);
                }
                catch (Exception)
                {
                    Console.WriteLine("La valeur entrée n'est pas un chiffre.");
                }
            } while (action <= 1 || action > Items.Count);
        }

        public void ChangeColor()
        {
            Console.WriteLine("Veuillez entrer la couleur de fond souhaitée :");
            string USColor = Console.ReadLine();
            if (USColor != Admin.Color.ToString())
            {
                ConsoleColor col;
                if (Enum.TryParse(USColor, true, out col))
                {
                    Console.BackgroundColor = col;
                    Admin.Color = col;
                }
            }
        }

        public void ChangeColorTXT()
        {
            Console.WriteLine("Veuillez entrer la couleur de texte souhaitée :");
            string USColor = Console.ReadLine();
            if (USColor != Admin.Colortxt.ToString())
            {
                ConsoleColor col;
                if (Enum.TryParse(USColor, true, out col))
                {
                    Console.ForegroundColor = col;
                    Admin.Colortxt = col;
                }
            }
        }

        public void GetKey(int action, MenuType menuType, int max)
        {
            if (menuType == MenuType.MainMenu)
            {
                switch (action)
                {
                    case 1: SerializationOpinion.AddOpinion(); ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 2: SerializationOpinion.ShowOpinion(); ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 3:
                        Meteo meteo = new Meteo();
                        meteo.RequestWeatherAsync();
                        ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 4: Calculator.CalculatorMethod(); ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 5:
                        Environment.Exit(0);
                        break;

                    case 6:

                        if (!File.Exists("admin.txt"))
                        {
                            Admin.CreateAdmin();
                        }
                        else if (!Admin.isConnected)
                        {
                            Admin.LogIn();
                        }
                        else if (Admin.isConnected)
                        {
                            Admin.LogOut();
                        }
                        RefreshItemsMainMenu();
                        ShowMenu(MenuType.MainMenu, true, 0);
                        break;

                    case 7:
                        PuissanceFour.Play();
                        ShowMenu(MenuType.MainMenu, true, 0);
                        break;

                    case 8:
                        Timer.TimerMethod();
                        ShowMenu(MenuType.MainMenu, true, 0);
                        break;

                    case 9:
                        ChangeColor();
                        ShowMenu(MenuType.MainMenu, true, 0);
                        break;

                    case 10:
                        ChangeColorTXT();
                        ShowMenu(MenuType.MainMenu, true, 0);
                        break;

                    default: break;
                }
            }
            else if (menuType == MenuType.ConsultOpinion)
            {
                switch (action)
                {
                    case 1:
                        Console.Clear();
                        SerializationOpinion.ShowOpinion();
                        break;

                    case 2:
                        SerializationOpinion.DeleteOpinion(max);
                        break;

                    case 3:
                        RefreshItemsMainMenu();
                        ShowMenu(MenuType.MainMenu, true, max);
                        break;

                    default: break;
                }
            }
        }
    }
}