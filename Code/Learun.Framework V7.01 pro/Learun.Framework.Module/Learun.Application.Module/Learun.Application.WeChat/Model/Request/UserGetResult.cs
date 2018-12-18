using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    public class UserGetResult : OperationResultsBase
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        /// <returns></returns>
        public string userid { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }

        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        /// <returns></returns>
        public List<int> department { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        /// <returns></returns>
        public string position { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        /// <returns></returns>
        public string mobile { get; set; }

        /// <summary>
        /// 性别。gender=0表示男，=1表示女
        /// </summary>
        /// <returns></returns>
        public string gender { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        /// <returns></returns>
        public string tel { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        /// <returns></returns>
        public string email { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        /// <returns></returns>
        public string weixinid { get; set; }

        /// <summary>
        /// 头像url。注：如果要获取小图将url最后的"/0"改成"/64"即可
        /// </summary>
        /// <returns></returns>
        public string avatar { get; set; }

        /// <summary>
        /// 关注状态: 1=已关注，2=已冻结，4=未关注
        /// </summary>
        /// <returns></returns>
        public int status { get; set; }
    }
}
