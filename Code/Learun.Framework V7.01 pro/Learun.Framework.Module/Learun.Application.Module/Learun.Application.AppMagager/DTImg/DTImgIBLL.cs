using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.AppMagager
{
    /// <summary> 
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-07-02 15:31 
    /// 描 述：App首页图片管理 
    /// </summary> 
    public interface DTImgIBLL
    {
        #region 获取数据

        /// <summary> 
        /// 获取列表数据 
        /// <summary> 
        /// <returns></returns> 
        IEnumerable<DTImgEntity> GetList();

        /// <summary> 
        /// 获取列表分页数据 
        /// <param name="pagination">分页参数</param> 
        /// <summary> 
        /// <returns></returns> 
        IEnumerable<DTImgEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary> 
        /// 获取实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        DTImgEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据 
        /// <summary> 
        /// 删除实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        void DeleteEntity(string keyValue);
        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        void SaveEntity(string keyValue, DTImgEntity entity);

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用2禁用</param>
        void UpdateState(string keyValue, int state);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetImg(string keyValue);
        #endregion
    }
}
