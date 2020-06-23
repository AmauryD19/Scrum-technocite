using System.Collections.Generic;

namespace ScrumLearning
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            MainMenu();
        }

        /// <summary>
        /// Shows the main menu to the user.
        /// </summary>
        public static void MainMenu()
        {
            List<string> list = new List<string> { Constants.NEW_OPINION_ITEM, Constants.CONSULT_DELETE_OPINION_ITEM, Constants.METEO_BXL_ITEM, Constants.CALCULATOR_ITEM, Constants.SAVE_QUIT_ITEM, Admin.MenuAdmin(), Constants.PUISSANCE_4_ITEM, Constants.TIMER_ITEM };
           
            Menu menu = new Menu(list,MenuType.MainMenu);

            menu.ShowMenu(MenuType.MainMenu, true, 0);
            
            //D@ssonville123456
        }
    }
}