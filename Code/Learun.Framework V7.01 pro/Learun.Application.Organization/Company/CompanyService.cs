using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Organization
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：公司管理
    /// </summary>
    public class CompanyService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public CompanyService()
        {
            fieldSql = @"
                    t.F_CompanyId,
                    t.F_Category,
                    t.F_ParentId,
                    t.F_EnCode,
                    t.F_ShortName,
                    t.F_FullName,
                    t.F_Nature,
                    t.F_OuterPhone,
                    t.F_InnerPhone,
                    t.F_Fax,
                    t.F_Postalcode,
                    t.F_Email,
                    t.F_Manager,
                    t.F_ProvinceId,
                    t.F_CityId,
                    t.F_CountyId,
                    t.F_Address,
                    t.F_WebAddress,
                    t.F_FoundedTime,
                    t.F_BusinessScope,
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
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取公司列表信息（全部）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CompanyEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_Company t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0  ORDER BY t.F_ParentId,t.F_FullName ");
                return this.BaseRepository().FindList<CompanyEntity>(strSql.ToString());
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
        /// 获取公司列表信息（微信用）
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public IEnumerable<CompanyEntity> GetWeChatList(string keyword)
        {
            try
            {
                var sql = @"SELECT t.F_CompanyId,t.F_ParentId,t.F_EnCode,t.F_FullName,t.F_Fax FROM LR_Base_Company t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0  AND t.F_EnCode IS NOT NULL 
                            UNION ALL
                            SELECT m.F_DepartmentId as F_CompanyId,m.F_CompanyId as F_ParentId,m.F_EnCode,m.F_FullName,m.F_Fax FROM LR_Base_Department m WHERE m.F_EnabledMark = 1 AND m.F_DeleteMark = 0 AND m.F_EnCode IS NOT NULL
                            ORDER BY t.F_ParentId,t.F_FullName";
                return this.BaseRepository().FindList<CompanyEntity>(sql);
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
        /// 虚拟删除公司
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                CompanyEntity entity = new CompanyEntity()
                {
                    F_CompanyId = keyValue,
                    F_DeleteMark = 1
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
        /// <summary>
        /// 保存公司表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="companyEntity">公司实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CompanyEntity companyEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    companyEntity.Modify(keyValue);
                    this.BaseRepository().Update(companyEntity);
                }
                else
                {
                    companyEntity.Create();
                    this.BaseRepository().Insert(companyEntity);
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
