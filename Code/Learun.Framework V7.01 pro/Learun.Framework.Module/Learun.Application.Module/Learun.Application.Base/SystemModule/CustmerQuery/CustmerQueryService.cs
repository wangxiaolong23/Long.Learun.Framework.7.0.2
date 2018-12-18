using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：自定义查询
    /// </summary>
    public class CustmerQueryService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public CustmerQueryService()
        {
            fieldSql = @"
                    t.F_CustmerQueryId,
                    t.F_Name,
                    t.F_UserId,
                    t.F_ModuleId,
                    t.F_ModuleUrl,
                    t.F_Formula,
                    t.F_QueryJson
                    ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取自定义查询（公共）分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<CustmerQueryEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" , m.F_FullName as ModuleName ");
                strSql.Append(" FROM LR_Base_CustmerQuery t ");
                strSql.Append(" LEFT JOIN LR_Base_Module m ON m.F_ModuleId = t.F_ModuleId ");
                strSql.Append(" WHERE F_UserId is NULL ");
                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND ( m.F_FullName like @keyword OR t.F_Name like @keyword ) ");
                    keyword = "%" + keyword + "%";
                }
                return this.BaseRepository().FindList<CustmerQueryEntity>(strSql.ToString(), new { keyword = keyword }, pagination);
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
        /// 获取自定义查询条件（公共）
        /// </summary>
        /// <param name="moduleUrl">访问的功能链接地址</param>
        /// <param name="userId">用户ID（用户ID为null表示公共）</param>
        /// <returns></returns>
        public IEnumerable<CustmerQueryEntity> GetList(string moduleUrl, string userId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_CustmerQuery t WHERE  F_ModuleUrl = @moduleUrl");
                if (string.IsNullOrEmpty(userId))
                {
                    strSql.Append(" AND F_UserId is NULL");
                }
                else
                {
                    strSql.Append(" AND F_UserId  = @userId ");
                }

                return this.BaseRepository().FindList<CustmerQueryEntity>(strSql.ToString(), new { moduleUrl = moduleUrl, userId = userId });
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public CustmerQueryEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<CustmerQueryEntity>(keyValue);
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
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                CustmerQueryEntity entity = new CustmerQueryEntity()
                {
                    F_CustmerQueryId = keyValue,
                };
                this.BaseRepository().Delete(entity);
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
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CustmerQueryEntity custmerQueryEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    custmerQueryEntity.Modify(keyValue);
                    this.BaseRepository().Update(custmerQueryEntity);
                }
                else
                {
                    custmerQueryEntity.Create();
                    this.BaseRepository().Insert(custmerQueryEntity);
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
