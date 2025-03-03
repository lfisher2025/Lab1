using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class AdminHomeModel : PageModel
    {
        
        public List<Dictionary<string, object>> TableData { get; set; } = new();
        public string UserID { get; set; }

        public IActionResult OnGet()
        {
            UserID = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(UserID))
            {
                return RedirectToPage("/HashedLogin/HashedLogin"); // Redirect if not logged in
            }

            SqlDataReader projectReader = DBClass.ViewAdminProjects();

            while (projectReader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < projectReader.FieldCount; i++)
                {
                    row[projectReader.GetName(i)] = projectReader[i];
                }
                TableData.Add(row);
            }

            DBClass.Lab1DBConnection.Close();

            return Page();
        }


 


        
    }
}
