using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.OA
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：新闻管理
    /// </summary>
    public class NewsService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="categoryId">类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM LR_OA_News t WHERE t.F_TypeId = 1 ");
                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND F_FullHead like @keyword");
                }
                return this.BaseRepository().FindList<NewsEntity>(strSql.ToString(), new { keyword = "%" + keyword + "%" }, pagination);
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
        /// 新闻公告实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NewsEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<NewsEntity>(keyValue);
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
        public void DeleteEntity(string keyValue)
        {
            try
            {
                NewsEntity entity = new NewsEntity()
                {
                    F_NewsId = keyValue,
                };
                this.BaseRepository().Delete(entity);
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
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">新闻公告实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, NewsEntity newsEntity)
        {
            try
            {
                newsEntity.F_TypeId = 1;
                if (!string.IsNullOrEmpty(keyValue))
                {
                    newsEntity.Modify(keyValue);
                    this.BaseRepository().Update(newsEntity);
                }
                else
                {
                    newsEntity.Create();
                    this.BaseRepository().Insert(newsEntity);
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
