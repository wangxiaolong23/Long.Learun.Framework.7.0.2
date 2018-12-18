using System.Collections.Generic;

namespace Learun.Application.WeChat
{
    public class DepartmentListResult :OperationResultsBase
    {
        /// <summary>
        /// 部门列表数据。以部门的order字段从小到大排列
        /// </summary>
        /// <returns></returns>
        public List<DepartmentItem> department { get; set; }


        public class DepartmentItem
        {
            /// <summary>
            /// 部门id
            /// </summary>
            /// <returns></returns>
            public string id { get; set; }

            /// <summary>
            /// 部门名称
            /// </summary>
            /// <returns></returns>
            public string name { get; set; }

            /// <summary>
            /// 父亲部门id。根部门为1
            /// </summary>
            /// <returns></returns>
            public string parentid { get; set; }
        } 
    }
}
