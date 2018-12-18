using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.AppMagager
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.03.16
    /// 描 述：移动端功能管理
    /// </summary>
    public class FunctionSerivce: RepositoryFactory
    {
        #region 属性 构造函数
        private string sql;
        public FunctionSerivce()
        {
            sql = @" 
                    t.F_Id,
                    t.F_Type,
                    t.F_FormId,
                    t.F_CodeId,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName,
                    t.F_Icon,
                    t.F_Name,
                    t.F_SchemeId,
                    t.F_EnabledMark,
                    t.F_SortCode,
                    t.F_Url,
                    t.F_IsSystem
                    ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        public IEnumerable<FunctionEntity> GetPageList(Pagination pagination, string keyword, string type)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(sql);
                strSql.Append(" FROM LR_App_Function t where 1=1 ");

                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND ( t.F_Name like @keyword ) ");
                    keyword = "%" + keyword + "%";
                }
                if (!string.IsNullOrEmpty(type))
                {
                    strSql.Append(" AND t.F_Type = @type ");
                }
                return this.BaseRepository().FindList<FunctionEntity>(strSql.ToString(), new {  keyword, type }, pagination);
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
        public IEnumerable<FunctionEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(sql);
                strSql.Append(" ,s.F_Scheme FROM LR_App_Function t LEFT JOIN LR_App_FnScheme s on  t.F_SchemeId = s.F_Id where t.F_EnabledMark = 1 ORDER BY F_SortCode ");

                return this.BaseRepository().FindList<FunctionEntity>(strSql.ToString());
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
        /// 获取移动功能模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FunctionSchemeEntity GetScheme(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FunctionSchemeEntity>(keyValue);
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
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FunctionEntity GetEntity(string keyValue) {
            try
            {
                return this.BaseRepository().FindEntity<FunctionEntity>(keyValue);
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
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                FunctionEntity entity = db.FindEntity<FunctionEntity>(keyValue);
                string schemeId = entity.F_SchemeId;
                if (!string.IsNullOrEmpty(schemeId)) {
                    db.Delete<FunctionSchemeEntity>(t => t.F_Id == schemeId);
                }
                db.Delete<FunctionEntity>(t => t.F_Id == keyValue);

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
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="functionEntity">功能信息</param>
        /// <param name="functionSchemeEntity">功能模板信息</param>
        public void SaveEntity(string keyValue, FunctionEntity functionEntity, FunctionSchemeEntity functionSchemeEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                // 如果是代码开发功能
                if (functionEntity.F_IsSystem == 1)
                {
                    if (!string.IsNullOrEmpty(functionEntity.F_SchemeId))
                    {
                        db.Delete<FunctionSchemeEntity>(t => t.F_Id == functionEntity.F_SchemeId);
                    }
                }
                else
                {
                    #region 模板信息
                    if (string.IsNullOrEmpty(functionEntity.F_SchemeId))
                    {
                        functionSchemeEntity.Create();
                        db.Insert(functionSchemeEntity);
                        functionEntity.F_SchemeId = functionSchemeEntity.F_Id;
                    }
                    else
                    {
                        functionSchemeEntity.Modify(functionEntity.F_SchemeId);
                        db.Update(functionSchemeEntity);
                    }
                    #endregion
                }

                if (string.IsNullOrEmpty(keyValue))
                {
                    functionEntity.Create();
                    db.Insert(functionEntity);
                }
                else
                {
                    functionEntity.Modify(keyValue);
                    db.Update(functionEntity);
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
        /// 更新状态
        /// </summary>
        /// <param name="keyValue">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                FunctionEntity entity = new FunctionEntity
                {
                    F_Id = keyValue,
                    F_EnabledMark = state
                };
                this.BaseRepository().Update(entity);
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
