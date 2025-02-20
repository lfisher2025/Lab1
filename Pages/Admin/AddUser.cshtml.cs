using System.Runtime.ConstrainedExecution;
using Lab1.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class AddUserModel : PageModel
    {
        
        public User NewUser = new User();


        public void OnGet()
        {
            
        }
        // The different post methods are supposed to add the user to the user table and also the respective child table (admin, employee representative, faculty)
        public void OnRepresentativePost (AddUserModel Representative) 
        {
            
        }

        public void OnEmployeePost (AddUserModel Employee) 
        {
            
        }

        public void OnFacultyPost(AddUserModel Faculty)
        {
        }

        public void OnAdminPost(AddUserModel Admin) 
        {
        }

        


    }
}
