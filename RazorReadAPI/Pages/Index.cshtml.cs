using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SuperHeroAPIDemo_G_NET9.Models;
using System.Net.Http.Headers;

namespace RazorReadAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<SuperHero> SuperHeroes { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // URL till vår lokal API endpoint
            string apiUrl = "https://localhost:7096/api/SuperHero";

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                SuperHeroes = JsonConvert.DeserializeObject<List<SuperHero>>(responseData);
            }
            return Page();
        }
    }
}
