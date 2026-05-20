using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureVinyl.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppDbContext _dbContext;
    private readonly HttpClient _httpClient;
    public List<Vinyl> Vinyls = new List<Vinyl>();
    public string VinylTestResult = string.Empty;

    public IndexModel(ILogger<IndexModel> logger, AppDbContext dbContext, HttpClient httpClient)
    {
        _logger = logger;
        _dbContext = dbContext;
        _httpClient = httpClient;
    }

    public void OnGet()
    {
        Vinyls = _dbContext.Vinyls.ToList();
        VinylTestResult = _httpClient.GetStringAsync("http://localhost:5067/api/vinyls").Result;
    }
}
