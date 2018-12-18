using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Excel
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：Excel数据导出设置
    /// </summary>
    public class ExcelExportService : RepositoryFactory
    {
        #region 构造函数和属性
 
        private string fieldSql;
        public ExcelExportService()
        {
            fieldSql= @"
                t.F_Id,
                t.F_Name,
                t.F_GridId,
                t.F_ModuleId,
                t.F_ModuleBtnId,
                t.F_BtnName,
                t.F_EnabledMark,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
            ";
        }
        #endregion
 
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
                var queryParam = queryJson.ToJObject();
                string keyword = "";
                string moduleId = "";

                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Excel_Export t where 1=1 ");
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                    strSql.Append(" AND t.F_Name like @keyword ");
                }
                if (!queryParam["moduleId"].IsEmpty())
                {
                    moduleId = queryParam["moduleId"].ToString();
                    strSql.Append(" AND t.F_ModuleId = @moduleId ");
                }


                return this.BaseRepository().FindList<ExcelExportEntity>(strSql.ToString(), new { keyword = keyword, moduleId = moduleId }, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Excel_Export t where t.F_ModuleId = @moduleId AND t.F_EnabledMark = 1 ");
                return this.BaseRepository().FindList<ExcelExportEntity>(strSql.ToString(), new {moduleId = moduleId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                return this.BaseRepository().FindEntity<ExcelExportEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                this.BaseRepository().Delete<ExcelExportEntity>(t=>t.F_Id == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
 
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ExcelExportEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
 
        #endregion
    }
}
