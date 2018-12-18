using System.Collections.Generic;

namespace Learun.Application.WeChat.WeChat
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-09-22 12:01
    /// 描 述：获取部门成员返回类
    /// </summary>
    public class UserListEntity
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 错误内容
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<SimpleUserEntity> userlist { get; set; }
    }

    public class SimpleUserEntity
    {
        /// <summary>
        /// 用户userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 成员所属部门
        /// </summary>
        public List<int> department { get; set; }
    }
}
