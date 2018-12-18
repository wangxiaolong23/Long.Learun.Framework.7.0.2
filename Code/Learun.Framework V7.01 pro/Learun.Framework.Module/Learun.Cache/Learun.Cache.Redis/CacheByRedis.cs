using Learun.Cache.Base;
using System;
using System.Configuration;
using System.Configuration;

namespace Learun.Cache.Redis
{
    /// <summary>
    /// 版 本 V2018.3.28
    /// Copyright (c) 2013-2050 上海力软信息技术有限公司
    /// 创建人：力软-系统开发组
    /// 日 期：2017.03.06
    /// 描 述：定义缓存接口
    /// 2018.4.6 bertchen 增加Redis 前缀
    /// </summary>
    public class CacheByRedis : ICache
    {
        #region Key-Value
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T Read<T>(string cacheKey, long dbId = 0) where T : class
        {
            return RedisCache.Get<T>(RedisPrev + cacheKey, dbId);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void Write<T>(string cacheKey, T value, long dbId = 0) where T : class
        {
            RedisCache.Set(RedisPrev + cacheKey, value, dbId);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void Write<T>(string cacheKey, T value, DateTime expireTime, long dbId = 0) where T : class
        {
            RedisCache.Set(RedisPrev + cacheKey, value, expireTime, dbId);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="TimeSpan">缓存时间</param>
        public void Write<T>(string cacheKey, T value, TimeSpan timeSpan, long dbId = 0) where T : class
        {
            RedisCache.Set(RedisPrev + cacheKey, value, timeSpan, dbId);
        }
        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void Remove(string cacheKey, long dbId = 0)
        {
            RedisCache.Remove(RedisPrev + cacheKey, dbId);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveAll(long dbId = 0)
        {
            RedisCache.RemoveAll(dbId);
        }

        /// <summary>
        /// 缓存前缀
        /// </summary>
        private static string _RedisPrev = "";
        private string RedisPrev
        {
            get
            {
                if (_RedisPrev.Length == 0)
                    _RedisPrev = ConfigurationManager.AppSettings["RedisPrev"].ToString();
                return _RedisPrev;
            }
        }
        #endregion
    }
}
