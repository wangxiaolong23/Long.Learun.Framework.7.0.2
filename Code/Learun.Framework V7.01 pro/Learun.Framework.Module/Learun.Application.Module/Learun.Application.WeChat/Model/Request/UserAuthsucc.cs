namespace Learun.Application.WeChat
{
    public class UserAuthsucc : OperationRequestBase<OperationResultsBase, HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/user/authsucc?access_token=ACCESS_TOKEN&userid={0}";
        protected override string Url()
        {
            return string.Format(url, userid);
        }

        /// <summary>
        /// 员工UserID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string userid { get; set; }
    }
}
