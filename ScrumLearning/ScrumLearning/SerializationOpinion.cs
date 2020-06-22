using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ScrumLearning
{
    class SerializationOpinion
    {
        public SerializationOpinion() { }

        public static void AddOpinion()
        {
            if (!File.Exists("opinions.xml"))
            {
                StreamWriter sw = File.CreateText("opinions.xml");
                sw.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                sw.WriteLine(@"<opinions>");
                sw.WriteLine(@"</opinions>");
                sw.Close();
            }

            XmlRootAttribute xRoot = new XmlRootAttribute
            {
                ElementName = "opinions"
            };

            XmlSerializer serializer = new XmlSerializer(typeof(List<Opinion>), xRoot);
            StreamReader reader = new StreamReader("opinions.xml");
            List<Opinion> opinions = (List<Opinion>)serializer.Deserialize(reader);
            reader.Close();

            Console.WriteLine("------------------------------------");
            Console.WriteLine("Veuillez entrer le titre du film à entrer : ");
            string title = Console.ReadLine();
            int year = 0;
            do
            {
                try
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("Veuillez entrer l'année de sortie du film : ");
                    year = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Les caractères entrés ne correspondent pas à une année.");
                }
            } while (year < 1895 || year > DateTime.Now.Year); // Cannot be before the first ever movie, and after the current year

            Console.WriteLine("------------------------------------");
            Console.WriteLine("Veuillez entrer le réalisateur du film : ");
            string director = Console.ReadLine();

            int note = -1;
            do
            {
                try
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("Veuillez entrer la note que vous voulez donner au film : ");
                    note = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Les caractères entrés ne correspondent pas à une note (de 0 à 10).");
                }
            } while (note > 10 || note < 0);

            string critic; // Contains the previous entry if it is wrong
            do
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Veuillez entrer un commentaire sur la critique : ");
                critic = Console.ReadLine();
            } while (critic.Length < 100 || critic.Length > 1000);

            opinions.Add(new Opinion() { Title = title, Year = year, Director = director, Note = note, Critic = critic });


            Write(serializer, opinions);
        }

        /// <summary>
        /// Shows a menu that will contains all the previouses opinions inside of the XML file.
        /// </summary>
        public static void ShowOpinion()
        {
            if (!File.Exists("opinions.xml"))
            {
                Console.WriteLine("Il n'y a pas de fichier d'où récupérer les données. Retour au menu principal.");
            }
            else
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "opinions";

                XmlSerializer serializer = new XmlSerializer(typeof(List<Opinion>), xRoot);
                StreamReader reader = new StreamReader("opinions.xml");
                List<Opinion> opinions = (List<Opinion>)serializer.Deserialize(reader);
                reader.Close();

                if (opinions.Any())
                {
                    Console.WriteLine("Quel est l'avis que vous souhaitez afficher ?");
                    foreach (var opinion in opinions)
                    {
                        Console.WriteLine($"{opinions.IndexOf(opinion) + 1}. {opinion.Title}, {opinion.Year}");
                    }

                    int.TryParse(Console.ReadLine(), out int nbOpinion);

                    while (nbOpinion <= 0 || nbOpinion > opinions.Count)
                    {
                        Console.WriteLine("Quel est l'avis que vous souhaitez afficher ?");
                        int.TryParse(Console.ReadLine(), out nbOpinion);
                    }
                    Console.Clear();
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine($"Titre : {opinions.ElementAt(nbOpinion - 1).Title}");
                    Console.WriteLine($"Année de réalisation : {opinions.ElementAt(nbOpinion - 1).Year}");
                    Console.WriteLine($"Réalisateur : {opinions.ElementAt(nbOpinion - 1).Director}");
                    Console.WriteLine($"Note : {opinions.ElementAt(nbOpinion - 1).Note}/10");
                    Console.WriteLine($"Critique : {opinions.ElementAt(nbOpinion - 1).Critic}");
                    Console.WriteLine("---------------------------------------");
                    Menu menu = new Menu(new List<string> { Constants.LIST_MOVIES_ITEM, Constants.DELETE_MOVIE_ITEM, Constants.MAIN_MENU_ITEM });
                    menu.ShowMenu(MenuType.ConsultOpinion, false, nbOpinion-1);
                }
                else
                {
                    Console.WriteLine("Il n'y a aucun avis à afficher.");
                    Console.ReadKey();
                }

            }
        }

        /// <summary>
        /// Allows to write the opinions inside of the XML file.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="opinions"></param>
        private static void Write(XmlSerializer serializer, List<Opinion> opinions)
        {
            var writer = new StreamWriter("opinions.xml", false);

            serializer.Serialize(writer, opinions);

            writer.Close();
        }

        public static void DeleteOpinion(int index)
        {
            if (!File.Exists("opinions.xml"))
            {
                Console.WriteLine("Il n'y a pas de fichier d'où récupérer les données. Retour au menu principal.");
            }
            else
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "opinions";

                XmlSerializer serializer = new XmlSerializer(typeof(List<Opinion>), xRoot);
                StreamReader reader = new StreamReader("opinions.xml");
                List<Opinion> opinions = (List<Opinion>)serializer.Deserialize(reader);
                reader.Close();

                opinions.RemoveAt(index);
                Write(serializer, opinions);

                Menu menu = new Menu(new List<string> { Constants.NEW_OPINION_ITEM, Constants.CONSULT_DELETE_OPINION_ITEM, Constants.METEO_BXL_ITEM, Constants.CALCULATOR_ITEM, Constants.SAVE_QUIT_ITEM });
                menu.ShowMenu(MenuType.MainMenu, true, 0);
            }
        }
    }
}