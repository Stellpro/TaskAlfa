using System.Collections.Generic;
using System.Net.Mail;

namespace Task.Mail
{
    public class MailData
    {
        public MailAddress FromEmail { get; set; }
        public MailAddress ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<Attachment> AttachmentData { get; set; } = new List<Attachment>();
        public MailAddress BccEmail { get; set; }
    }
}
