using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public class UserGet : OperationRequestBase<UserGetResult,HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=ACCESS_TOKEN&userid={0}";
        protected override string Url()
        {
            return string.Format(url, userid);
        }

        /// <summary>
        /// 员工UserID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string userid { get; set; }

    }
}
