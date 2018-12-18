
namespace Learun.Application.WeChat
{
    public class UserGetuserinfo : OperationRequestBase<UserGetuserinfoResult, HttpGetRequest>
    {
        private string url =
            "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=ACCESS_TOKEN&code={0}&agentid={1}";

        protected override string Url()
        {
            return string.Format(url, code, agentid);
        }

        /// <summary>
        /// 通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string code { get; set; }

        /// <summary>
        /// 跳转链接时所在的企业应用ID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string agentid { get; set; }
    }
}
