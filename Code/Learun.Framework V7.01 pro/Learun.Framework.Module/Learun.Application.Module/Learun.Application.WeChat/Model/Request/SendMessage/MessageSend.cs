using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public abstract class MessageSend : OperationRequestBase<MessageSendResult, HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送
        /// </summary>
        /// <returns></returns>
        public string touser { get; set; }

        /// <summary>
        /// PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数
        /// </summary>
        /// <returns></returns>
        public string toparty { get; set; }

        /// <summary>
        /// TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数
        /// </summary>
        /// <returns></returns>
        public string totag { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public abstract string msgtype { get; }

        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string agentid { get; set; }

        public string safe { get; set; }
    }
}
