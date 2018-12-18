
namespace Learun.Application.WeChat
{

    /// <summary>
    /// 暂未测试通过
    /// 一直返回{"errcode":40068,"errmsg":"invalid tagid"}
    /// </summary>
    public class TagGet : OperationRequestBase<OperationResultsBase, HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token=ACCESS_TOKEN&tagid={0}";
        protected override string Url()
        {
            return string.Format(url, tagid);
        }

        /// <summary>
        /// 标签ID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string tagid { get; set; }
    }
}
