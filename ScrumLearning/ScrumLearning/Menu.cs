using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum MenuType { MainMenu, ConsultOpinion }

namespace ScrumLearning
{
    class Menu
    {

        public List<string> Items { get; set; }

        public Menu(List<string> items)
        {
            Items = items;

        }

        public void ShowMenu(MenuType menuType, bool clearConsole, int max)
        {
            if (clearConsole) Console.Clear();

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

        public void GetKey(int action, MenuType menuType, int max)
        {
            if (menuType == MenuType.MainMenu)
            {
                switch (action)
                {
                    case 1: SerializationOpinion.AddOpinion(); ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 2: SerializationOpinion.ShowOpinion(); ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 3: /*BXL_METEO */; ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 4: /* CALCULATOR */; ShowMenu(MenuType.MainMenu, true, 0); break;
                    case 5: Environment.Exit(0);
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
                        Items = new List<string> { Constants.NEW_OPINION_ITEM, Constants.CONSULT_DELETE_OPINION_ITEM, Constants.METEO_BXL_ITEM, Constants.CALCULATOR_ITEM, Constants.SAVE_QUIT_ITEM };
                        ShowMenu(MenuType.MainMenu, true, max);
                        break;
                    default: break;

                }
            }
        }

    }

}
