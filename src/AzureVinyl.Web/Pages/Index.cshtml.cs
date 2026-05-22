using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureVinyl.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    public string EnvironmentName { get; set; } = string.Empty;
    public List<Vinyl> Vinyls = new();

    public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, HttpClient httpClient)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public void OnGet()
    {
        EnvironmentName = _configuration["EnvironmentName"];
        try
        {
            string baseApiUrl = _configuration["ApiSettings:BaseUrl"];
            Vinyls = _httpClient.GetFromJsonAsync<List<Vinyl>>(baseApiUrl + "/api/vinyls").Result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching vinyls.");
        }
    }
}
