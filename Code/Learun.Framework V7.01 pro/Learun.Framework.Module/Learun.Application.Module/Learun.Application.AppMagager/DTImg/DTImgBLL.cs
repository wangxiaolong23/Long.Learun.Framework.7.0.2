using Learun.Util;
using System;
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
    public class DTImgBLL: DTImgIBLL
    {
        private DTImgService dTImgService = new DTImgService();

        #region 获取数据

        /// <summary> 
        /// 获取列表数据 
        /// <summary> 
        /// <returns></returns> 
        public IEnumerable<DTImgEntity> GetList()
        {
            try
            {
                return dTImgService.GetList();
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
        /// 获取列表分页数据 
        /// <param name="pagination">分页参数</param> 
        /// <summary> 
        /// <returns></returns> 
        public IEnumerable<DTImgEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return dTImgService.GetPageList(pagination, queryJson);
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
        /// 获取实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public DTImgEntity GetEntity(string keyValue)
        {
            try
            {
                return dTImgService.GetEntity(keyValue);
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
                DTImgEntity entity = GetEntity(keyValue);
                if (entity != null)
                {
                    if (!string.IsNullOrEmpty(entity.F_FileName))
                    {
                        string fileHeadImg = Config.GetValue("fileAppDTImg");
                        string fileImg = string.Format("{0}/{1}{2}", fileHeadImg, entity.F_Id, entity.F_FileName);
                        if (DirFileHelper.IsExistFile(fileImg))
                        {
                            System.IO.File.Delete(fileImg);
                        }
                    }
                }


                dTImgService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, DTImgEntity entity)
        {
            try
            {
                dTImgService.SaveEntity(keyValue, entity);
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
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用2禁用</param>
        public void UpdateState(string keyValue, int state)
        {
            DTImgEntity entity = new DTImgEntity();
            entity.F_EnabledMark = state;
            SaveEntity(keyValue, entity);
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetImg(string keyValue)
        {
            DTImgEntity entity = GetEntity(keyValue);
            string img = "";
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.F_FileName))
                {
                    string fileHeadImg = Config.GetValue("fileAppDTImg");
                    string fileImg = string.Format("{0}/{1}{2}", fileHeadImg, entity.F_Id, entity.F_FileName);
                    if (DirFileHelper.IsExistFile(fileImg))
                    {
                        img = fileImg;
                        FileDownHelper.DownLoadnew(img);
                        return;
                    }
                }
            }
            else
            {
                img = "/Content/images/add.jpg";
            }
            if (string.IsNullOrEmpty(img))
            {
                img = "/Content/images/add.jpg";
            }
            FileDownHelper.DownLoad(img);
        }
        #endregion
    }
}
