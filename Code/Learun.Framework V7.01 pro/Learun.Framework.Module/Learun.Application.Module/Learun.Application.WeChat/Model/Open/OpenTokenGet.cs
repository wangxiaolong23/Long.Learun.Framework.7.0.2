using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public class OpenTokenGet : OperationRequestBase<OpenTokenGetResult, HttpGetRequest>
    {
        private string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        protected override string Url()
        {
            return string.Format(url, appid, secret,code);
        }

        /// <summary>
        /// 应用唯一标识，在微信开放平台提交应用审核通过后获得
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string appid { get; set; }
        /// <summary>
        /// 应用密钥AppSecret，在微信开放平台提交应用审核通过后获得
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string secret { get; set; }
        /// 填写第一步获取的code参数
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string code { get; set; }
    }
}
