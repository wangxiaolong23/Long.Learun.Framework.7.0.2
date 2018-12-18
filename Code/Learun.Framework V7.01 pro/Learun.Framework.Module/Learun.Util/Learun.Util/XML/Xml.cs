using System.Xml;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：xml文件操作
    /// </summary>
    public static class Xml
    {
        /// <summary>
        /// 往项目里包含文件
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="aspxCsFileList"></param>
        public static void WriteCsproj(string csprojPath, string filePath, bool isContent)
        {
            //1。初始化一个xml实例
            XmlDocument xmlDoc = new XmlDocument();
            //2。导入指定xml文件
            xmlDoc.Load(csprojPath);
            //3。查找节点Project
            XmlNodeList root_childlist = xmlDoc.ChildNodes;
            XmlNode root_Project = null; ;
            foreach (XmlNode xn in root_childlist)
            {
                if (xn.Name == "Project")
                {
                    root_Project = xn;
                    break;
                }
            }
            //4。查找Project节点下的ItemGroup节点，确定内容节点Content和编译节点Compile所在的ItemGroup
            XmlNodeList childlist_Project = root_Project.ChildNodes;//根节点的字节点
            foreach (XmlNode xn in childlist_Project)
            {
                if (xn.Name == "ItemGroup")
                {
                    if (isContent)
                    {
                        if (xn.FirstChild.Name == "Content")
                        {
                            XmlElement xe_Content = xmlDoc.CreateElement("Content", xmlDoc.DocumentElement.NamespaceURI);//创建一个节点
                            xe_Content.SetAttribute("Include", filePath);//设置该节点genre属性
                            xn.AppendChild(xe_Content);
                            break;
                        }
                    }
                    else
                    {
                        if (xn.FirstChild.Name == "Compile")
                        {
                            XmlElement xe_Compile = xmlDoc.CreateElement("Compile", xmlDoc.DocumentElement.NamespaceURI);//创建一个节点
                            xe_Compile.SetAttribute("Include", filePath);//设置该节点genre属性
                            xn.AppendChild(xe_Compile);
                            break;
                        }
                    }
                }
            }
           
            //5。保存修改后的文件
            xmlDoc.Save(csprojPath);
        }
    }
}
