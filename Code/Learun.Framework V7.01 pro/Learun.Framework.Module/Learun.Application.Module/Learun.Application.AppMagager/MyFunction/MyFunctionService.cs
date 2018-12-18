using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.AppMagager
{
    /// <summary> 
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-06-26 10:32 
    /// 描 述：我的常用移动应用 
    /// </summary> 
    public class MyFunctionService: RepositoryFactory
    {
        #region 构造函数和属性 

        private string fieldSql;
        public MyFunctionService()
        {
            fieldSql = @" 
                t.F_Id, 
                t.F_UserId, 
                t.F_FunctionId,
                t.F_Sort
            ";
        }
        #endregion

        #region 获取数据 

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="userId">用户主键ID</param>
        /// <returns></returns>
        public IEnumerable<MyFunctionEntity> GetList(string userId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_App_MyFunction t where t.F_UserId = @userId Order by  t.F_Sort ");
                return this.BaseRepository().FindList<MyFunctionEntity>(strSql.ToString(),new { userId });
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
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public void SaveEntity(string userId,string strFunctionId)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                string[] functionIds = strFunctionId.Split(',');
                db.Delete<MyFunctionEntity>(t=>t.F_UserId.Equals(userId));
                int num = 0;
                foreach (var functionId in functionIds) {
                    MyFunctionEntity entity = new MyFunctionEntity();
                    entity.Create();
                    entity.F_UserId = userId;
                    entity.F_FunctionId = functionId;
                    entity.F_Sort = num;
                    db.Insert(entity);
                    num++;
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
