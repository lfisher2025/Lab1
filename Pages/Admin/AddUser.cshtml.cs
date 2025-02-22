using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using Lab1.Pages.Data_Classes;
using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab1.Pages.Admin
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public String UserType { get; set; }
    
        public String SelectUser { get; set; }
        [BindProperty] public String FirstName { get; set; }
        [BindProperty] public String LastName { get; set; }
        [BindProperty] public String MiddleInitial { get; set; }
        [BindProperty] public String PhoneNumber { get; set; }
        [BindProperty] public String Email { get; set; }
        public String currentUserID { get; set; }
      


        public void OnGet()
        {
            
        }


        public void OnPost()
        {
            User NewUser = new User();
            NewUser.FirstName = FirstName;
            NewUser.LastName = LastName;
            NewUser.Email = Email;
            NewUser.MiddleInitial = MiddleInitial;
            NewUser.PhoneNumber = PhoneNumber;
            NewUser.UserType = UserType;

            currentUserID = HttpContext.Session.GetString("UserID");
            int UserID = Convert.ToInt32(currentUserID);

            DBClass.AddUser(NewUser,UserID);
        }

        public IActionResult OnPostPopulateHandler()
        {
            ModelState.Clear();

            FirstName = "Luke";
            LastName = "Fisher";
            Email = "fishe4lj@dukes.jmu.edu";
            MiddleInitial = "J";
            PhoneNumber = "1234567890";
            UserType = "employee";

            User NewUser = new User();
            NewUser.FirstName = FirstName;
            NewUser.LastName = LastName;
            NewUser.Email = Email;
            NewUser.MiddleInitial = MiddleInitial;
            NewUser.PhoneNumber = PhoneNumber;
            NewUser.UserType = UserType;

            currentUserID = HttpContext.Session.GetString("UserID");
            int UserID = Convert.ToInt32(currentUserID);

            DBClass.AddUser(NewUser, UserID);

            return Page();
        }

        public IActionResult OnPostClearHandler()
        {
            ModelState.Clear();
            return Page();
        }
    }
}
