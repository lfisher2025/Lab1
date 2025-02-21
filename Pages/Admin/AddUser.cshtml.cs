using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Lab1.Pages.Data_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public int UserType { get; set; }
        public AddUserModel Representative { get; set; }
        public AddUserModel Employee { get; set; }
        public AddUserModel Faculty { get; set; }
        public AddUserModel Admin { get; set; }
        public String SelectUser { get; set; }
        [Required] public String FirstName { get; set; }
        [Required] public String LastName { get; set; }
        [Required] public String MiddleInitial { get; set; }
        [Required] public String PhoneNumber { get; set; }
        [Required] public String Email { get; set; }
        //public User NewUser = new User();


        public void OnGet()
        {
            
        }
        // The different post methods are supposed to add the user to the user table and also the respective child table (admin, employee representative, faculty)
        public IActionResult OnRepresentativePost (AddUserModel Representative) 
        {
            SelectUser = Representative.SelectUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RedirectToPage("AddUser");
            //code for saving to DB goes here
        }

        public IActionResult OnEmployeePost (AddUserModel Employee) 
        {
            SelectUser = Employee.SelectUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RedirectToPage("AddUser");
            //code for saving to DB here
        }

        public IActionResult OnFacultyPost(AddUserModel Faculty)
        {
            SelectUser = Faculty.SelectUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RedirectToPage("AddUser");
            //code for saving to DB here
        }

        public IActionResult OnAdminPost(AddUserModel Admin) 
        {
            SelectUser = Admin.SelectUser;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RedirectToPage("AddUser");
            //code for saving to DB here
        }

        


    }
}
