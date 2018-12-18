using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.OA.Gantt
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.20
    /// 描 述：项目计划
    /// </summary>
    public class JQueryGanttBLL : JQueryGanttIBLL
    {
        private JQueryGanttService jQueryGanttService = new JQueryGanttService();

        #region 获取数据
        /// <summary>
        /// 获取甘特图数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public IEnumerable<JQueryGanttEntity> GetList(string queryJson)
        {
            try
            {
                return jQueryGanttService.GetList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取甘特图实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public JQueryGanttEntity GetEntity(string keyValue)
        {
            try
            {
                return jQueryGanttService.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                jQueryGanttService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">邮件发送实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, JQueryGanttEntity entity)
        {
            try
            {
                jQueryGanttService.SaveEntity(keyValue, entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion
    }
}