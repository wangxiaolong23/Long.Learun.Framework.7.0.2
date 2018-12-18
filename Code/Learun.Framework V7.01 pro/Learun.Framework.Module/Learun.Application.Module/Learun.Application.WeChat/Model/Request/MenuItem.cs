using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    public class MenuItem
    {
        /// <summary>
        /// 二级菜单数组，个数应为1~5个
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> sub_button { get; set; }

        /// <summary>
        /// 菜单的响应动作类型，目前有click、view两种类型
        /// </summary>
        /// <returns></returns>
        public string type { get; set; }

        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }

        /// <summary>
        /// click类型必须
        /// 菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>
        /// <returns></returns>
        public string key { get; set; }

        /// <summary>
        /// view类型必须
        /// 网页链接，员工点击菜单可打开链接，不超过256字节
        /// </summary>
        /// <returns></returns>
        public string url { get; set; }
    }
}
