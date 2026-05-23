using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureVinyl.Web.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        [BindProperty]
        public Vinyl Vinyl { get; set; } = new();
        public string EnvironmentName { get; set; } = string.Empty;

        public CreateModel(ILogger<IndexModel> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public void OnGet()
        {
            EnvironmentName = _configuration["EnvironmentName"];
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string baseApiUrl = _configuration["ApiSettings:BaseUrl"];
                var response = _httpClient.PostAsJsonAsync(baseApiUrl + "/api/vinyls", Vinyl).Result;
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("An error occurred while saving vinyl.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving vinyl.");
            }

            return RedirectToPage("Index");
        }
    }
}
