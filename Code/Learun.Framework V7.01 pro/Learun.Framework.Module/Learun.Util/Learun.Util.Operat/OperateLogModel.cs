namespace Learun.Util.Operat
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：操作日志模型
    /// </summary>
    public class OperateLogModel
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType type { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 功能地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string dataJson { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo userInfo { get; set; }
        /// <summary>
        /// 来源对象主键
        /// </summary>
        public string sourceObjectId { get; set; }
        /// <summary>
        /// 来源日志内容
        /// </summary>
        public string sourceContentJson { get; set; }
    }
}
