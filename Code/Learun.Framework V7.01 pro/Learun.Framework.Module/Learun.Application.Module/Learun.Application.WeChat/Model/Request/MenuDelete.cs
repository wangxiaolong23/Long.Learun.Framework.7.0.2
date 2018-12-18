
namespace Learun.Application.WeChat
{
    public class MenuDelete : OperationRequestBase<OperationResultsBase, HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN&agentid={0}";
        protected override string Url()
        {
            return string.Format(url, agentid);
        }
        
        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string agentid { private get; set; }
    }
}
