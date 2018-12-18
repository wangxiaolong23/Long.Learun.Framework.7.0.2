using System;
using System.Collections.Generic;

namespace Learun.Util
{
    public class MailModel
    {
        public string UID { get; set; }
        public string To { get; set; }
        public string ToName { get; set; }
        public string CC { get; set; }
        public string CCName { get; set; }
        public string Bcc { get; set; }
        public string BccName { get; set; }
        public string Subject { get; set; }
        public string BodyText { get; set; }
        public List<MailFile> Attachment { get; set; }
        public DateTime Date { get; set; }
    }
}
