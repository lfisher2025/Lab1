using System.Data.SqlClient;
using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class ViewProjectModel : PageModel
    {
        private readonly string connectionString = "Server=localhost;Database=Lab1;Trusted_Connection=True";

        [BindProperty(SupportsGet = true)]
        public string ProjectName { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime DueDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime SubmissionDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool CompleteStatus { get; set; }

        public List<Project> Projects { get; set; } = new();

        public void OnGet()
        {
      
        }
       public IActionResult OnPost()
        {
        Project tempProject = new Project();
            tempProject.name = ProjectName;
            tempProject.DueDate = DueDate;
            tempProject.submissionDate = SubmissionDate;
            tempProject.CompleteStatus = CompleteStatus;


            SqlDataReader projectReader = DBClass.ViewProject(tempProject);

            while (projectReader.Read())
            {
                Projects.Add(new Project
                {
                    name = projectReader["ProjectName"].ToString(),
                    DueDate = projectReader["dueDate"] is DBNull ? (DateTime?)null : (DateTime)projectReader["dueDate"],
                    submissionDate = projectReader["submissionDate"] is DBNull ? (DateTime?)null : (DateTime)projectReader["submissionDate"],
                    CompleteStatus = projectReader["completeStatus"] is DBNull ? (bool?)null : (bool)projectReader["completeStatus"]

                }
                );
            }
            DBClass.Lab1DBConnection.Close();
            return Page();
        }
       

  
    }
}
