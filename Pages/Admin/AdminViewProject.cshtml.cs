using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Lab1.Pages.Admin
{
    public class ViewProjectModel : PageModel
    {
        public List<Project> Projects { get; set; }

        public ViewProjectModel()
        {
            Projects = new List<Project>();
        }
        public void OnGet()
        {
            SqlDataReader ViewProjects = DBClass.ViewAdminProjects();
            while (ViewProjects.Read())
            {
                Projects.Add(new Project
                {
                    projectID = Int32.Parse(ViewProjects["projectID"].ToString()),
                    name = ViewProjects["name"].ToString(),
                });
            }
            DBClass.Lab1DBConnection.Close();
        }
    }
}
