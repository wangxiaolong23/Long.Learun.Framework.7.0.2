using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Redis;

namespace Learun.Cache.Redis
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.03
    /// 描 述：redis操作方法
    /// </summary>
    public class RedisCache
    {
        #region -- 连接信息 --
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static RedisConfigInfo redisConfigInfo = RedisConfigInfo.GetConfig();
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static PooledRedisClientManager CreateManager(long dbId)
        {
            string[] writeServerList = SplitString(redisConfigInfo.WriteServerList, ",");
            string[] readServerList = SplitString(redisConfigInfo.ReadServerList, ",");

            return new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = redisConfigInfo.MaxWritePoolSize,
                                 MaxReadPoolSize = redisConfigInfo.MaxReadPoolSize,
                                 AutoStart = redisConfigInfo.AutoStart,
                                 DefaultDb = dbId
                             });
        }
        /// <summary>
        /// 字串转数组
        /// </summary>
        /// <param name="strSource">字串</param>
        /// <param name="split">分隔符</param>
        /// <returns></returns>
        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }
        /// <summary>
        /// 获取redis客户端根据库ID号
        /// </summary>
        /// <param name="dbId">redis库Id</param>
        /// <returns></returns>
        private static PooledRedisClientManager GetClientManager(long dbId)
        {            
            return CreateManager(dbId);
        }

        #endregion

        #region -- Item --
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dbId">库Id</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t, long dbId = 0)
        {
            var clientManager = GetClientManager(dbId);
            IRedisClient redis = clientManager.GetClient();
            var res = redis.Set<T>(key, t);
            clientManager.DisposeClient((RedisNativeClient)redis);
            redis.Dispose();
            clientManager.Dispose();
            return res;
        }
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="timeSpan">保存时间</param>
        /// <param name="dbId">库Id</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t, TimeSpan timeSpan, long dbId = 0)
        {
            var clientManager = GetClientManager(dbId);
            IRedisClient redis = clientManager.GetClient();
            var res = redis.Set<T>(key, t, timeSpan);
            clientManager.DisposeClient((RedisNativeClient)redis);
            redis.Dispose();
            clientManager.Dispose();
            return res;
        }
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dateTime">过期时间</param>
        /// <returns></returns>
        public static bool Set<T>(string key, T t, DateTime dateTime, long dbId = 0)
        {
            var clientManager = GetClientManager(dbId);
            IRedisClient redis = clientManager.GetClient();
            var res = redis.Set<T>(key, t, dateTime);
            clientManager.DisposeClient((RedisNativeClient)redis);
            redis.Dispose();
            clientManager.Dispose();
            return res;
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static T Get<T>(string key, long dbId = 0) where T : class
        {
            var clientManager = GetClientManager(dbId);
            IRedisClient redis = clientManager.GetClient();
            var res = redis.Get<T>(key);
            clientManager.DisposeClient((RedisNativeClient)redis);
            redis.Dispose();
            clientManager.Dispose();
            return res;
        }
        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key">键值</param>
        public static bool Remove(string key, long dbId = 0)
        {
            var clientManager = GetClientManager(dbId);
            IRedisClient redis = clientManager.GetClient();
            var res = redis.Remove(key);
            clientManager.DisposeClient((RedisNativeClient)redis);
            redis.Dispose();
            clientManager.Dispose();
            return res;
        }
        /// <summary>
        /// 清空所有缓存
        /// </summary>
        public static void RemoveAll(long dbId = 0)
        {
            var clientManager = GetClientManager(dbId);
            IRedisClient redis = clientManager.GetClient();
            redis.FlushDb();
            clientManager.DisposeClient((RedisNativeClient)redis);
            redis.Dispose();
            clientManager.Dispose();
        }
        #endregion

        #region -- List --
        /// <summary>
        /// 添加列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dbId">库</param>
        public static void List_Add<T>(string key, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var redisTypedClient = redis.As<T>();
                redisTypedClient.AddItemToList(redisTypedClient.Lists[key], t);
            }
        }
        /// <summary>
        /// 移除列表某个值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool List_Remove<T>(string key, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var redisTypedClient = redis.As<T>();
                return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], t) > 0;
            }
        }
        /// <summary>
        /// 移除列表所有值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="dbId">库Id</param>
        public static void List_RemoveAll<T>(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var redisTypedClient = redis.As<T>();
                redisTypedClient.Lists[key].RemoveAll();
            }
        }
        /// <summary>
        /// 获取列表数据条数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        public static long List_Count(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                return redis.GetListCount(key);
            }
        }
        /// <summary>
        /// 获取指定条数列表数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="start">开始编号</param>
        /// <param name="count">条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static List<T> List_GetRange<T>(string key, int start, int count, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var c = redis.As<T>();
                return c.Lists[key].GetRange(start, start + count - 1);
            }
        }
        /// <summary>
        /// 获取列表所有数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="dbId">库数据</param>
        /// <returns></returns>
        public static List<T> List_GetList<T>(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var c = redis.As<T>();
                return c.Lists[key].GetRange(0, c.Lists[key].Count);
            }
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static List<T> List_GetList<T>(string key, int pageIndex, int pageSize, long dbId = 0)
        {
            int start = pageSize * (pageIndex - 1);
            return List_GetRange<T>(key, start, pageSize, dbId);
        }
        #endregion

        #region -- Set --
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        public static void Set_Add<T>(string key, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var redisTypedClient = redis.As<T>();
                redisTypedClient.Sets[key].Add(t);
            }
        }
        /// <summary>
        /// 集合是否包含指定数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool Set_Contains<T>(string key, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var redisTypedClient = redis.As<T>();
                return redisTypedClient.Sets[key].Contains(t);
            }
        }
        /// <summary>
        /// 移除集合某个值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool Set_Remove<T>(string key, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var redisTypedClient = redis.As<T>();
                return redisTypedClient.Sets[key].Remove(t);
            }
        }
        #endregion

        #region -- Hash --
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool Hash_Exist<T>(string key, string dataKey, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                return redis.HashContainsEntry(key, dataKey);
            }
        }

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool Hash_Set<T>(string key, string dataKey, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
                return redis.SetEntryInHash(key, dataKey, value);
            }
        }
        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool Hash_Remove(string key, string dataKey, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                return redis.RemoveEntryFromHash(key, dataKey);
            }
        }
        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <param name="key">hashID</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool Hash_Remove(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                return redis.Remove(key);
            }
        }
        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static T Hash_Get<T>(string key, string dataKey, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                string value = redis.GetValueFromHash(key, dataKey);
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(value);
            }
        }
        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static List<T> Hash_GetAll<T>(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var list = redis.GetHashValues(key);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var value = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
                        result.Add(value);
                    }
                    return result;
                }
                return null;
            }
        }
        #endregion

        #region -- SortedSet --
        /// <summary>
        ///  添加数据到 SortedSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">集合id</param>
        /// <param name="t">数值</param>
        /// <param name="score">排序码</param>
        /// <param name="dbId">库</param>
        public static bool SortedSet_Add<T>(string key, T t, double score, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
                return redis.AddItemToSortedSet(key, value, score);
            }
        }
        /// <summary>
        /// 移除数据从SortedSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">集合id</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static bool SortedSet_Remove<T>(string key, T t, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                string value = ServiceStack.Text.JsonSerializer.SerializeToString<T>(t);
                return redis.RemoveItemFromSortedSet(key, value);
            }
        }
        /// <summary>
        /// 修剪SortedSet
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="size">保留的条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static long SortedSet_Trim(string key, int size, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                return redis.RemoveRangeFromSortedSet(key, size, 9999999);
            }
        }
        /// <summary>
        /// 获取SortedSet的长度
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static long SortedSet_Count(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                return redis.GetSortedSetCount(key);
            }
        }

        /// <summary>
        /// 获取SortedSet的分页数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static List<T> SortedSet_GetList<T>(string key, int pageIndex, int pageSize, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var list = redis.GetRangeFromSortedSet(key, (pageIndex - 1) * pageSize, pageIndex * pageSize - 1);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var data = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
                        result.Add(data);
                    }
                    return result;
                }
            }
            return null;
        }


        /// <summary>
        /// 获取SortedSet的全部数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="key">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        public static List<T> SortedSet_GetListALL<T>(string key, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                var list = redis.GetRangeFromSortedSet(key, 0, 9999999);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var data = ServiceStack.Text.JsonSerializer.DeserializeFromString<T>(item);
                        result.Add(data);
                    }
                    return result;
                }
            }
            return null;
        }
        #endregion

        #region 公用方法
        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="datetime">过期时间</param>
        /// <param name="dbId">库</param>
        public static void SetExpire(string key, DateTime datetime, long dbId = 0)
        {
            using (IRedisClient redis = CreateManager(dbId).GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }
        #endregion
    }
}
