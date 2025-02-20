using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.Data_Classes;

namespace Lab1.Pages.Admin
{
    public class AddBusinessPartnerModel : PageModel
    {
        [BindProperty]
        public String CompanyName { get; set; }

        [BindProperty]
        public String RepresentativeID { get; set; }

        [BindProperty]
        public int StatusSelect {  get; set; }

        public String Status;
        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (StatusSelect == 1)
            {
                Status = "Prospect";

            }
            else if (StatusSelect == 2)
            {
                Status = "Initial-Contact";
            }
            else if (StatusSelect == 3)
            {
                Status = "In-Negotiaion";
            }
            else if (StatusSelect == 4)
            {
                Status = "Memo-Signed";
            }
            else if (StatusSelect == 5)
            {
                Status = "Active-Partner";
            }
            //Below is commented to avoid compiler errors, trying to create a businessPartner object from data classes, for some reason it doesnt like that idea
            BusinessPartner NewPartner = new BusinessPartner();
            
        }
    }
}
