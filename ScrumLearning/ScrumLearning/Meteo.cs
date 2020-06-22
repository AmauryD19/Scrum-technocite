using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    public class Meteo
    {
        private const string URL = "http://api.weatherstack.com/current?access_key=4b1f093cfb9c45bb03bf1601fa7ac88b&query=Brussels";
        private int temperature;
        static readonly HttpClient client = new HttpClient();

        public async Task RequestWeatherAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(URL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                var webclient = new WebClient();
                string resultat = webclient.DownloadString(URL);
                

            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught !");
                Console.WriteLine("Message : " + e.Message);
            }
            
        }
        
    }   
}
