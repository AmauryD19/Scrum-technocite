using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScrumLearning
{
    public class Meteo
    {
        private const string URL = "http://api.weatherstack.com/current?access_key=4b1f093cfb9c45bb03bf1601fa7ac88b&query=";

        private static readonly HttpClient client = new HttpClient();

        public async Task RequestWeatherAsync()
        {
            try
            {
                Console.WriteLine("--------");
                Console.WriteLine("Veuillez entrer la ville à rechercher :");
                string city = Console.ReadLine();
                HttpResponseMessage response = await client.GetAsync(URL + city);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var webclient = new WebClient();
                string resultat = webclient.DownloadString(URL + city);
                DeseJson desejson = JsonConvert.DeserializeObject<DeseJson>(resultat);
                Console.WriteLine("la température aujourd'hui a " + city + " est de : " + desejson.current.temperature + "°C.");
                Console.ReadKey();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught !");
                Console.WriteLine("Message : " + e.Message);
            }
        }
    }
}