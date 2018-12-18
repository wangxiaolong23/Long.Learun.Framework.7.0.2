
namespace Learun.Application.WeChat
{
    class TagCreate : OperationRequestBase<TagCreateResult,HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名
        /// </summary>
        /// <returns></returns>
        [Length(1,64)]
        [IsNotNull]
        public string tagname { get; set; }
    }
}
