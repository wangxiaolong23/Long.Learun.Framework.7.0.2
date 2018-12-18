
namespace Learun.Application.WeChat
{
   public class SendText : MessageSend
    {
        public override string msgtype
        {
            get { return "text"; }
        }

        [IsNotNull]
        public SendText.SendItem text { get; set; }

        public class SendItem
        {
            /// <summary>
            /// 消息内容
            /// </summary>
            /// <returns></returns>
            [IsNotNull]
            public string content { get; set; }
        }
    } 
}
