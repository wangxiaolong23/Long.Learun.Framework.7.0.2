using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;

namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：时段过滤
    /// </summary>
    public class FilterTimeBLL : FilterTimeIBLL
    {
        private FilterTimeService filterTimeService = new FilterTimeService();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_filterTime_"; // +主键
        #endregion

        #region 获取数据
        /// <summary>
        /// 过滤时段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FilterTimeEntity GetEntity(string keyValue)
        {
            try
            {
                FilterTimeEntity entity = cache.Read<FilterTimeEntity>(cacheKey + keyValue, CacheId.filterTime);
                if (entity == null)
                {
                    entity = filterTimeService.GetEntity(keyValue);
                    cache.Write<FilterTimeEntity>(cacheKey + keyValue, entity, CacheId.filterTime);
                }
                return entity;
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
        /// 删除过滤时段
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntiy(string keyValue)
        {
            try
            {
                filterTimeService.DeleteEntiy(keyValue);
                cache.Remove(cacheKey + keyValue, CacheId.filterTime);
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
        /// 保存过滤时段表单（新增、修改）
        /// </summary>
        /// <param name="filterTimeEntity">过滤时段实体</param>
        /// <returns></returns>
        public void SaveForm(FilterTimeEntity filterTimeEntity)
        {
            try
            {
                string keyValue = "";
                FilterTimeEntity entity = filterTimeService.GetEntity(filterTimeEntity.F_FilterTimeId);
                if (entity != null)
                {
                    keyValue = entity.F_FilterTimeId;
                    cache.Remove(cacheKey + keyValue, CacheId.filterTime);
                }
                filterTimeService.SaveForm(keyValue, filterTimeEntity);
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

        #region 处理时间过滤
        /// <summary>
        /// 处理时间过滤
        /// </summary>
        /// <returns></returns>
        public bool FilterTime()
        {
            bool res = true;
            UserInfo userInfo = LoginUserInfo.Get();
            if (userInfo.isSystem)
            {
                return true;
            }
            FilterTimeEntity entity = GetEntity(userInfo.userId);
            res = FilterTime(entity);
            if (!res) {
                return res;
            }
            if(!string.IsNullOrEmpty(userInfo.roleIds)){
                string[] roleIdList = userInfo.roleIds.Split(',');
                foreach (string roleId in roleIdList) {
                    entity = GetEntity(roleId);
                    res = FilterTime(entity);
                    if (!res)
                    {
                        return res;
                    }
                }
            }
            return res;
        }
        /// <summary>
        /// 处理时间过滤
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        private bool FilterTime(FilterTimeEntity entity)
        {
            bool res = true;
            if (entity == null)
            {
                return res;
            }
            int weekday = Time.GetNumberWeekDay(DateTime.Now);
            string time = DateTime.Now.ToString("HH") + ":00";
            string strFilterTime = "";
            switch (weekday)
            {
                case 1:
                    strFilterTime = entity.F_WeekDay1;
                    break;
                case 2:
                    strFilterTime = entity.F_WeekDay2;
                    break;
                case 3:
                    strFilterTime = entity.F_WeekDay3;
                    break;
                case 4:
                    strFilterTime = entity.F_WeekDay4;
                    break;
                case 5:
                    strFilterTime = entity.F_WeekDay5;
                    break;
                case 6:
                    strFilterTime = entity.F_WeekDay6;
                    break;
                case 7:
                    strFilterTime = entity.F_WeekDay7;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(strFilterTime))
            {
                //当前时段包含在限制时段中
                if (strFilterTime.IndexOf(time) >= 0)
                {
                    res = false;
                }
            }
            return res;
        }
        #endregion
    }
}
