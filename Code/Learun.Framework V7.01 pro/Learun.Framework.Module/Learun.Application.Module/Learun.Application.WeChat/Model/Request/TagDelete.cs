using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public class TagDelete : OperationRequestBase<OperationResultsBase, HttpGetRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token=ACCESS_TOKEN&tagid={0}";
        protected override string Url()
        {
            return string.Format(url,tagid);
        }

        /// <summary>
        /// 标签ID
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string tagid { get; set; }
    }
}
