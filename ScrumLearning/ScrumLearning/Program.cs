using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using static ScrumLearning.Meteo;

namespace ScrumLearning
{
    class Program
    {
         static void Main(string[] args)
        {
            Meteo meteo = new Meteo();
            meteo.RequestWeatherAsync();
            Console.WriteLine("zeffdez");
            Console.ReadKey();
        }
    }
}
