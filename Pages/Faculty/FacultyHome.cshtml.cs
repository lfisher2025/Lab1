using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Faculty
{
    public class FacultyHomeModel : PageModel
    {
        [BindProperty] public int[] Projects { get; set; }
        public String MultiSelectMessage { get; set; }
        public void OnGet()
        {
        }
    }
}
