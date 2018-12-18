
namespace Learun.Application.WeChat
{
    class SendFile : MessageSend
    {
        public override string msgtype
        {
            get { return "file"; }
        }

        [IsNotNull]
        public SendFile.SendItem file { get; set; }

        public class SendItem
        {
            public string media_id { get; set; }
        }
    }
}
