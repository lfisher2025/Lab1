using System.ComponentModel.DataAnnotations;

namespace Lab1.Pages.Data_Classes
{
    public class Message
    {
        [Required]
        public int messageID { get; set; }
        [Required]
        public int senderID { get; set; }
        [Required]
        public int recipientID { get; set; }
        [Required]
        public String content { get; set; }
        [Required]
        public String readStatus { get; set; }
        [Required]
        public DateTime DATETIME { get; set; }
    }
}
