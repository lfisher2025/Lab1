using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;


namespace Lab1.Pages.Faculty
{
    public class AddGrantModel : PageModel
    {
        [BindProperty]
        public String GrantName { get; set; }
        [BindProperty]
        public double Amount { get; set; }
        [BindProperty]
        public DateTime AwardDate { get; set; }
        [BindProperty]
        public int FacultyID { get; set; }
        [BindProperty]
        public int BusinessPartnerID { get; set; }
        [BindProperty]
        public int StatusSelect { get; set; }
        public String Status;


        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (StatusSelect == 1)
            {
                Status = "In-Progress";
            }
            else if (StatusSelect == 2)
            {
                Status = "Accepted";
            }
            else if (StatusSelect == 3)
            {
                Status = "Rejected";
            }

            Grant NewGrant = new Grant();
            NewGrant.Name = GrantName;
            NewGrant.Amount = Amount;   
            NewGrant.BusinessID = BusinessPartnerID;

            DBClass.AddGrant(NewGrant);

            DBClass.Lab1DBConnection.Close();
        }
    }
}
