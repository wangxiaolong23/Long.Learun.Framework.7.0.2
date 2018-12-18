namespace Learun.Application.WebApi
{
    /// <summary>
    /// 提交实体数据
    /// </summary>
    public class ReqEntity<T> where T : class
    {
        public string keyValue { get; set; }
        public T entity { get; set; }
    }
}