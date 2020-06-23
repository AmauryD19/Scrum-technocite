using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScrumLearning
{
    public class Meteo
    {
        private const string URL = "http://api.weatherstack.com/current?access_key=4b1f093cfb9c45bb03bf1601fa7ac88b&query=Brussels";

        private static readonly HttpClient client = new HttpClient();

        public async Task RequestWeatherAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(URL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var webclient = new WebClient();
                string resultat = webclient.DownloadString(URL);
                DeseJson desejson = JsonConvert.DeserializeObject<DeseJson>(resultat);
                Console.WriteLine("la température aujourd'hui a Bruxelles est de : " + desejson.current.temperature + "°C.");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught !");
                Console.WriteLine("Message : " + e.Message);
            }
        }
    }
}