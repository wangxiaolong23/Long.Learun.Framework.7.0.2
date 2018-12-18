
namespace Learun.Util
{
    /// <summary>
    /// api请求数据结构
    /// </summary>
    /// <typeparam name="T">数据结构</typeparam>
    public class ReqParameter<T> where T : class
    {
        /// <summary>
        /// 接口票据
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 登录设备标识
        /// </summary>
        public string loginMark { get; set; }
        /// <summary>
        /// 接口数据
        /// </summary>
        public T data { get; set; }
    }
    /// <summary>
    /// 请求数据结构
    /// </summary>
    public class ReqParameter {
        /// <summary>
        /// 接口票据
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 登录设备标识
        /// </summary>
        public string loginMark { get; set; }
    }
}
