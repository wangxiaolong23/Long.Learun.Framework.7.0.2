using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.OA.Email.EmailSend
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件发送管理
    /// </summary>
    public interface EmailSendIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取发送邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        IEnumerable<EmailSendEntity> GetSendList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取邮件发送实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        EmailSendEntity GetSendEntity(string keyValue);

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
        /// <param name="sendEntity">邮件发送实体</param>
        /// <returns></returns>
        void SaveSendEntity(string keyValue, EmailSendEntity sendEntity);
        #endregion
    }
}