using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.IM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：即时通讯消息内容
    /// </summary>
    public class IMMsgBLL: IMMsgIBLL
    {
        private IMMsgService iMMsgService = new IMMsgService();

        #region 获取数据

        /// <summary>
        /// 获取列表数据(最近的10条聊天记录)
        /// <summary>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetList(string sendUserId, string recvUserId)
        {
            try
            {
                return iMMsgService.GetList(sendUserId, recvUserId);
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
        /// 获取列表数据(小于某个时间点的5条记录)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetListByTime(string myUserId, string otherUserId, DateTime time)
        {
            try
            {
                return iMMsgService.GetListByTime(myUserId, otherUserId, time);
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
        /// 获取列表数据(大于某个时间的所有数据)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetListByTime2(string myUserId, string otherUserId, DateTime time)
        {
            try
            {
                return iMMsgService.GetListByTime2(myUserId, otherUserId, time);
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
        /// 获取列表分页数据
        /// <summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="sendUserId"></param>
        /// <param name="recvUserId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetPageList(Pagination pagination, string sendUserId, string recvUserId, string keyword)
        {
            try
            {
                return iMMsgService.GetPageList(pagination, sendUserId, recvUserId, keyword);
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
                iMMsgService.DeleteEntity(keyValue);
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
        /// 保存实体数据（新增）
        /// <param name="entity">实体</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(IMMsgEntity entity)
        {
            try
            {
                iMMsgService.SaveEntity(entity);
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
    }
}
