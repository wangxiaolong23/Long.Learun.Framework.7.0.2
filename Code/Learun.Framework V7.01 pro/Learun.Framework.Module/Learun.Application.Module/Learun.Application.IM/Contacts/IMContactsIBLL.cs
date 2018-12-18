using System;
using System.Collections.Generic;

namespace Learun.Application.IM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.05.31
    /// 描 述：最近联系人列表
    /// </summary>
    public interface IMContactsIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<IMContactsEntity> GetList(string userId);
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        IEnumerable<IMContactsEntity> GetList(string userId, DateTime time);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userId">发送人</param>
        /// <param name="otherUserId">接收人</param>
        /// <returns></returns>
        IMContactsEntity GetEntity(string userId, string otherUserId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="entity">实体数据</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(IMContactsEntity entity);
        /// <summary>
        /// 更新记录读取状态
        /// </summary>
        /// <param name="myUserId">自己本身用户ID</param>
        /// <param name="otherUserId">对方用户ID</param>
        void UpdateState(string myUserId, string otherUserId);
        #endregion
    }
}
