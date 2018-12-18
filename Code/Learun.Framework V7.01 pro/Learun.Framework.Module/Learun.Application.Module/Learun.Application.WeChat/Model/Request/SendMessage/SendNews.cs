using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    public class SendNews : MessageSend
    {
        public override string msgtype
        {
            get { return "news"; }
        }

        [IsNotNull]
        public SendNews.SendItemLoist news { get; set; }

        public class SendItemLoist
        {
            public List<SendNews.SendItem> articles { get; set; }
        }


        public class SendItem
        {
            /// <summary>
            /// 标题
            /// </summary>
            /// <returns></returns>
            public string title { get; set; }

            /// <summary>
            /// 描述
            /// </summary>
            /// <returns></returns>
            public string description { get; set; }

            /// <summary>
            /// 点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份。具体参考“9 微信页面跳转员工身份查询”
            /// </summary>
            /// <returns></returns>
            public string url { get; set; }

            /// <summary>
            /// 图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片
            /// </summary>
            /// <returns></returns>
            public string picurl { get; set; }
        }
    }
}
