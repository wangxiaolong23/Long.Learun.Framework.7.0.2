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
    /// 描 述：Excel数据导入设置
    /// </summary>
    public class ExcelImportService : RepositoryFactory
    {
        #region 构造函数和属性
 
        private string fieldSql;
        private string fieldSql2;

        public ExcelImportService()
        {
            fieldSql= @"
                t.F_Id,
                t.F_Name,
                t.F_ModuleId,
                t.F_ModuleBtnId,
                t.F_BtnName,
                t.F_DbId,
                t.F_DbTable,
                t.F_ErrorType,
                t.F_EnabledMark,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
            ";
            fieldSql2 = @"
                t.F_Id,
                t.F_ImportId,
                t.F_Name,
                t.F_ColName,
                t.F_OnlyOne,
                t.F_RelationType,
                t.F_DataItemCode,
                t.F_Value,
                t.F_DataSourceId,
                t.F_SortCode
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件参数</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                string keyword = "";
                string moduleId = "";

                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Excel_Import t where 1=1 ");

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
                return this.BaseRepository().FindList<ExcelImportEntity>(strSql.ToString(), new { keyword = keyword, moduleId = moduleId }, pagination);
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
        /// 获取导入配置列表根据模块ID
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportEntity> GetList(string moduleId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Excel_Import t where t.F_ModuleId = @moduleId AND t.F_EnabledMark = 1 ");

                return this.BaseRepository().FindList<ExcelImportEntity>(strSql.ToString(), new { moduleId = moduleId });
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
        /// 获取配置信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ExcelImportEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<ExcelImportEntity>(keyValue);
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
        /// 获取配置字段列表
        /// </summary>
        /// <param name="importId">配置信息主键</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportFieldEntity> GetFieldList(string importId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql2);
                strSql.Append(" FROM LR_Excel_ImportFileds t where t.F_ImportId=@importId ORDER By  t.F_SortCode ");

                return this.BaseRepository().FindList<ExcelImportFieldEntity>(strSql.ToString(), new { importId = importId });
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
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {

            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<ExcelImportEntity>(t => t.F_Id == keyValue);
                db.Delete<ExcelImportFieldEntity>(t => t.F_ImportId == keyValue);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ExcelImportEntity entity, List<ExcelImportFieldEntity> filedList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                }
                string importId = entity.F_Id;
                db.Delete<ExcelImportFieldEntity>(t => t.F_ImportId == importId);
                foreach (var item in filedList)
                {
                    item.F_ImportId = importId;
                    item.Create();
                    db.Insert(item);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        /// 更新配置主表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void UpdateEntity(string keyValue, ExcelImportEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
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
