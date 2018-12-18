using Learun.Application.Base.SystemModule;
using Learun.Application.BaseModule.CodeGeneratorModule;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Learun.Application.Base.CodeGeneratorModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：代码生成器类
    /// </summary>
    public class CodeGenerator
    {
        private DatabaseTableIBLL databaseTableIBLL = new DatabaseTableBLL();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();


        #region 通用
        /// <summary>
        /// 注释头
        /// </summary>
        /// <param name="baseConfigModel">配置信息</param>
        /// <returns></returns>
        private string NotesCreate(CodeBaseConfigModel baseConfigModel) {
            UserInfo userInfo = LoginUserInfo.Get();

            StringBuilder sb = new StringBuilder();
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架\r\n");
            sb.Append("    /// Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + userInfo.realName + "\r\n");
            sb.Append("    /// 日 期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.describe + "\r\n");
            sb.Append("    /// </summary>\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 注释头
        /// </summary>
        /// <param name="baseConfigModel">配置信息</param>
        /// <returns></returns>
        private string NotesCreate(BaseModel baseInfo)
        {
            UserInfo userInfo = LoginUserInfo.Get();

            StringBuilder sb = new StringBuilder();
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架\r\n");
            sb.Append("    /// Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + userInfo.realName + "\r\n");
            sb.Append("    /// 日 期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
            sb.Append("    /// 描 述：" + baseInfo.describe + "\r\n");
            sb.Append("    /// </summary>\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 后端区域名
        /// </summary>
        /// <param name="strBackArea">后端区域</param>
        /// <returns></returns>
        private string getBackArea(string strBackArea)
        {
            if (string.IsNullOrEmpty(strBackArea))
            {
                return "";
            }
            else {
                return "." + strBackArea;
            }
        }
        #endregion

        #region 实体类
        /// <summary>
        /// 实体类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public string EntityCreate(CodeBaseConfigModel baseConfigModel,string tableName)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                string pkDataType = "";
                string pkName = "";

                string pk = "";
                string createUserId = "";
                string createUserName = "";
                string createDate = "";
                string modifyUserId = "";
                string modifyUserName = "";
                string modifyDate = "";

                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n");

                sb.Append("namespace " + baseConfigModel.backProject + getBackArea(baseConfigModel.backArea) + "\r\n\r\n");
                sb.Append("{\r\n");



                sb.Append(NotesCreate(baseConfigModel));
                sb.Append("    public class " + tableName + "Entity \r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 实体成员\r\n");

                #region 设置字段根据数据库字段
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(baseConfigModel.databaseLinkId, tableName);
                foreach (var field in fieldList)
                {
                    string datatype = databaseTableIBLL.FindModelsType(field.f_datatype);

                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + field.f_remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    if (field.f_key == "1" && (datatype == "int?" || datatype == "decimal?"))// 考虑到自增量
                    {
                        sb.Append("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                    sb.Append("        [Column(\"" + field.f_column.ToUpper() + "\")]\r\n");
                    sb.Append("        public " + datatype + " " + field.f_column + " { get; set; }\r\n");

                    #region 创建时间和修改时间
                    if (field.f_key == "1")
                    {
                        if (datatype == "string")
                        {
                            pk = "            this." + field.f_column + " = Guid.NewGuid().ToString();\r\n";
                        }
                        pkDataType = datatype;
                        pkName = field.f_column;
                    }
                    if (field.f_column == "F_CreateUserId")
                    {
                        createUserId = "            this.F_CreateUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_CreateUserName")
                    {
                        createUserName = "            this.F_CreateUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_CreateDate")
                    {
                        createDate = "            this.F_CreateDate = DateTime.Now;\r\n";
                    }

                    if (field.f_column == "F_ModifyUserId")
                    {
                        modifyUserId = "            this.F_ModifyUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_ModifyUserName")
                    {
                        modifyUserName = "            this.F_ModifyUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_ModifyDate")
                    {
                        modifyDate = "            this.F_ModifyDate = DateTime.Now;\r\n";
                    }
                    #endregion
                }


                #endregion

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 扩展操作\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 新增调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        public void Create()\r\n");
                sb.Append("        {\r\n");
                sb.Append(pk);
                sb.Append(createDate);
                if (!string.IsNullOrEmpty(createUserId) || !string.IsNullOrEmpty(createUserName))
                {
                    sb.Append("            UserInfo userInfo = LoginUserInfo.Get();\r\n");
                }
                sb.Append(createUserId);
                sb.Append(createUserName);
                sb.Append("        }\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 编辑调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
                sb.Append("        public void Modify(" + pkDataType + " keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            this." + pkName + " = keyValue;\r\n");
                sb.Append(modifyDate);
                if (!string.IsNullOrEmpty(modifyUserId) || !string.IsNullOrEmpty(modifyUserName))
                {
                    sb.Append("            UserInfo userInfo = LoginUserInfo.Get();\r\n");
                }
                sb.Append(modifyUserId);
                sb.Append(modifyUserName);
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 实体类创建(自定义开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">数据表</param>
        /// <param name="pkey">主键</param>
        /// <param name="baseInfo">基础信息</param>
        /// <returns></returns>
        public string EntityCreate(string databaseLinkId, string tableName, string pkey, BaseModel baseInfo, ColModel colDataObj, bool isMain)
        {

            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();


                StringBuilder sb = new StringBuilder();

                string pkDataType = "";
                string pkName = "";

                string pk = "";
                string createUserId = "";
                string createUserName = "";
                string createDate = "";
                string modifyUserId = "";
                string modifyUserName = "";
                string modifyDate = "";

                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");



                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + tableName + "Entity \r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 实体成员\r\n");

                Dictionary<string, string> fieldMap = new Dictionary<string, string>();

                #region 设置字段根据数据库字段
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, tableName);
                foreach (var field in fieldList)
                {
                    fieldMap.Add(field.f_column, field.f_column);

                    string datatype = databaseTableIBLL.FindModelsType(field.f_datatype);

                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + field.f_remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    if (field.f_key == "1" && (datatype == "int?" || datatype == "decimal?"))// 考虑到自增量
                    {
                        sb.Append("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                    sb.Append("        [Column(\"" + field.f_column.ToUpper() + "\")]\r\n");
                    sb.Append("        public " + datatype + " " + field.f_column + " { get; set; }\r\n");

                    #region 创建时间和修改时间
                    if (field.f_column == pkey)
                    {
                        if (datatype == "string")
                        {
                            pk = "            this." + field.f_column + " = Guid.NewGuid().ToString();\r\n";
                        }
                        pkDataType = datatype;
                        pkName = field.f_column;
                    }
                    if (field.f_column == "F_CreateUserId")
                    {
                        createUserId = "            this.F_CreateUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_CreateUserName")
                    {
                        createUserName = "            this.F_CreateUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_CreateDate")
                    {
                        createDate = "            this.F_CreateDate = DateTime.Now;\r\n";
                    }

                    if (field.f_column == "F_ModifyUserId")
                    {
                        modifyUserId = "            this.F_ModifyUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_ModifyUserName")
                    {
                        modifyUserName = "            this.F_ModifyUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_ModifyDate")
                    {
                        modifyDate = "            this.F_ModifyDate = DateTime.Now;\r\n";
                    }
                    #endregion
                }
                #endregion

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 扩展操作\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 新增调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        public void Create()\r\n");
                sb.Append("        {\r\n");
                sb.Append(pk);
                sb.Append(createDate);
                if (!string.IsNullOrEmpty(createUserId) || !string.IsNullOrEmpty(createUserName))
                {
                    sb.Append("            UserInfo userInfo = LoginUserInfo.Get();\r\n");
                }
                sb.Append(createUserId);
                sb.Append(createUserName);
                sb.Append("        }\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 编辑调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
                sb.Append("        public void Modify(" + pkDataType + " keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            this." + pkName + " = keyValue;\r\n");
                sb.Append(modifyDate);
                if (!string.IsNullOrEmpty(modifyUserId) || !string.IsNullOrEmpty(modifyUserName))
                {
                    sb.Append("            UserInfo userInfo = LoginUserInfo.Get();\r\n");
                }
                sb.Append(modifyUserId);
                sb.Append(modifyUserName);
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n");

                // 如果是主表需要增加额外字段
                if (isMain)
                {
                    sb.Append("        #region 扩展字段\r\n");

                    foreach (var col in colDataObj.fields)
                    {
                        if (!fieldMap.ContainsKey(col.field))
                        {
                            sb.Append("        /// <summary>\r\n");
                            sb.Append("        /// " + col.fieldName + "\r\n");
                            sb.Append("        /// </summary>\r\n");
                            sb.Append("        [NotMapped]\r\n");
                            sb.Append("        public string " + col.field + " { get; set; }\r\n");
                        }
                    }

                    sb.Append("        #endregion\r\n");
                }
               

                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 实体类创建(自定义开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">数据表</param>
        /// <param name="pkey">主键</param>
        /// <param name="baseInfo">基础信息</param>
        /// <param name="workField">流程关联字段</param>
        /// <returns></returns>
        public string WfEntityCreate(string databaseLinkId, string tableName, string pkey, BaseModel baseInfo, ColModel colDataObj, bool isMain, string workField)
        {

            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();


                StringBuilder sb = new StringBuilder();

                string pkDataType = "";
                string pkName = "";

                string pk = "";
                string createUserId = "";
                string createUserName = "";
                string createDate = "";
                string modifyUserId = "";
                string modifyUserName = "";
                string modifyDate = "";

                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");



                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + tableName + "Entity \r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 实体成员\r\n");

                Dictionary<string, string> fieldMap = new Dictionary<string, string>();

                #region 设置字段根据数据库字段
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, tableName);
                foreach (var field in fieldList)
                {
                    fieldMap.Add(field.f_column, field.f_column);

                    string datatype = databaseTableIBLL.FindModelsType(field.f_datatype);

                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + field.f_remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    if (field.f_key == "1" && (datatype == "int?" || datatype == "decimal?"))// 考虑到自增量
                    {
                        sb.Append("        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]");
                    }
                    sb.Append("        [Column(\"" + field.f_column.ToUpper() + "\")]\r\n");
                    sb.Append("        public " + datatype + " " + field.f_column + " { get; set; }\r\n");

                    #region 创建时间和修改时间
                    if (field.f_column == pkey)
                    {
                        if (datatype == "string" && (workField != field.f_column || !isMain))
                        {
                            pk = "            this." + field.f_column + " = Guid.NewGuid().ToString();\r\n";
                        }
                        pkDataType = datatype;
                        pkName = field.f_column;
                    }
                    if (field.f_column == "F_CreateUserId")
                    {
                        createUserId = "            this.F_CreateUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_CreateUserName")
                    {
                        createUserName = "            this.F_CreateUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_CreateDate")
                    {
                        createDate = "            this.F_CreateDate = DateTime.Now;\r\n";
                    }

                    if (field.f_column == "F_ModifyUserId")
                    {
                        modifyUserId = "            this.F_ModifyUserId = userInfo.userId;\r\n";
                    }
                    if (field.f_column == "F_ModifyUserName")
                    {
                        modifyUserName = "            this.F_ModifyUserName = userInfo.realName;\r\n";
                    }
                    if (field.f_column == "F_ModifyDate")
                    {
                        modifyDate = "            this.F_ModifyDate = DateTime.Now;\r\n";
                    }
                    #endregion
                }


                #endregion

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 扩展操作\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 新增调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        public void Create()\r\n");
                sb.Append("        {\r\n");
                sb.Append(pk);
                sb.Append(createDate);
                if (!string.IsNullOrEmpty(createUserId) || !string.IsNullOrEmpty(createUserName))
                {
                    sb.Append("            UserInfo userInfo = LoginUserInfo.Get();\r\n");
                }
                sb.Append(createUserId);
                sb.Append(createUserName);
                sb.Append("        }\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 编辑调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
                sb.Append("        public void Modify(" + pkDataType + " keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            this." + pkName + " = keyValue;\r\n");
                sb.Append(modifyDate);
                if (!string.IsNullOrEmpty(modifyUserId) || !string.IsNullOrEmpty(modifyUserName))
                {
                    sb.Append("            UserInfo userInfo = LoginUserInfo.Get();\r\n");
                }
                sb.Append(modifyUserId);
                sb.Append(modifyUserName);
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n");

                // 如果是主表需要增加额外字段
                if (isMain)
                {
                    sb.Append("        #region 扩展字段\r\n");

                    foreach (var col in colDataObj.fields)
                    {
                        if (!fieldMap.ContainsKey(col.field))
                        {
                            sb.Append("        /// <summary>\r\n");
                            sb.Append("        /// " + col.fieldName + "\r\n");
                            sb.Append("        /// </summary>\r\n");
                            sb.Append("        [NotMapped]\r\n");
                            sb.Append("        public string " + col.field + " { get; set; }\r\n");
                        }
                    }

                    sb.Append("        #endregion\r\n");
                }


                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 实体映射类
        /// <summary>
        /// 创建实体映射类（EF必须）
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string MappingCreate(CodeBaseConfigModel baseConfigModel, string tableName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("using " + baseConfigModel.backProject + getBackArea(baseConfigModel.backArea) + ";\r\n");
                sb.Append("using System.Data.Entity.ModelConfiguration;\r\n\r\n");

                sb.Append("namespace  Learun.Application.Mapping\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseConfigModel));
                sb.Append("    public class " + tableName + "Map : EntityTypeConfiguration<" + tableName + "Entity>\r\n");
                sb.Append("    {\r\n");
                sb.Append("        public " + tableName + "Map()\r\n");
                sb.Append("        {\r\n");
                sb.Append("            #region 表、主键\r\n");
                sb.Append("            //表\r\n");
                sb.Append("            this.ToTable(\"" + tableName.ToUpper() + "\");\r\n");
                sb.Append("            //主键\r\n");
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(baseConfigModel.databaseLinkId, tableName);
                foreach (var field in fieldList)
                {
                    if (field.f_key == "1")
                    {
                        sb.Append("            this.HasKey(t => t." + field.f_column + ");\r\n");
                    }
                }
                sb.Append("            #endregion\r\n\r\n");

                sb.Append("            #region 配置关系\r\n");
                sb.Append("            #endregion\r\n");
                sb.Append("        }\r\n");
                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建实体映射类（EF必须）(自定义开发模板)
        /// </summary>
        /// <param name="tableName">数据表</param>
        /// <param name="pkey">主键</param>
        /// <param name="baseInfo">基础信息</param>
        /// <returns></returns>
        public string MappingCreate(string tableName, string pkey, BaseModel baseInfo)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Data.Entity.ModelConfiguration;\r\n\r\n");

                sb.Append("namespace  Learun.Application.Mapping\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + tableName + "Map : EntityTypeConfiguration<" + tableName + "Entity>\r\n");
                sb.Append("    {\r\n");
                sb.Append("        public " + tableName + "Map()\r\n");
                sb.Append("        {\r\n");
                sb.Append("            #region 表、主键\r\n");
                sb.Append("            //表\r\n");
                sb.Append("            this.ToTable(\"" + tableName.ToUpper() + "\");\r\n");
                sb.Append("            //主键\r\n");
                sb.Append("            this.HasKey(t => t." + pkey + ");\r\n");
                sb.Append("            #endregion\r\n\r\n");

                sb.Append("            #region 配置关系\r\n");
                sb.Append("            #endregion\r\n");
                sb.Append("        }\r\n");
                sb.Append("    }\r\n");
                sb.Append("}\r\n\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 服务类
        /// <summary>
        /// 获取服务类函数体字串
        /// </summary>
        /// <param name="content">函数功能内容</param>
        /// <returns></returns>
        private string getServiceTry(string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append(content);
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception ex)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                if (ex is ExceptionEx)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw;\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw ExceptionEx.ThrowServiceException(ex);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 获取服务类函数体字串(事务)
        /// </summary>
        /// <param name="content">函数功能内容</param>
        /// <returns></returns>
        private string getTransServiceTry(string content, string dbname)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("        {\r\n");
            sb.Append("            var db = this.BaseRepository(" + dbname + ").BeginTrans();\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append(content);
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception ex)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                db.Rollback();\r\n");
            sb.Append("                if (ex is ExceptionEx)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw;\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw ExceptionEx.ThrowServiceException(ex);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 设置左关联递归表
        /// </summary>
        /// <param name="queryDbTableMap">需要查询的表数据</param>
        /// <param name="queryDbTableIndex">需要查询的表数据（下标）</param>
        /// <param name="dbTableMap">所有表数据</param>
        /// <param name="tableName">表名</param>
        private void SetLeftTable(Dictionary<string, DbTableModel> queryDbTableMap, Dictionary<string, int> queryDbTableIndex, Dictionary<string, DbTableModel> dbTableMap, string tableName, string mainTable)
        {
            if (!string.IsNullOrEmpty(tableName) && !queryDbTableMap.ContainsKey(tableName))
            {
                queryDbTableMap.Add(tableName, dbTableMap[tableName]);
                queryDbTableIndex.Add(tableName, queryDbTableMap.Count);
                if (!string.IsNullOrEmpty(dbTableMap[tableName].relationName) && dbTableMap[tableName].relationName != mainTable)
                {
                    SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, dbTableMap[tableName].relationName, mainTable);
                }
            }
        }

        /// <summary>
        /// 获取删除数据关联表数据
        /// </summary>
        /// <param name="TableTree">当前表数据</param>
        /// <param name="pDbTable">父级表数据</param>
        /// <param name="content">拼接代码内容</param>
        /// <param name="mainTable">主表名称</param>
        private string DeleteToSelectSql(List<TreeModelEx<DbTableModel>> TableTree, DbTableModel pDbTable,string mainTable)
        {
            string content = "";
            foreach (var tree in TableTree)
            {
                if (tree.ChildNodes.Count > 0)
                {
                    if (tree.parentId == "0" || pDbTable == null)
                    {
                        content += "                var " + Str.FirstLower(tree.data.name) + "Entity = Get" + tree.data.name + "Entity(keyValue); \r\n";
                    }
                    else
                    {
                        content += "                var " + Str.FirstLower(tree.data.name) + "Entity = Get" + tree.data.name + "Entity(" + Str.FirstLower(pDbTable.name) + "Entity." + tree.data.relationField + "); \r\n";
                    }
                }
               content += DeleteToSelectSql(tree.ChildNodes, tree.data, mainTable);
            }
            return content;
        }
        /// <summary>
        /// 获取更新数据关联表数据
        /// </summary>
        /// <param name="TableTree">当前表数据</param>
        /// <param name="pDbTable">父级表数据</param>
        /// <param name="content">拼接代码内容</param>
        /// <param name="mainTable">主表名称</param>
        private string UpdateToSelectSql(List<TreeModelEx<DbTableModel>> TableTree, DbTableModel pDbTable, string mainTable)
        {
            string content = "";
            foreach (var tree in TableTree)
            {
                if (tree.ChildNodes.Count > 0)
                {
                    if (tree.parentId == "0" || pDbTable == null)
                    {
                        content += "                    var " + Str.FirstLower(tree.data.name) + "EntityTmp = Get" + tree.data.name + "Entity(keyValue); \r\n";
                    }
                    else
                    {
                        content += "                    var " + Str.FirstLower(tree.data.name) + "EntityTmp = Get" + tree.data.name + "Entity(" + Str.FirstLower(pDbTable.name) + "Entity." + tree.data.relationField + "); \r\n";
                    }
                }
               content += UpdateToSelectSql(tree.ChildNodes, tree.data, mainTable);
            }
            return content;
        }

        private Dictionary<string, string> InsertGuidMap = new Dictionary<string, string>();
        /// <summary>
        /// 获取新增数据关联表数据
        /// </summary>
        /// <param name="TableTree">当前表数据</param>
        /// <param name="content">拼接代码内容</param>
        /// <param name="mainTable">主表名称</param>
        /// <param name="mainPk">主表主键</param>
        private void InsertToSelectSql(List<TreeModelEx<DbTableModel>> TableTree, string content, string mainTable, string mainPk)
        {
            foreach (var tree in TableTree)
            {
                if (!string.IsNullOrEmpty(tree.data.relationName))
                {
                    if (tree.data.relationName != mainTable || tree.data.relationField != mainPk)
                    {
                        if (!InsertGuidMap.ContainsKey(tree.data.relationName + "|" + tree.data.relationField))
                        {
                            InsertGuidMap.Add(tree.data.relationName + "|" + tree.data.relationField, "1");
                            content += "                var " + Str.FirstLower(tree.data.relationName) + "Entity." + tree.data.relationField + " = Guid.NewGuid().ToString(); \r\n";
                        }
                    }
                }
                InsertToSelectSql(tree.ChildNodes, content, mainTable, mainPk);
            }
        }
        /// <summary>
        /// 服务类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string ServiceCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                // 添加数据库配置
                string dbname = "";
                DatabaseLinkEntity dbEntity = databaseLinkIBLL.GetEntity(baseConfigModel.databaseLinkId);
                string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
                if (connectionString != dbEntity.F_DbConnection)
                {
                    if (ConfigurationManager.ConnectionStrings[dbEntity.F_DBName] == null)
                    {
                        string providerName = "System.Data.SqlClient";
                        if (dbEntity.F_DbType == "MySql")
                        {
                            providerName = "MySql.Data.MySqlClient";
                        }
                        else if (dbEntity.F_DbType == "Oracle")
                        {
                            providerName = "Oracle.ManagedDataAccess.Client";
                        }
                        Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dbEntity.F_DBName, dbEntity.F_DbConnection, providerName);
                    }
                    dbname = "\"" + dbEntity.F_DBName + "\"";
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("using Dapper;\r\n");
                sb.Append("using Learun.DataBase.Repository;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Text;\r\n\r\n");
                

                sb.Append("namespace " + baseConfigModel.backProject + getBackArea(baseConfigModel.backArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseConfigModel));
                sb.Append("    public class " + baseConfigModel.name + "Service : RepositoryFactory\r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 构造函数和属性\r\n\r\n");
                sb.Append("        private string fieldSql;\r\n");
                sb.Append("        public " + baseConfigModel.name + "Service()\r\n");
                sb.Append("        {\r\n");
                sb.Append("            fieldSql=@\"\r\n");
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(baseConfigModel.databaseLinkId, baseConfigModel.tableName);
                string filedsql = "";
                string pk = "";
                foreach (var field in fieldList)
                {
                    if (filedsql != "")
                    {
                        filedsql += ",\r\n";
                    }
                    filedsql += "                t." + field.f_column;
                    if (field.f_key == "1")
                    {
                        pk = field.f_column;
                    }
                }
                sb.Append(filedsql + "\r\n            \";\r\n");
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 获取数据\r\n\r\n");
                string content = "";
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.tableName + "Entity> GetList( string queryJson )\r\n");
                content = "";

                content += "                //参考写法\r\n";
                content += "                //var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                //var dp = new DynamicParameters(new { });\r\n";
                content += "                //dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";

                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(fieldSql);\r\n";
                content += "                strSql.Append(\" FROM " + baseConfigModel.tableName + " t \");\r\n";
                content += "                return this.BaseRepository(" + dbname + ").FindList<" + baseConfigModel.tableName + "Entity>(strSql.ToString());\r\n";
                sb.Append(getServiceTry(content));
                // 获取列表分页数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表分页数据\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.tableName + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(fieldSql);\r\n";
                content += "                strSql.Append(\" FROM " + baseConfigModel.tableName + " t \");\r\n";
                content += "                return this.BaseRepository(" + dbname + ").FindList<" + baseConfigModel.tableName + "Entity>(strSql.ToString(), pagination);\r\n";
                sb.Append(getServiceTry(content));
                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public " + baseConfigModel.tableName + "Entity GetEntity(string keyValue)\r\n");
                content = "";
                content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + baseConfigModel.tableName + "Entity>(keyValue);\r\n";
                sb.Append(getServiceTry(content));
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                this.BaseRepository(" + dbname + ").Delete<" + baseConfigModel.tableName + "Entity>(t=>t." + pk + " == keyValue);\r\n";
                sb.Append(getServiceTry(content));
                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void SaveEntity(string keyValue, " + baseConfigModel.tableName + "Entity entity)\r\n");
                content = "";
                content += "                if (!string.IsNullOrEmpty(keyValue))\r\n";
                content += "                {\r\n";
                content += "                    entity.Modify(keyValue);\r\n";
                content += "                    this.BaseRepository(" + dbname + ").Update(entity);\r\n";
                content += "                }\r\n";
                content += "                else\r\n";
                content += "                {\r\n";
                content += "                    entity.Create();\r\n";
                content += "                    this.BaseRepository(" + dbname + ").Insert(entity);\r\n";
                content += "                }\r\n";
                sb.Append(getServiceTry(content));

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 服务类创建(自定义开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接地址主键</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string ServiceCreate(string databaseLinkId, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData, BaseModel baseInfo)
        {
            try
            {
                #region 添加数据库配置
                string dbname = "";
                DatabaseLinkEntity dbEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
                if (connectionString != dbEntity.F_DbConnection)
                {
                    if (ConfigurationManager.ConnectionStrings[dbEntity.F_DBName] == null)
                    {
                        string providerName = "System.Data.SqlClient";
                        if (dbEntity.F_DbType == "MySql")
                        {
                            providerName = "MySql.Data.MySqlClient";
                        }
                        else if (dbEntity.F_DbType == "Oracle")
                        {
                            providerName = "Oracle.ManagedDataAccess.Client";
                        }
                        Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dbEntity.F_DBName, dbEntity.F_DbConnection, providerName);
                    }
                    dbname = "\"" + dbEntity.F_DBName + "\"";
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Dapper;\r\n");
                sb.Append("using Learun.DataBase.Repository;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Text;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Service : RepositoryFactory\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }

                // 列表数据
                string querySqlField = "                t." + mainPkey;                                         // 查询数据字段
                Dictionary<string, DbTableModel> queryDbTableMap = new Dictionary<string, DbTableModel>();      // 需要查询的表
                Dictionary<string, int> queryDbTableIndex = new Dictionary<string, int>();
                foreach (var col in colData.fields)
                {
                    string tableName = compontMap[col.id].table;

                    if (querySqlField != "")
                    {
                        querySqlField += ",\r\n";
                    }

                    if (tableName == mainTable)
                    {
                        querySqlField += "                t." + col.field;
                    }
                    else
                    {
                        SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, tableName, mainTable);// 添加左查询关联表
                        querySqlField += "                t" + queryDbTableIndex[tableName].ToString() + "." + col.field;
                    }
                }
                if (string.IsNullOrEmpty(querySqlField))
                {
                    IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, mainTable);
                    foreach (var field in fieldList)
                    {
                        if (querySqlField != "")
                        {
                            querySqlField += ",\r\n";
                        }
                        querySqlField += "                t." + field.f_column;
                    }
                }
                #endregion

                #region 数据查询
                // 查询条件数据
                foreach (var queryFiled in queryData.fields)
                {
                    string tableName = compontMap[queryFiled.id].table;
                    if (tableName != mainTable)
                    {
                        SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, tableName, mainTable);// 添加左查询关联表
                    }
                }

                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                }
                else
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetList(string queryJson)\r\n");
                }

                string content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(@\"\r\n" + querySqlField + "\r\n                \");\r\n";
                content += "                strSql.Append(\"  FROM " + mainTable + " t \");\r\n";

                foreach (var key in queryDbTableMap.Keys)
                {
                    string ct = "t" + queryDbTableIndex[key].ToString();
                    string pt = "t";
                    if (queryDbTableMap[key].relationName != mainTable)
                    {
                        pt = "t" + queryDbTableIndex[key].ToString();
                    }
                    content += "                strSql.Append(\"  LEFT JOIN " + queryDbTableMap[key].name + " " + ct + " ON " + ct + "." + queryDbTableMap[key].field + " = " + pt + "." + queryDbTableMap[key].relationField + " \");\r\n";
                }
                // 条件查询设置
                content += "                strSql.Append(\"  WHERE 1=1 \");\r\n";
                // 时间查询
                content += "                var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                var dp = new DynamicParameters(new { });\r\n";
                if (queryData.isDate == "1" && !string.IsNullOrEmpty(queryData.DateField))
                {
                    content += "                if (!queryParam[\"StartTime\"].IsEmpty() && !queryParam[\"EndTime\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    dp.Add(\"endTime\", queryParam[\"EndTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    strSql.Append(\" AND ( t." + queryData.DateField + " >= @startTime AND t." + queryData.DateField + " <= @endTime ) \");\r\n";
                    content += "                }\r\n";
                }

                foreach (var queryFiled in queryData.fields)
                {
                    content += "                if (!queryParam[\"" + compontMap[queryFiled.id].field + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    if (compontMap[queryFiled.id].type == "text" || compontMap[queryFiled.id].type == "textarea" || compontMap[queryFiled.id].type == "texteditor" || compontMap[queryFiled.id].type == "encode")
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\", \"%\" + queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString() + \"%\", DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                    }
                    else
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\",queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString(), DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";
                        }
                    }
                    content += "                }\r\n";
                }

                if (colData.isPage == "1") // 分页
                {
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + mainTable + "Entity>(strSql.ToString(),dp, pagination);\r\n";
                }
                else
                {
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + mainTable + "Entity>(strSql.ToString(),dp);\r\n";
                }
                sb.Append(getServiceTry(content));

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue)\r\n");
                    content = "";
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == keyValue );\r\n";
                    sb.Append(getServiceTry(content));
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue)\r\n");
                    content = "";
                    if (tableOne.name == mainTable)
                    {
                        content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + tableOne.name + "Entity>(keyValue);\r\n";
                    }
                    else
                    {
                        content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == keyValue);\r\n";
                    }
                    sb.Append(getServiceTry(content));
                }
               
                // 获取树形数据列表
                if (colData.isTree == "1" && colData.treeSource == "2") {
                    content = "";
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取树形数据\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public DataTable GetSqlTree()\r\n");
                    content = "                return this.BaseRepository().FindTable(\" " + colData.treeSql + " \");\r\n";
                    sb.Append(getServiceTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                if (dbTableList.Count == 1)
                {
                    content += "                this.BaseRepository(" + dbname + ").Delete<" + mainTable + "Entity>(t=>t." + mainPkey + " == keyValue);\r\n";
                    sb.Append(getServiceTry(content));
                }
                else
                {
                    content += DeleteToSelectSql(TableTree, null, mainTable);
                    foreach (var tableOne in dbTableList)
                    {
                        if (tableOne.name != mainTable)// 关联的表不是主表
                        {
                            content += "                db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "Entity." + tableOne.relationField + ");\r\n";
                        }
                        else
                        {
                            content += "                db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.pk + " == keyValue);\r\n";
                        }
                    }
                    content += "                db.Commit();\r\n";
                    sb.Append(getTransServiceTry(content, dbname));
                }

                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "string keyValue, " + mainTable + "Entity entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                sb.Append("        public void SaveEntity(" + paramStr + ")\r\n");
                content = "";
                content += "                if (!string.IsNullOrEmpty(keyValue))\r\n";
                content += "                {\r\n";

                // 更新
                if (dbTableList.Count == 1)
                {
                    content += "                    entity.Modify(keyValue);\r\n";
                    content += "                    this.BaseRepository(" + dbname + ").Update(entity);\r\n";

                }
                else
                {
                    content += UpdateToSelectSql(TableTree, null, mainTable);
                    content += "                    entity.Modify(keyValue);\r\n";
                    content += "                    db.Update(entity);\r\n";

                    foreach (var tableOne in dbTableList)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) != null)
                        {
                            content += "                    db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ");\r\n";
                            // 如果是编辑表格数据
                            content += "                    foreach (" + tableOne.name + "Entity item in " + Str.FirstLower(tableOne.name) + "List)\r\n";
                            content += "                    {\r\n";
                            content += "                        item.Create();\r\n";
                            content += "                        item." + tableOne.field + " = " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ";\r\n";
                            content += "                        db.Insert(item);\r\n";
                            content += "                    }\r\n";
                        }
                        else if (tableOne.name != mainTable)// 不是
                        {
                            content += "                    db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ");\r\n";

                            content += "                    " + Str.FirstLower(tableOne.name) + "Entity.Create();\r\n";
                            content += "                    " + Str.FirstLower(tableOne.name) + "Entity." + tableOne.field + " = " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ";\r\n";
                            content += "                    db.Insert(" + Str.FirstLower(tableOne.name) + "Entity);\r\n";
                        }
                    }
                }


                content += "                }\r\n";
                content += "                else\r\n";
                content += "                {\r\n";

                // 新增
                if (dbTableList.Count == 1)
                {
                    content += "                    entity.Create();\r\n";
                    content += "                    this.BaseRepository(" + dbname + ").Insert(entity);\r\n";
                }
                else
                {
                    content += "                    entity.Create();\r\n";
                    content += "                    db.Insert(entity);\r\n";

                    InsertToSelectSql(TableTree, content, mainTable, mainPkey);

                    foreach (var tableOne in dbTableList)
                    {
                        if (tableOne.name != mainTable)
                        {
                            string entityName = Str.FirstLower(tableOne.relationName) + "Entity.";
                            if (tableOne.relationName == mainTable)
                            {
                                entityName = "entity.";
                            }

                            if (girdDbTableList.Find(t => t.name == tableOne.name) != null)
                            {
                                // 如果是编辑表格数据
                                content += "                    foreach (" + tableOne.name + "Entity item in " + Str.FirstLower(tableOne.name) + "List)\r\n";
                                content += "                    {\r\n";
                                content += "                        item.Create();\r\n";
                                content += "                        item." + tableOne.field + " = " + entityName + tableOne.relationField + ";\r\n";
                                content += "                        db.Insert(item);\r\n";
                                content += "                    }\r\n";
                            }
                            else if (tableOne.name != mainTable)// 不是
                            {
                                content += "                    " + Str.FirstLower(tableOne.name) + "Entity.Create();\r\n";
                                content += "                    " + Str.FirstLower(tableOne.name) + "Entity." + tableOne.field + " = " + entityName + tableOne.relationField + ";\r\n";
                                content += "                    db.Insert(" + Str.FirstLower(tableOne.name) + "Entity);\r\n";
                            }
                        }
                    }


                }
                content += "                }\r\n";

                if (dbTableList.Count > 1)
                {
                    content += "                db.Commit();\r\n";
                }


                if (dbTableList.Count == 1)
                {
                    sb.Append(getServiceTry(content));
                }
                else
                {
                    sb.Append(getTransServiceTry(content, dbname));
                }

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 服务类创建(流程系统表单开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接地址主键</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="workField">流程关联字段</param>
        /// <returns></returns>
        public string WfServiceCreate(string databaseLinkId, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData, BaseModel baseInfo, string workField)
        {
            try
            {
                #region 添加数据库配置
                string dbname = "";
                DatabaseLinkEntity dbEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
                if (connectionString != dbEntity.F_DbConnection)
                {
                    if (ConfigurationManager.ConnectionStrings[dbEntity.F_DBName] == null)
                    {
                        string providerName = "System.Data.SqlClient";
                        if (dbEntity.F_DbType == "MySql")
                        {
                            providerName = "MySql.Data.MySqlClient";
                        }
                        else if (dbEntity.F_DbType == "Oracle")
                        {
                            providerName = "Oracle.ManagedDataAccess.Client";
                        }
                        Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dbEntity.F_DBName, dbEntity.F_DbConnection, providerName);
                    }
                    dbname = "\"" + dbEntity.F_DBName + "\"";
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Dapper;\r\n");
                sb.Append("using Learun.DataBase.Repository;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Text;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Service : RepositoryFactory\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }

                // 列表数据
                string querySqlField = "                t." + mainPkey;                                         // 查询数据字段
                Dictionary<string, DbTableModel> queryDbTableMap = new Dictionary<string, DbTableModel>();      // 需要查询的表
                Dictionary<string, int> queryDbTableIndex = new Dictionary<string, int>();
                foreach (var col in colData.fields)
                {
                    string tableName = compontMap[col.id].table;

                    if (querySqlField != "")
                    {
                        querySqlField += ",\r\n";
                    }

                    if (tableName == mainTable)
                    {
                        querySqlField += "                t." + col.field;
                    }
                    else
                    {
                        SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, tableName, mainTable);// 添加左查询关联表
                        querySqlField += "                t" + queryDbTableIndex[tableName].ToString() + "." + col.field;
                    }
                }
                if (string.IsNullOrEmpty(querySqlField))
                {
                    IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, mainTable);
                    foreach (var field in fieldList)
                    {
                        if (querySqlField != "")
                        {
                            querySqlField += ",\r\n";
                        }
                        querySqlField += "                t." + field.f_column;
                    }
                }
                #endregion

                #region 数据查询
                // 查询条件数据
                foreach (var queryFiled in queryData.fields)
                {
                    string tableName = compontMap[queryFiled.id].table;
                    if (tableName != mainTable)
                    {
                        SetLeftTable(queryDbTableMap, queryDbTableIndex, dbTableMap, tableName, mainTable);// 添加左查询关联表
                    }
                }

                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                }
                else
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetList(string queryJson)\r\n");
                }

                string content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(@\"\r\n" + querySqlField + "\r\n                \");\r\n";
                content += "                strSql.Append(\"  FROM " + mainTable + " t \");\r\n";

                foreach (var key in queryDbTableMap.Keys)
                {
                    string ct = "t" + queryDbTableIndex[key].ToString();
                    string pt = "t";
                    if (queryDbTableMap[key].relationName != mainTable)
                    {
                        pt = "t" + queryDbTableIndex[key].ToString();
                    }
                    content += "                strSql.Append(\"  LEFT JOIN " + queryDbTableMap[key].name + " " + ct + " ON " + ct + "." + queryDbTableMap[key].field + " = " + pt + "." + queryDbTableMap[key].relationField + " \");\r\n";
                }
                // 条件查询设置
                content += "                strSql.Append(\"  WHERE 1=1 \");\r\n";
                // 时间查询
                content += "                var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                var dp = new DynamicParameters(new { });\r\n";
                if (queryData.isDate == "1" && !string.IsNullOrEmpty(queryData.DateField))
                {
                    content += "                if (!queryParam[\"StartTime\"].IsEmpty() && !queryParam[\"EndTime\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    dp.Add(\"endTime\", queryParam[\"EndTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    strSql.Append(\" AND ( t." + queryData.DateField + " >= @startTime AND t." + queryData.DateField + " <= @endTime ) \");\r\n";
                    content += "                }\r\n";
                }

                foreach (var queryFiled in queryData.fields)
                {
                    content += "                if (!queryParam[\"" + compontMap[queryFiled.id].field + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    if (compontMap[queryFiled.id].type == "text" || compontMap[queryFiled.id].type == "textarea" || compontMap[queryFiled.id].type == "texteditor" || compontMap[queryFiled.id].type == "encode")
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\", \"%\" + queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString() + \"%\", DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                    }
                    else
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\",queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString(), DbType.String);\r\n";
                        if (compontMap[queryFiled.id].table == mainTable)
                        {
                            content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";

                        }
                        else
                        {
                            content += "                    strSql.Append(\" AND t" + queryDbTableIndex[compontMap[queryFiled.id].table] + "." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";
                        }
                    }
                    content += "                }\r\n";
                }

                if (colData.isPage == "1") // 分页
                {
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + mainTable + "Entity>(strSql.ToString(),dp, pagination);\r\n";
                }
                else
                {
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + mainTable + "Entity>(strSql.ToString(),dp);\r\n";
                }
                sb.Append(getServiceTry(content));

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue)\r\n");
                    content = "";
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == keyValue );\r\n";
                    sb.Append(getServiceTry(content));
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue)\r\n");
                    content = "";
                    if (tableOne.name == mainTable)
                    {
                        content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + tableOne.name + "Entity>(keyValue);\r\n";
                    }
                    else
                    {
                        content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == keyValue);\r\n";
                    }
                    sb.Append(getServiceTry(content));
                }

                // 根据流程实例ID获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取主表实体数据\r\n");
                sb.Append("        /// <param name=\"processId\">流程实例ID</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public " + mainTable + "Entity GetEntityByProcessId(string processId)\r\n");
                content = "";
                content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + mainTable + "Entity>(t=>t." + workField + " == processId);\r\n";
                sb.Append(getServiceTry(content));

                // 获取树形数据列表
                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    content = "";
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取树形数据\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public DataTable GetSqlTree()\r\n");
                    content = "                return this.BaseRepository().FindTable(\" " + colData.treeSql + " \");\r\n";
                    sb.Append(getServiceTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                if (dbTableList.Count == 1)
                {
                    content += "                this.BaseRepository(" + dbname + ").Delete<" + mainTable + "Entity>(t=>t." + mainPkey + " == keyValue);\r\n";
                    sb.Append(getServiceTry(content));
                }
                else
                {
                    content += DeleteToSelectSql(TableTree, null, mainTable);
                    foreach (var tableOne in dbTableList)
                    {
                        if (tableOne.name != mainTable)// 关联的表不是主表
                        {
                            content += "                db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "Entity." + tableOne.relationField + ");\r\n";
                        }
                        else
                        {
                            content += "                db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.pk + " == keyValue);\r\n";
                        }
                    }
                    content += "                db.Commit();\r\n";
                    sb.Append(getTransServiceTry(content, dbname));
                }

                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "string keyValue, " + mainTable + "Entity entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                sb.Append("        public void SaveEntity(" + paramStr + ")\r\n");
                content = "";
                content += "                if (!string.IsNullOrEmpty(keyValue))\r\n";
                content += "                {\r\n";

                // 更新
                if (dbTableList.Count == 1)
                {
                    content += "                    entity.Modify(keyValue);\r\n";
                    content += "                    this.BaseRepository(" + dbname + ").Update(entity);\r\n";

                }
                else
                {
                    content += UpdateToSelectSql(TableTree, null, mainTable);
                    content += "                    entity.Modify(keyValue);\r\n";
                    content += "                    db.Update(entity);\r\n";

                    foreach (var tableOne in dbTableList)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) != null)
                        {
                            content += "                    db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ");\r\n";
                            // 如果是编辑表格数据
                            content += "                    foreach (" + tableOne.name + "Entity item in " + Str.FirstLower(tableOne.name) + "List)\r\n";
                            content += "                    {\r\n";
                            content += "                        item.Create();\r\n";
                            content += "                        item." + tableOne.field + " = " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ";\r\n";
                            content += "                        db.Insert(item);\r\n";
                            content += "                    }\r\n";
                        }
                        else if (tableOne.name != mainTable)// 不是
                        {
                            content += "                    db.Delete<" + tableOne.name + "Entity>(t=>t." + tableOne.field + " == " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ");\r\n";

                            content += "                    " + Str.FirstLower(tableOne.name) + "Entity.Create();\r\n";
                            content += "                    " + Str.FirstLower(tableOne.name) + "Entity." + tableOne.field + " = " + Str.FirstLower(tableOne.relationName) + "EntityTmp." + tableOne.relationField + ";\r\n";
                            content += "                    db.Insert(" + Str.FirstLower(tableOne.name) + "Entity);\r\n";
                        }
                    }
                }


                content += "                }\r\n";
                content += "                else\r\n";
                content += "                {\r\n";

                // 新增
                if (dbTableList.Count == 1)
                {
                    content += "                    entity.Create();\r\n";
                    content += "                    this.BaseRepository(" + dbname + ").Insert(entity);\r\n";
                }
                else
                {
                    content += "                    entity.Create();\r\n";
                    content += "                    db.Insert(entity);\r\n";

                    InsertToSelectSql(TableTree, content, mainTable, mainPkey);

                    foreach (var tableOne in dbTableList)
                    {
                        if (tableOne.name != mainTable)
                        {
                            string entityName = Str.FirstLower(tableOne.relationName) + "Entity.";
                            if (tableOne.relationName == mainTable)
                            {
                                entityName = "entity.";
                            }

                            if (girdDbTableList.Find(t => t.name == tableOne.name) != null)
                            {
                                // 如果是编辑表格数据
                                content += "                    foreach (" + tableOne.name + "Entity item in " + Str.FirstLower(tableOne.name) + "List)\r\n";
                                content += "                    {\r\n";
                                content += "                        item.Create();\r\n";
                                content += "                        item." + tableOne.field + " = " + entityName + tableOne.relationField + ";\r\n";
                                content += "                        db.Insert(item);\r\n";
                                content += "                    }\r\n";
                            }
                            else if (tableOne.name != mainTable)// 不是
                            {
                                content += "                    " + Str.FirstLower(tableOne.name) + "Entity.Create();\r\n";
                                content += "                    " + Str.FirstLower(tableOne.name) + "Entity." + tableOne.field + " = " + entityName + tableOne.relationField + ";\r\n";
                                content += "                    db.Insert(" + Str.FirstLower(tableOne.name) + "Entity);\r\n";
                            }
                        }
                    }


                }
                content += "                }\r\n";

                if (dbTableList.Count > 1)
                {
                    content += "                db.Commit();\r\n";
                }


                if (dbTableList.Count == 1)
                {
                    sb.Append(getServiceTry(content));
                }
                else
                {
                    sb.Append(getTransServiceTry(content, dbname));
                }

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 服务类创建(流程系统表单开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接地址主键</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string GridEditServiceCreate(string databaseLinkId,string dbTable,string dbTablePk, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData, BaseModel baseInfo)
        {
            try
            {
                #region 添加数据库配置
                string dbname = "";
                DatabaseLinkEntity dbEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
                if (connectionString != dbEntity.F_DbConnection)
                {
                    if (ConfigurationManager.ConnectionStrings[dbEntity.F_DBName] == null)
                    {
                        string providerName = "System.Data.SqlClient";
                        if (dbEntity.F_DbType == "MySql")
                        {
                            providerName = "MySql.Data.MySqlClient";
                        }
                        else if (dbEntity.F_DbType == "Oracle")
                        {
                            providerName = "Oracle.ManagedDataAccess.Client";
                        }
                        Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dbEntity.F_DBName, dbEntity.F_DbConnection, providerName);
                    }
                    dbname = "\"" + dbEntity.F_DBName + "\"";
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Dapper;\r\n");
                sb.Append("using Learun.DataBase.Repository;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Text;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Service : RepositoryFactory\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 传入参数数据处理
                // 列表数据
                string querySqlField = "";                                         // 查询数据字段
                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(databaseLinkId, dbTable);
                foreach (var field in fieldList)
                {
                    if (querySqlField != "")
                    {
                        querySqlField += ",\r\n";
                    }
                    querySqlField += "                t." + field.f_column;
                }
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public IEnumerable<" + dbTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                }
                else
                {
                    sb.Append("        public IEnumerable<" + dbTable + "Entity> GetList(string queryJson)\r\n");
                }

                string content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT \");\r\n";
                content += "                strSql.Append(@\"\r\n" + querySqlField + "\r\n                \");\r\n";
                content += "                strSql.Append(\"  FROM " + dbTable + " t \");\r\n";

                // 条件查询设置
                content += "                strSql.Append(\"  WHERE 1=1 \");\r\n";
                // 时间查询
                content += "                var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                var dp = new DynamicParameters(new { });\r\n";
                if (queryData.isDate == "1" && !string.IsNullOrEmpty(queryData.DateField))
                {
                    content += "                if (!queryParam[\"StartTime\"].IsEmpty() && !queryParam[\"EndTime\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    dp.Add(\"endTime\", queryParam[\"EndTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    strSql.Append(\" AND ( t." + queryData.DateField + " >= @startTime AND t." + queryData.DateField + " <= @endTime ) \");\r\n";
                    content += "                }\r\n";
                }

                foreach (var queryFiled in queryData.fields)
                {
                    content += "                if (!queryParam[\"" + compontMap[queryFiled.id].field + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    if (compontMap[queryFiled.id].type == "text" || compontMap[queryFiled.id].type == "textarea" || compontMap[queryFiled.id].type == "texteditor" || compontMap[queryFiled.id].type == "encode")
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\", \"%\" + queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString() + \"%\", DbType.String);\r\n";
                        content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " Like @" + compontMap[queryFiled.id].field + " \");\r\n";
                    }
                    else
                    {
                        content += "                    dp.Add(\"" + compontMap[queryFiled.id].field + "\",queryParam[\"" + compontMap[queryFiled.id].field + "\"].ToString(), DbType.String);\r\n";
                        content += "                    strSql.Append(\" AND t." + compontMap[queryFiled.id].field + " = @" + compontMap[queryFiled.id].field + " \");\r\n";
                    }
                    content += "                }\r\n";
                }

                if (colData.isPage == "1") // 分页
                {
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + dbTable + "Entity>(strSql.ToString(),dp, pagination);\r\n";
                }
                else
                {
                    content += "                return this.BaseRepository(" + dbname + ").FindList<" + dbTable + "Entity>(strSql.ToString(),dp);\r\n";
                }
                sb.Append(getServiceTry(content));

                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表实体数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public " + dbTable + "Entity GetEntity(string keyValue)\r\n");
                content = "";
                content += "                return this.BaseRepository(" + dbname + ").FindEntity<" + dbTable + "Entity>(keyValue);\r\n";
                sb.Append(getServiceTry(content));

                // 获取树形数据列表
                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    content = "";
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取树形数据\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public DataTable GetSqlTree()\r\n");
                    content = "                return this.BaseRepository().FindTable(\" " + colData.treeSql + " \");\r\n";
                    sb.Append(getServiceTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据
                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                this.BaseRepository(" + dbname + ").Delete<" + dbTable + "Entity>(t=>t." + dbTablePk + " == keyValue);\r\n";
                sb.Append(getServiceTry(content));

                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <param name=\"entity\">实体数据</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                sb.Append("        public void SaveEntity(string keyValue, " + dbTable + "Entity entity)\r\n");
                content = "";
                content += "                if (!string.IsNullOrEmpty(keyValue))\r\n";
                content += "                {\r\n";
                content += "                    entity.Modify(keyValue);\r\n";
                content += "                    this.BaseRepository(" + dbname + ").Update(entity);\r\n";
                content += "                }\r\n";
                content += "                else\r\n";
                content += "                {\r\n";
                content += "                    entity.Create();\r\n";
                content += "                    this.BaseRepository(" + dbname + ").Insert(entity);\r\n";
                content += "                }\r\n";
                sb.Append(getServiceTry(content));

                // 更新列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"list\">实体数据列表</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                sb.Append("        public void SaveList(List<" + dbTable + "Entity> list)\r\n");
                content = "";
                content += "                foreach(var item in list)";
                content += "                {\r\n";
                content += "                    item.Modify(item." + dbTablePk + ");\r\n";
                content += "                    db.Update(item);\r\n";
                content += "                }\r\n";
                content += "                db.Commit();\r\n";
                sb.Append(getTransServiceTry(content, dbname));


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 服务类创建(报表开发模板)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接地址主键</param>
        /// <param name="strSql">sql语句</param>
        /// <param name="collist">列表数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string ReportServiceCreate(string databaseLinkId, string strSql, List<ReportColModel> collist, QueryModel queryData,BaseModel baseInfo)
        {
            try
            {
                #region 添加数据库配置
                string dbname = "";
                DatabaseLinkEntity dbEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
                if (connectionString != dbEntity.F_DbConnection)
                {
                    if (ConfigurationManager.ConnectionStrings[dbEntity.F_DBName] == null)
                    {
                        string providerName = "System.Data.SqlClient";
                        if (dbEntity.F_DbType == "MySql")
                        {
                            providerName = "MySql.Data.MySqlClient";
                        }
                        else if (dbEntity.F_DbType == "Oracle")
                        {
                            providerName = "Oracle.ManagedDataAccess.Client";
                        }
                        Config.UpdateOrCreateConnectionString("XmlConfig\\database.config", dbEntity.F_DBName, dbEntity.F_DbConnection, providerName);
                    }
                    dbname = "\"" + dbEntity.F_DBName + "\"";
                }
                #endregion

                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Dapper;\r\n");
                sb.Append("using Learun.DataBase.Repository;\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Collections.Generic;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Text;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Service : RepositoryFactory\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 传入参数数据处理
                // 列表数据
                string querySqlField = "";                                         // 查询数据字段
                foreach (var field in collist)
                {
                    if (querySqlField != "")
                    {
                        querySqlField += ",\r\n";
                    }
                    querySqlField += "                t." +field.field;
                }
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取报表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public DataTable GetList(string queryJson)\r\n");
                string content = "";
                content += "                var strSql = new StringBuilder();\r\n";
                content += "                strSql.Append(\"SELECT  \");\r\n";
                content += "                strSql.Append(@\"\r\n" + querySqlField + "\r\n                \");\r\n";
                content += "                strSql.Append(\"  FROM (" + strSql + ")t \");\r\n";

                // 条件查询设置
                content += "                strSql.Append(\"  WHERE 1=1 \");\r\n";
                // 时间查询
                content += "                var queryParam = queryJson.ToJObject();\r\n";
                content += "                // 虚拟参数\r\n";
                content += "                var dp = new DynamicParameters(new { });\r\n";
                if (queryData.isDate == "1" && !string.IsNullOrEmpty(queryData.DateField))
                {
                    content += "                if (!queryParam[\"StartTime\"].IsEmpty() && !queryParam[\"EndTime\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"startTime\", queryParam[\"StartTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    dp.Add(\"endTime\", queryParam[\"EndTime\"].ToDate(), DbType.DateTime);\r\n";
                    content += "                    strSql.Append(\" AND ( t." + queryData.DateField + " >= @startTime AND t." + queryData.DateField + " <= @endTime ) \");\r\n";
                    content += "                }\r\n";
                }

                foreach (var queryFiled in queryData.fields)
                {
                    content += "                if (!queryParam[\"" + queryFiled.field + "\"].IsEmpty())\r\n";
                    content += "                {\r\n";
                    content += "                    dp.Add(\"" + queryFiled.field + "\", \"%\" + queryParam[\"" + queryFiled.field + "\"].ToString() + \"%\", DbType.String);\r\n";
                    content += "                    strSql.Append(\" AND t." + queryFiled.field + " Like @" + queryFiled.field + " \");\r\n";
                    content += "                }\r\n";
                }
                content += "                return this.BaseRepository(" + dbname + ").FindTable(strSql.ToString(),dp);\r\n";
                sb.Append(getServiceTry(content));

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                sb.Append("        #endregion \r\n\r\n");
                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 业务接口类
        /// <summary>
        /// 业务接口类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string IBllCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + baseConfigModel.backProject + getBackArea(baseConfigModel.backArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseConfigModel));
                sb.Append("    public interface " + baseConfigModel.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        IEnumerable<" + baseConfigModel.tableName + "Entity> GetList( string queryJson );\r\n");
                // 获取列表分页数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表分页数据\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        IEnumerable<" + baseConfigModel.tableName + "Entity> GetPageList(Pagination pagination, string queryJson);\r\n");

                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        " + baseConfigModel.tableName + "Entity GetEntity(string keyValue);\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void DeleteEntity(string keyValue);\r\n");

                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void SaveEntity(string keyValue, " + baseConfigModel.tableName + "Entity entity);\r\n");


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 业务接口类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string IBllCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public interface " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }

                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson);\r\n");
                }
                else
                {
                    sb.Append("        IEnumerable<" + mainTable + "Entity> GetList(string queryJson);\r\n");
                }

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue);\r\n");
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue);\r\n");
                }

                if (colData.isTree == "1" && colData.treeSource == "2") {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        List<TreeModel> GetTree();\r\n");
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void DeleteEntity(string keyValue);\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "string keyValue, " + mainTable + "Entity entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                sb.Append("        void SaveEntity(" + paramStr + ");\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 业务接口类创建(流程系统表单开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string WfIBllCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public interface " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }

                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson);\r\n");
                }
                else
                {
                    sb.Append("        IEnumerable<" + mainTable + "Entity> GetList(string queryJson);\r\n");
                }

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue);\r\n");
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue);\r\n");
                }

                // 根据流程实例ID获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取主表实体数据\r\n");
                sb.Append("        /// <param name=\"processId\">流程实例ID</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        " + mainTable + "Entity GetEntityByProcessId(string processId);\r\n");

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        List<TreeModel> GetTree();\r\n");
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void DeleteEntity(string keyValue);\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "string keyValue, " + mainTable + "Entity entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                sb.Append("        void SaveEntity(" + paramStr + ");\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 业务接口类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTable">数据表数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string GridEditIBllCreate(BaseModel baseInfo, string dbTable, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public interface " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        IEnumerable<" + dbTable + "Entity> GetPageList(Pagination pagination, string queryJson);\r\n");
                }
                else
                {
                    sb.Append("        IEnumerable<" + dbTable + "Entity> GetList(string queryJson);\r\n");
                }

                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取" + dbTable + "表实体数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        " + dbTable + "Entity GetEntity(string keyValue);\r\n");

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        List<TreeModel> GetTree();\r\n");
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void DeleteEntity(string keyValue);\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <param name=\"entity\">实体</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void SaveEntity(string keyValue, " + dbTable + "Entity entity);\r\n");

                //保存实体列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"list\">实体列表</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        void SaveList(List<" + dbTable + "Entity> list);\r\n");
                
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 业务接口类创建(报表开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string ReportIBllCreate(BaseModel baseInfo)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public interface " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        DataTable GetList(string queryJson);\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                sb.Append("        #endregion \r\n\r\n");
                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 业务类
        /// <summary>
        /// 获取服务类函数体字串
        /// </summary>
        /// <param name="content">函数功能内容</param>
        /// <returns></returns>
        private string getBllTry(string content)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append(content);
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception ex)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                if (ex is ExceptionEx)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw;\r\n");
            sb.Append("                }\r\n");
            sb.Append("                else\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    throw ExceptionEx.ThrowBusinessException(ex);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 业务类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string BllCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + baseConfigModel.backProject + getBackArea(baseConfigModel.backArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseConfigModel));
                sb.Append("    public class " + baseConfigModel.name + "BLL : " + baseConfigModel.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseConfigModel.name + "Service " + Str.FirstLower(baseConfigModel.name) + "Service = new " + baseConfigModel.name + "Service();\r\n\r\n");
                sb.Append("        #region 获取数据\r\n\r\n");
                string content = "";
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.tableName + "Entity> GetList( string queryJson )\r\n");
                content = "";
                content += "                return " + Str.FirstLower(baseConfigModel.name) + "Service.GetList(queryJson);\r\n";
                sb.Append(getBllTry(content));
                // 获取列表分页数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表分页数据\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.tableName + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                content = "";
                content += "                return " + Str.FirstLower(baseConfigModel.name) + "Service.GetPageList(pagination, queryJson);\r\n";
                sb.Append(getBllTry(content));
                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public " + baseConfigModel.tableName + "Entity GetEntity(string keyValue)\r\n");
                content = "";
                content += "                return " + Str.FirstLower(baseConfigModel.name) + "Service.GetEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseConfigModel.name) + "Service.DeleteEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));
                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void SaveEntity(string keyValue, " + baseConfigModel.tableName + "Entity entity)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseConfigModel.name) + "Service.SaveEntity(keyValue, entity);\r\n";
                sb.Append(getBllTry(content));
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 业务类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string BllCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n"); 
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "BLL : " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "Service " + Str.FirstLower(baseInfo.name) + "Service = new " + baseInfo.name + "Service();\r\n\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                string content = "";
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetPageList(pagination, queryJson);\r\n";
                }
                else
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetList(string queryJson)\r\n");
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetList(queryJson);\r\n";
                }
                sb.Append(getBllTry(content));

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue)\r\n");
                    content = "";
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.Get" + tableOne.name + "List(keyValue);\r\n";
                    sb.Append(getBllTry(content));
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue)\r\n");
                    content = "";
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.Get" + tableOne.name + "Entity(keyValue);\r\n";
                    sb.Append(getBllTry(content));
                }

                if (colData.isTree == "1" && colData.treeSource == "2") {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("         public List<TreeModel> GetTree()\r\n");
                    content = "";
                    content += "                DataTable list = "+ Str.FirstLower(baseInfo.name) +"Service.GetSqlTree();\r\n";
                    content += "                List<TreeModel> treeList = new List<TreeModel>();\r\n";
                    content += "                foreach (DataRow item in list.Rows)\r\n";
                    content += "                {\r\n";
                    content += "                    TreeModel node = new TreeModel\r\n";
                    content += "                    {\r\n";
                    content += "                        id = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        text = item[\"" + colData.treefieldShow + "\"].ToString(),\r\n";
                    content += "                        value = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        showcheck = false,\r\n";
                    content += "                        checkstate = 0,\r\n";
                    content += "                        isexpand = true,\r\n";
                    content += "                        parentId = item[\"" + colData.treeParentId + "\"].ToString()\r\n";
                    content += "                    };\r\n";
                    content += "                    treeList.Add(node);";
                    content += "                }\r\n";
                    content += "                return treeList.ToTree();\r\n";
                    sb.Append(getBllTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.DeleteEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));


                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "string keyValue, " + mainTable + "Entity entity,";
                string paramStr2 = "keyValue, entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public void SaveEntity(" + paramStr + ")\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.SaveEntity(" + paramStr2 + ");\r\n";
                sb.Append(getBllTry(content));

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 业务类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string WfBllCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "BLL : " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "Service " + Str.FirstLower(baseInfo.name) + "Service = new " + baseInfo.name + "Service();\r\n\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                string content = "";
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetPageList(pagination, queryJson);\r\n";
                }
                else
                {
                    sb.Append("        public IEnumerable<" + mainTable + "Entity> GetList(string queryJson)\r\n");
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetList(queryJson);\r\n";
                }
                sb.Append(getBllTry(content));

                // 获取编辑列表数据
                foreach (var tableOne in girdDbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public IEnumerable<" + tableOne.name + "Entity> Get" + tableOne.name + "List(string keyValue)\r\n");
                    content = "";
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.Get" + tableOne.name + "List(keyValue);\r\n";
                    sb.Append(getBllTry(content));
                }

                // 获取实体数据
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取" + tableOne.name + "表实体数据\r\n");
                    sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + tableOne.name + "Entity Get" + tableOne.name + "Entity(string keyValue)\r\n");
                    content = "";
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.Get" + tableOne.name + "Entity(keyValue);\r\n";
                    sb.Append(getBllTry(content));
                }

                // 根据流程实例ID获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取主表实体数据\r\n");
                sb.Append("        /// <param name=\"processId\">流程实例ID</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public " + mainTable + "Entity GetEntityByProcessId(string processId)\r\n");
                content = "";
                content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetEntityByProcessId(processId);\r\n";
                sb.Append(getBllTry(content));

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("         public List<TreeModel> GetTree()\r\n");
                    content = "";
                    content += "                DataTable list = " + Str.FirstLower(baseInfo.name) + "Service.GetSqlTree();\r\n";
                    content += "                List<TreeModel> treeList = new List<TreeModel>();\r\n";
                    content += "                foreach (DataRow item in list.Rows)\r\n";
                    content += "                {\r\n";
                    content += "                    TreeModel node = new TreeModel\r\n";
                    content += "                    {\r\n";
                    content += "                        id = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        text = item[\"" + colData.treefieldShow + "\"].ToString(),\r\n";
                    content += "                        value = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        showcheck = false,\r\n";
                    content += "                        checkstate = 0,\r\n";
                    content += "                        isexpand = true,\r\n";
                    content += "                        parentId = item[\"" + colData.treeParentId + "\"].ToString()\r\n";
                    content += "                    };\r\n";
                    content += "                    treeList.Add(node);";
                    content += "                }\r\n";
                    content += "                return treeList.ToTree();\r\n";
                    sb.Append(getBllTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.DeleteEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));


                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");

                // 函数传入参数
                string paramStr = "string keyValue, " + mainTable + "Entity entity,";
                string paramStr2 = "keyValue, entity,";
                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                        }
                        else
                        {
                            paramStr += "List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public void SaveEntity(" + paramStr + ")\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.SaveEntity(" + paramStr2 + ");\r\n";
                sb.Append(getBllTry(content));

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 业务类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTable">数据表数据</param>
        /// <param name="dbTablePk">数据表数据主键</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string GridEditBllCreate(BaseModel baseInfo, string dbTable,string dbTablePk, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "BLL : " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "Service " + Str.FirstLower(baseInfo.name) + "Service = new " + baseInfo.name + "Service();\r\n\r\n");
                #endregion

                

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                string content = "";
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public IEnumerable<" + dbTable + "Entity> GetPageList(Pagination pagination, string queryJson)\r\n");
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetPageList(pagination, queryJson);\r\n";
                }
                else
                {
                    sb.Append("        public IEnumerable<" + dbTable + "Entity> GetList(string queryJson)\r\n");
                    content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetList(queryJson);\r\n";
                }
                sb.Append(getBllTry(content));

                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public " + dbTable + "Entity GetEntity(string keyValue)\r\n");
                content = "";
                content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));


                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("         public List<TreeModel> GetTree()\r\n");
                    content = "";
                    content += "                DataTable list = " + Str.FirstLower(baseInfo.name) + "Service.GetSqlTree();\r\n";
                    content += "                List<TreeModel> treeList = new List<TreeModel>();\r\n";
                    content += "                foreach (DataRow item in list.Rows)\r\n";
                    content += "                {\r\n";
                    content += "                    TreeModel node = new TreeModel\r\n";
                    content += "                    {\r\n";
                    content += "                        id = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        text = item[\"" + colData.treefieldShow + "\"].ToString(),\r\n";
                    content += "                        value = item[\"" + colData.treefieldId + "\"].ToString(),\r\n";
                    content += "                        showcheck = false,\r\n";
                    content += "                        checkstate = 0,\r\n";
                    content += "                        isexpand = true,\r\n";
                    content += "                        parentId = item[\"" + colData.treeParentId + "\"].ToString()\r\n";
                    content += "                    };\r\n";
                    content += "                    treeList.Add(node);";
                    content += "                }\r\n";
                    content += "                return treeList.ToTree();\r\n";
                    sb.Append(getBllTry(content));
                }


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void DeleteEntity(string keyValue)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.DeleteEntity(keyValue);\r\n";
                sb.Append(getBllTry(content));

                // 新增和更新
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void SaveEntity(string keyValue, " + dbTable + "Entity entity)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.SaveEntity(keyValue, entity);\r\n";
                sb.Append(getBllTry(content));


                // 更新列表数据
                content = "";
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"list\">实体数据列表</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        public void SaveList(List<" + dbTable + "Entity> list)\r\n");
                content = "";
                content += "                " + Str.FirstLower(baseInfo.name) + "Service.SaveList(list);\r\n";
                sb.Append(getBllTry(content));


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 业务类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string ReportBllCreate(BaseModel baseInfo)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace " + backProject + getBackArea(baseInfo.outputArea) + "\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "BLL : " + baseInfo.name + "IBLL\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "Service " + Str.FirstLower(baseInfo.name) + "Service = new " + baseInfo.name + "Service();\r\n\r\n");
                #endregion



                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                string content = "";
                sb.Append("        public DataTable GetList(string queryJson)\r\n");
                content += "                return " + Str.FirstLower(baseInfo.name) + "Service.GetList(queryJson);\r\n";
                sb.Append(getBllTry(content));

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                sb.Append("        #endregion \r\n\r\n");
                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 控制器类
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="TableTree"></param>
        /// <param name="girdDbTableList"></param>
        /// <param name="baseInfo"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetEntityCode(List<TreeModelEx<DbTableModel>> TableTree, List<DbTableModel> girdDbTableList, BaseModel baseInfo, string tableName)
        {
            string res = "";
            foreach (var tableOne in TableTree)
            {

                string keyvalue = "keyValue";

                if (!string.IsNullOrEmpty(tableName))
                {
                    keyvalue = tableOne.data.relationName + "Data." + tableOne.data.relationField;
                }

                if (girdDbTableList.FindAll(t => t.name == tableOne.data.name).Count > 0)
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "List( " + keyvalue + " );\r\n";
                }
                else
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "Entity( " + keyvalue + " );\r\n";
                }

                if (tableOne.ChildNodes.Count > 0)
                {
                    res += GetEntityCode(tableOne.ChildNodes, girdDbTableList, baseInfo, tableOne.data.name);
                }

            }
            return res;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="TableTree"></param>
        /// <param name="baseInfo"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetEntityCode(List<TreeModelEx<DbTableModel>> TableTree, BaseModel baseInfo, string tableName)
        {
            string res = "";
            foreach (var tableOne in TableTree)
            {

                string keyvalue = "keyValue";

                if (!string.IsNullOrEmpty(tableName))
                {
                    keyvalue = tableOne.data.relationName + "Data." + tableOne.data.relationField;
                }

                res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "Entity( " + keyvalue + " );\r\n";

                if (tableOne.ChildNodes.Count > 0)
                {
                    res += GetEntityCode(tableOne.ChildNodes, baseInfo, tableOne.data.name);
                }

            }
            return res;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="TableTree"></param>
        /// <param name="girdDbTableList"></param>
        /// <param name="baseInfo"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private string GetWfEntityCode(List<TreeModelEx<DbTableModel>> TableTree, List<DbTableModel> girdDbTableList, BaseModel baseInfo, string tableName)
        {
            string res = "";
            foreach (var tableOne in TableTree)
            {

                string keyvalue = "processId";

                if (!string.IsNullOrEmpty(tableName))
                {
                    keyvalue = tableOne.data.relationName + "Data." + tableOne.data.relationField;
                }

                if (keyvalue == "processId")
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetEntityByProcessId( " + keyvalue + " );\r\n";
                }
                else if (girdDbTableList.FindAll(t => t.name == tableOne.data.name).Count > 0)
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "List( " + keyvalue + " );\r\n";
                }
                else
                {
                    res += "            var " + tableOne.data.name + "Data = " + Str.FirstLower(baseInfo.name) + "IBLL.Get" + tableOne.data.name + "Entity( " + keyvalue + " );\r\n";
                }

                if (tableOne.ChildNodes.Count > 0)
                {
                    res += GetWfEntityCode(tableOne.ChildNodes, girdDbTableList, baseInfo, tableOne.data.name);
                }

            }
            return res;
        }
        /// <summary>
        /// 控制器类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string ControllerCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("using " + baseConfigModel.backProject + getBackArea(baseConfigModel.backArea) + ";\r\n");
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using System.Web.Mvc;\r\n\r\n");

                sb.Append("namespace Learun.Application.Web.Areas." + baseConfigModel.area + ".Controllers\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseConfigModel));
                sb.Append("    public class " + baseConfigModel.name + "Controller : MvcControllerBase\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseConfigModel.name + "IBLL " + Str.FirstLower(baseConfigModel.name) + "IBLL = new " + baseConfigModel.name + "BLL();\r\n\r\n");

                sb.Append("        #region 视图功能\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 主页面\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Index()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单页\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Form()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetList( string queryJson )\r\n");
                sb.Append("        {\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseConfigModel.name) + "IBLL.GetList(queryJson);\r\n");
                sb.Append("            return Success(data);\r\n");
                sb.Append("        }\r\n");

                // 获取列表分页数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表分页数据\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetPageList(string pagination, string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            Pagination paginationobj = pagination.ToObject<Pagination>();\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseConfigModel.name) + "IBLL.GetPageList(paginationobj, queryJson);\r\n");
                sb.Append("            var jsonData = new\r\n");
                sb.Append("            {\r\n");
                sb.Append("                rows = data,\r\n");
                sb.Append("                total = paginationobj.total,\r\n");
                sb.Append("                page = paginationobj.page,\r\n");
                sb.Append("                records = paginationobj.records\r\n");
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");
                // 获取实体数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表单数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetFormData(string keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseConfigModel.name) + "IBLL.GetEntity(keyValue);\r\n");
                sb.Append("            return Success(data);\r\n");
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("        #region 提交数据\r\n\r\n");

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult DeleteForm(string keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseConfigModel.name) + "IBLL.DeleteEntity(keyValue);\r\n");
                sb.Append("            return Success(\"删除成功！\");\r\n");
                sb.Append("        }\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [ValidateAntiForgeryToken]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult SaveForm(string keyValue," + baseConfigModel.tableName + "Entity entity)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseConfigModel.name) + "IBLL.SaveEntity(keyValue, entity);\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 控制器类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string ControllerCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Web.Mvc;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace Learun.Application.Web.Areas." + baseInfo.outputArea + ".Controllers\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Controller : MvcControllerBase\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "IBLL " + Str.FirstLower(baseInfo.name) + "IBLL = new " + baseInfo.name + "BLL();\r\n\r\n");

                sb.Append("        #region 视图功能\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 主页面\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Index()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单页\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Form()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public ActionResult GetPageList(string pagination, string queryJson)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            Pagination paginationobj = pagination.ToObject<Pagination>();\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetPageList(paginationobj, queryJson);\r\n");
                    sb.Append("            var jsonData = new\r\n");
                    sb.Append("            {\r\n");
                    sb.Append("                rows = data,\r\n");
                    sb.Append("                total = paginationobj.total,\r\n");
                    sb.Append("                page = paginationobj.page,\r\n");
                    sb.Append("                records = paginationobj.records\r\n");
                    sb.Append("            };\r\n");
                    sb.Append("            return Success(jsonData);\r\n");
                    sb.Append("        }\r\n");
                }
                else
                {
                    sb.Append("        public ActionResult GetList(string queryJson)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetList(queryJson);\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }
                // 获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表单数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetFormData(string keyValue)\r\n");
                sb.Append("        {\r\n");
                string strEntityCode = GetEntityCode(TableTree, girdDbTableList, baseInfo, "");
                sb.Append(strEntityCode);
                sb.Append("            var jsonData = new {\r\n");
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("                " + tableOne.name + " = " + tableOne.name + "Data,\r\n");
                }
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    // 获取左侧树形数据
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        [HttpGet]\r\n");
                    sb.Append("        [AjaxOnly]\r\n");
                    sb.Append("        public ActionResult GetTree()\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetTree();\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult DeleteForm(string keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.DeleteEntity(keyValue);\r\n");
                sb.Append("            return Success(\"删除成功！\");\r\n");
                sb.Append("        }\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [ValidateAntiForgeryToken]\r\n");
                sb.Append("        [AjaxOnly]\r\n");


                // 函数传入参数
                string paramStr = "string keyValue, string strEntity,";
                string paramStr2 = "keyValue,entity,";
                string paramStr3 = "            " + mainTable + "Entity entity = strEntity.ToObject<" + mainTable + "Entity>();\r\n";

                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += " string str" + Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr3 += "            " + tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity = str" + Str.FirstLower(tableOne.name) + "Entity.ToObject<" + tableOne.name + "Entity>();\r\n";
                        }
                        else
                        {
                            paramStr += " string str" + Str.FirstLower(tableOne.name) + "List,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                            paramStr3 += "            List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List = str" + Str.FirstLower(tableOne.name) + "List.ToObject<List<" + tableOne.name + "Entity>>();\r\n";

                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public ActionResult SaveForm(" + paramStr + ")\r\n");
                sb.Append("        {\r\n");
                sb.Append(paramStr3);
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.SaveEntity(" + paramStr2 + ");\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 控制器类创建(流程系统表单开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTableList">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string WfControllerCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Web.Mvc;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace Learun.Application.Web.Areas." + baseInfo.outputArea + ".Controllers\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Controller : MvcControllerBase\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "IBLL " + Str.FirstLower(baseInfo.name) + "IBLL = new " + baseInfo.name + "BLL();\r\n\r\n");

                sb.Append("        #region 视图功能\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 主页面\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Index()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单页\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Form()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                #endregion

                #region 传入参数数据处理
                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }
                TableTree = TableTree.ToTree();

                // 表单数据遍历
                List<DbTableModel> girdDbTableList = new List<DbTableModel>();      // 需要查询的表
                foreach (var compontKey in compontMap.Keys)
                {
                    if (compontMap[compontKey].type == "girdtable")
                    {
                        girdDbTableList.Add(dbTableMap[compontMap[compontKey].table]);
                    }
                }
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public ActionResult GetPageList(string pagination, string queryJson)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            Pagination paginationobj = pagination.ToObject<Pagination>();\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetPageList(paginationobj, queryJson);\r\n");
                    sb.Append("            var jsonData = new\r\n");
                    sb.Append("            {\r\n");
                    sb.Append("                rows = data,\r\n");
                    sb.Append("                total = paginationobj.total,\r\n");
                    sb.Append("                page = paginationobj.page,\r\n");
                    sb.Append("                records = paginationobj.records\r\n");
                    sb.Append("            };\r\n");
                    sb.Append("            return Success(jsonData);\r\n");
                    sb.Append("        }\r\n");
                }
                else
                {
                    sb.Append("        public ActionResult GetList(string queryJson)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetList(queryJson);\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }
                // 获取表单数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表单数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetFormData(string keyValue)\r\n");
                sb.Append("        {\r\n");
                string strEntityCode = GetEntityCode(TableTree, girdDbTableList, baseInfo, "");
                sb.Append(strEntityCode);
                sb.Append("            var jsonData = new {\r\n");
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("                " + tableOne.name + " = " + tableOne.name + "Data,\r\n");
                }
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                // 根据流程实例获取表单
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取表单数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetFormDataByProcessId(string processId)\r\n");
                sb.Append("        {\r\n");
                strEntityCode = GetWfEntityCode(TableTree, girdDbTableList, baseInfo, "");
                sb.Append(strEntityCode);
                sb.Append("            var jsonData = new {\r\n");
                foreach (var tableOne in dbTableList)
                {
                    sb.Append("                " + tableOne.name + " = " + tableOne.name + "Data,\r\n");
                }
                sb.Append("            };\r\n");
                sb.Append("            return Success(jsonData);\r\n");
                sb.Append("        }\r\n");

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    // 获取左侧树形数据
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        [HttpGet]\r\n");
                    sb.Append("        [AjaxOnly]\r\n");
                    sb.Append("        public ActionResult GetTree()\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetTree();\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult DeleteForm(string keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.DeleteEntity(keyValue);\r\n");
                sb.Append("            return Success(\"删除成功！\");\r\n");
                sb.Append("        }\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [ValidateAntiForgeryToken]\r\n");
                sb.Append("        [AjaxOnly]\r\n");


                // 函数传入参数
                string paramStr = "string keyValue, string strEntity,";
                string paramStr2 = "keyValue,entity,";
                string paramStr3 = "            " + mainTable + "Entity entity = strEntity.ToObject<" + mainTable + "Entity>();\r\n";

                foreach (var tableOne in dbTableList)
                {
                    string tableName = tableOne.name;
                    if (tableOne.name != mainTable)
                    {
                        if (girdDbTableList.Find(t => t.name == tableOne.name) == null)
                        {
                            paramStr += " string str" + Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "Entity,";
                            paramStr3 += "            " + tableOne.name + "Entity " + Str.FirstLower(tableOne.name) + "Entity = str" + Str.FirstLower(tableOne.name) + "Entity.ToObject<" + tableOne.name + "Entity>();\r\n";
                        }
                        else
                        {
                            paramStr += " string str" + Str.FirstLower(tableOne.name) + "List,";
                            paramStr2 += Str.FirstLower(tableOne.name) + "List,";
                            paramStr3 += "            List<" + tableOne.name + "Entity> " + Str.FirstLower(tableOne.name) + "List = str" + Str.FirstLower(tableOne.name) + "List.ToObject<List<" + tableOne.name + "Entity>>();\r\n";

                        }
                    }
                }
                paramStr = paramStr.Remove(paramStr.Length - 1, 1);
                paramStr2 = paramStr2.Remove(paramStr2.Length - 1, 1);
                sb.Append("        public ActionResult SaveForm(" + paramStr + ")\r\n");
                sb.Append("        {\r\n");
                sb.Append(paramStr3);
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.SaveEntity(" + paramStr2 + ");\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 控制器类创建(编辑列表开发模板：扩展一个保存多条数据的函数)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <param name="dbTable">数据表数据</param>
        /// <param name="compontMap">表单组件数据</param>
        /// <param name="colData">列表数据</param>
        /// <returns></returns>
        public string GridEditControllerCreate(BaseModel baseInfo, string dbTable, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Web.Mvc;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace Learun.Application.Web.Areas." + baseInfo.outputArea + ".Controllers\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Controller : MvcControllerBase\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "IBLL " + Str.FirstLower(baseInfo.name) + "IBLL = new " + baseInfo.name + "BLL();\r\n\r\n");

                sb.Append("        #region 视图功能\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 主页面\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Index()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单页\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Form()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                if (colData.isPage == "1") // 分页
                {
                    sb.Append("        public ActionResult GetPageList(string pagination, string queryJson)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            Pagination paginationobj = pagination.ToObject<Pagination>();\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetPageList(paginationobj, queryJson);\r\n");
                    sb.Append("            var jsonData = new\r\n");
                    sb.Append("            {\r\n");
                    sb.Append("                rows = data,\r\n");
                    sb.Append("                total = paginationobj.total,\r\n");
                    sb.Append("                page = paginationobj.page,\r\n");
                    sb.Append("                records = paginationobj.records\r\n");
                    sb.Append("            };\r\n");
                    sb.Append("            return Success(jsonData);\r\n");
                    sb.Append("        }\r\n");
                }
                else
                {
                    sb.Append("        public ActionResult GetList(string queryJson)\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetList(queryJson);\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }

                if (colData.isTree == "1" && colData.treeSource == "2")
                {
                    // 获取左侧树形数据
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// 获取左侧树形数据\r\n");
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        [HttpGet]\r\n");
                    sb.Append("        [AjaxOnly]\r\n");
                    sb.Append("        public ActionResult GetTree()\r\n");
                    sb.Append("        {\r\n");
                    sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetTree();\r\n");
                    sb.Append("            return Success(data);\r\n");
                    sb.Append("        }\r\n");
                }

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 提交数据\r\n\r\n");

                #endregion

                #region 提交数据

                // 删除
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 删除实体数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult DeleteForm(string keyValue)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.DeleteEntity(keyValue);\r\n");
                sb.Append("            return Success(\"删除成功！\");\r\n");
                sb.Append("        }\r\n");


                // 新增和更新
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据（新增、修改）\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [ValidateAntiForgeryToken]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult SaveForm(string keyValue, " + dbTable + "Entity entity)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.SaveEntity(keyValue,entity);\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");


                // 保存数据列表
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 保存实体数据列表r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"list\">列表数据</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpPost]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult SaveList(List<" + dbTable + "Entity> list)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            " + Str.FirstLower(baseInfo.name) + "IBLL.SaveList(list);\r\n");
                sb.Append("            return Success(\"保存成功！\");\r\n");
                sb.Append("        }\r\n");


                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 控制器类创建(报表开发模板)
        /// </summary>
        /// <param name="baseInfo">基础数据</param>
        /// <returns></returns>
        public string ReportControllerCreate(BaseModel baseInfo)
        {
            try
            {
                #region 类信息
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("using Learun.Util;\r\n");
                sb.Append("using System.Data;\r\n");
                sb.Append("using " + backProject + getBackArea(baseInfo.outputArea) + ";\r\n");
                sb.Append("using System.Web.Mvc;\r\n");
                sb.Append("using System.Collections.Generic;\r\n\r\n");

                sb.Append("namespace Learun.Application.Web.Areas." + baseInfo.outputArea + ".Controllers\r\n");
                sb.Append("{\r\n");
                sb.Append(NotesCreate(baseInfo));
                sb.Append("    public class " + baseInfo.name + "Controller : MvcControllerBase\r\n");
                sb.Append("    {\r\n");
                sb.Append("        private " + baseInfo.name + "IBLL " + Str.FirstLower(baseInfo.name) + "IBLL = new " + baseInfo.name + "BLL();\r\n\r\n");

                sb.Append("        #region 视图功能\r\n\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 主页面\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Index()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 表单页\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult Form()\r\n");
                sb.Append("        {\r\n");
                sb.Append("             return View();\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");
                #endregion

                #region 数据查询
                // 获取数据
                sb.Append("        #region 获取数据\r\n\r\n");
                // 获取列表数据
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取页面显示列表数据\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns></returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        [AjaxOnly]\r\n");
                sb.Append("        public ActionResult GetList(string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            var data = " + Str.FirstLower(baseInfo.name) + "IBLL.GetList(queryJson);\r\n");
                sb.Append("            return Success(data);\r\n");
                sb.Append("        }\r\n");

                sb.Append("        #endregion\r\n\r\n");

                sb.Append("    }\r\n");
                sb.Append("}\r\n");

                #endregion
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 页面类
        /// <summary>
        /// 业务类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string IndexCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseConfigModel.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
                sb.Append("}\r\n");

                sb.Append("<div class=\"lr-layout lr-layout-left-center\" id=\"lr_layout\">\r\n");
                sb.Append("    <div class=\"lr-layout-left\">\r\n");
                sb.Append("        <div class=\"lr-layout-wrap\">\r\n");
                sb.Append("            <div class=\"lr-layout-title\">树形目录</div>\r\n");
                sb.Append("            <div id=\"tree\" class=\"lr-layout-body\"></div>\r\n");
                sb.Append("        </div>\r\n");
                sb.Append("    </div>\r\n");
                sb.Append("    <div class=\"lr-layout-center\">\r\n");
                sb.Append("        <div class=\"lr-layout-wrap\">\r\n");
                sb.Append("            <div class=\"lr-layout-title\">标题</div>\r\n");
                sb.Append("            <div class=\"lr-layout-tool\">\r\n");
                sb.Append("                <div class=\"lr-layout-tool-left\">\r\n");
                sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                sb.Append("                        <input id=\"txt_Keyword\" type=\"text\" class=\"form-control\" placeholder=\"请输入要查询关键字\" />\r\n");
                sb.Append("                    </div>\r\n");
                sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                sb.Append("                        <a id=\"btn_Search\" class=\"btn btn-primary btn-sm\"><i class=\"fa fa-search\"></i>&nbsp;查询</a>\r\n");
                sb.Append("                    </div>\r\n");
                sb.Append("                </div>\r\n");
                sb.Append("                <div class=\"lr-layout-tool-right\">\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\">\r\n");
                sb.Append("                        <a id=\"lr_refresh\" class=\"btn btn-default\"><i class=\"fa fa-refresh\"></i></a>\r\n");
                sb.Append("                    </div>\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");
                sb.Append("                        <a id=\"lr_add\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;新增</a>\r\n");
                sb.Append("                        <a id=\"lr_edit\"  class=\"btn btn-default\"><i class=\"fa fa-pencil-square-o\"></i>&nbsp;编辑</a>\r\n");
                sb.Append("                        <a id=\"lr_delete\" class=\"btn btn-default\"><i class=\"fa fa-trash-o\"></i>&nbsp;删除</a>\r\n");
                sb.Append("                    </div>\r\n");
                sb.Append("                </div>\r\n");
                sb.Append("            </div>\r\n");
                sb.Append("            <div class=\"lr-layout-body\" id=\"gridtable\"></div>\r\n");
                sb.Append("        </div>\r\n");
                sb.Append("    </div>\r\n");
                sb.Append("</div>\r\n");
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseConfigModel.area + "/Views/" + baseConfigModel.name + "/Index.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 业务类创建(自定义开发模板) 
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="compontMap"></param>
        /// <param name="queryData"></param>
        /// <param name="colData"></param>
        /// <returns></returns>
        public string IndexCreate(BaseModel baseInfo, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseInfo.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
                sb.Append("}\r\n");

                sb.Append("<div class=\"lr-layout " + (colData.isTree == "1" ? "lr-layout-left-center\"  id=\"lr_layout\" " : "\"") + "  >\r\n");
                if (colData.isTree == "1") {
                    sb.Append("    <div class=\"lr-layout-left\">\r\n");
                    sb.Append("        <div class=\"lr-layout-wrap\">\r\n");
                    sb.Append("            <div class=\"lr-layout-title lrlg \">树形列表</div>\r\n");
                    sb.Append("            <div id=\"dataTree\" class=\"lr-layout-body\"></div>\r\n");
                    sb.Append("        </div>\r\n");
                    sb.Append("    </div>\r\n");
                }

                sb.Append("    <div class=\"lr-layout-center\">\r\n");
                sb.Append("        <div class=\"lr-layout-wrap "+ (colData.isTree == "1" ? "" : "lr-layout-wrap-notitle") + " \">\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            <div class=\"lr-layout-title\">\r\n");
                    sb.Append("                <span id=\"titleinfo\" class=\"lrlg\">列表信息</span>\r\n");
                    sb.Append("            </div>\r\n");
                }

                sb.Append("            <div class=\"lr-layout-tool\">\r\n");
                sb.Append("                <div class=\"lr-layout-tool-left\">\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"datesearch\"></div>\r\n");
                    sb.Append("                    </div>\r\n");
                }
                if (queryData.fields.Count > 0)
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"multiple_condition_query\">\r\n");
                    sb.Append("                            <div class=\"lr-query-formcontent\">\r\n");
                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        sb.Append("                                <div class=\"col-xs-" + (12 / Convert.ToInt32(item.portion)) + " lr-form-item\">\r\n");
                        sb.Append("                                    <div class=\"lr-form-item-title\">" + compont.title + "</div>\r\n");

                        switch (compont.type) {
                            case "text":
                            case "textarea":
                            case "datetimerange":
                            case "texteditor":
                            case "encode":
                                sb.Append("                                    <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control\" />\r\n");
                                break;
                            case "radio":
                            case "checkbox":
                            case "select":
                            case "organize":
                            case "currentInfo":
                                sb.Append("                                    <div id=\"" + compont.field + "\"></div>\r\n");
                                break;
                        }
                        sb.Append("                                </div>\r\n");
                    }
                    sb.Append("                            </div>\r\n");
                    sb.Append("                        </div>\r\n");
                    sb.Append("                    </div>\r\n");
                }

                sb.Append("                </div>\r\n");
                sb.Append("                <div class=\"lr-layout-tool-right\">\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\">\r\n");
                sb.Append("                        <a id=\"lr_refresh\" class=\"btn btn-default\"><i class=\"fa fa-refresh\"></i></a>\r\n");
                sb.Append("                    </div>\r\n");

                if (colData.btns.Length > 0)
                {
                    sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");

                    foreach (string btn in colData.btns) {
                        switch (btn) {
                            case "add":
                                sb.Append("                        <a id=\"lr_add\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;新增</a>\r\n");
                                break;
                            case "edit":
                                sb.Append("                        <a id=\"lr_edit\"  class=\"btn btn-default\"><i class=\"fa fa-pencil-square-o\"></i>&nbsp;编辑</a>\r\n");
                                break;
                            case "delete":
                                sb.Append("                        <a id=\"lr_delete\" class=\"btn btn-default\"><i class=\"fa fa-trash-o\"></i>&nbsp;删除</a>\r\n");
                                break;
                            case "print":
                                sb.Append("                        <a id=\"lr_print\"   class=\"btn btn-default\"><i class=\"fa fa-print\"></i>&nbsp;打印</a>\r\n");
                                break;
                        }
                    }
                    sb.Append("                    </div>\r\n");
                }
                if (colData.btnexs.Count > 0)
                {
                    sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");
                    foreach (var btn in colData.btnexs)
                    {
                        sb.Append("                        <a id=\"" + btn.id + "\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;" + btn.name + "</a>\r\n");
                    }
                    sb.Append("                    </div>\r\n");
                }


                sb.Append("                </div>\r\n");
                sb.Append("            </div>\r\n");
                sb.Append("            <div class=\"lr-layout-body\" id=\"gridtable\"></div>\r\n");
                sb.Append("        </div>\r\n");
                sb.Append("    </div>\r\n");
                sb.Append("</div>\r\n");
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseInfo.outputArea + "/Views/" + baseInfo.name + "/Index.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 业务类创建(自定义开发模板) 
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="compontMap"></param>
        /// <param name="queryData"></param>
        /// <param name="colData"></param>
        /// <returns></returns>
        public string GridEditIndexCreate(BaseModel baseInfo, Dictionary<string, CodeFormCompontModel> compontMap, QueryModel queryData, ColModel colData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseInfo.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
                sb.Append("}\r\n");

                sb.Append("<div class=\"lr-layout " + (colData.isTree == "1" ? "lr-layout-left-center\"  id=\"lr_layout\" " : "\"") + "  >\r\n");
                if (colData.isTree == "1")
                {
                    sb.Append("    <div class=\"lr-layout-left\">\r\n");
                    sb.Append("        <div class=\"lr-layout-wrap\">\r\n");
                    sb.Append("            <div class=\"lr-layout-title lrlg \">树形列表</div>\r\n");
                    sb.Append("            <div id=\"dataTree\" class=\"lr-layout-body\"></div>\r\n");
                    sb.Append("        </div>\r\n");
                    sb.Append("    </div>\r\n");
                }

                sb.Append("    <div class=\"lr-layout-center\">\r\n");
                sb.Append("        <div class=\"lr-layout-wrap " + (colData.isTree == "1" ? "" : "lr-layout-wrap-notitle") + " \">\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            <div class=\"lr-layout-title\">\r\n");
                    sb.Append("                <span id=\"titleinfo\" class=\"lrlg\">列表信息</span>\r\n");
                    sb.Append("            </div>\r\n");
                }

                sb.Append("            <div class=\"lr-layout-tool\">\r\n");
                sb.Append("                <div class=\"lr-layout-tool-left\">\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"datesearch\"></div>\r\n");
                    sb.Append("                    </div>\r\n");
                }
                if (queryData.fields.Count > 0)
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"multiple_condition_query\">\r\n");
                    sb.Append("                            <div class=\"lr-query-formcontent\">\r\n");
                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        sb.Append("                                <div class=\"col-xs-" + (12 / Convert.ToInt32(item.portion)) + " lr-form-item\">\r\n");
                        sb.Append("                                    <div class=\"lr-form-item-title\">" + compont.title + "</div>\r\n");

                        switch (compont.type)
                        {
                            case "text":
                                sb.Append("                                    <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control\" />\r\n");
                                break;
                            case "radio":
                            case "checkbox":
                            case "select":
                            case "organize":
                            case "currentInfo":
                                sb.Append("                                    <div id=\"" + compont.field + "\"></div>\r\n");
                                break;
                        }
                        sb.Append("                                </div>\r\n");
                    }
                    sb.Append("                            </div>\r\n");
                    sb.Append("                        </div>\r\n");
                    sb.Append("                    </div>\r\n");
                }

                sb.Append("                </div>\r\n");
                sb.Append("                <div class=\"lr-layout-tool-right\">\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\">\r\n");
                sb.Append("                        <a id=\"lr_refresh\" class=\"btn btn-default\"><i class=\"fa fa-refresh\"></i></a>\r\n");
                sb.Append("                    </div>\r\n");

                if (colData.btns.Length > 0)
                {
                    sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");

                    foreach (string btn in colData.btns)
                    {
                        switch (btn)
                        {
                            case "add":
                                sb.Append("                        <a id=\"lr_add\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;新增</a>\r\n");
                                break;
                            case "save":
                                sb.Append("                        <a id=\"lr_save\"  class=\"btn btn-default\"><i class=\"fa fa-pencil-square-o\"></i>&nbsp;保存</a>\r\n");
                                break;
                            case "delete":
                                sb.Append("                        <a id=\"lr_delete\" class=\"btn btn-default\"><i class=\"fa fa-trash-o\"></i>&nbsp;删除</a>\r\n");
                                break;
                            case "print":
                                sb.Append("                        <a id=\"lr_print\"   class=\"btn btn-default\"><i class=\"fa fa-print\"></i>&nbsp;打印</a>\r\n");
                                break;
                        }
                    }
                    sb.Append("                    </div>\r\n");
                }
                if (colData.btnexs.Count > 0)
                {
                    sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");
                    foreach (var btn in colData.btnexs)
                    {
                        sb.Append("                        <a id=\"" + btn.id + "\"   class=\"btn btn-default\"><i class=\"fa fa-plus\"></i>&nbsp;" + btn.name + "</a>\r\n");
                    }
                    sb.Append("                    </div>\r\n");
                }


                sb.Append("                </div>\r\n");
                sb.Append("            </div>\r\n");
                sb.Append("            <div class=\"lr-layout-body\" id=\"gridtable\"></div>\r\n");
                sb.Append("        </div>\r\n");
                sb.Append("    </div>\r\n");
                sb.Append("</div>\r\n");
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseInfo.outputArea + "/Views/" + baseInfo.name + "/Index.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 业务类创建(报表开发模板) 
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string ReportIndexCreate(BaseModel baseInfo, QueryModel queryData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseInfo.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
                sb.Append("}\r\n");

                sb.Append("<div class=\"lr-layout\" >\r\n");

                sb.Append("    <div class=\"lr-layout-center\">\r\n");
                sb.Append("        <div class=\"lr-layout-wrap lr-layout-wrap-notitle \">\r\n");
                sb.Append("            <div class=\"lr-layout-tool\">\r\n");
                sb.Append("                <div class=\"lr-layout-tool-left\">\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"datesearch\"></div>\r\n");
                    sb.Append("                    </div>\r\n");
                }
                if (queryData.fields.Count > 0)
                {
                    sb.Append("                    <div class=\"lr-layout-tool-item\">\r\n");
                    sb.Append("                        <div id=\"multiple_condition_query\">\r\n");
                    sb.Append("                            <div class=\"lr-query-formcontent\">\r\n");
                    foreach (var item in queryData.fields)
                    {
                        sb.Append("                                <div class=\"col-xs-" + (12 / Convert.ToInt32(item.portion)) + " lr-form-item\">\r\n");
                        sb.Append("                                    <div class=\"lr-form-item-title\">" + item.name + "</div>\r\n");

                        sb.Append("                                    <input id=\"" + item.field + "\" type=\"text\" class=\"form-control\" />\r\n");

                        sb.Append("                                </div>\r\n");
                    }
                    sb.Append("                            </div>\r\n");
                    sb.Append("                        </div>\r\n");
                    sb.Append("                    </div>\r\n");
                }

                sb.Append("                </div>\r\n");
                sb.Append("                <div class=\"lr-layout-tool-right\">\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\">\r\n");
                sb.Append("                        <a id=\"lr_refresh\" class=\"btn btn-default\"><i class=\"fa fa-refresh\"></i></a>\r\n");
                sb.Append("                    </div>\r\n");
                sb.Append("                    <div class=\" btn-group btn-group-sm\" learun-authorize=\"yes\">\r\n");
                sb.Append("                        <a id=\"lr_outport\"   class=\"btn btn-default\"><i class=\"fa fa-sign-out\"></i>&nbsp;导出</a>\r\n");
                sb.Append("                        <a id=\"lr_print\"   class=\"btn btn-default\"><i class=\"fa fa-print\"></i>&nbsp;打印</a>\r\n");
                sb.Append("                    </div>\r\n");
                sb.Append("                </div>\r\n");
                sb.Append("            </div>\r\n");
                sb.Append("            <div class=\"lr-layout-body\" id=\"gridtable\"></div>\r\n");
                sb.Append("        </div>\r\n");
                sb.Append("    </div>\r\n");
                sb.Append("</div>\r\n");
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseInfo.outputArea + "/Views/" + baseInfo.name + "/Index.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 页面JS类
        /// <summary>
        /// 业务JS类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string IndexJSCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                UserInfo userInfo = LoginUserInfo.Get();


                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(baseConfigModel.databaseLinkId, baseConfigModel.tableName);
                string col = "";
                string pk = "";
                foreach (var fileditem in fieldList)
                {
                    col += "                        { label: '" + fileditem.f_remark + "', name: '" + fileditem.f_column + "', width: 200, align: \"left\" },\r\n";
                    if (fileditem.f_key == "1")
                    {
                        pk = fileditem.f_column;
                    }
                }

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseConfigModel.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var selectedRow;\r\n");
                sb.Append("var refreshGirdData;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initGird();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");
                sb.Append("            // 查询\r\n");
                sb.Append("            $('#btn_Search').on('click', function () {\r\n");
                sb.Append("                var keyword = $('#txt_Keyword').val();\r\n");
                sb.Append("                page.search({ keyword: keyword });\r\n");
                sb.Append("            });\r\n");
                sb.Append("            // 刷新\r\n");
                sb.Append("            $('#lr_refresh').on('click', function () {\r\n");
                sb.Append("                location.reload();\r\n");
                sb.Append("            });\r\n");
                sb.Append("            // 新增\r\n");
                sb.Append("            $('#lr_add').on('click', function () {\r\n");
                sb.Append("                selectedRow = null;\r\n");
                sb.Append("                learun.layerForm({\r\n");
                sb.Append("                    id: 'form',\r\n");
                sb.Append("                    title: '新增',\r\n");
                sb.Append("                    url: top.$.rootUrl + '/" + baseConfigModel.area + "/" + baseConfigModel.name + "/Form',\r\n");
                sb.Append("                    width: 700,\r\n");
                sb.Append("                    height: 400,\r\n");
                sb.Append("                    callBack: function (id) {\r\n");
                sb.Append("                        return top[id].acceptClick(refreshGirdData);\r\n");
                sb.Append("                    }\r\n");
                sb.Append("                });\r\n");
                sb.Append("            });\r\n");
                sb.Append("            // 编辑\r\n");
                sb.Append("            $('#lr_edit').on('click', function () {\r\n");
                sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + pk + "');\r\n");
                sb.Append("                selectedRow = $('#gridtable').jfGridGet('rowdata');\r\n");
                sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                sb.Append("                    learun.layerForm({\r\n");
                sb.Append("                        id: 'form',\r\n");
                sb.Append("                        title: '编辑',\r\n");
                sb.Append("                        url: top.$.rootUrl + '/" + baseConfigModel.area + "/" + baseConfigModel.name + "/Form?keyValue=' + keyValue,\r\n");
                sb.Append("                        width: 700,\r\n");
                sb.Append("                        height: 400,\r\n");
                sb.Append("                        callBack: function (id) {\r\n");
                sb.Append("                            return top[id].acceptClick(refreshGirdData);\r\n");
                sb.Append("                        }\r\n");
                sb.Append("                    });\r\n");
                sb.Append("                }\r\n");
                sb.Append("            });\r\n");
                sb.Append("            // 删除\r\n");
                sb.Append("            $('#lr_delete').on('click', function () {\r\n");
                sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + pk + "');\r\n");
                sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                sb.Append("                    learun.layerConfirm('是否确认删除该项！', function (res) {\r\n");
                sb.Append("                        if (res) {\r\n");
                sb.Append("                            learun.deleteForm(top.$.rootUrl + '/" + baseConfigModel.area + "/" + baseConfigModel.name + "/DeleteForm', { keyValue: keyValue}, function () {\r\n");
                sb.Append("                            });\r\n");
                sb.Append("                        }\r\n");
                sb.Append("                    });\r\n");
                sb.Append("                }\r\n");
                sb.Append("            });\r\n");
                sb.Append("        },\r\n");
                sb.Append("        initGird: function () {\r\n");
                sb.Append("            $('#gridtable').lrAuthorizeJfGrid({\r\n");
                sb.Append("                url: top.$.rootUrl + '/" + baseConfigModel.area + "/" + baseConfigModel.name + "/GetPageList',\r\n");
                sb.Append("                headData: [\r\n");
                sb.Append(col);
                sb.Append("                ],\r\n");
                sb.Append("                mainId:'" + pk + "',\r\n");
                sb.Append("                isPage: true\r\n");
                sb.Append("            });\r\n");
                sb.Append("            page.search();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        search: function (param) {\r\n");
                sb.Append("            param = param || {};\r\n");
                sb.Append("            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    refreshGirdData = function () {\r\n");
                sb.Append("        page.search();\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 业务JS类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="dbTableList"></param>
        /// <param name="compontMap"></param>
        /// <param name="colData"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string IndexJSCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData, QueryModel queryData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }


                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var refreshGirdData;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");

                if (queryData.isDate == "1")
                {
                    sb.Append("    var startTime;\r\n");
                    sb.Append("    var endTime;\r\n");
                }

                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initGird();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            // 初始化左侧树形数据\r\n");
                    sb.Append("            $('#dataTree').lrtree({\r\n");
                    if (colData.treeSource == "1")
                    {// 数据源获取
                        sb.Append("                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetTree?code=" + colData.treeSourceId + "&parentId=" + colData.treeParentId + "&Id=" + colData.treefieldId + "&showId=" + colData.treefieldShow + "',\r\n");
                    }
                    else
                    {// sql语句的
                        sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetTree',\r\n");
                    }
                    sb.Append("                nodeClick: function (item) {\r\n");
                    sb.Append("                    page.search({ " + colData.treefieldRe + ": item.value });\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");                   
                }

                if (queryData.isDate == "1")
                {
                    sb.Append("            // 时间搜索框\r\n");
                    sb.Append("            $('#datesearch').lrdate({\r\n");
                    sb.Append("                dfdata: [\r\n");
                    sb.Append("                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }\r\n");
                    sb.Append("                ],\r\n");
                    sb.Append("                // 月\r\n");
                    sb.Append("                mShow: false,\r\n");
                    sb.Append("                premShow: false,\r\n");
                    sb.Append("                // 季度\r\n");
                    sb.Append("                jShow: false,\r\n");
                    sb.Append("                prejShow: false,\r\n");
                    sb.Append("                // 年\r\n");
                    sb.Append("                ysShow: false,\r\n");
                    sb.Append("                yxShow: false,\r\n");
                    sb.Append("                preyShow: false,\r\n");
                    sb.Append("                yShow: false,\r\n");
                    sb.Append("                // 默认\r\n");
                    sb.Append("                dfvalue: '1',\r\n");
                    sb.Append("                selectfn: function (begin, end) {\r\n");
                    sb.Append("                    startTime = begin;\r\n");
                    sb.Append("                    endTime = end;\r\n");
                    sb.Append("                    page.search();\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.fields.Count > 0)
                {
                    sb.Append("            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {\r\n");
                    sb.Append("                page.search(queryJson);\r\n");
                    sb.Append("            }, " + (queryData.height > 0 ? queryData.height : 220) + ", " + (queryData.width > 0 ? queryData.width : 400) + ");\r\n");

                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        if (compont.type == "select")
                        {
                            if (compont.dataSource == "0")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                            }
                        }
                        else if (compont.type == "organize" || compont.type == "currentInfo")
                        {
                            if (compont.dataType == "company")
                            {
                                sb.Append("            $('#" + compont.field + "').lrCompanySelect();\r\n");
                            }
                            else if (compont.dataType == "department")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDepartmentSelect();\r\n");
                            }
                            else if (compont.dataType == "user")
                            {
                                sb.Append("            $('#" + compont.field + "').lrUserSelect(0);\r\n");
                            }
                        }
                        else if (compont.type == "radio" || compont.type == "checkbox")
                        {

                            sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                            sb.Append("                type: '" + compont.type + "',\r\n");
                            if (compont.dataSource == "0")
                            {
                                sb.Append("                code: '" + compont.itemCode + "',\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                dataType: 'dataSource',\r\n");
                                sb.Append("                code: '" + vlist[0] + "',\r\n");
                                sb.Append("                value: '" + vlist[2] + "',\r\n");
                                sb.Append("                text: '" + vlist[1] + "',\r\n");
                            }
                            sb.Append("            });\r\n");
                        }
                    }
                }

                sb.Append("            // 刷新\r\n");
                sb.Append("            $('#lr_refresh').on('click', function () {\r\n");
                sb.Append("                location.reload();\r\n");
                sb.Append("            });\r\n");

                foreach (var btn in colData.btns) {
                    switch (btn) {
                        case "add":
                            sb.Append("            // 新增\r\n");
                            sb.Append("            $('#lr_add').on('click', function () {\r\n");
                            sb.Append("                learun.layerForm({\r\n");
                            sb.Append("                    id: 'form',\r\n");
                            sb.Append("                    title: '新增',\r\n");
                            sb.Append("                    url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form',\r\n");
                            sb.Append("                    width: 600,\r\n");
                            sb.Append("                    height: 400,\r\n");
                            sb.Append("                    callBack: function (id) {\r\n");
                            sb.Append("                        return top[id].acceptClick(refreshGirdData);\r\n");
                            sb.Append("                    }\r\n");
                            sb.Append("                });\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "edit":
                            sb.Append("            // 编辑\r\n");
                            sb.Append("            $('#lr_edit').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + mainPkey + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerForm({\r\n");
                            sb.Append("                        id: 'form',\r\n");
                            sb.Append("                        title: '编辑',\r\n");
                            sb.Append("                        url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form?keyValue=' + keyValue,\r\n");
                            sb.Append("                        width: 600,\r\n");
                            sb.Append("                        height: 400,\r\n");
                            sb.Append("                        callBack: function (id) {\r\n");
                            sb.Append("                            return top[id].acceptClick(refreshGirdData);\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "delete":
                            sb.Append("            // 删除\r\n");
                            sb.Append("            $('#lr_delete').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + mainPkey + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerConfirm('是否确认删除该项！', function (res) {\r\n");
                            sb.Append("                        if (res) {\r\n");
                            sb.Append("                            learun.deleteForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/DeleteForm', { keyValue: keyValue}, function () {\r\n");
                            sb.Append("                                refreshGirdData();\r\n");
                            sb.Append("                            });\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "print":
                            sb.Append("            // 打印\r\n");
                            sb.Append("            $('#lr_print').on('click', function () {\r\n");
                            sb.Append("                $('#gridtable').jqprintTable();\r\n");
                            sb.Append("            });\r\n");
                            break;
                    }
                }

                foreach (var btn in colData.btnexs)
                {
                    sb.Append("            // " + btn.name + "\r\n");
                    sb.Append("            $('#" + btn.id + "').on('click', function () {\r\n");
                    sb.Append("            });\r\n");
                }

                sb.Append("        },\r\n");
                sb.Append("        // 初始化列表\r\n");
                sb.Append("        initGird: function () {\r\n");
                sb.Append("            $('#gridtable').lrAuthorizeJfGrid({\r\n");
                // 判断是否分页
                if (colData.isPage == "1")
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetPageList',\r\n");
                }
                else
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetList',\r\n");
                }

                sb.Append("                headData: [\r\n");
                foreach (var col in colData.fields)
                {
                    sb.Append("                    { label: \"" + col.fieldName + "\", name: \"" + col.field + "\", width: " + col.width + ", align: \"" + col.align + "\"");

                    CodeFormCompontModel compont = compontMap[col.id];
                    if (compont.type == "select" || compont.type == "radio")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data['" + vlist[1] + "']);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "checkbox") {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getsAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getsAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 textId: '" + vlist[1] + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "organize" || compont.type == "currentInfo") {
                        if (compont.dataType == "company" || compont.dataType == "department" || compont.dataType == "user")
                        {
                            sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                            sb.Append("                             learun.clientdata.getAsync('"+ compont.dataType + "', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.name);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    sb.Append("},\r\n");
                }
                sb.Append("                ],\r\n");
                sb.Append("                mainId:'" + mainPkey + "',\r\n");
                if (colData.isPage == "1")
                {
                    sb.Append("                isPage: true\r\n");
                }
                sb.Append("            });\r\n");
                if (queryData.isDate != "1")
                {
                    sb.Append("            page.search();\r\n");
                }
                sb.Append("        },\r\n");
                sb.Append("        search: function (param) {\r\n");
                sb.Append("            param = param || {};\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("            param.StartTime = startTime;\r\n");
                    sb.Append("            param.EndTime = endTime;\r\n");
                }
                sb.Append("            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    refreshGirdData = function () {\r\n");
                sb.Append("        page.search();\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 业务JS类创建(流程系统表单开发模板)
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="dbTableList"></param>
        /// <param name="compontMap"></param>
        /// <param name="colData"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string WfIndexJSCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData, QueryModel queryData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                // 寻找主表 和 将表数据转成树形数据
                string mainTable = "";
                string mainPkey = "";
                Dictionary<string, DbTableModel> dbTableMap = new Dictionary<string, DbTableModel>();
                List<TreeModelEx<DbTableModel>> TableTree = new List<TreeModelEx<DbTableModel>>();
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                    dbTableMap.Add(tableOne.name, tableOne);

                    TreeModelEx<DbTableModel> treeone = new TreeModelEx<DbTableModel>();
                    treeone.data = tableOne;
                    treeone.id = tableOne.name;
                    treeone.parentId = tableOne.relationName;
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        treeone.parentId = "0";
                    }
                    TableTree.Add(treeone);
                }


                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var refreshGirdData;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");

                if (queryData.isDate == "1")
                {
                    sb.Append("    var startTime;\r\n");
                    sb.Append("    var endTime;\r\n");
                }

                sb.Append("    var processId = '';\r\n");
                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initGird();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            // 初始化左侧树形数据\r\n");
                    sb.Append("            $('#dataTree').lrtree({\r\n");
                    if (colData.treeSource == "1")
                    {// 数据源获取
                        sb.Append("                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetTree?code=" + colData.treeSourceId + "&parentId=" + colData.treeParentId + "&Id=" + colData.treefieldId + "&showId=" + colData.treefieldShow + "',\r\n");
                    }
                    else
                    {// sql语句的
                        sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetTree',\r\n");
                    }
                    sb.Append("                nodeClick: function (item) {\r\n");
                    sb.Append("                    page.search({ " + colData.treefieldRe + ": item.value });\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.isDate == "1")
                {
                    sb.Append("            // 时间搜索框\r\n");
                    sb.Append("            $('#datesearch').lrdate({\r\n");
                    sb.Append("                dfdata: [\r\n");
                    sb.Append("                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }\r\n");
                    sb.Append("                ],\r\n");
                    sb.Append("                // 月\r\n");
                    sb.Append("                mShow: false,\r\n");
                    sb.Append("                premShow: false,\r\n");
                    sb.Append("                // 季度\r\n");
                    sb.Append("                jShow: false,\r\n");
                    sb.Append("                prejShow: false,\r\n");
                    sb.Append("                // 年\r\n");
                    sb.Append("                ysShow: false,\r\n");
                    sb.Append("                yxShow: false,\r\n");
                    sb.Append("                preyShow: false,\r\n");
                    sb.Append("                yShow: false,\r\n");
                    sb.Append("                // 默认\r\n");
                    sb.Append("                dfvalue: '1',\r\n");
                    sb.Append("                selectfn: function (begin, end) {\r\n");
                    sb.Append("                    startTime = begin;\r\n");
                    sb.Append("                    endTime = end;\r\n");
                    sb.Append("                    page.search();\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.fields.Count > 0)
                {
                    sb.Append("            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {\r\n");
                    sb.Append("                page.search(queryJson);\r\n");
                    sb.Append("            }, " + (queryData.height > 0 ? queryData.height : 220) + ", " + (queryData.width > 0 ? queryData.width : 400) + ");\r\n");

                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        if (compont.type == "select")
                        {
                            if (compont.dataSource == "0")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                            }
                        }
                        else if (compont.type == "organize" || compont.type == "currentInfo")
                        {
                            if (compont.dataType == "company")
                            {
                                sb.Append("            $('#" + compont.field + "').lrCompanySelect();\r\n");
                            }
                            else if (compont.dataType == "department")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDepartmentSelect();\r\n");
                            }
                            else if (compont.dataType == "user")
                            {
                                sb.Append("            $('#" + compont.field + "').lrUserSelect(0);\r\n");
                            }
                        }
                        else if (compont.type == "radio" || compont.type == "checkbox")
                        {

                            sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                            sb.Append("                type: '" + compont.type + "',\r\n");
                            if (compont.dataSource == "0")
                            {
                                sb.Append("                code: '" + compont.itemCode + "',\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                dataType: 'dataSource',\r\n");
                                sb.Append("                code: '" + vlist[0] + "',\r\n");
                                sb.Append("                value: '" + vlist[2] + "',\r\n");
                                sb.Append("                text: '" + vlist[1] + "',\r\n");
                            }
                            sb.Append("            });\r\n");
                        }
                    }
                }

                sb.Append("            // 刷新\r\n");
                sb.Append("            $('#lr_refresh').on('click', function () {\r\n");
                sb.Append("                location.reload();\r\n");
                sb.Append("            });\r\n");

                foreach (var btn in colData.btns)
                {
                    switch (btn)
                    {
                        case "add":
                            sb.Append("            // 新增\r\n");
                            sb.Append("            $('#lr_add').on('click', function () {\r\n");
                            sb.Append("                learun.layerForm({\r\n");
                            sb.Append("                    id: 'form',\r\n");
                            sb.Append("                    title: '新增',\r\n");
                            sb.Append("                    url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form',\r\n");
                            sb.Append("                    width: 600,\r\n");
                            sb.Append("                    height: 400,\r\n");
                            sb.Append("                    callBack: function (id) {\r\n");
                            sb.Append("                        var res = false;\r\n");
                            sb.Append("                        // 验证数据\r\n");
                            sb.Append("                        res = top[id].validForm();\r\n");
                            sb.Append("                        // 保存数据\r\n");
                            sb.Append("                        if (res) {\r\n");
                            sb.Append("                            processId = learun.newGuid();\r\n");
                            sb.Append("                            res = top[id].save(processId, refreshGirdData);\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                        return res;\r\n");
                            sb.Append("                    }\r\n");
                            sb.Append("                });\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "edit":
                            sb.Append("            // 编辑\r\n");
                            sb.Append("            $('#lr_edit').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + mainPkey + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerForm({\r\n");
                            sb.Append("                        id: 'form',\r\n");
                            sb.Append("                        title: '编辑',\r\n");
                            sb.Append("                        url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form?keyValue=' + keyValue,\r\n");
                            sb.Append("                        width: 600,\r\n");
                            sb.Append("                        height: 400,\r\n");
                            sb.Append("                        callBack: function (id) {\r\n");

                            sb.Append("                            var res = false;\r\n");
                            sb.Append("                            // 验证数据\r\n");
                            sb.Append("                            res = top[id].validForm();\r\n");
                            sb.Append("                            // 保存数据\r\n");
                            sb.Append("                            if (res) {\r\n");
                            sb.Append("                                res = top[id].save('', function () {\r\n");
                            sb.Append("                                    page.search();\r\n");
                            sb.Append("                                });\r\n");
                            sb.Append("                            }\r\n");
                            sb.Append("                            return res;\r\n");

                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "delete":
                            sb.Append("            // 删除\r\n");
                            sb.Append("            $('#lr_delete').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + mainPkey + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerConfirm('是否确认删除该项！', function (res) {\r\n");
                            sb.Append("                        if (res) {\r\n");
                            sb.Append("                            learun.deleteForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/DeleteForm', { keyValue: keyValue}, function () {\r\n");
                            sb.Append("                                refreshGirdData();\r\n");
                            sb.Append("                            });\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "print":
                            sb.Append("            // 打印\r\n");
                            sb.Append("            $('#lr_print').on('click', function () {\r\n");
                            sb.Append("                $('#gridtable').jqprintTable();\r\n");
                            sb.Append("            });\r\n");
                            break;
                    }
                }

                foreach (var btn in colData.btnexs)
                {
                    sb.Append("            // " + btn.name + "\r\n");
                    sb.Append("            $('#" + btn.id + "').on('click', function () {\r\n");
                    sb.Append("            });\r\n");
                }

                sb.Append("        },\r\n");
                sb.Append("        // 初始化列表\r\n");
                sb.Append("        initGird: function () {\r\n");
                sb.Append("            $('#gridtable').lrAuthorizeJfGrid({\r\n");
                // 判断是否分页
                if (colData.isPage == "1")
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetPageList',\r\n");
                }
                else
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetList',\r\n");
                }

                sb.Append("                headData: [\r\n");
                foreach (var col in colData.fields)
                {
                    sb.Append("                    { label: \"" + col.fieldName + "\", name: \"" + col.field + "\", width: " + col.width + ", align: \"" + col.align + "\"");

                    CodeFormCompontModel compont = compontMap[col.id];
                    if (compont.type == "select" || compont.type == "radio")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data['" + vlist[1] + "']);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "checkbox")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getsAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getsAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 textId: '" + vlist[1] + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "organize" || compont.type == "currentInfo")
                    {
                        if (compont.dataType == "company" || compont.dataType == "department" || compont.dataType == "user")
                        {
                            sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                            sb.Append("                             learun.clientdata.getAsync('"+ compont.dataType + "', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.name);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    sb.Append("},\r\n");
                }
                sb.Append("                ],\r\n");
                sb.Append("                mainId:'" + mainPkey + "',\r\n");
                if (colData.isPage == "1")
                {
                    sb.Append("                isPage: true\r\n");
                }
                sb.Append("            });\r\n");
                if (queryData.isDate != "1")
                {
                    sb.Append("            page.search();\r\n");
                }
                sb.Append("        },\r\n");
                sb.Append("        search: function (param) {\r\n");
                sb.Append("            param = param || {};\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("            param.StartTime = startTime;\r\n");
                    sb.Append("            param.EndTime = endTime;\r\n");
                }
                sb.Append("            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    refreshGirdData = function (res, postData) {\r\n");
                sb.Append("        if (res.code == 200)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            // 发起流程\r\n");
                sb.Append("            learun.workflowapi.create({\r\n");
                sb.Append("                isNew: true,\r\n");
                sb.Append("                schemeCode: '',// 填写流程对应模板编号\r\n");
                sb.Append("                processId: processId,\r\n");
                sb.Append("                processName: '系统表单流程',// 对应流程名称\r\n");
                sb.Append("                processLevel: '1',\r\n");
                sb.Append("                description: '',\r\n");
                sb.Append("                formData: JSON.stringify(postData),\r\n");
                sb.Append("                callback: function (res, data) {\r\n");
                sb.Append("                }\r\n");
                sb.Append("            });\r\n");
                sb.Append("            page.search();\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 业务JS类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="dbTablePk"></param>
        /// <param name="compontMap"></param>
        /// <param name="colData"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string GridEditIndexJSCreate(BaseModel baseInfo,string dbTablePk, Dictionary<string, CodeFormCompontModel> compontMap, ColModel colData, QueryModel queryData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var refreshGirdData;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var map = {};\r\n");


                if (queryData.isDate == "1")
                {
                    sb.Append("    var startTime;\r\n");
                    sb.Append("    var endTime;\r\n");
                }

                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initGird();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (colData.isTree == "1")
                {
                    sb.Append("            // 初始化左侧树形数据\r\n");
                    sb.Append("            $('#dataTree').lrtree({\r\n");
                    if (colData.treeSource == "1")
                    {// 数据源获取
                        sb.Append("                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetTree?code=" + colData.treeSourceId + "&parentId=" + colData.treeParentId + "&Id=" + colData.treefieldId + "&showId=" + colData.treefieldShow + "',\r\n");
                    }
                    else
                    {// sql语句的
                        sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetTree',\r\n");
                    }
                    sb.Append("                nodeClick: function (item) {\r\n");
                    sb.Append("                    page.search({ " + colData.treefieldRe + ": item.value });\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.isDate == "1")
                {
                    sb.Append("            // 时间搜索框\r\n");
                    sb.Append("            $('#datesearch').lrdate({\r\n");
                    sb.Append("                dfdata: [\r\n");
                    sb.Append("                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }\r\n");
                    sb.Append("                ],\r\n");
                    sb.Append("                // 月\r\n");
                    sb.Append("                mShow: false,\r\n");
                    sb.Append("                premShow: false,\r\n");
                    sb.Append("                // 季度\r\n");
                    sb.Append("                jShow: false,\r\n");
                    sb.Append("                prejShow: false,\r\n");
                    sb.Append("                // 年\r\n");
                    sb.Append("                ysShow: false,\r\n");
                    sb.Append("                yxShow: false,\r\n");
                    sb.Append("                preyShow: false,\r\n");
                    sb.Append("                yShow: false,\r\n");
                    sb.Append("                // 默认\r\n");
                    sb.Append("                dfvalue: '1',\r\n");
                    sb.Append("                selectfn: function (begin, end) {\r\n");
                    sb.Append("                    startTime = begin;\r\n");
                    sb.Append("                    endTime = end;\r\n");
                    sb.Append("                    page.search();\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.fields.Count > 0)
                {
                    sb.Append("            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {\r\n");
                    sb.Append("                page.search(queryJson);\r\n");
                    sb.Append("            }, " + (queryData.height > 0 ? queryData.height : 220) + ", " + (queryData.width > 0 ? queryData.width : 400) + ");\r\n");

                    foreach (var item in queryData.fields)
                    {
                        CodeFormCompontModel compont = compontMap[item.id];
                        if (compont.type == "select")
                        {
                            if (compont.dataSource == "0")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                            }
                        }
                        else if (compont.type == "organize" || compont.type == "currentInfo")
                        {
                            if (compont.dataType == "company")
                            {
                                sb.Append("            $('#" + compont.field + "').lrCompanySelect();\r\n");
                            }
                            else if (compont.dataType == "department")
                            {
                                sb.Append("            $('#" + compont.field + "').lrDepartmentSelect();\r\n");
                            }
                            else if (compont.dataType == "user")
                            {
                                sb.Append("            $('#" + compont.field + "').lrUserSelect(0);\r\n");
                            }
                        }
                        else if (compont.type == "radio" || compont.type == "checkbox")
                        {

                            sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                            sb.Append("                type: '" + compont.type + "',\r\n");
                            if (compont.dataSource == "0")
                            {
                                sb.Append("                code: '" + compont.itemCode + "',\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                dataType: 'dataSource',\r\n");
                                sb.Append("                code: '" + vlist[0] + "',\r\n");
                                sb.Append("                value: '" + vlist[2] + "',\r\n");
                                sb.Append("                text: '" + vlist[1] + "',\r\n");
                            }
                            sb.Append("            });\r\n");
                        }
                    }
                }

                sb.Append("            // 刷新\r\n");
                sb.Append("            $('#lr_refresh').on('click', function () {\r\n");
                sb.Append("                location.reload();\r\n");
                sb.Append("            });\r\n");

                foreach (var btn in colData.btns)
                {
                    switch (btn)
                    {
                        case "add":
                            sb.Append("            // 新增\r\n");
                            sb.Append("            $('#lr_add').on('click', function () {\r\n");
                            sb.Append("                learun.layerForm({\r\n");
                            sb.Append("                    id: 'form',\r\n");
                            sb.Append("                    title: '新增',\r\n");
                            sb.Append("                    url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/Form',\r\n");
                            sb.Append("                    width: 600,\r\n");
                            sb.Append("                    height: 400,\r\n");
                            sb.Append("                    callBack: function (id) {\r\n");
                            sb.Append("                        return top[id].acceptClick(refreshGirdData);\r\n");
                            sb.Append("                    }\r\n");
                            sb.Append("                });\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "save":
                            sb.Append("            // 保存\r\n");
                            sb.Append("            $('#lr_save').on('click', function () {\r\n");
                            sb.Append("                var rows = $('#gridtable').jfGridGet('rowdatas');\r\n");
                            sb.Append("                var postData = [];\r\n");
                            sb.Append("                $.each(rows, function (_index, _item) {\r\n");
                            sb.Append("                    if (map[_item." + dbTablePk + "]) {\r\n");
                            sb.Append("                        postData.push(_item);\r\n");
                            sb.Append("                    }\r\n");
                            sb.Append("                });\r\n");
                            sb.Append("                if (postData.length > 0) {\r\n");
                            sb.Append("                    $.lrSaveForm(top.$.rootUrl +  '/" + baseInfo.outputArea + "/" + baseInfo.name + "/SaveList', { list: postData }, function (res) {\r\n");
                            sb.Append("                        refreshGirdData();\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("                else {\r\n");
                            sb.Append("                    learun.alert.warning(\"没有数据需要保存!\");\r\n");
                            sb.Append("                }\r\n");

                            sb.Append("            });\r\n");
                            break;
                        case "delete":
                            sb.Append("            // 删除\r\n");
                            sb.Append("            $('#lr_delete').on('click', function () {\r\n");
                            sb.Append("                var keyValue = $('#gridtable').jfGridValue('" + dbTablePk + "');\r\n");
                            sb.Append("                if (learun.checkrow(keyValue)) {\r\n");
                            sb.Append("                    learun.layerConfirm('是否确认删除该项！', function (res) {\r\n");
                            sb.Append("                        if (res) {\r\n");
                            sb.Append("                            learun.deleteForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/DeleteForm', { keyValue: keyValue}, function () {\r\n");
                            sb.Append("                                refreshGirdData();\r\n");
                            sb.Append("                            });\r\n");
                            sb.Append("                        }\r\n");
                            sb.Append("                    });\r\n");
                            sb.Append("                }\r\n");
                            sb.Append("            });\r\n");
                            break;
                        case "print":
                            sb.Append("            // 打印\r\n");
                            sb.Append("            $('#lr_print').on('click', function () {\r\n");
                            sb.Append("                $('#gridtable').jqprintTable();\r\n");
                            sb.Append("            });\r\n");
                            break;
                    }
                }

                foreach (var btn in colData.btnexs)
                {
                    sb.Append("            // " + btn.name + "\r\n");
                    sb.Append("            $('#" + btn.id + "').on('click', function () {\r\n");
                    sb.Append("            });\r\n");
                }

                sb.Append("        },\r\n");
                sb.Append("        // 初始化列表\r\n");
                sb.Append("        initGird: function () {\r\n");
                sb.Append("            $('#gridtable').lrAuthorizeJfGrid({\r\n");
                // 判断是否分页
                if (colData.isPage == "1")
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetPageList',\r\n");
                }
                else
                {
                    sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetList',\r\n");
                }

                sb.Append("                headData: [\r\n");
                foreach (var col in colData.fields)
                {
                    sb.Append("                    { label: \"" + col.fieldName + "\", name: \"" + col.field + "\", width: " + col.width + ", align: \"" + col.align + "\"");
                    CodeFormCompontModel compont = compontMap[col.id];
                    if (compont.type == "select" || compont.type == "radio")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data.text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 callback: function (_data) {\r\n");
                            sb.Append("                                     callback(_data['" + vlist[1] + "']);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    else if (compont.type == "checkbox")
                    {
                        sb.Append(",\r\n                        formatterAsync: function (callback, value, row, op,$cell) {\r\n");
                        if (compont.dataSource == "0")
                        {
                            sb.Append("                             learun.clientdata.getsAsync('dataItem', {\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 code: '" + compont.itemCode + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                        else
                        {
                            string[] vlist = compont.dataSourceId.Split(',');
                            sb.Append("                             learun.clientdata.getsAsync('custmerData', {\r\n");
                            sb.Append("                                 url: '/LR_SystemModule/DataSource/GetDataTable?code=' + '" + vlist[0] + "',\r\n");
                            sb.Append("                                 key: value,\r\n");
                            sb.Append("                                 keyId: '" + vlist[2] + "',\r\n");
                            sb.Append("                                 textId: '" + vlist[1] + "',\r\n");
                            sb.Append("                                 callback: function (text) {\r\n");
                            sb.Append("                                     callback(text);\r\n");
                            sb.Append("                                 }\r\n");
                            sb.Append("                             });\r\n");
                            sb.Append("                        }");
                        }
                    }
                    switch (compont.type)
                    {
                        case "text":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'input',\r\n");
                            sb.Append("                            change: function (data, num) {\r\n");
                            sb.Append("                                map[data." + dbTablePk + "] = '1';\r\n");
                            sb.Append("                            }\r\n");

                            sb.Append("                        }\r\n");
                            break;
                        case "select":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'select',\r\n");
                            sb.Append("                            change: function (data, num) {\r\n");
                            sb.Append("                                map[data." + dbTablePk + "] = '1';\r\n");
                            sb.Append("                            },\r\n");
                            sb.Append("                            init: function (data, $edit) {\r\n");

                            if (compont.dataSource == "0")
                            {
                                sb.Append("                            learun.clientdata.getAllAsync('dataItem', {\r\n");
                                sb.Append("                                code:'" + compont.itemCode + "',\r\n");
                                sb.Append("                                callback: function (dataes) {\r\n");
                                sb.Append("                                    var list=[];\r\n");
                                sb.Append("                                    $.each(dataes, function (_index, _item) {\r\n");
                                sb.Append("                                        list.push({ id: _item.value, text: _item.text, title: _item.text, k: _index });\r\n");
                                sb.Append("                                    });\r\n");
                                sb.Append("                                    $edit.lrselectRefresh({\r\n");
                                sb.Append("                                        data: list\r\n");
                                sb.Append("                                    });\r\n");
                                sb.Append("                                }\r\n");
                                sb.Append("                            });\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                            learun.clientdata.getAllAsync('sourceData', {\r\n");
                                sb.Append("                                code:'" + vlist[0] + "',\r\n");
                                sb.Append("                                callback: function (dataes) {\r\n");
                                sb.Append("                                    $edit.lrselectRefresh({\r\n");
                                sb.Append("                                        value: '" + vlist[2] + "',\r\n");
                                sb.Append("                                        text:'" + vlist[1] + "',\r\n");
                                sb.Append("                                        title:'" + vlist[1] + "',\r\n");
                                sb.Append("                                        data: dataes\r\n");
                                sb.Append("                                    });\r\n");
                                sb.Append("                                }\r\n");
                                sb.Append("                            });\r\n");
                            }
                            sb.Append("                             }\r\n");
                            sb.Append("                        }\r\n");
                            break;
                        case "radio":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'radio',\r\n");
                            sb.Append("                            change: function (data, num) {\r\n");
                            sb.Append("                                map[data." + dbTablePk + "] = '1';\r\n");
                            sb.Append("                            },\r\n");
                            if (compont.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + compont.itemCode + "',\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + vlist[0] + "',\r\n");
                                sb.Append("                            text:'" + vlist[1] + "',\r\n");
                                sb.Append("                            value:'" + vlist[2] + "',\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "checkbox":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'checkbox',\r\n");
                            sb.Append("                            change: function (data, num) {\r\n");
                            sb.Append("                                map[data." + dbTablePk + "] = '1';\r\n");
                            sb.Append("                            },\r\n");
                            if (compont.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + compont.itemCode + "',\r\n");
                            }
                            else
                            {
                                string[] vlist = compont.dataSourceId.Split(',');
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + vlist[0] + "',\r\n");
                                sb.Append("                            text:'" + vlist[1] + "',\r\n");
                                sb.Append("                            value:'" + vlist[2] + "',\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "datetime":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'datatime',\r\n");
                            sb.Append("                            change: function (data, num) {\r\n");
                            sb.Append("                                map[data." + dbTablePk + "] = '1';\r\n");
                            sb.Append("                            },\r\n");
                            if (compont.dateformat == "0")
                            {
                                sb.Append("                            dateformat: '0'\r\n");
                            }
                            else
                            {
                                sb.Append("                            dateformat: '1'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                    }
                    sb.Append("                    },\r\n");
                }
                sb.Append("                ],\r\n");
                sb.Append("                mainId:'" + dbTablePk + "',\r\n");
                if (colData.isPage == "1")
                {
                    sb.Append("                isPage: true\r\n");
                }
                sb.Append("            });\r\n");
                if (queryData.isDate != "1")
                {
                    sb.Append("            page.search();\r\n");
                }
                sb.Append("        },\r\n");
                sb.Append("        search: function (param) {\r\n");
                sb.Append("            param = param || {};\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("            param.StartTime = startTime;\r\n");
                    sb.Append("            param.EndTime = endTime;\r\n");
                }
                sb.Append("            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    refreshGirdData = function () {\r\n");
                sb.Append("        page.search();\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 业务JS类创建(报表开发模板)
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="collist"></param>
        /// <param name="queryData"></param>
        /// <returns></returns>
        public string ReportIndexJSCreate(BaseModel baseInfo, List<ReportColModel> collist, QueryModel queryData)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var refreshGirdData;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var map = {};\r\n");


                if (queryData.isDate == "1")
                {
                    sb.Append("    var startTime;\r\n");
                    sb.Append("    var endTime;\r\n");
                }

                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initGird();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (queryData.isDate == "1")
                {
                    sb.Append("            // 时间搜索框\r\n");
                    sb.Append("            $('#datesearch').lrdate({\r\n");
                    sb.Append("                dfdata: [\r\n");
                    sb.Append("                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },\r\n");
                    sb.Append("                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }\r\n");
                    sb.Append("                ],\r\n");
                    sb.Append("                // 月\r\n");
                    sb.Append("                mShow: false,\r\n");
                    sb.Append("                premShow: false,\r\n");
                    sb.Append("                // 季度\r\n");
                    sb.Append("                jShow: false,\r\n");
                    sb.Append("                prejShow: false,\r\n");
                    sb.Append("                // 年\r\n");
                    sb.Append("                ysShow: false,\r\n");
                    sb.Append("                yxShow: false,\r\n");
                    sb.Append("                preyShow: false,\r\n");
                    sb.Append("                yShow: false,\r\n");
                    sb.Append("                // 默认\r\n");
                    sb.Append("                dfvalue: '1',\r\n");
                    sb.Append("                selectfn: function (begin, end) {\r\n");
                    sb.Append("                    startTime = begin;\r\n");
                    sb.Append("                    endTime = end;\r\n");
                    sb.Append("                    page.search();\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            });\r\n");
                }

                if (queryData.fields.Count > 0)
                {
                    sb.Append("            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {\r\n");
                    sb.Append("                page.search(queryJson);\r\n");
                    sb.Append("            }, " + (queryData.height > 0 ? queryData.height : 220) + ", " + (queryData.width > 0 ? queryData.width : 400) + ");\r\n");
                }

                sb.Append("            // 刷新\r\n");
                sb.Append("            $('#lr_refresh').on('click', function () {\r\n");
                sb.Append("                location.reload();\r\n");
                sb.Append("            });\r\n");

                sb.Append("            // 打印\r\n");
                sb.Append("            $('#lr_print').on('click', function () {\r\n");
                sb.Append("                $('#gridtable').jqprintTable();\r\n");
                sb.Append("            });\r\n");

                sb.Append("            // 导出\r\n");
                sb.Append("            $('#lr_outport').on('click', function () {\r\n");

                sb.Append("                learun.download({\r\n");
                sb.Append("                    method: 'POST',\r\n");
                sb.Append("                    url: '/Utility/ExportExcel',\r\n");
                sb.Append("                    param: {\r\n");
                sb.Append("                        fileName: '导出数据列表',\r\n");
                sb.Append("                        columnJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').headData),\r\n");
                sb.Append("                        dataJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').rowdatas)\r\n");
                sb.Append("                    }\r\n");
                sb.Append("                });\r\n");
                sb.Append("            });\r\n");


                sb.Append("        },\r\n");
                sb.Append("        // 初始化列表\r\n");
                sb.Append("        initGird: function () {\r\n");
                sb.Append("            $('#gridtable').lrAuthorizeJfGrid({\r\n");
                sb.Append("                url: top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetList',\r\n");
                sb.Append("                headData: [\r\n");
                foreach (var col in collist)
                {
                    sb.Append("                    { label: \"" + col.name + "\", name: \"" + col.field + "\", width: " + col.width + ", align: \"left\" },\r\n");
                }
                sb.Append("                ],\r\n");
                sb.Append("            });\r\n");
                if (queryData.isDate != "1")
                {
                    sb.Append("            page.search();\r\n");
                }
                sb.Append("        },\r\n");
                sb.Append("        search: function (param) {\r\n");
                sb.Append("            param = param || {};\r\n");
                if (queryData.isDate == "1")
                {
                    sb.Append("            param.StartTime = startTime;\r\n");
                    sb.Append("            param.EndTime = endTime;\r\n");
                }
                sb.Append("            $('#gridtable').jfGridSet('reload',{ queryJson: JSON.stringify(param) });\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    refreshGirdData = function () {\r\n");
                sb.Append("        page.search();\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 表单类
        /// <summary>
        /// 获取表单是否是必填字段标记
        /// </summary>
        /// <param name="verify"></param>
        /// <returns></returns>
        private string GetFontHtml(string verify)
        {
            var res = "";
            switch (verify)
            {
                case "NotNull":
                case "Num":
                case "Email":
                case "EnglishStr":
                case "Phone":
                case "Fax":
                case "Mobile":
                case "MobileOrPhone":
                case "Uri":
                    res = "<font face=\"宋体\">*</font>";
                    break;
            }
            return res;
        }

        /// <summary>
        /// 生成表单组件
        /// </summary>
        /// <param name="sb">字串容器</param>
        /// <param name="componts">组件列表</param>
        private void CreateFormCompont(StringBuilder sb, List<CodeFormCompontModel> componts)
        {
            foreach (var compont in componts)
            {
                if (compont.type == "label") {
                    sb.Append("    <div class=\"col-xs-" + (12 / Convert.ToInt32(compont.proportion)) + " lr-form-item\" style=\"padding:0;line-height:38px;text-align:center;font-size:20px;font-weight:bold;color:#333;\" >\r\n");
                    sb.Append("        <span>" + compont.table + "</span>\r\n");
                    sb.Append("    </div>\r\n");
                }
                else if (compont.type == "girdtable")
                {
                    sb.Append("    <div class=\"col-xs-12 lr-form-item lr-form-item-grid\" >\r\n");
                    sb.Append("        <div id=\"" + compont.table + "\"></div>\r\n");
                    sb.Append("    </div>\r\n");
                }
                else
                {
                    sb.Append("    <div class=\"col-xs-" + (12 / Convert.ToInt32(compont.proportion)) + " lr-form-item\"  data-table=\"" + compont.table + "\" >\r\n");
                    sb.Append("        <div class=\"lr-form-item-title\">" + compont.title + GetFontHtml(compont.verify) + "</div>\r\n");

                    string strValid = "";
                    if (!string.IsNullOrEmpty(compont.verify))
                    {
                        strValid = "isvalid=\"yes\" checkexpession=\"" + compont.verify + "\"";
                    }

                    switch (compont.type)
                    {
                        case "text":
                        case "datetimerange":
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control\" " + strValid + " />\r\n");
                            break;
                        case "textarea":
                            sb.Append("        <textarea id=\"" + compont.field + "\" class=\"form-control\" style=\"height:"+ compont.height + "px;\" " + strValid + " ></textarea>\r\n");
                            break;
                        case "texteditor":
                            sb.Append("        <div id=\"" + compont.field + "\" style=\"height:" + compont.height + "px;\"></div>\r\n");
                            break;
                        case "radio":
                        case "checkbox":
                            sb.Append("        <div id=\"" + compont.field + "\"></div>\r\n");
                            break;
                        case "select":
                        case "upload":
                        case "organize":
                            sb.Append("        <div id=\"" + compont.field + "\" " + strValid + " ></div>\r\n");
                            break;
                        case "datetime":
                            string dateformat = compont.dateformat == "0" ? "yyyy-MM-dd" : "yyyy-MM-dd HH:mm";
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" class=\"form-control lr-input-wdatepicker\" onfocus=\"WdatePicker({ dateFmt:'" + dateformat + "',onpicked: function () { $('#" + compont.field + "').trigger('change'); } })\"  " + strValid + " />\r\n");
                            break;
                        case "encode":
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" readonly class=\"form-control\" />\r\n");
                            break;
                        case "currentInfo":
                            sb.Append("        <input id=\"" + compont.field + "\" type=\"text\" readonly class=\"form-control currentInfo lr-currentInfo-"+ compont.dataType + "\" />\r\n");
                            break;
                    }
                    sb.Append("    </div>\r\n");
                }
            }
        }

        /// <summary>
        /// 表单类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string FormCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseConfigModel.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Form.cshtml\";\r\n");
                sb.Append("}\r\n");

                IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(baseConfigModel.databaseLinkId, baseConfigModel.tableName);
                sb.Append("<div class=\"lr-form-wrap\" id=\"form\">\r\n");
                foreach (var fileditem in fieldList)
                {
                    sb.Append("    <div class=\"col-xs-6 lr-form-item\">\r\n");
                    sb.Append("        <div class=\"lr-form-item-title\">" + fileditem.f_remark + "<font face=\"宋体\">*</font></div>\r\n");
                    sb.Append("        <input id=\"" + fileditem.f_column + "\" type=\"text\" class=\"form-control\" isvalid=\"yes\" checkexpession=\"NotNull\" />\r\n");
                    sb.Append("    </div>\r\n");
                }
                sb.Append("</div>\r\n");
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseConfigModel.area + "/Views/" + baseConfigModel.name + "/Form.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 表单类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string FormCreate(BaseModel baseInfo, List<CodeFormTabModel> formData, Dictionary<string, CodeFormCompontModel> compontMap)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("@{\r\n");
                sb.Append("    ViewBag.Title = \"" + baseInfo.describe + "\";\r\n");
                sb.Append("    Layout = \"~/Views/Shared/_Form.cshtml\";\r\n");
                sb.Append("}\r\n");

                if (formData.Count == 1)// 一个选项卡的时候
                {
                    sb.Append("<div class=\"lr-form-wrap\">\r\n");
                    CreateFormCompont(sb, formData[0].componts);
                    sb.Append("</div>\r\n");
                }
                else// 多个选项卡的时候
                {
                    sb.Append("<div class=\"lr-form-tabs\" id=\"lr_form_tabs\">\r\n");
                    sb.Append("    <ul class=\"nav nav-tabs\">\r\n");
                    int num = 1;
                    foreach (var tab in formData)
                    {
                        sb.Append("        <li><a data-value=\"tab" + num + "\">" + tab.text + "</a></li>\r\n");
                        num++;
                    }
                    sb.Append("    </ul>\r\n</div>\r\n");
                    num = 1;
                    sb.Append("<div class=\"tab-content lr-tab-content\" id=\"lr_tab_content\">\r\n");
                    foreach (var tab in formData)
                    {
                        sb.Append("<div class=\"lr-form-wrap tab-pane\" id=\"tab" + num + "\">\r\n");
                        CreateFormCompont(sb, tab.componts);
                        sb.Append("</div>\r\n");

                        num++;
                    }
                    sb.Append("</div>\r\n");
                }
                sb.Append("@Html.AppendJsFile(\"/Areas/" + baseInfo.outputArea + "/Views/" + baseInfo.name + "/Form.js\")\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region 表单JS类
        /// <summary>
        /// 构建编辑表头
        /// </summary>
        /// <param name="sb">构建容器</param>
        /// <param name="list">表头列表信息</param>
        private void BulidGridHead(StringBuilder sb, List<TreeModelEx<CodeGridFieldModel>> list,string table)
        {
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.data.field))
                {
                    sb.Append("                    {\r\n");
                    sb.Append("                        label: '" + item.data.name + "', name: 'l" + Guid.NewGuid().ToString().Replace("-", "") + "', width:100, align: '" + item.data.align + "'");
                    if (item.ChildNodes != null && item.ChildNodes.Count > 0) {
                        sb.Append("                        \r\n,children[");
                        BulidGridHead(sb, item.ChildNodes, table);
                        sb.Append("                        ]\r\n");
                    }
                    sb.Append("                    },\r\n");
                }
                else
                {
                    sb.Append("                    {\r\n");
                    sb.Append("                        label: '" + item.data.name + "', name: '" + item.data.field + "', width:" + item.data.width + ", align: '" + item.data.align + "'");
                    switch (item.data.type) {
                        case "input":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'input'\r\n");
                            sb.Append("                        }\r\n");
                            break;
                        case "select":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'select',\r\n");

                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'"+ item.data.itemCode + "'\r\n");
                            }
                            else
                            {
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + item.data.dataSourceId + "',\r\n");
                                sb.Append("                            op:{\r\n");
                                sb.Append("                                value: '" + item.data.saveField + "',\r\n");
                                sb.Append("                                text:'" + item.data.showField + "',\r\n");
                                sb.Append("                                title:'" + item.data.showField + "'\r\n");
                                sb.Append("                            }\r\n");
                            }
                            sb.Append("                             }\r\n");
                            sb.Append("                        }\r\n");
                            break;
                        case "radio":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'radio',\r\n");
                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + item.data.itemCode + "',\r\n");
                            }
                            else
                            {
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + item.data.dataSourceId + "',\r\n");
                                sb.Append("                            text:'" + item.data.showField + "',\r\n");
                                sb.Append("                            value:'" + item.data.saveField + "',\r\n");
                            }
                            if (!string.IsNullOrEmpty(item.data.dfvalue)) {
                                sb.Append("                            dfvalue:'" + item.data.dfvalue + "'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "checkbox":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'checkbox',\r\n");
                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                            datatype: 'dataItem',\r\n");
                                sb.Append("                            code:'" + item.data.itemCode + "',\r\n");
                            }
                            else
                            {
                                sb.Append("                            datatype: 'dataSource',\r\n");
                                sb.Append("                            code:'" + item.data.dataSourceId + "',\r\n");
                                sb.Append("                            text:'" + item.data.showField + "',\r\n");
                                sb.Append("                            value:'" + item.data.saveField + "',\r\n");
                            }
                            if (!string.IsNullOrEmpty(item.data.dfvalue))
                            {
                                sb.Append("                            dfvalue:'" + item.data.dfvalue + "'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "datetime":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'datatime',\r\n");
                            if (item.data.datetime == "date")
                            {
                                sb.Append("                            dateformat: '0'\r\n");
                            }
                            else
                            {
                                sb.Append("                            dateformat: '1'\r\n");
                            }
                            sb.Append("                        }\r\n");
                            break;
                        case "layer":
                            sb.Append("\r\n                        ,edit:{\r\n");
                            sb.Append("                            type:'layer',\r\n");
                            sb.Append("                            change: function (data, rownum, selectData) {\r\n");

                            foreach (var item2 in item.data.layerData) {
                                sb.Append("                                data." + item2.value + " = selectData." + item2.name + ";\r\n");
                            }
                            sb.Append("                                $('#" + table + "').jfGridSet('updateRow', rownum);\r\n");
                            sb.Append("                            },\r\n");

                            sb.Append("                            op: {\r\n");
                            if (string.IsNullOrEmpty(item.data.layerW))
                            {
                                item.data.layerW = "600";
                            }
                            if (string.IsNullOrEmpty(item.data.layerH))
                            {
                                item.data.layerH = "400";
                            }
                            sb.Append("                                width: " + item.data.layerW + ",\r\n");
                            sb.Append("                                height: " + item.data.layerH + ",\r\n");
                            sb.Append("                                colData: [\r\n");

                            foreach (var item2 in item.data.layerData)
                            {
                                sb.Append("                                    { label: \"" + item2.label + "\", name: \"" + item2.name + "\", width: " + item2.width + ", align: \"" + item2.align + "\" },\r\n");
                            }
                            sb.Append("                                ],\r\n");
                            if (item.data.dataSource == "0")
                            {
                                sb.Append("                                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',\r\n");
                                sb.Append("                                param: { itemCode: '" + item.data.itemCode + "' }\r\n");
                            }
                            else {
                                sb.Append("                                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataTable',\r\n");
                                sb.Append("                                param: { code: '" + item.data.dataSourceId + "'\r\n} ");
                            }

                            sb.Append("                            }\r\n");
                            sb.Append("                        }\r\n");
                            break;
                    }
                    sb.Append("                    },\r\n");
                }
            }
        }

        /// <summary>
        /// 表单JS类创建
        /// </summary>
        /// <param name="baseConfigModel">基础配置信息</param>
        /// <returns></returns>
        public string FormJsCreate(CodeBaseConfigModel baseConfigModel)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseConfigModel.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var acceptClick;\r\n");
                sb.Append("var keyValue = request('keyValue');\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var selectedRow = learun.frameTab.currentIframe().selectedRow;\r\n");
                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            page.initData();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");
                sb.Append("        },\r\n");
                sb.Append("        initData: function () {\r\n");
                sb.Append("            if (!!selectedRow) {\r\n");
                sb.Append("                $('#form').lrSetFormData(selectedRow);\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    // 保存数据\r\n");
                sb.Append("    acceptClick = function (callBack) {\r\n");
                sb.Append("        if (!$('#form').lrValidform()) {\r\n");
                sb.Append("            return false;\r\n");
                sb.Append("        }\r\n");
                sb.Append("        var postData = $('#form').lrGetFormData();\r\n");
                sb.Append("        $.lrSaveForm(top.$.rootUrl + '/" + baseConfigModel.area + "/" + baseConfigModel.name + "/SaveForm?keyValue=' + keyValue, postData, function (res) {\r\n");
                sb.Append("            // 保存成功后才回调\r\n");
                sb.Append("            if (!!callBack) {\r\n");
                sb.Append("                callBack();\r\n");
                sb.Append("            }\r\n");
                sb.Append("        });\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 表单JS类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础配置信息</param>
        /// <param name="dbTableList">数据表信息</param>
        /// <param name="formData">表单信息</param>
        /// <param name="compontMap">表单组件信息映射表</param>
        /// <returns></returns>
        public string FormJsCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, List<CodeFormTabModel> formData,Dictionary<string, CodeFormCompontModel> compontMap)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var acceptClick;\r\n");
                sb.Append("var keyValue = request('keyValue');\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            $('.lr-form-wrap').lrscroll();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("            page.initData();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (formData.Count > 1)
                {
                    sb.Append("            $('#lr_form_tabs').lrFormTab();\r\n");
                    sb.Append("            $('#lr_form_tabs ul li').eq(0).trigger('click');\r\n");
                }

                // 编辑表格组件
                List<CodeFormCompontModel> girdcomponts = new List<CodeFormCompontModel>();


                foreach (var tab in formData)
                {
                    foreach (var compont in tab.componts)
                    {
                        switch (compont.type)
                        {
                            case "girdtable":
                                sb.Append("            $('#" + compont.table + "').jfGrid({\r\n");
                                sb.Append("                headData: [\r\n");

                                List<TreeModelEx<CodeGridFieldModel>> headTree = new List<TreeModelEx<CodeGridFieldModel>>();
                                foreach (var item in compont.fieldsData)
                                {
                                    TreeModelEx<CodeGridFieldModel> treeItem = new TreeModelEx<CodeGridFieldModel>();
                                    treeItem.id = item.id;
                                    treeItem.parentId = item.parentId;
                                    treeItem.data = item;

                                    headTree.Add(treeItem);
                                }
                                headTree.ToTree();

                                BulidGridHead(sb, headTree, compont.table);

                                sb.Append("                ],\r\n");
                                sb.Append("                isEdit: true,\r\n");
                                sb.Append("                height: 400\r\n");
                                sb.Append("            });\r\n");
                                girdcomponts.Add(compont);
                                break;
                            case "texteditor":
                                sb.Append("            var " + compont.field + "UE =  UE.getEditor('" + compont.field + "');\r\n");
                                sb.Append("            $('#" + compont.field + "')[0].ue =  " + compont.field + "UE;");
                                if (!string.IsNullOrEmpty(compont.dfvalue)) {
                                    sb.Append("            " + compont.field + "UE.ready(function () { \r\n");
                                    sb.Append("            " + compont.field + "UE.setContent(" + compont.dfvalue + ");\r\n");
                                    sb.Append("            });\r\n");
                                }
                                break;
                            case "radio":
                            case "checkbox":
                                sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                                sb.Append("                type: '" + compont.type + "',\r\n");
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("                code: '" + compont.itemCode + "',\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("                dataType: 'dataSource',\r\n");
                                    sb.Append("                code: '" + vlist[0] + "',\r\n");
                                    sb.Append("                value: '" + vlist[2] + "',\r\n");
                                    sb.Append("                text: '" + vlist[1] + "',\r\n");
                                }
                                sb.Append("            });\r\n");
                                break;
                            case "select":
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                                }
                                break;
                            case "datetimerange":
                                sb.Append("            $('#" + compontMap[compont.startTime].field + "').on('change', function () {\r\n");
                                sb.Append("                var st = $(this).val();\r\n");
                                sb.Append("                var et = $('#" + compontMap[compont.endTime].field + "').val();\r\n");
                                sb.Append("                if (!!st && !!et) {\r\n");
                                sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");

                                sb.Append("            $('#" + compontMap[compont.endTime].field + "').on('change', function () {\r\n");
                                sb.Append("                var et = $('#" + compontMap[compont.startTime].field + "').val();\r\n");
                                sb.Append("                var et = $(this).val();\r\n");
                                sb.Append("                if (!!st && !!et) {\r\n");
                                sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");

                                break;
                            case "encode":
                                sb.Append("            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/CodeRule/GetEnCode', { code: '" + compont.rulecode + "' }, function (data) {\r\n");
                                sb.Append("                if (!$('#" + compont.field + "').val()) {\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(data);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");
                                break;
                            case "organize":
                                switch (compont.dataType)
                                {
                                    case "user"://用户
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrformselect({\r\n");
                                            sb.Append("                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectOnlyForm',\r\n");
                                            sb.Append("                layerUrlW: 400,\r\n");
                                            sb.Append("                layerUrlH: 300,\r\n");
                                            sb.Append("                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                value: 'F_UserId',\r\n");
                                            sb.Append("                text: 'F_RealName',\r\n");
                                            sb.Append("                title: 'F_RealName',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");
                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                if (value == '')\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: '',\r\n");
                                            sb.Append("                        data: []\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("                else\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: top.$.rootUrl + '/LR_OrganizationModule/User/GetList',\r\n");
                                            sb.Append("                        param: { departmentId: value }\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("            })\r\n");
                                        }
                                        break;
                                    case "department"://部门
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true,\r\n");
                                            sb.Append("                url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                param: {} \r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");

                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                    url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                    param: { companyId: value }\r\n");
                                            sb.Append("                });\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        break;
                                    case "company"://公司
                                        sb.Append("            $('#" + compont.field+ "').lrCompanySelect({});\r\n");
                                        break;
                                }
                                break;
                            case "currentInfo":
                                switch (compont.dataType) {
                                    case "company":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).companyId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('company', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).companyId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "department":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).departmentId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('department', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).departmentId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "user":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).userId;\r\n");
                                        sb.Append("            $('#" + compont.field + "').val(learun.clientdata.get(['userinfo']).realName);\r\n");
                                        break;
                                    case "time":
                                        sb.Append("            $('#" + compont.field + "').val(learun.formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss'));\r\n");
                                        break;
                                }
                                break;
                            case "upload":
                                sb.Append("            $('#" + compont.field + "').lrUploader();\r\n");
                                break;
                        }
                    }
                }
                sb.Append("        },\r\n");
                sb.Append("        initData: function () {\r\n");
                sb.Append("            if (!!keyValue) {\r\n");
                sb.Append("                $.lrSetForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetFormData?keyValue=' + keyValue, function (data) {\r\n");
                sb.Append("                    for (var id in data) {\r\n");
                sb.Append("                        if (!!data[id].length && data[id].length > 0) {\r\n");

                sb.Append("                            $('#' + id ).jfGridSet('refreshdata', data[id]);\r\n");

                sb.Append("                        }\r\n");
                sb.Append("                        else {\r\n");
                sb.Append("                            $('[data-table=\"' + id + '\"]').lrSetFormData(data[id]);\r\n");
                sb.Append("                        }\r\n");
                sb.Append("                    }\r\n");
                sb.Append("                });\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    // 保存数据\r\n");
                sb.Append("    acceptClick = function (callBack) {\r\n");
                sb.Append("        if (!$('body').lrValidform()) {\r\n");
                sb.Append("            return false;\r\n");
                sb.Append("        }\r\n");

                if (dbTableList.Count == 1)
                {
                    sb.Append("        var postData = {\r\n");
                    sb.Append("            strEntity: JSON.stringify($('body').lrGetFormData())\r\n");
                    sb.Append("        };\r\n");
                }
                else
                {
                    sb.Append("        var postData = {};\r\n");
                    foreach (var table in dbTableList)
                    {
                        if (girdcomponts.FindAll(t => t.table == table.name).Count >= 1)
                        {
                            sb.Append("        postData.str" + Str.FirstLower(table.name) + "List = JSON.stringify($('#" + table.name + "').jfGridGet('rowdatas'));\r\n");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(table.relationName))
                            {
                                sb.Append("        postData.strEntity = JSON.stringify($('[data-table=\"" + table.name + "\"]').lrGetFormData());\r\n");
                            }
                            else
                            {
                                sb.Append("        postData.str" + Str.FirstLower(table.name) + "Entity = JSON.stringify($('[data-table=\"" + table.name + "\"]').lrGetFormData());\r\n");
                            }
                        }
                    }
                }
                sb.Append("        $.lrSaveForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/SaveForm?keyValue=' + keyValue, postData, function (res) {\r\n");
                sb.Append("            // 保存成功后才回调\r\n");
                sb.Append("            if (!!callBack) {\r\n");
                sb.Append("                callBack();\r\n");
                sb.Append("            }\r\n");
                sb.Append("        });\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 表单JS类创建(流程系统表单开发模板)
        /// </summary>
        /// <param name="baseInfo">基础配置信息</param>
        /// <param name="dbTableList">数据表信息</param>
        /// <param name="formData">表单信息</param>
        /// <param name="compontMap">表单组件信息映射表</param>
        /// <param name="workField">流程关联字段</param> 
        /// <returns></returns>
        public string WfFormJsCreate(BaseModel baseInfo, List<DbTableModel> dbTableList, List<CodeFormTabModel> formData, Dictionary<string, CodeFormCompontModel> compontMap,string workField)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                string mainTable = "";
                string mainPkey = "";
                foreach (var tableOne in dbTableList)
                {
                    if (string.IsNullOrEmpty(tableOne.relationName))
                    {
                        mainTable = tableOne.name;
                        mainPkey = tableOne.pk;
                    }
                }

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var acceptClick;\r\n");
                sb.Append("var keyValue = request('keyValue');\r\n");

                sb.Append("// 设置权限\r\n");
                sb.Append("var setAuthorize;\r\n");
                sb.Append("// 设置表单数据\r\n");
                sb.Append("var setFormData;\r\n");
                sb.Append("// 验证数据是否填写完整\r\n");
                sb.Append("var validForm;\r\n");
                sb.Append("// 保存数据\r\n");
                sb.Append("var save;\r\n");

                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");

                sb.Append("    // 设置权限\r\n");
                sb.Append("    setAuthorize = function (data) {\r\n");
                sb.Append("    };\r\n");

                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            $('.lr-form-wrap').lrscroll();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("            page.initData();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (formData.Count > 1)
                {
                    sb.Append("            $('#lr_form_tabs').lrFormTab();\r\n");
                    sb.Append("            $('#lr_form_tabs ul li').eq(0).trigger('click');\r\n");
                }

                // 编辑表格组件
                List<CodeFormCompontModel> girdcomponts = new List<CodeFormCompontModel>();


                foreach (var tab in formData)
                {
                    foreach (var compont in tab.componts)
                    {
                        switch (compont.type)
                        {
                            case "girdtable":
                                sb.Append("            $('#" + compont.table + "').jfGrid({\r\n");
                                sb.Append("                headData: [\r\n");

                                List<TreeModelEx<CodeGridFieldModel>> headTree = new List<TreeModelEx<CodeGridFieldModel>>();
                                foreach (var item in compont.fieldsData)
                                {
                                    TreeModelEx<CodeGridFieldModel> treeItem = new TreeModelEx<CodeGridFieldModel>();
                                    treeItem.id = item.id;
                                    treeItem.parentId = item.parentId;
                                    treeItem.data = item;

                                    headTree.Add(treeItem);
                                }
                                headTree.ToTree();

                                BulidGridHead(sb, headTree, compont.table);

                                sb.Append("                ],\r\n");
                                sb.Append("                isEdit: true,\r\n");
                                sb.Append("                height: 400\r\n");
                                sb.Append("            });\r\n");
                                girdcomponts.Add(compont);
                                break;
                            case "texteditor":
                                sb.Append("            var " + compont.field + "UE =  UE.getEditor('" + compont.field + "');\r\n");
                                sb.Append("            $('#" + compont.field + "')[0].ue =  " + compont.field + "UE;");
                                if (!string.IsNullOrEmpty(compont.dfvalue))
                                {
                                    sb.Append("            " + compont.field + "UE.ready(function () { \r\n");
                                    sb.Append("            " + compont.field + "UE.setContent(" + compont.dfvalue + ");\r\n");
                                    sb.Append("            });\r\n");
                                }
                                break;
                            case "radio":
                            case "checkbox":
                                sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                                sb.Append("                type: '" + compont.type + "',\r\n");
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("                code: '" + compont.itemCode + "',\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("                dataType: 'dataSource',\r\n");
                                    sb.Append("                code: '" + vlist[0] + "',\r\n");
                                    sb.Append("                value: '" + vlist[2] + "',\r\n");
                                    sb.Append("                text: '" + vlist[1] + "',\r\n");
                                }
                                sb.Append("            });\r\n");
                                break;
                            case "select":
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                                }
                                break;
                            case "datetimerange":
                                sb.Append("            $('#" + compontMap[compont.startTime].field + "').on('change', function () {\r\n");
                                sb.Append("                var st = $(this).val();\r\n");
                                sb.Append("                var et = $('#" + compontMap[compont.endTime].field + "').val();\r\n");
                                sb.Append("                if (!!st && !!et) {\r\n");
                                sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");

                                sb.Append("            $('#" + compontMap[compont.endTime].field + "').on('change', function () {\r\n");
                                sb.Append("                var et = $('#" + compontMap[compont.startTime].field + "').val();\r\n");
                                sb.Append("                var et = $(this).val();\r\n");
                                sb.Append("                if (!!st && !!et) {\r\n");
                                sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");

                                break;
                            case "encode":
                                sb.Append("            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/CodeRule/GetEnCode', { code: '" + compont.rulecode + "' }, function (data) {\r\n");
                                sb.Append("                if (!$('#" + compont.field + "').val()) {\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(data);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");
                                break;
                            case "organize":
                                switch (compont.dataType)
                                {
                                    case "user"://用户
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrformselect({\r\n");
                                            sb.Append("                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectOnlyForm',\r\n");
                                            sb.Append("                layerUrlW: 400,\r\n");
                                            sb.Append("                layerUrlH: 300,\r\n");
                                            sb.Append("                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                value: 'F_UserId',\r\n");
                                            sb.Append("                text: 'F_RealName',\r\n");
                                            sb.Append("                title: 'F_RealName',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");
                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                if (value == '')\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: '',\r\n");
                                            sb.Append("                        data: []\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("                else\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: top.$.rootUrl + '/LR_OrganizationModule/User/GetList',\r\n");
                                            sb.Append("                        param: { departmentId: value }\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("            })\r\n");
                                        }
                                        break;
                                    case "department"://部门
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true,\r\n");
                                            sb.Append("                url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                param: {} \r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");

                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                    url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                    param: { companyId: value }\r\n");
                                            sb.Append("                });\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        break;
                                    case "company"://公司
                                        sb.Append("            $('#" + compont.field + "').lrCompanySelect({});\r\n");
                                        break;
                                }
                                break;
                            case "currentInfo":
                                switch (compont.dataType)
                                {
                                    case "company":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).companyId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('company', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).companyId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "department":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).departmentId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('department', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).departmentId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "user":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).userId;\r\n");
                                        sb.Append("            $('#" + compont.field + "').val(learun.clientdata.get(['userinfo']).realName);\r\n");
                                        break;
                                    case "time":
                                        sb.Append("            $('#" + compont.field + "').val(learun.formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss'));\r\n");
                                        break;
                                }
                                break;
                            case "upload":
                                sb.Append("            $('#" + compont.field + "').lrUploader();\r\n");
                                break;
                        }
                    }
                }
                sb.Append("        },\r\n");
                sb.Append("        initData: function () {\r\n");
                sb.Append("            if (!!keyValue) {\r\n");
                sb.Append("                $.lrSetForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetFormData?keyValue=' + keyValue, function (data) {\r\n");
                sb.Append("                    for (var id in data) {\r\n");
                sb.Append("                        if (!!data[id].length && data[id].length > 0) {\r\n");

                sb.Append("                            $('#' + id ).jfGridSet('refreshdata', data[id]);\r\n");

                sb.Append("                        }\r\n");
                sb.Append("                        else {\r\n");
                sb.Append("                            $('[data-table=\"' + id + '\"]').lrSetFormData(data[id]);\r\n");
                sb.Append("                        }\r\n");
                sb.Append("                    }\r\n");
                sb.Append("                });\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");


                sb.Append("    // 设置表单数据\r\n");
                sb.Append("    setFormData = function (processId) {\r\n");
                sb.Append("        if (!!processId) {\r\n");
                sb.Append("            $.lrSetForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/GetFormDataByProcessId?processId=' + processId, function (data) {\r\n");
                sb.Append("                for (var id in data) {\r\n");
                sb.Append("                    if (!!data[id] && data[id].length > 0) {\r\n");


                sb.Append("                            $('#' + id ).jfGridSet('refreshdata', data[id]);\r\n");

                sb.Append("                        }\r\n");
                sb.Append("                        else {\r\n");

                sb.Append("                        if(id == '" + mainTable + "'){\r\n");
                sb.Append("                            keyValue = data[id]." + mainPkey + ";\r\n");
                sb.Append("                        }\r\n");

                sb.Append("                            $('[data-table=\"' + id + '\"]').lrSetFormData(data[id]);\r\n");
                sb.Append("                        }\r\n");
                sb.Append("                    }\r\n");
                sb.Append("                });\r\n");
                sb.Append("            }\r\n");
                sb.Append("        }\r\n");

                sb.Append("    // 验证数据是否填写完整\r\n");
                sb.Append("    validForm = function () {\r\n");
                sb.Append("        if (!$('#form').lrValidform()) {\r\n");
                sb.Append("            return false;\r\n");
                sb.Append("        }\r\n");
                sb.Append("        return true;\r\n");
                sb.Append("    };\r\n");

                sb.Append("    // 保存数据\r\n");
                sb.Append("    save = function (processId, callBack, i) {\r\n");

                if (dbTableList.Count == 1)
                {

                    sb.Append("        var formData = $('body').lrGetFormData();\r\n");

                    sb.Append("        if(!!processId){\r\n");
                    sb.Append("            formData." + workField + " =processId;\r\n");
                    sb.Append("        }\r\n");

                    sb.Append("        var postData = {\r\n");
                    sb.Append("            strEntity: JSON.stringify(formData)\r\n");
                    sb.Append("        };\r\n");



                }
                else
                {
                    sb.Append("        var postData = {};\r\n");
                    foreach (var table in dbTableList)
                    {
                        if (girdcomponts.FindAll(t => t.table == table.name).Count >= 1)
                        {
                            sb.Append("        postData.str" + Str.FirstLower(table.name) + "List = JSON.stringify($('#" + table.name + "').jfGridGet('rowdatas'));\r\n");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(table.relationName))
                            {
                                sb.Append("        var formData = $('[data-table=\"" + table.name + "\"]').lrGetFormData();\r\n");
                                sb.Append("        if(!!processId){\r\n");
                                sb.Append("            formData." + workField + " =processId;\r\n");
                                sb.Append("        }\r\n");
                                sb.Append("        postData.strEntity = JSON.stringify(formData);\r\n");
                            }
                            else
                            {
                                sb.Append("        postData.str" + Str.FirstLower(table.name) + "Entity = JSON.stringify($('[data-table=\"" + table.name + "\"]').lrGetFormData());\r\n");
                            }
                        }
                    }
                }


                sb.Append("        $.lrSaveForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/SaveForm?keyValue=' + keyValue, postData, function (res) {\r\n");
                sb.Append("            // 保存成功后才回调\r\n");
                sb.Append("            if (!!callBack) {\r\n");
                sb.Append("                callBack(res, formData, i);\r\n");
                sb.Append("            }\r\n");
                sb.Append("        });\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 表单JS类创建(自定义开发模板)
        /// </summary>
        /// <param name="baseInfo">基础配置信息</param>
        /// <param name="formData">表单信息</param>
        /// <param name="compontMap">表单组件信息映射表</param>
        /// <returns></returns>
        public string GridEditFormJsCreate(BaseModel baseInfo,List<CodeFormTabModel> formData, Dictionary<string, CodeFormCompontModel> compontMap)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                UserInfo userInfo = LoginUserInfo.Get();

                sb.Append("/*");
                sb.Append(" * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)\r\n");
                sb.Append(" * Copyright (c) 2013-2018 上海力软信息技术有限公司\r\n");
                sb.Append(" * 创建人：" + userInfo.realName + "\r\n");
                sb.Append(" * 日  期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n");
                sb.Append(" * 描  述：" + baseInfo.describe + "\r\n");
                sb.Append(" */\r\n");
                sb.Append("var acceptClick;\r\n");
                sb.Append("var bootstrap = function ($, learun) {\r\n");
                sb.Append("    \"use strict\";\r\n");
                sb.Append("    var page = {\r\n");
                sb.Append("        init: function () {\r\n");
                sb.Append("            $('.lr-form-wrap').lrscroll();\r\n");
                sb.Append("            page.bind();\r\n");
                sb.Append("        },\r\n");
                sb.Append("        bind: function () {\r\n");

                if (formData.Count > 1)
                {
                    sb.Append("            $('#lr_form_tabs').lrFormTab();\r\n");
                    sb.Append("            $('#lr_form_tabs ul li').eq(0).trigger('click');\r\n");
                }

                // 编辑表格组件
                List<CodeFormCompontModel> girdcomponts = new List<CodeFormCompontModel>();


                foreach (var tab in formData)
                {
                    foreach (var compont in tab.componts)
                    {
                        switch (compont.type)
                        {
                            case "radio":
                            case "checkbox":
                                sb.Append("            $('#" + compont.field + "').lrRadioCheckbox({\r\n");
                                sb.Append("                type: '" + compont.type + "',\r\n");
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("                code: '" + compont.itemCode + "',\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("                dataType: 'dataSource',\r\n");
                                    sb.Append("                code: '" + vlist[0] + "',\r\n");
                                    sb.Append("                value: '" + vlist[2] + "',\r\n");
                                    sb.Append("                text: '" + vlist[1] + "',\r\n");
                                }
                                sb.Append("            });\r\n");
                                break;
                            case "select":
                                if (compont.dataSource == "0")
                                {
                                    sb.Append("            $('#" + compont.field + "').lrDataItemSelect({ code: '" + compont.itemCode + "' });\r\n");
                                }
                                else
                                {
                                    string[] vlist = compont.dataSourceId.Split(',');
                                    sb.Append("            $('#" + compont.field + "').lrDataSourceSelect({ code: '" + vlist[0] + "',value: '" + vlist[2] + "',text: '" + vlist[1] + "' });\r\n");
                                }
                                break;
                            case "datetimerange":
                                sb.Append("            $('#" + compontMap[compont.startTime].field + "').on('change', function () {\r\n");
                                sb.Append("                var st = $(this).val();\r\n");
                                sb.Append("                var et = $('#" + compontMap[compont.endTime].field + "').val();\r\n");
                                sb.Append("                if (!!st && !!et) {\r\n");
                                sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");

                                sb.Append("            $('#" + compontMap[compont.endTime].field + "').on('change', function () {\r\n");
                                sb.Append("                var et = $('#" + compontMap[compont.startTime].field + "').val();\r\n");
                                sb.Append("                var et = $(this).val();\r\n");
                                sb.Append("                if (!!st && !!et) {\r\n");
                                sb.Append("                    var diff = learun.parseDate(st).DateDiff('d', et) + 1;\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(diff);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");

                                break;
                            case "encode":
                                sb.Append("            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/CodeRule/GetEnCode', { code: '" + compont.rulecode + "' }, function (data) {\r\n");
                                sb.Append("                if (!$('#" + compont.field + "').val()) {\r\n");
                                sb.Append("                    $('#" + compont.field + "').val(data);\r\n");
                                sb.Append("                }\r\n");
                                sb.Append("            });\r\n");
                                break;
                            case "organize":
                                switch (compont.dataType)
                                {
                                    case "user"://用户
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrformselect({\r\n");
                                            sb.Append("                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectOnlyForm',\r\n");
                                            sb.Append("                layerUrlW: 400,\r\n");
                                            sb.Append("                layerUrlH: 300,\r\n");
                                            sb.Append("                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                value: 'F_UserId',\r\n");
                                            sb.Append("                text: 'F_RealName',\r\n");
                                            sb.Append("                title: 'F_RealName',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");
                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                if (value == '')\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: '',\r\n");
                                            sb.Append("                        data: []\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("                else\r\n");
                                            sb.Append("                {\r\n");
                                            sb.Append("                    $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                        url: top.$.rootUrl + '/LR_OrganizationModule/User/GetList',\r\n");
                                            sb.Append("                        param: { departmentId: value }\r\n");
                                            sb.Append("                    });\r\n");
                                            sb.Append("                }\r\n");
                                            sb.Append("            })\r\n");
                                        }
                                        break;
                                    case "department"://部门
                                        if (string.IsNullOrEmpty(compont.relation))
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true,\r\n");
                                            sb.Append("                url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                param: {} \r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        else
                                        {
                                            sb.Append("            $('#" + compont.field + "').lrselect({\r\n");
                                            sb.Append("                type: 'tree',\r\n");
                                            sb.Append("                allowSearch: true\r\n");
                                            sb.Append("            });\r\n");

                                            sb.Append("            $('#" + compontMap[compont.relation].field + "').on('change', function () {\r\n");
                                            sb.Append("                var value = $(this).lrselectGet();\r\n");
                                            sb.Append("                $('#" + compont.field + "').lrselectRefresh({\r\n");
                                            sb.Append("                    url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',\r\n");
                                            sb.Append("                    param: { companyId: value }\r\n");
                                            sb.Append("                });\r\n");
                                            sb.Append("            });\r\n");
                                        }
                                        break;
                                    case "company"://公司
                                        sb.Append("            $('#" + compont.field + "').lrCompanySelect({});\r\n");
                                        break;
                                }
                                break;
                            case "currentInfo":
                                switch (compont.dataType)
                                {
                                    case "company":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).companyId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('company', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).companyId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "department":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).departmentId;\r\n");
                                        sb.Append("            learun.clientdata.getAsync('department', {\r\n");
                                        sb.Append("                key: learun.clientdata.get(['userinfo']).departmentId,\r\n");
                                        sb.Append("                callback: function (_data) {\r\n");
                                        sb.Append("                    $('#" + compont.field + "').val(_data.name);\r\n");
                                        sb.Append("                }\r\n");
                                        sb.Append("            });\r\n");
                                        break;
                                    case "user":
                                        sb.Append("            $('#" + compont.field + "')[0].lrvalue = learun.clientdata.get(['userinfo']).userId;\r\n");
                                        sb.Append("            $('#" + compont.field + "').val(learun.clientdata.get(['userinfo']).realName);\r\n");
                                        break;
                                    case "time":
                                        sb.Append("            $('#" + compont.field + "').val(learun.formatDate(new Date(), 'yyyy-MM-dd hh:mm:ss'));\r\n");
                                        break;
                                }
                                break;
                            case "upload":
                                sb.Append("            $('#" + compont.field + "').lrUploader();\r\n");
                                break;
                        }
                    }
                }
                sb.Append("        }\r\n");
                sb.Append("    };\r\n");
                sb.Append("    // 保存数据\r\n");
                sb.Append("    acceptClick = function (callBack) {\r\n");
                sb.Append("        if (!$('body').lrValidform()) {\r\n");
                sb.Append("            return false;\r\n");
                sb.Append("        }\r\n");
                sb.Append("        var postData = $('body').lrGetFormData();\r\n");
                sb.Append("        $.lrSaveForm(top.$.rootUrl + '/" + baseInfo.outputArea + "/" + baseInfo.name + "/SaveForm', postData, function (res) {\r\n");
                sb.Append("            // 保存成功后才回调\r\n");
                sb.Append("            if (!!callBack) {\r\n");
                sb.Append("                callBack();\r\n");
                sb.Append("            }\r\n");
                sb.Append("        });\r\n");
                sb.Append("    };\r\n");
                sb.Append("    page.init();\r\n");
                sb.Append("}\r\n");

                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 创建文件
        /// <summary>
        /// 创建代码文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <param name="projectPath">项目路径</param>
        /// <param name="filePath">文件相对路径</param>
        /// <param name="isContent">是否是内容还是编译文件</param>
        public void CreateCodeFile(string path, string content,string projectPath,string filePath,bool isContent)
        {
            try
            {
                if (!System.IO.File.Exists(path))
                {
                    DirFileHelper.CreateFileContent(path, content);
                    Xml.WriteCsproj(projectPath, filePath, isContent);
                }
                else // 测试用
                {
                    DirFileHelper.CreateFileContent(path, content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 创建实体映射类
        /// <summary>
        /// 创建实体映射代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeBaseConfigModel">表名</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateMapCodeFile(CodeBaseConfigModel codeBaseConfigModel, string tableName, string codeContent)
        {
            try
            {
                string backArea = "";
                if (!string.IsNullOrEmpty(codeBaseConfigModel.backArea))
                {
                    backArea = codeBaseConfigModel.backArea + "\\";
                }
                if (!string.IsNullOrEmpty(codeBaseConfigModel.mapping))
                {
                    backArea = codeBaseConfigModel.mapping + "\\";
                }

                CreateCodeFile(codeBaseConfigModel.mappingDirectory + "\\" + backArea + tableName + "Map.cs", codeContent, codeBaseConfigModel.mappingDirectory + "\\Learun.Application.Mapping.csproj", backArea + tableName + "Map.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建实体映射代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateMapCodeFile(BaseModel baseInfo, string tableName, string codeContent)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                CreateCodeFile(baseInfo.mappingDirectory + "\\" + baseInfo.outputArea + "\\" + tableName + "Map.cs", codeContent, baseInfo.mappingDirectory + "\\Learun.Application.Mapping.csproj", baseInfo.outputArea + "\\" + tableName + "Map.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建实体类
        /// <summary>
        /// 创建实体代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeBaseConfigModel">表名</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateEntityCodeFile(CodeBaseConfigModel codeBaseConfigModel, string tableName, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + tableName + "Entity.cs", codeContent, codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backProject + ".csproj", codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + tableName + "Entity.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建实体代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateEntityCodeFile(BaseModel baseInfo, string tableName, string codeContent)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                CreateCodeFile(baseInfo.serviceDirectory + "\\" + baseInfo.outputArea + "\\" + baseInfo.name + "\\" + tableName + "Entity.cs", codeContent, baseInfo.serviceDirectory + "\\" + backProject + ".csproj", baseInfo.outputArea + "\\" + baseInfo.name + "\\" + tableName + "Entity.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建服务类
        /// <summary>
        /// 创建服务代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateSerivceCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {

                CreateCodeFile(codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + codeBaseConfigModel.name + "Service.cs", codeContent, codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backProject + ".csproj", codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + codeBaseConfigModel.name + "Service.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建服务代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateSerivceCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                CreateCodeFile(baseInfo.serviceDirectory + "\\" + baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "Service.cs", codeContent, baseInfo.serviceDirectory + "\\" + backProject + ".csproj", baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "Service.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建业务类
        /// <summary>
        /// 创建业务代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateBLLCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + codeBaseConfigModel.name + "BLL.cs", codeContent, codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backProject + ".csproj", codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + codeBaseConfigModel.name + "BLL.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建业务代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateBLLCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                CreateCodeFile(baseInfo.serviceDirectory + "\\" + baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "BLL.cs", codeContent, baseInfo.serviceDirectory + "\\" + backProject + ".csproj", baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "BLL.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建业务接口类
        /// <summary>
        /// 创建业务接口代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateIBLLCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + codeBaseConfigModel.name + "IBLL.cs", codeContent, codeBaseConfigModel.serviceDirectory + "\\" + codeBaseConfigModel.backProject + ".csproj", codeBaseConfigModel.backArea + "\\" + codeBaseConfigModel.name + "\\" + codeBaseConfigModel.name + "IBLL.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建业务代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateIBLLCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                string backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
                CreateCodeFile(baseInfo.serviceDirectory + "\\" + baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "IBLL.cs", codeContent, baseInfo.serviceDirectory + "\\" + backProject + ".csproj", baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "IBLL.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建控制器
        /// <summary>
        /// 创建控制器代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateControllerCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.webDirectory + "\\Areas\\" + codeBaseConfigModel.area + "\\Controllers\\" + codeBaseConfigModel.name + "Controller.cs", codeContent, codeBaseConfigModel.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + codeBaseConfigModel.area + "\\Controllers\\" + codeBaseConfigModel.name + "Controller.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 创建控制器代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateControllerCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                CreateCodeFile(baseInfo.webDirectory + "\\Areas\\" + baseInfo.outputArea + "\\Controllers\\" + baseInfo.name + "Controller.cs", codeContent, baseInfo.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + baseInfo.outputArea + "\\Controllers\\" + baseInfo.name + "Controller.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建页面
        /// <summary>
        /// 创建页面代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateIndexCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.webDirectory + "\\Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Index.cshtml", codeContent, codeBaseConfigModel.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Index.cshtml", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建页面代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateIndexCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                CreateCodeFile(baseInfo.webDirectory + "\\Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Index.cshtml", codeContent, baseInfo.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Index.cshtml", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建页面js
        /// <summary>
        /// 创建页面js代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateIndexJSCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.webDirectory + "\\Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Index.js", codeContent, codeBaseConfigModel.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Index.js", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建页面js代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateIndexJSCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                CreateCodeFile(baseInfo.webDirectory + "\\Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Index.js", codeContent, baseInfo.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Index.js", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建表单
        /// <summary>
        /// 创建表单代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateFormCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.webDirectory + "\\Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Form.cshtml", codeContent, codeBaseConfigModel.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Form.cshtml", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建表单代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateFormCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                CreateCodeFile(baseInfo.webDirectory + "\\Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Form.cshtml", codeContent, baseInfo.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Form.cshtml", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建表单js
        /// <summary>
        /// 创建表单js代码文件
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateFormJSCodeFile(CodeBaseConfigModel codeBaseConfigModel, string codeContent)
        {
            try
            {
                CreateCodeFile(codeBaseConfigModel.webDirectory + "\\Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Form.js", codeContent, codeBaseConfigModel.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + codeBaseConfigModel.area + "\\Views\\" + codeBaseConfigModel.name + "\\Form.js", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建表单js代码文件(自定义模板)
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateFormJSCodeFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                CreateCodeFile(baseInfo.webDirectory + "\\Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Form.js", codeContent, baseInfo.webDirectory + "\\Learun.Application.Web.csproj", "Areas\\" + baseInfo.outputArea + "\\Views\\" + baseInfo.name + "\\Form.js", true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region 移动api
        /// <summary>
        /// 创建webapi文件
        /// </summary>
        /// <param name="baseInfo">配置信息</param>
        /// <param name="codeContent">代码内容</param>
        public void CreateApiFile(BaseModel baseInfo, string codeContent)
        {
            try
            {
                CreateCodeFile(baseInfo.apiDirectory + "\\Modules\\" + baseInfo.outputArea + "\\" + baseInfo.name + "Api.cs", codeContent, baseInfo.apiDirectory + "\\Learun.Application.WebApi.csproj", "Modules\\" + baseInfo.outputArea + "\\" + baseInfo.name + "Api.cs", false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 移动功能页面
        /// <summary>
        /// 创建移动页面Index
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="codeContent"></param>
        /// <param name="type"></param>
        public void CreateAppIndexCodeFile(BaseModel baseInfo, string codeContent, string type)
        {
            try
            {
                DirFileHelper.CreateFileContent(baseInfo.appDirectory + "\\www\\pages\\" + baseInfo.outputArea + "\\" + baseInfo.name + "\\" + baseInfo.name + "." + type, codeContent);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建移动页面Form
        /// </summary>
        /// <param name="baseInfo"></param>
        /// <param name="codeContent"></param>
        /// <param name="type"></param>
        public void CreateAppFormCodeFile(BaseModel baseInfo, string codeContent, string type)
        {
            try
            {
                DirFileHelper.CreateFileContent(baseInfo.appDirectory + "\\www\\pages\\" + baseInfo.outputArea + "\\" + baseInfo.name + "\\form\\form." + type, codeContent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #endregion
    }
}
