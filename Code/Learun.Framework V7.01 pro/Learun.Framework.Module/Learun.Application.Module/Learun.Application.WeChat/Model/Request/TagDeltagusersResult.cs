using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.WeChat
{
    public class TagDeltagusersResult : OperationResultsBase
    {
        /// <summary>
        /// 不在权限内的员工ID列表，以“|”分隔
        /// </summary>
        /// <returns></returns>
        public string invalidlist { get; set; }
    }
}
