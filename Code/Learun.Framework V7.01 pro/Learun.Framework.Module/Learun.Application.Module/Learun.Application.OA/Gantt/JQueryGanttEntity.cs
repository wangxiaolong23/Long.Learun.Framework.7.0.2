using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.OA.Gantt

{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-06-20 15:42
    /// 描 述：项目计划
    /// </summary>
    public class JQueryGanttEntity 
    {
        #region 实体成员
        /// <summary>
        /// id
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// name
        /// </summary>
        /// <returns></returns>
        [Column("NAME")]
        public string name { get; set; }
        /// <summary>
        /// progress
        /// </summary>
        /// <returns></returns>
        [Column("PROGRESS")]
        public int? progress { get; set; }
        /// <summary>
        /// progressByWorklog
        /// </summary>
        /// <returns></returns>
        [Column("PROGRESSBYWORKLOG")]
        public bool progressByWorklog { get; set; }
        /// <summary>
        /// relevance
        /// </summary>
        /// <returns></returns>
        [Column("RELEVANCE")]
        public int? relevance { get; set; }
        /// <summary>
        /// type
        /// </summary>
        /// <returns></returns>
        [Column("TYPE")]
        public string type { get; set; }
        /// <summary>
        /// typeId
        /// </summary>
        /// <returns></returns>
        [Column("TYPEID")]
        public string typeId { get; set; }
        /// <summary>
        /// description
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string description { get; set; }
        /// <summary>
        /// code
        /// </summary>
        /// <returns></returns>
        [Column("CODE")]
        public string code { get; set; }
        /// <summary>
        /// level
        /// </summary>
        /// <returns></returns>
        [Column("LEVEL")]
        public int? level { get; set; }
        /// <summary>
        /// status
        /// </summary>
        /// <returns></returns>
        [Column("STATUS")]
        public string status { get; set; }
        /// <summary>
        /// depends
        /// </summary>
        /// <returns></returns>
        [Column("DEPENDS")]
        public string depends { get; set; }
        /// <summary>
        /// canWrite
        /// </summary>
        /// <returns></returns>
        [Column("CANWRITE")]
        public bool canWrite { get; set; }
        /// <summary>
        /// start
        /// </summary>
        /// <returns></returns>
        [Column("START")]
        public string start { get; set; }
        /// <summary>
        /// duration
        /// </summary>
        /// <returns></returns>
        [Column("DURATION")]
        public string duration { get; set; }
        /// <summary>
        /// end
        /// </summary>
        /// <returns></returns>
        [Column("END")]
        public string end { get; set; }
        /// <summary>
        /// startIsMilestone
        /// </summary>
        /// <returns></returns>
        [Column("STARTISMILESTONE")]
        public bool startIsMilestone { get; set; }
        /// <summary>
        /// endIsMilestone
        /// </summary>
        /// <returns></returns>
        [Column("ENDISMILESTONE")]
        public bool endIsMilestone { get; set; }
        /// <summary>
        /// collapsed
        /// </summary>
        /// <returns></returns>
        [Column("COLLAPSED")]
        public bool collapsed { get; set; }
        /// <summary>
        /// hasChild
        /// </summary>
        /// <returns></returns>
        [Column("HASCHILD")]
        public bool hasChild { get; set; }
        /// <summary>
        /// assigs
        /// </summary>
        /// <returns></returns>
        [Column("ASSIGS")]
        public string assigs { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.id = keyValue;
        }
        #endregion
    }
}

