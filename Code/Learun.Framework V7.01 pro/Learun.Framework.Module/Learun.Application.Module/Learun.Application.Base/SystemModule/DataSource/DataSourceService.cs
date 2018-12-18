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
    /// 描 述：数据源
    /// </summary>
    public class DataSourceService : RepositoryFactory
    {
        #region 属性 构造函数
        private string fieldSql;
        public DataSourceService()
        {
            fieldSql = @" 
                    t.F_Id,
                    t.F_Code,
                    t.F_Name,
                    t.F_DbId,
                    t.F_Description,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_CreateDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName,
                    t.F_ModifyDate
                    ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_DataSource t ");
                strSql.Append(" WHERE 1=1 ");

                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                    keyword = "%" + keyword + "%";
                }
                return this.BaseRepository().FindList<DataSourceEntity>(strSql.ToString(), new { keyword = keyword }, pagination);
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
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataSourceEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_DataSource t ");
                return this.BaseRepository().FindList<DataSourceEntity>(strSql.ToString());
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
        /// <param name="code">编码</param>
        /// <returns></returns>
        public DataSourceEntity GetEntityByCode(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<DataSourceEntity>(t => t.F_Code.Equals(code));
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
        public DataSourceEntity GetEntity(string keyValue) {
            try
            {
                return this.BaseRepository().FindEntity<DataSourceEntity>(keyValue);
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
        /// 删除数据源
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                DataSourceEntity entity = new DataSourceEntity()
                {
                    F_Id = keyValue,
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
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataSourceEntity">数据源实体</param>
        /// <returns></returns>
        public bool SaveEntity(string keyValue, DataSourceEntity dataSourceEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    DataSourceEntity entity = this.BaseRepository().FindEntity<DataSourceEntity>(t => t.F_Code.Equals(dataSourceEntity.F_Code) && t.F_Id != keyValue);
                    if (entity != null)
                    {
                        return false;
                    }
                    dataSourceEntity.Modify(keyValue);
                    this.BaseRepository().Update(dataSourceEntity);
                }
                else
                {
                    DataSourceEntity entity = GetEntityByCode(dataSourceEntity.F_Code);
                    if (entity != null) {
                        return false;
                    }
                    dataSourceEntity.Create();
                    this.BaseRepository().Insert(dataSourceEntity);
                }
                return true;
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
