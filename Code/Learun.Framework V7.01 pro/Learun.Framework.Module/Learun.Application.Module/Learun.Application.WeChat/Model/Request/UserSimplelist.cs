using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    /// <summary>
    /// 获取部门成员
    /// </summary>
    public class UserSimplelist : OperationRequestBase<UserSimplelistResult,HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token=ACCESS_TOKEN&department_id={0}&fetch_child={1}&status={2}";
        protected override string Url()
        {
            return string.Format(url, department_id, fetch_child, (int)status);
        }

        /// <summary>
        /// 获取的部门id
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string department_id { get; set; }

        /// <summary>
        /// 1/0：是否递归获取子部门下面的成员
        /// </summary>
        /// <returns></returns>
        public int fetch_child { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public UserStatus status { get; set; }

        public enum UserStatus
        {
            /// <summary>
            /// 获取全部
            /// </summary>
            All = 0,

            /// <summary>
            /// 已关注
            /// </summary>
            Concerned = 1,

            /// <summary>
            /// 禁用
            /// </summary>
            Forbidden = 2,

            /// <summary>
            /// 未关注
            /// </summary>
            NoConcerned = 4

        }
    }
}
