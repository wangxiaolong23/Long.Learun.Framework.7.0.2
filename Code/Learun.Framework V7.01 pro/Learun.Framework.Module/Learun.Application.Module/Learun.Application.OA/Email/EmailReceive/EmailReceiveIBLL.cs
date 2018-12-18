using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.OA.Email.EmailReceive
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件接收管理
    /// </summary>
    public interface EmailReceiveIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取收取邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        IEnumerable<EmailReceiveEntity> GetReceiveList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取邮件接收实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        EmailReceiveEntity GetReceiveEntity(string keyValue);
        /// <summary>
        /// 获取邮件接收个数
        /// </summary>
        /// <returns></returns>
        int GetCount();
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="receiveEntity">邮件接收实体</param>
        /// <returns></returns>
        void SaveReceiveEntity(string keyValue, EmailReceiveEntity receiveEntity);
        #endregion
    }
}