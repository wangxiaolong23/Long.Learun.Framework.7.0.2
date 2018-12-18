using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    public class OpenTokenGetResult : OperationResultsBase
    {
        /// <summary>
        /// 接口调用凭证
        /// </summary>
        /// <returns></returns>
        public string access_token { get; set; }

        /// <summary>
        /// access_token接口调用凭证超时时间，单位（秒）
        /// </summary>
        /// <returns></returns>
        public string expires_in { get; set; }

        /// <summary>
        /// 用户刷新access_token
        /// </summary>
        /// <returns></returns>
        public string refresh_token { get; set; }

        /// <summary>
        /// 授权用户唯一标识
        /// </summary>
        /// <returns></returns>
        public string openid { get; set; }

        /// <summary>
        /// 用户授权的作用域，使用逗号（,）分隔 
        /// </summary>
        /// <returns></returns>
        public string scope { get; set; }

        /// <summary>
        /// 当且仅当该网站应用已获得该用户的userinfo授权时，才会出现该字段。
        /// </summary>
        /// <returns></returns>
        public string unionid { get; set; }
    }
}
