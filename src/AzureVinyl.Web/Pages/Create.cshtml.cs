using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureVinyl.Web.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Vinyl Vinyl { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save via api call here

            return RedirectToPage("Index");
        }
    }
}
