using Learun.Util;

namespace Learun.Application.WebApi
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class ReqPageParam
    {
        public Pagination pagination { get; set; }
        public string queryJson { get; set; }
    }
}