using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.DataClasses;
using Lab1.Pages.DB;
using System.Data.SqlClient;
using Lab1.Pages.Data_Classes;

namespace Lab1.Pages.Faculty
{
    public class EditGrantModel : PageModel
    {
        [BindProperty]
        public Grant GrantToUpdate { get; set; }
        public EditGrantModel()
        {
            GrantToUpdate = new Grant();
        }
        public void OnGet(int grantid)
        {
            SqlDataReader singleGrant = DBClass.SingleGrantReader(grantid);

            while (singleGrant.Read())
            {
                GrantToUpdate.GrantID = Int32.Parse(singleGrant["GrantID"].ToString());
                GrantToUpdate.Name = singleGrant["Name"].ToString();
                GrantToUpdate.Category = singleGrant["Category"].ToString();
                GrantToUpdate.GrantStatus = singleGrant["GrantStatus"].ToString();
                GrantToUpdate.Amount = Convert.ToDouble(singleGrant["Amount"].ToString());
            }
            DBClass.Lab1DBConnection.Close();
        }

        public IActionResult OnPost()
        {
            DBClass.EditGrant(GrantToUpdate);
            DBClass.Lab1DBConnection.Close();
            return RedirectToPage("/Faculty/ViewGrant");
        }
    }
}
