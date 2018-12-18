using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Excel
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：Excel数据导出设置
    /// </summary>
    public class ExcelExportBLL : ExcelExportIBLL
    {
        private ExcelExportService excelExportService = new ExcelExportService();

        #region 获取数据
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<ExcelExportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return excelExportService.GetPageList(pagination, queryJson);
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
        /// 获取列表
        /// <param name="moduleId">功能模块主键</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<ExcelExportEntity> GetList(string moduleId)
        {
            try
            {
                return excelExportService.GetList(moduleId);
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
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public ExcelExportEntity GetEntity(string keyValue)
        {
            try
            {
                return excelExportService.GetEntity(keyValue);
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
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                excelExportService.DeleteEntity(keyValue);
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
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <param name="entity">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ExcelExportEntity entity)
        {
            try
            {
                excelExportService.SaveEntity(keyValue, entity);
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
