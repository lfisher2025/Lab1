using Lab1.Pages.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab1.Pages.Data_Classes;

namespace Lab1.Pages.Messaging
{
    public class InboxModel : PageModel 
    {
        [BindProperty]
        public string Content { get; set; }

        public void OnGet(int UserID)
        {
            //Get the User ID of current user and display their messages. 
            System.Data.SqlClient.SqlDataReader MessagesResult = DBClass.ViewUserMessages(UserID);

            while (MessagesResult.Read())
            {
                Message message = new Message();

                message.content = MessagesResult.GetString(0);

               //Trying to loop through to print each message that is retrieved from the database.
            }
        }

    }
}
