
namespace Learun.Application.WeChat
{
    class SendImage : MessageSend
    {
        public override string msgtype
        {
            get { return "image"; }
        }

        [IsNotNull]
        public SendImage.SendItem image { get; set; }

        public class SendItem
        {
            public string media_id { get; set; }
        }
    }
}
