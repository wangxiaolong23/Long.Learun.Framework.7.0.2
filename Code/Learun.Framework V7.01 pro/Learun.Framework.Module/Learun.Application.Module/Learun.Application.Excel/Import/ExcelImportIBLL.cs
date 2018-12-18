using Learun.Util;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Excel
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：Excel数据导入设置
    /// </summary>
    public interface ExcelImportIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件参数</param>
        /// <returns></returns>
        IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取导入配置列表根据模块ID
        /// </summary>
        /// <param name="moduleId">功能模块主键</param>
        /// <returns></returns>
        IEnumerable<ExcelImportEntity> GetList(string moduleId);
        /// <summary>
        /// 获取配置信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        ExcelImportEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取配置字段列表
        /// </summary>
        /// <param name="importId">配置信息主键</param>
        /// <returns></returns>
        IEnumerable<ExcelImportFieldEntity> GetFieldList(string importId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体数据</param>
        /// <param name="filedList">字段列表</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, ExcelImportEntity entity, List<ExcelImportFieldEntity> filedList);
        /// <summary>
        /// 更新配置主表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void UpdateEntity(string keyValue, ExcelImportEntity entity);
        #endregion

        #region
        /// <summary>
        /// excel 数据导入（未导入数据写入缓存）
        /// </summary>
        /// <param name="templateId">导入模板主键</param>
        /// <param name="fileId">文件ID</param>
        /// <param name="dt">导入数据</param>
        /// <returns></returns>
        string ImportTable(string templateId, string fileId, DataTable dt);
        /// <summary>
        /// 获取excel导入的错误数据
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        DataTable GetImportError(string fileId);
        #endregion
    }
}
