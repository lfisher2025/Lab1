using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class AdminHomeModel : PageModel
    {
        
        public List<Dictionary<string, object>> TableData { get; set; } = new();

    
        public void OnGet()
        {
            //Gather the projects for the current User 

            //Will need to collect the user id from the session, added later

            //SqlDataReader projectReader = DBClass.ViewAdminProjects(12);

            //while (projectReader.Read())
            //{
            //    var row = new Dictionary<string, object>();
            //    for (int i = 0; i < projectReader.FieldCount; i++)
            //    {
            //        row[projectReader.GetName(i)] = projectReader[i];
            //    }
            //    TableData.Add(row);
            //}

            DBClass.Lab1DBConnection.Close();


        }
    }
}
