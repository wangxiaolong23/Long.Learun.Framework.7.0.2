using System.Collections.Generic;

namespace Learun.Application.WeChat.WeChat
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-09-22 12:01
    /// 描 述：获取微信成员返回实体类
    /// </summary>
    public class GetUserEntity
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
        /// 成员UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string english_name { get; set; }
        /// <summary>
        /// 手机号码。企业内必须唯一，mobile/email二者不能同时为空
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 成员所属部门id列表,不超过20个
        /// </summary>
        public List<int> department { get; set; }
        /// <summary>
        /// 部门内的排序值，默认为0，成员次序以创建时间从小到大排列。数量必须和department一致，数值越大排序越前面。有效的值范围是[0, 2^32)
        /// </summary>
        public List<int> order { get; set; }
        /// <summary>
        /// 职位信息
        /// </summary>
        public string position { get; set; }
        /// <summary>
        /// 性别。1表示男性，2表示女性
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 邮箱。长度为0~64个字节。企业内必须唯一，mobile/email二者不能同时为空
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public int isleader { get; set; }
        /// <summary>
        /// 成员头像的mediaid
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 启用/禁用成员。1表示启用成员，0表示禁用成员
        /// </summary>
        public int enable { get; set; }
        /// <summary>
        /// 激活状态: 1=已激活，2=已禁用，4=未激活
        /// </summary>
        public int status { get; set; }
    }
}
