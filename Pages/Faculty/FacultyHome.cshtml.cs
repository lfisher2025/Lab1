using System.Data.SqlClient;
using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab1.Pages.Faculty
{
    public class FacultyHomeModel : PageModel
    {
         public List<SelectListItem> Projects { get; set; } = new();
        public String MultiSelectMessage { get; set; }
        public String UserID { get; set; }
        public int SelectedProject { get; set; }


        public IActionResult OnGet()
        {
            // gather the current user ID, the populate button logs as a user, if you want to test with a valid employee id, enter 3 as your user id

            UserID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(UserID))
            {
                return RedirectToPage("/HashedLogin/HashedLogin"); // Redirect if not currently logged in
            }


            // Gather all projects for the current user
            int facultyID = Convert.ToInt32(UserID);

            SqlDataReader reader = DBClass.ViewFacultyProjects(facultyID);

            while (reader.Read())
            {
                Projects.Add(new SelectListItem
                {
                    Value = reader["projectID"].ToString(),  // Project ID as value
                    Text = reader["name"].ToString()  // Project name as displayed text
                });
            }
            DBClass.Lab1DBConnection.Close();
            return Page();

        }
    }
}
