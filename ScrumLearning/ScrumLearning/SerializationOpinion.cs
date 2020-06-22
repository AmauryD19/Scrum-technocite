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
        public static void Serialize()
        {
            if (!File.Exists("opinions.xml"))
            {
                StreamWriter sw = File.CreateText("opinions.xml");
                sw.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?>");
                sw.WriteLine(@"<opinions>");
                sw.WriteLine(@"</opinions>");
                sw.Close();
            }

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "opinions";

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
            } while (critic.Length < 100 || critic.Length > 1000 );

            opinions.Add(new Opinion() { Title = title, Year = year,Director = director,Note = note, Critic = critic });
            var writer = new StreamWriter("opinions.xml", false);

            serializer.Serialize(writer, opinions);

            writer.Close();
        }
    }
}
