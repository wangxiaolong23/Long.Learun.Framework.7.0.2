using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    internal class SendMpnews : MessageSend
    {
        public override string msgtype
        {
            get { return "mpnews"; }
        }

        [IsNotNull]
        public SendMpnews.SendItemLoist mpnews { get; set; }

        public class SendItemLoist
        {
            public List<SendMpnews.SendItem> articles { get; set; }
        }


        public class SendItem
        {
            /// <summary>
            /// 图文消息缩略图的media_id, 可以在上传多媒体文件接口中获得。此处thumb_media_id即上传接口返回的media_id
            /// </summary>
            /// <returns></returns>
            public string thumb_media_id { get; set; }

            /// <summary>
            /// 图文消息的标题
            /// </summary>
            /// <returns></returns>
            public string title { get; set; }

            /// <summary>
            /// 图文消息的作者
            /// </summary>
            /// <returns></returns>
            public string author { get; set; }

            /// <summary>
            /// 图文消息点击“阅读原文”之后的页面链接
            /// </summary>
            /// <returns></returns>
            public string content_source_url { get; set; }

            /// <summary>
            /// 图文消息的内容，支持html标签
            /// </summary>
            /// <returns></returns>
            public string content { get; set; }

            /// <summary>
            /// 图文消息的描述
            /// </summary>
            /// <returns></returns>
            public string digest { get; set; }

            /// <summary>
            /// 是否显示封面，1为显示，0为不显示
            /// </summary>
            /// <returns></returns>
            public string show_cover_pic { get; set; }
        }
    }
}
