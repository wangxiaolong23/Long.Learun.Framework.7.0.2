using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：自定义查询
    /// </summary>
    public class CustmerQueryEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("F_CUSTMERQUERYID")]
        public string F_CustmerQueryId { get; set; }
        /// <summary>
        /// 查询名称
        /// </summary>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 所属成员
        /// </summary>
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 功能模块ID
        /// </summary>
        [Column("F_MODULEID")]
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 所属功能连接地址
        /// </summary>
        [Column("F_MODULEURL")]
        public string F_ModuleUrl { get; set; }
        /// <summary>
        /// 查询的公式
        /// </summary>
        [Column("F_FORMULA")]
        public string F_Formula { get; set; }
        /// <summary>
        /// 查询的条件
        /// </summary>
        [Column("F_QUERYJSON")]
        public string F_QueryJson { get; set; }


        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_CustmerQueryId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_CustmerQueryId = keyValue;
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 模块名称
        /// </summary>
        [Column("MODULENAME")]
        [NotMapped]
        public string ModuleName { get; set; }
        #endregion
    }
}
