
namespace Learun.Application.WeChat
{
    public class DepartmentUpdate : OperationRequestBase<OperationResultsBase,HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token=ACCESS_TOKEN";
        }

        /// <summary>
        /// 部门id
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string id { get; set; }

        /// <summary>
        /// 更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数
        /// </summary>
        /// <returns></returns>
        [Length(0,64)]
        public string name { get; set; }

        /// <summary>
        /// 父亲部门id。根部门id为1
        /// </summary>
        /// <returns></returns>
        public string parentid { get; set; }

        /// <summary>
        /// 在父部门中的次序。从1开始，数字越大排序越靠后
        /// </summary>
        /// <returns></returns>
        public string order { get; set; }
    }
}
