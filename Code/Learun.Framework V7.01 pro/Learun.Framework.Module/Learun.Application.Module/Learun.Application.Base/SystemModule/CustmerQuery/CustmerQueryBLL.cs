using Learun.Application.Organization;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：自定义查询
    /// </summary>
    public class CustmerQueryBLL : CustmerQueryIBLL
    {
        #region 属性
        private CustmerQueryService custmerQueryService = new CustmerQueryService();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_custmerQuery_";// +功能地址 + 用户主键（可无）
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
                return custmerQueryService.GetPageList(pagination, keyword);
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
        /// 获取自定义查询条件
        /// </summary>
        /// <param name="moduleUrl">访问的功能链接地址</param>
        /// <param name="userId">用户ID（用户ID为null表示公共）</param>
        /// <returns></returns>
        public List<CustmerQueryEntity> GetList(string moduleUrl, string userId)
        {
            try
            {
                string key = cacheKey + moduleUrl;
                if (!string.IsNullOrEmpty(userId))
                {
                    key += '_' + userId;
                }
                List<CustmerQueryEntity> list = cache.Read<List<CustmerQueryEntity>>(key, CacheId.custmerQuery);
                if (list == null)
                {
                    list = (List<CustmerQueryEntity>)custmerQueryService.GetList(moduleUrl, userId);
                    cache.Write<List<CustmerQueryEntity>>(key, list, CacheId.custmerQuery);
                }
                return list;
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
        /// 获取自定义查询条件(用于具体使用)
        /// </summary>
        /// <param name="moduleUrl"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CustmerQueryEntity> GetCustmerList(string moduleUrl, string userId)
        {
            try
            {
                List<CustmerQueryEntity> list = GetList(moduleUrl, userId);
                List<CustmerQueryEntity> publiclist = GetList(moduleUrl, "");
                publiclist.AddRange(list);
                return publiclist;
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
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                CustmerQueryEntity custmerQueryEntity = custmerQueryService.GetEntity(keyValue);
                string key = cacheKey + custmerQueryEntity.F_ModuleUrl;
                if (!string.IsNullOrEmpty(custmerQueryEntity.F_UserId))
                {
                    key += '_' + custmerQueryEntity.F_UserId;
                }
                custmerQueryService.DeleteEntity(keyValue);
                cache.Remove(key, CacheId.custmerQuery);
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
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CustmerQueryEntity custmerQueryEntity)
        {
            try
            {
                string key = cacheKey + custmerQueryEntity.F_ModuleUrl;
                if (!string.IsNullOrEmpty(custmerQueryEntity.F_UserId))
                {
                    key += '_' + custmerQueryEntity.F_UserId;
                }
                custmerQueryService.SaveEntity(keyValue, custmerQueryEntity);
                cache.Remove(key, CacheId.custmerQuery);
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

        #region 扩展方法
        /// <summary>
        /// 将条件转化成sql语句
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="formula">公式，没有就默认采用and连接</param>
        /// <returns></returns>
        public string ConditionToSql(string queryJson, string formula, UserEntity userEntity)
        {
            try
            {
                string strSql = "";
                List<CustmerQueryModel> list = queryJson.ToObject<List<CustmerQueryModel>>();
                if (string.IsNullOrEmpty(formula))
                {
                    for (int i = 1; i < list.Count + 1; i++)
                    {
                        strSql = formula.Replace("" + i, "{@learun" + i + "learun@}");
                    }
                }
                else
                {
                    for (int i = 1; i < list.Count + 1; i++)
                    {
                        if (strSql != "")
                        {
                            strSql += " AND ";
                        }
                        strSql += " {@learun" + i + "learun@} ";
                    }
                }

                int num = 1;
                foreach (var item in list)
                {
                    string strone =" " + item.field;
                    string value = getValue(item.type, item.value, userEntity);
                    switch (item.condition)
                    {
                        case 1:// 等于
                            strone += " = " + value;
                            break;
                        case 6:// 不等于
                            strone += " != " + value;
                            break;
                        case 7:// 包含
                            strone += " like %" + value+"%";
                            break;
                        case 8:// 不包含
                            strone += " not like %" + value + "%";
                            break;
                        default:
                            break;
                    }
                    strone += " ";
                    strSql.Replace("{@learun" + num + "learun@}", strone);
                    num++;
                }

                return strSql;
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
        /// 获取数据
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="value">数据值</param>
        /// <returns></returns>
        private string getValue(int type, string value, UserEntity userEntity)
        {
            string text = "";
            switch (type)
            {
                case 1:// 文本
                    text = value;
                    break;
                case 4:// 当前账号
                    text = userEntity.F_UserId;
                    break;
                case 5:// 当前公司
                    text = userEntity.F_CompanyId;
                    break;
                case 6:// 当前部门
                    text = userEntity.F_DepartmentId;
                    break;
                case 7:// 当前岗位
                    //text = userEntity.F_PostId;
                    break;
                default:
                    text = value;
                    break;
            }

            return text;
        }
        #endregion
    }
}
