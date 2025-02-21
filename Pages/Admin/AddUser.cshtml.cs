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

    }
}
