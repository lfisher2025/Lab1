using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.Faculty
{
    public class ViewGrantModel : PageModel
    {
        public List<Grant> GrantInfo { get; set; }

        public ViewGrantModel()
        {
            GrantInfo = new List<Grant>();
        }
        public void OnGet()
        {
            SqlDataReader ViewGrants = DBClass.ViewAllGrants();
            while (ViewGrants.Read())
            {
                GrantInfo.Add(new Grant
                {
                    GrantID = Int32.Parse(ViewGrants["GrantID"].ToString()),
                    Name = ViewGrants["Name"].ToString(),
                    Category = ViewGrants["Category"].ToString(),
                    GrantStatus = ViewGrants["GrantStatus"].ToString()
                });
            }
            DBClass.Lab1DBConnection.Close();
        }
    }
}
