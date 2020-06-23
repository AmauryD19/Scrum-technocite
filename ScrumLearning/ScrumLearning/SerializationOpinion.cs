using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ScrumLearning
{
    internal class SerializationOpinion
    {
        public SerializationOpinion()
        {
        }

        /// <summary>
        /// Prompts the user to add a new opinion to the already existing ones.
        /// </summary>
        public static void AddOpinion()
        {
            if (!File.Exists("opinions.xml")) // If no file, create a new one
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
            List<Opinion> opinions = (List<Opinion>)serializer.Deserialize(reader); // Get the already existing opinions
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
            } while (note > 10 || note < 0); // Note needs to be between 0 and 10

            string critic;
            do
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Veuillez entrer un commentaire sur la critique : ");
                int buffSize = 1002;
                Stream inStream = Console.OpenStandardInput(buffSize);
                Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, buffSize));
                critic = Console.ReadLine();
            } while (critic.Length < 100 || critic.Length > 1000); // Needs to have between 100 and 1000 characters

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
                        Console.WriteLine($"{opinions.IndexOf(opinion) + 1}. {opinion.Title}, {opinion.Year}"); // Shows the index, followed by the title and the year
                    }

                    int.TryParse(Console.ReadLine(), out int nbOpinion);

                    while (nbOpinion <= 0 || nbOpinion > opinions.Count) // The number entered needs to be between 1 and the maximum possible for an index
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
                    Menu menu = new Menu(new List<string> { Constants.LIST_MOVIES_ITEM, Constants.DELETE_MOVIE_ITEM, Constants.MAIN_MENU_ITEM }, MenuType.ConsultOpinion); // Creates a new menu
                    menu.ShowMenu(MenuType.ConsultOpinion, false, nbOpinion - 1);
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

                // Shows the basic menu to the user
                Program.MainMenu();
            }
        }
    }
}