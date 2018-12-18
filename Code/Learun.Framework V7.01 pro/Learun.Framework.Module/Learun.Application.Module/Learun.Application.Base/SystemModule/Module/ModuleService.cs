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
    /// 描 述：功能模块
    /// </summary>
    public class ModuleService : RepositoryFactory
    {
        #region 属性 构造函数
        private string fieldSql;
        private string btnfieldSql;
        private string colfieldSql;
        private string formfieldSql;

        public ModuleService()
        {
            fieldSql = @" 
                    t.F_ModuleId,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_Icon,
                    t.F_UrlAddress,
                    t.F_Target,
                    t.F_IsMenu,
                    t.F_AllowExpand,
                    t.F_IsPublic,
                    t.F_AllowEdit,
                    t.F_AllowDelete,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                    ";
            btnfieldSql = @"
                    t.F_ModuleButtonId,
                    t.F_ModuleId,
                    t.F_ParentId,
                    t.F_Icon,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_ActionAddress,
                    t.F_SortCode
                    ";
            colfieldSql = @"
                    t.F_ModuleColumnId,
                    t.F_ModuleId,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_SortCode,
                    t.F_Description
                    ";
            formfieldSql = @"
                    t.F_ModuleFormId,
                    t.F_ModuleId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_SortCode
                    ";
        }
        #endregion

        #region 功能模块
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetList()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + fieldSql + " FROM LR_Base_Module t WHERE t.F_DeleteMark = 0 Order By t.F_ParentId,t.F_SortCode ");
                return this.BaseRepository().FindList<ModuleEntity>(strSql.ToString());
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
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<ModuleEntity>(keyValue);
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

        #region 模块按钮
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetButtonList(string moduleId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + btnfieldSql + " FROM LR_Base_ModuleButton t WHERE t.F_ModuleId = @moduleId ORDER BY t.F_SortCode ");
                return this.BaseRepository().FindList<ModuleButtonEntity>(strSql.ToString(), new { moduleId = moduleId });
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

        #region 模块视图列表
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetColumnList(string moduleId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + colfieldSql + " FROM LR_Base_ModuleColumn t WHERE t.F_ModuleId = @moduleId ORDER BY t.F_SortCode ");
                return this.BaseRepository().FindList<ModuleColumnEntity>(strSql.ToString(), new { moduleId = moduleId });
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

        #region 模块表单字段
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleFormEntity> GetFormList(string moduleId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + formfieldSql + " FROM LR_Base_ModuleForm t WHERE t.F_ModuleId = @moduleId ORDER BY t.F_SortCode ");
                return this.BaseRepository().FindList<ModuleFormEntity>(strSql.ToString(), new { moduleId = moduleId });
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
        /// 虚拟删除模块功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Delete(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<ModuleEntity>(t => t.F_ModuleId.Equals(keyValue));
                db.Delete<ModuleButtonEntity>(t => t.F_ModuleId.Equals(keyValue));
                db.Delete<ModuleColumnEntity>(t => t.F_ModuleId.Equals(keyValue));
                db.Delete<ModuleFormEntity>(t => t.F_ModuleId.Equals(keyValue));


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
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ModuleEntity moduleEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    moduleEntity.Modify(keyValue);
                    this.BaseRepository().Update(moduleEntity);
                }
                else
                {
                    moduleEntity.Create();
                    this.BaseRepository().Insert(moduleEntity);
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
        /// <summary>
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <param name="moduleButtonEntitys">按钮列表</param>
        /// <param name="moduleColumnEntitys">视图列集合</param>
        public void SaveEntity(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonEntitys, List<ModuleColumnEntity> moduleColumnEntitys, List<ModuleFormEntity> moduleFormEntitys)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {

                if (string.IsNullOrEmpty(moduleEntity.F_ParentId) || moduleEntity.F_ParentId == "-1")
                {
                    moduleEntity.F_ParentId = "0";
                }

                if (string.IsNullOrEmpty(keyValue))
                {
                    // 新增
                    moduleEntity.Create();
                    db.Insert(moduleEntity);
                }
                else
                {
                    // 编辑
                    moduleEntity.Modify(keyValue);
                    db.Update(moduleEntity);
                    db.Delete<ModuleButtonEntity>(t => t.F_ModuleId.Equals(keyValue));
                    db.Delete<ModuleColumnEntity>(t => t.F_ModuleId.Equals(keyValue));
                    db.Delete<ModuleFormEntity>(t => t.F_ModuleId.Equals(keyValue));
                }
                int num = 0;
                if (moduleButtonEntitys != null)
                {
                    foreach (var item in moduleButtonEntitys)
                    {
                        item.F_SortCode = num;
                        item.F_ModuleId = moduleEntity.F_ModuleId;
                        if (moduleButtonEntitys.Find(t => t.F_ModuleButtonId == item.F_ParentId) == null)
                        {
                            item.F_ParentId = "0";
                        }
                        db.Insert(item);
                        num++;
                    }
                }

                if (moduleColumnEntitys != null)
                {
                    num = 0;
                    foreach (var item in moduleColumnEntitys)
                    {
                        item.F_SortCode = num;
                        item.F_ModuleId = moduleEntity.F_ModuleId;
                        db.Insert(item);
                        num++;
                    }
                }
                if (moduleFormEntitys != null)
                {
                    num = 0;
                    foreach (var item in moduleFormEntitys)
                    {
                        item.F_SortCode = num;
                        item.F_ModuleId = moduleEntity.F_ModuleId;
                        db.Insert(item);
                        num++;
                    }
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
        #endregion
    }
}
