namespace Learun.Cache.Base
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.06
    /// 描 述：缓存库分配
    /// </summary>
    public static class CacheId
    {
        #region 0号库（基础信息）
        /// <summary>
        /// 功能模块
        /// </summary>
        public static long module { get { return 0; } }
        /// <summary>
        /// 数据库管理
        /// </summary>
        public static long database { get { return 0; } }
        /// <summary>
        /// 数据字典
        /// </summary>
        public static long dataItem { get { return 0; } }
        /// <summary>
        /// 行政区域信息
        /// </summary>
        public static long area { get { return 0; } }
        /// <summary>
        /// 编码规则
        /// </summary>
        public static long codeRule { get { return 0; } }
        /// <summary>
        /// 自定查询
        /// </summary>
        public static long custmerQuery { get { return 0; } }
        /// <summary>
        /// 数据源
        /// </summary>
        public static long dataSource { get { return 0; } }
        /// <summary>
        /// 时间过滤信息
        /// </summary>
        public static long filterTime { get { return 0; } }
        /// <summary>
        /// IP过滤信息
        /// </summary>
        public static long filterIP { get { return 0; } }
        /// <summary>
        /// 接口管理
        /// </summary>
        public static long Interface { get { return 0; } }
        #endregion

        #region 1号库（单位组织信息：公司，部门，岗位，角色，人员）
        /// <summary>
        /// 公司
        /// </summary>
        public static long company { get { return 1; } }
        /// <summary>
        /// 部门
        /// </summary>
        public static long department { get { return 1; } }
        /// <summary>
        /// 岗位
        /// </summary>
        public static long post { get { return 1; } }
        /// <summary>
        /// 角色
        /// </summary>
        public static long role { get { return 1; } }
        /// <summary>
        /// 人员对应关系
        /// </summary>
        public static long userRelation { get { return 1; } }
        /// <summary>
        /// 功能权限
        /// </summary>
        public static long authorize { get { return 1; } }
        /// <summary>
        /// 数据权限
        /// </summary>
        public static long dataAuthorize { get { return 1; } }
        #endregion

        #region 2号库 登录信息
        /// <summary>
        /// 登录信息
        /// </summary>
        public static long loginInfo { get { return 2; } }
        #endregion

        #region 3号库 附件上传（文件分片数据存储）
        /// <summary>
        /// 附件
        /// </summary>
        public static long annexes { get { return 3; } }
        /// <summary>
        /// excel导入
        /// </summary>
        public static long excel { get { return 3; } }
        #endregion

        #region 4号库(工作流)
        /// <summary>
        /// 工作流模板
        /// </summary>
        public static long workflow { get { return 4; } }
        /// <summary>
        /// 表单模板
        /// </summary>
        public static long formscheme { get { return 4; } }
        /// <summary>
        /// 表单与功能对应关系
        /// </summary>
        public static long formRelation { get { return 4; } }
        #endregion

        #region 5号库
        /// <summary>
        /// 人员
        /// </summary>
        public static long user { get { return 5; } }
        #endregion

        #region 6号库
        /// <summary>
        /// jscss
        /// </summary>
        public static long jscss { get { return 6; } }
        #endregion

        #region 7号库
        /// <summary>
        /// 语言包
        /// </summary>
        public static long language { get { return 7; } }
        #endregion

        #region 8号库
        #endregion

        #region 9号库
        #endregion

        #region 10号库
        #endregion

        #region 11号库
        #endregion

        #region 12号库
        #endregion

        #region 13号库
        #endregion

        #region 14号库
        #endregion

        #region 15号库
        #endregion

    }
}
