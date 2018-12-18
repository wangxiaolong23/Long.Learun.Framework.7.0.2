
namespace Learun.Application.WeChat
{
    /// <summary>
    /// 若部分userid非法，则返回
    /// {
    ///     "errcode": 0,
    ///     "errmsg": "invalid userlist failed"
    ///     "invalidlist"："usr1|usr2|usr"
    /// }
    /// 
    /// 当包含userid全部非法时返回
    /// {
    ///     "errcode": 40070,
    ///     "errmsg": "all list invalid "
    /// }
    /// </summary>
    public class TagAddtagusersResult : OperationResultsBase
    {
        /// <summary>
        /// 不在权限内的员工ID列表，以“|”分隔
        /// </summary>
        /// <returns></returns>
        public string invalidlist { get; set; }
    }
}
