using System.Net.Mail;

namespace project_new.Models
{
    public class EmailEntity
    {
        public string FromEmailAddress { get; set; }
        public string ToEmailAddress { get; set; }
        public string Subject { get; set; }
        public string EmailBodyMessage { get; set; }
        public Attachment Attachment { get; internal set; }
    }
}
