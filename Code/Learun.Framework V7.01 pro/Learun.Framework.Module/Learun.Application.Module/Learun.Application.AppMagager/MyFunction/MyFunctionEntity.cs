using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.AppMagager
{
    /// <summary> 
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-06-26 10:32 
    /// 描 述：我的常用移动应用 
    /// </summary> 
    public class MyFunctionEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 用户主键ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary> 
        /// 功能主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FUNCTIONID")]
        public string F_FunctionId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [Column("F_SORT")]
        public int? F_Sort { get; set; }

        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
        }
        /// <summary> 
        /// 编辑调用 
        /// </summary> 
        /// <param name="keyValue"></param> 
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
    }
}
