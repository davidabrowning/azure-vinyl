using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureVinyl.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;
    public List<Vinyl> Vinyls = new();

    public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public void OnGet()
    {
        try
        {
            Vinyls = _httpClient.GetFromJsonAsync<List<Vinyl>>("http://localhost:5067/api/vinyls").Result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching vinyls.");
        }
    }
}
