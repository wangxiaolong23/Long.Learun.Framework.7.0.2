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
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：IP过滤
    /// </summary>
    public class FilterIPBLL : FilterIPIBLL
    {
        private FilterIPService filterIPService = new FilterIPService();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_filterIP_"; // +主键
        #endregion

        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterIPEntity> GetList(string objectId, string visitType)
        {
            try
            {
                IEnumerable<FilterIPEntity> list = cache.Read<IEnumerable<FilterIPEntity>>(cacheKey + visitType + "_" + objectId, CacheId.filterIP);
                if (list == null)
                {
                    list = filterIPService.GetList(objectId, visitType);
                    cache.Write<IEnumerable<FilterIPEntity>>(cacheKey + visitType + "_" + objectId, list, CacheId.filterIP);
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
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FilterIPEntity GetEntity(string keyValue)
        {
            try
            {
                return filterIPService.GetEntity(keyValue);
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
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntiy(string keyValue)
        {
            try
            {
                FilterIPEntity entity = GetEntity(keyValue);
                cache.Remove(cacheKey + entity.F_VisitType + "_" + entity.F_ObjectId, CacheId.filterIP);
                filterIPService.DeleteEntiy(keyValue);
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
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            try
            {
                cache.Remove(cacheKey + filterIPEntity.F_VisitType + "_" + filterIPEntity.F_ObjectId, CacheId.filterIP);
                filterIPService.SaveForm(keyValue, filterIPEntity);
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

        #region IP过滤处理
        /// <summary>
        /// IP地址过滤
        /// </summary>
        /// <returns></returns>
        public bool FilterIP()
        {
            string[] roleIdList = null;
            UserInfo userInfo = LoginUserInfo.Get();
            if (userInfo.isSystem)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(userInfo.roleIds))
            {
                roleIdList = userInfo.roleIds.Split(',');
            }

            #region 黑名单处理
            IEnumerable<FilterIPEntity> blackIPList = GetList(userInfo.userId, "0");
            bool isBlack = CheckArea(blackIPList);
            if (isBlack)
            {
                return false;
            }
            if (roleIdList != null)
            {
                foreach (string role in roleIdList)
                {
                    blackIPList = GetList(role, "0");
                    isBlack = CheckArea(blackIPList);
                    if (isBlack)
                    {
                        return false;
                    }
                }
            }
            #endregion

            #region 白名单处理
            bool makeWhite = false;
            List<FilterIPEntity> whiteIPList = (List<FilterIPEntity>)GetList(userInfo.userId, "1");
            if (whiteIPList.Count > 0)
            {
                makeWhite = true;
            }
            bool isWhite = CheckArea(whiteIPList);
            if (isWhite)
            {
                return true;
            }
            if (roleIdList != null)
            {
                foreach (string role in roleIdList)
                {
                    whiteIPList = (List<FilterIPEntity>)GetList(role, "1");
                    if (whiteIPList.Count > 0)
                    {
                        makeWhite = true;
                    }
                    isWhite = CheckArea(whiteIPList);
                    if (isWhite)
                    {
                        return true;
                    }
                }
            }
            if (makeWhite)
            {
                return false;
            }
            #endregion
            return true;
        }
        /// <summary>
        /// 判断当前登陆用户IP是否在IP段中
        /// </summary>
        /// <param name="ipList">Ip列表</param>
        /// <returns></returns>
        private bool CheckArea(IEnumerable<FilterIPEntity> ipList)
        {
            if (ipList == null)
            {
                return false;
            }
            foreach (var item in ipList)
            {
                string strIP = item.F_IPLimit;
                string[] ipArry = strIP.Split(',');
                //黑名单起始IP
                string[] startArry = ipArry[0].Split('.');
                string startHead = startArry[0] + "." + startArry[1] + "." + startArry[2];
                int start = int.Parse(startArry[3]);
                //黑名单结束IP
                string[] endArry = ipArry[1].Split('.');
                string endHead = endArry[0] + "." + endArry[1] + "." + endArry[2];
                int end = int.Parse(endArry[3]);
                //当前IP
                string strIpAddress = Net.Ip;
                string[] ipAddressArry = strIpAddress.Split('.');
                string ipAddressHead = ipAddressArry[0] + "." + ipAddressArry[1] + "." + ipAddressArry[2];
                int ipAddress = int.Parse(ipAddressArry[3]);
                if (ipAddressHead == startHead)
                {
                    if (ipAddress >= start && ipAddress <= end)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
