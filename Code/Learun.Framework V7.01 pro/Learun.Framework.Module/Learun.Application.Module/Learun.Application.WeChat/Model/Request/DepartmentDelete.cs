
namespace Learun.Application.WeChat
{
    public class DepartmentDelete : OperationRequestBase<OperationResultsBase,HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token=ACCESS_TOKEN&id={0}";
        protected override string Url()
        {
            return string.Format(url, id);
        }

        /// <summary>
        /// 部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string id { get; set; }
    }
}
