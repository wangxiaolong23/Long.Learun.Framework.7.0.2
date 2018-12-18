
namespace Learun.Application.WeChat
{
    /// <summary>
    /// 邀请用户关注
    /// </summary>
    public class UserInvite : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 员工UserID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string userid { get; set; }

    }
}
