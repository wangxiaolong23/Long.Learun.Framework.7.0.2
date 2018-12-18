using System.Web;

namespace Learun.Application.WeChat
{
    /// <summary>
    /// 企业获取code
    /// 企业如果需要员工在跳转到企业网页时带上员工的身份信息
    /// #wechat_redirect	 是	 微信终端使用此参数判断是否需要带上身份信息
    /// </summary>
    public class Oauth2Authorize
    {
        private string url =
            "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect";
        public string GetAuthorizeUrl()
        {
            return string.Format(url,
                HttpUtility.UrlEncode(appid),
                HttpUtility.UrlEncode(redirect_uri),
                HttpUtility.UrlEncode(response_type),
                HttpUtility.UrlEncode(scope),
                HttpUtility.UrlEncode(state));
        }

        /// <summary>
        /// 企业的CorpID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string appid { get; set; }

        /// <summary>
        /// 授权后重定向的回调链接地址，请使用urlencode对链接进行处理
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string redirect_uri { get; set; }

        /// <summary>
        /// 返回类型，此时固定为：code
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string response_type { get { return "code"; } }

        /// <summary>
        /// 应用授权作用域，此时固定为：snsapi_base
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string scope { get { return "snsapi_base"; } }

        /// <summary>
        /// 重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值
        /// </summary>
        /// <returns></returns>
        public string state { get; set; }
    }
}
