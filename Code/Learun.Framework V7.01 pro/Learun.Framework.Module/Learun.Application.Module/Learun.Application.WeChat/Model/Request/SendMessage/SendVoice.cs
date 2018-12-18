
namespace Learun.Application.WeChat
{
    class SendVoice : MessageSend
    {
        public override string msgtype
        {
            get { return "voice"; }
        }

        [IsNotNull]
        public SendVoice.SendItem voice { get; set; }

        public class SendItem
        {
            public string media_id { get; set; }
        }
    }
}
