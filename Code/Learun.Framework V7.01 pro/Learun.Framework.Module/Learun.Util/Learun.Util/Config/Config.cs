using System;
using System.Configuration;
using System.Web;
using System.Xml;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：Config文件操作
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 根据Key取Value值
        /// </summary>
        /// <param name="key"></param>
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }
        /// <summary>
        /// 根据Key修改Value
        /// </summary>
        /// <param name="key">要修改的Key</param>
        /// <param name="value">要修改为的值</param>
        public static void SetValue(string key, string value)
        {
            System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
            xDoc.Load(HttpContext.Current.Server.MapPath("~/XmlConfig/system.config"));
            System.Xml.XmlNode xNode;
            System.Xml.XmlElement xElem1;
            System.Xml.XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");

            xElem1 = (System.Xml.XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            if (xElem1 != null) xElem1.SetAttribute("value", value);
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", key);
                xElem2.SetAttribute("value", value);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(HttpContext.Current.Server.MapPath("~/XmlConfig/system.config"));
        }
        /// <summary>
        /// 更新或新增[connectionStrings]节点的子节点值，存在则更新子节点,不存在则新增子节点，返回成功与否布尔值
        /// </summary>
        /// <param name="configurationFile">要操作的配置文件名称,枚举常量</param>
        /// <param name="name">子节点name值</param>
        /// <param name="connectionString">子节点connectionString值</param>
        /// <param name="providerName">子节点providerName值</param>
        /// <returns>返回成功与否布尔值</returns>
        public static bool UpdateOrCreateConnectionString(string configurationFile, string name, string connectionString, string providerName)
        {
            bool isSuccess = false;
            string filename = string.Empty;

            filename = System.AppDomain.CurrentDomain.BaseDirectory + configurationFile;

            XmlDocument doc = new XmlDocument();
            doc.Load(filename); //加载配置文件

            XmlNode node = doc.SelectSingleNode("//connectionStrings");   //得到[connectionStrings]节点

            try
            {
                ////得到[connectionStrings]节点中关于Name的子节点
                XmlElement element = (XmlElement)node.SelectSingleNode("//add[@name='" + name + "']");

                if (element != null)
                {
                    //存在则更新子节点
                    element.SetAttribute("connectionString", connectionString);
                    element.SetAttribute("providerName", providerName);
                }
                else
                {
                    //不存在则新增子节点
                    XmlElement subElement = doc.CreateElement("add");
                    subElement.SetAttribute("name", name);
                    subElement.SetAttribute("connectionString", connectionString);
                    subElement.SetAttribute("providerName", providerName);
                    node.AppendChild(subElement);
                }

                //保存至配置文件(方式二)
                doc.Save(filename);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                throw ex;
            }

            return isSuccess;
        }
    }
}
