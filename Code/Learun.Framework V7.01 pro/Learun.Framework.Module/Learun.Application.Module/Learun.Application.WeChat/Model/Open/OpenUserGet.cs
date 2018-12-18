using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public class OpenUserGet : OperationRequestBase<OpenUserGetResult, HttpGetRequest>
    {
        private string url = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}lang=zh_CN";
        protected override string Url()
        {
            return string.Format(url, access_token, openid);
        }

        /// <summary>
        /// 普通用户标识，对该公众帐号唯一
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string openid { get; set; }
        /// <summary>
        /// token
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string access_token { get; set; }
    }
}
