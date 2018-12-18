using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public class MenuCreate : OperationRequestBase<OperationResultsBase, HttpPostRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN&agentid={0}";
        protected override string Url()
        {
            return string.Format(url, agentid);
        }

        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string agentid { private get; set; }

        /// <summary>
        /// 一级菜单数组，个数应为1~3个
        /// </summary>
        /// <returns></returns>
        public List<MenuItem> button { get; set; }
    }
}
