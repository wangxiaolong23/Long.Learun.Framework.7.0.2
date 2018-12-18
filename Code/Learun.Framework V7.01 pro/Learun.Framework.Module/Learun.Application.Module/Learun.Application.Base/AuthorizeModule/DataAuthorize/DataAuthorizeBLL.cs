using Learun.Application.Base.SystemModule;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：力软框架开发组
    /// 日 期：2017-06-21 16:30
    /// 描 述：数据权限
    /// </summary>
    public class DataAuthorizeBLL : DataAuthorizeIBLL
    {
        private DataAuthorizeService dataAuthorizeService = new DataAuthorizeService();
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_dataauthorize_";

        private InterfaceIBLL interfaceIBLL = new InterfaceBLL(); 

        #region 获取数据
        /// <summary>
        /// 获取条件列表数据
        /// </summary>
        /// <param name="relationId">关系主键</param>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeConditionEntity> GetDataAuthorizeConditionList(string relationId)
        {
            try
            {
                IEnumerable<DataAuthorizeConditionEntity> list = cache.Read<IEnumerable<DataAuthorizeConditionEntity>>(cacheKey + relationId, CacheId.dataAuthorize);
                if (list == null)
                {
                    list = dataAuthorizeService.GetDataAuthorizeConditionList(relationId);
                    cache.Write<IEnumerable<DataAuthorizeConditionEntity>>(cacheKey + relationId, list, CacheId.dataAuthorize);
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
        /// 获取数据权限对应关系数据列表
        /// <param name="interfaceId">接口主键</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeRelationEntity> GetRelationList(string interfaceId)
        {
            try
            {
                IEnumerable<DataAuthorizeRelationEntity> list = cache.Read<IEnumerable<DataAuthorizeRelationEntity>>(cacheKey + interfaceId, CacheId.dataAuthorize);
                if (list == null)
                {
                    list = dataAuthorizeService.GetRelationList(interfaceId);
                    cache.Write<IEnumerable<DataAuthorizeRelationEntity>>(cacheKey + interfaceId, list, CacheId.dataAuthorize);
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
        /// 获取数据权限对应关系数据列表
        /// <param name="interfaceId">接口主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <param name="objectId">对象主键</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeRelationEntity> GetRelationPageList(Pagination pagination, string interfaceId, string keyword, string objectId)
        {
            try
            {
                return dataAuthorizeService.GetRelationPageList(pagination, interfaceId, keyword, objectId);
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
        /// 获取数据权限条件列
        /// </summary>
        /// <param name="interfaceId">接口主键</param>
        /// <param name="objectId">对应角色或用户主键</param>
        /// <returns></returns>
        public IEnumerable<DataAuthorizeConditionEntity> GetConditionList(string interfaceId, string objectId)
        {
            try
            {
                List<DataAuthorizeRelationEntity> list = (List<DataAuthorizeRelationEntity>)GetRelationList(interfaceId);
                DataAuthorizeRelationEntity relationEntity = list.Find(t => t.F_ObjectId == objectId);
                if (relationEntity != null)
                {
                    return GetDataAuthorizeConditionList(relationEntity.F_Id);
                }
                else
                {
                    return new List<DataAuthorizeConditionEntity>();
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DataAuthorizeRelationEntity GetRelationEntity(string keyValue)
        {
            try
            {
                return dataAuthorizeService.GetRelationEntity(keyValue);
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
                var entity = dataAuthorizeService.GetRelationEntity(keyValue);
                cache.Remove(cacheKey + entity.F_InterfaceId, CacheId.dataAuthorize);
                cache.Remove(cacheKey + entity.F_Id, CacheId.dataAuthorize);
                dataAuthorizeService.DeleteEntity(keyValue);
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
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, DataAuthorizeRelationEntity relationEntity, List<DataAuthorizeConditionEntity> conditionEntityList)
        {
            try
            {
                cache.Remove(cacheKey + relationEntity.F_InterfaceId, CacheId.dataAuthorize);
                cache.Remove(cacheKey + keyValue, CacheId.dataAuthorize);
                dataAuthorizeService.SaveEntity(keyValue, relationEntity, conditionEntityList);
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
        /// 设置查询语句
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <returns></returns>
        public bool SetWhereSql(string url)
        {
            try
            {
                UserInfo userInfo = LoginUserInfo.Get();
                if (userInfo.isSystem)
                {
                    return true;
                }
                // 判断该接口注册了
                InterfaceEntity interfaceEntity = interfaceIBLL.GetEntityByUrl(url);
                if (interfaceEntity == null)
                {
                    // 如果接口没有注册则不作过滤
                    return true;
                }
                else
                {
                    List<DataAuthorizeRelationEntity> relationList = (List<DataAuthorizeRelationEntity>)GetRelationList(interfaceEntity.F_Id);
                    if (relationList.Count > 0)
                    {
                        
                        relationList = relationList.FindAll(t => t.F_ObjectId.Equals(userInfo.userId) || t.F_ObjectId.Like(userInfo.roleIds));
                        if (relationList.Count > 0)
                        {
                            string whereSql = "";
                            DbWhere dbWhere = new DbWhere();
                            dbWhere.dbParameters = new List<FieldValueParam>();

                            int relationnum = 0;
                            foreach (var item in relationList)
                            {
                                if (whereSql != "")
                                {
                                    whereSql += " OR ";
                                }
                                whereSql += " ( ";
                                string strSql = "";
                                List<DataAuthorizeConditionEntity> conditionList = (List<DataAuthorizeConditionEntity>)GetDataAuthorizeConditionList(item.F_Id);

                                if (!string.IsNullOrEmpty(item.F_Formula))
                                {
                                    strSql = item.F_Formula;
                                    for (int i = 1; i < conditionList.Count + 1; i++)
                                    {
                                        strSql = strSql.Replace("" + i, "{@learun" + i + "learun@}");
                                    }
                                }
                                else
                                {
                                    for (int i = 1; i < conditionList.Count + 1; i++)
                                    {
                                        if (strSql != "")
                                        {
                                            strSql += " AND ";
                                        }
                                        strSql += " {@learun" + i + "learun@} ";
                                    }
                                }

                                int num = 1;

                                foreach (var conditionItem in conditionList)
                                {
                                    string strone = " " + conditionItem.F_FieldId;
                                    string value = " @" + conditionItem.F_FieldId + relationnum;

                                    FieldValueParam dbParameter = new FieldValueParam();
                                    dbParameter.name = conditionItem.F_FieldId + relationnum;
                                    dbParameter.value = getValue(conditionItem.F_FiledValueType, conditionItem.F_FiledValue);
                                    dbParameter.type = conditionItem.F_FieldType;
                                    dbWhere.dbParameters.Add(dbParameter);
                                    //[{ value: 1, text: '等于' }, { value: 2, text: '大于' }, { value: 3, text: '大于等于' }, { value: 4, text: '小于' }, { value: 5, text: '小于等于' }, { value: 6, text: '包含' }, { value: 7, text: '包含于' }, { value: 8, text: '不等于' }, { value: 9, text: '不包含' }, { value: 10, text: '不包含于' }],
                                    switch (conditionItem.F_Symbol)
                                    {
                                        case 1:// 等于
                                            strone += " = " + value;
                                            break;
                                        case 2:// 大于
                                            strone += " > " + value;
                                            break;
                                        case 3:// 大于等于
                                            strone += " >= " + value;
                                            break;
                                        case 4:// 小于
                                            strone += " < " + value;
                                            break;
                                        case 5:// 小于等于
                                            strone += " <= " + value;
                                            break;
                                        case 6:// 包含
                                            strone += " like %" + value + "%";
                                            break;
                                        case 7:// 包含于
                                            strone += " in ( '" + value.Replace(",", "','") + "' )";
                                            break;
                                        case 8:// 不等于
                                            strone += " != " + value;
                                            break;
                                        case 9:// 不包含
                                            strone += " not like %" + value + "%";
                                            break;
                                        case 10:// 不包含于
                                            strone += " not in ( '" + value.Replace(",","','") + "' )";
                                            break;
                                        default:
                                            break;
                                    }
                                    strone += " ";
                                    strSql = strSql.Replace("{@learun" + num + "learun@}", strone);
                                    num++;
                                }

                                whereSql += strSql;
                                whereSql += " ) ";
                                relationnum++;
                            }
                            dbWhere.sql = whereSql;
                            WebHelper.AddHttpItems("DataAhthorCondition", dbWhere);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // 该接口没做权限过滤
                        return true;
                    }
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 设置查询语句
        /// </summary>
        /// <returns></returns>
        public bool SetWhereSql()
        {
            UserInfo userInfo = LoginUserInfo.Get();
            if (userInfo.departmentIds != null)
            {
                string whereSql = string.Format(" CHARINDEX(F_DepartmentId,'{0}') > 0", string.Join(",", userInfo.departmentIds));
                DbWhere dbWhere = new DbWhere();
                dbWhere.sql = whereSql;
                WebHelper.AddHttpItems("DataAhthorCondition", dbWhere);
            }
            return true;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <param name="value">数据值</param>
        /// <returns></returns>
        private string getValue(int? type, string value)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            //1.文本2.登录者ID3.登录者账号4.登录者公司5.登录者部门6.登录者岗位7.登录者角色
            string text = "";
            switch (type)
            {
                case 1:// 文本
                    text = value;
                    break;
                case 2:// 登录者ID
                    text = userInfo.userId;
                    break;
                case 3:// 登录者账号
                    text = userInfo.account;
                    break;
                case 4:// 登录者公司
                    text = userInfo.companyId;
                    break;
                case 41:// 登录者公司及下属公司
                    foreach (var id in userInfo.companyIds) {
                        if (text != "") {
                            text += ",";
                        }
                        text += id;
                    }
                    break;
                case 5:// 登录者部门
                    text = userInfo.departmentId;
                    break;
                case 51:// 登录者部门及下属部门
                    foreach (var id in userInfo.departmentIds)
                    {
                        if (text != "")
                        {
                            text += ",";
                        }
                        text += id;
                    }
                    break;
                case 6:// 登录者岗位
                    text = userInfo.postIds;
                    break;
                case 7:// 登录者角色
                    text = userInfo.roleIds;
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
