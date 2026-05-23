using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureVinyl.Web.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        [BindProperty]
        public Vinyl Vinyl { get; set; } = null!;

        public EditModel(ILogger<IndexModel> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                string baseApiUrl = _configuration["ApiSettings:BaseUrl"];
                Vinyl = await _httpClient.GetFromJsonAsync<Vinyl>($"{baseApiUrl}/api/vinyls/{id}");
                if (Vinyl == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading vinyl.");
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                string baseApiUrl = _configuration["ApiSettings:BaseUrl"];

                var response = await _httpClient.PutAsJsonAsync($"{baseApiUrl}/api/vinyls/{Vinyl.Id}", Vinyl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error updating vinyl.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vinyl.");
                return Page();
            }

            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                string baseApiUrl = _configuration["ApiSettings:BaseUrl"];

                var response = await _httpClient.DeleteAsync($"{baseApiUrl}/api/vinyls/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error deleting vinyl.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vinyl.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
