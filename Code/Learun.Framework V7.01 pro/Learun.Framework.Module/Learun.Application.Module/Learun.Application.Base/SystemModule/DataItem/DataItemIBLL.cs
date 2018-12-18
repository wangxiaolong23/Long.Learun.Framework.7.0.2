using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：数据字典
    /// </summary>
    public interface DataItemIBLL
    {
        #region 数据字典分类
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        List<DataItemEntity> GetClassifyList();
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="keyword">关键词（名称/编码）</param>
        /// <param name="enabledMark">是否只取有效</param>
        /// <returns></returns>
        List<DataItemEntity> GetClassifyList(string keyword, bool enabledMark = true);
        /// <summary>
        /// 获取分类树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetClassifyTree();
        /// <summary>
        /// 判断分类编号是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemCode">编码</param>
        /// <returns></returns>
        bool ExistItemCode(string keyValue, string itemCode);
        /// <summary>
        /// 判断分类名称是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemName">名称</param>
        /// <returns></returns>
        bool ExistItemName(string keyValue, string itemName);
        /// <summary>
        /// 保存分类数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        void SaveClassifyEntity(string keyValue, DataItemEntity entity);
        /// <summary>
        /// 虚拟删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDeleteClassify(string keyValue);
        /// <summary>
        /// 通过编号获取字典分类实体
        /// </summary>
        /// <param name="itemCode">编码</param>
        /// <returns></returns>
        DataItemEntity GetClassifyEntityByCode(string itemCode);
        #endregion

        #region 字典明细
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        List<DataItemDetailEntity> GetDetailList(string itemCode);
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <param name="keyword">关键词（名称/值）</param>
        /// <returns></returns>
        List<DataItemDetailEntity> GetDetailList(string itemCode, string keyword);

        /// <summary>
        /// 获取数据字典详细映射数据
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Dictionary<string, DataItemModel>> GetModelMap();
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        List<DataItemDetailEntity> GetDetailListByParentId(string itemCode, string parentId);
        /// <summary>
        /// 获取字典明细树形数据
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        List<TreeModel> GetDetailTree(string itemCode);
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemValue">项目值</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        bool ExistDetailItemValue(string keyValue, string itemValue, string itemCode);
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemName">项目名</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        bool ExistDetailItemName(string keyValue, string itemName, string itemCode);
        /// <summary>
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        void SaveDetailEntity(string keyValue, DataItemDetailEntity entity);
        /// <summary>
        /// 虚拟删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDeleteDetail(string keyValue);
        #endregion
    }
}
