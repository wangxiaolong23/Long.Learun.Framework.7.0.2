
namespace Learun.Application.WeChat
{
    public class TagUpdate : OperationRequestBase<OperationResultsBase,HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token=ACCESS_TOKEN";
        }

        [IsNotNull]
        public string tagid { get; set; }

        [IsNotNull]
        [Length(1,64)]
        public string tagname { get; set; }
    }
}
