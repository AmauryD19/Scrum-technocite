using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu(new List<string> { Constants.NEW_OPINION_ITEM, Constants.CONSULT_DELETE_OPINION_ITEM, Constants.METEO_BXL_ITEM, Constants.CALCULATOR_ITEM, Constants.SAVE_QUIT_ITEM });
            menu.ShowMenu(MenuType.MainMenu, true, 0);
        }
    }
}
