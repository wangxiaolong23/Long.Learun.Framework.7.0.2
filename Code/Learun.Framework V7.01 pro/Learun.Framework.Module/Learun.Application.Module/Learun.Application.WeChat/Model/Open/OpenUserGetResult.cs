using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    public class OpenUserGetResult : OperationResultsBase
    {
        /// <summary>
        /// 普通用户的标识，对当前开发者帐号唯一
        /// </summary>
        /// <returns></returns>
        public string openid { get; set; }

        /// <summary>
        /// 普通用户昵称
        /// </summary>
        /// <returns></returns>
        public string nickname { get; set; }

        /// <summary>
        /// 普通用户性别，1为男性，2为女性
        /// </summary>
        /// <returns></returns>
        public string sex { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        /// <returns></returns>
        public string province { get; set; }

        /// <summary>
        /// 城市 
        /// </summary>
        /// <returns></returns>
        public string city { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        /// <returns></returns>
        public string country { get; set; }

        /// <summary>
        /// 头像URL
        /// </summary>
        /// <returns></returns>
        public string headimgurl { get; set; }

        /// <summary>
        /// 用户统一标识。针对一个微信开放平台帐号下的应用，同一用户的unionid是唯一的
        /// </summary>
        /// <returns></returns>
        public string unionid { get; set; }
    }
}
