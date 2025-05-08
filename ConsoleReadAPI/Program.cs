using Newtonsoft.Json;
using SuperHeroAPIDemo_G_NET9.Models;
using System.Net.Http.Headers;

namespace ConsoleReadAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Denna Console App anropar en web API som finns lokalt i samma solution!
            Console.WriteLine("Anropar Web API...");
            
            // URL till vår lokal API endpoint
            string apiUrl = "https://localhost:7096/api/SuperHero";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();
                    var superheroes = JsonConvert.DeserializeObject<List<SuperHero>>(responseData);

                    Console.WriteLine("Svar från API:");
                    Console.WriteLine("==============");
                    foreach (var superhero in superheroes)
                    {
                        Console.WriteLine($"{superhero.Id}: {superhero.Name}");
                    }

                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
            }

            Console.WriteLine("Valfri knapp för att avsluta...");
            Console.ReadKey();
        }
    }
}
