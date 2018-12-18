using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    /// <summary>
    /// 获取部门成员接口返回结果
    /// </summary>
    public class UserSimplelistResult : OperationResultsBase
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        /// <returns></returns>
        public List<UserSimplelistItem> userlist { get; set; }

        public class UserSimplelistItem
        {
            /// <summary>
            /// 员工UserID。对应管理端的帐号
            /// </summary>
            /// <returns></returns>
            public string userid { get; set; }

            /// <summary>
            /// 成员名称
            /// </summary>
            /// <returns></returns>
            public string name { get; set; }
        }
    }
}
