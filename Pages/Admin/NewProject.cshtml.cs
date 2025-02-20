using Lab1.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class NewProjectModel : PageModel
    {
        [BindProperty]
        public String ProjectName { get; set; }

        [BindProperty]
        public int GrantID { get; set; }

        [BindProperty]
        public int EmployeeID { get; set; }

        [BindProperty]
        public String DueDate { get; set; }

//[BindProperty]
//public List<Grant> 


        public void OnGet()
        {
        }
    }
}
