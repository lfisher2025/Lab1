using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;

namespace Lab1.Pages.Admin
{
    public class AddBusinessPartnerModel : PageModel
    {
        [BindProperty]
        public String CompanyName { get; set; }

        [BindProperty]
        public int RepresentativeID { get; set; }

        [BindProperty]
        public int StatusSelect {  get; set; }

        public String Status;

        //Eventually, the OnGet method will need to select the representatives from the database for a user to select one to attach to the business
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
            
            BusinessPartner NewPartner = new BusinessPartner();
            NewPartner.name = CompanyName;
            NewPartner.status = Status;
            NewPartner.representativeID = RepresentativeID;

            DBClass.AddBusinessPartner(NewPartner);

            DBClass.Lab1DBConnection.Close();

            
        }
    }
}
