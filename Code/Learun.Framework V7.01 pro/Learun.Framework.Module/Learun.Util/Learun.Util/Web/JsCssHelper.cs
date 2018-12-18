using System;
using System.IO;
using System.Reflection;
using System.Text;
using Yahoo.Yui.Compressor;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：js,css,文件压缩和下载
    /// </summary>
    public  class JsCssHelper
    {
        private static JavaScriptCompressor javaScriptCompressor = new JavaScriptCompressor();
        private static CssCompressor cssCompressor = new CssCompressor();

        #region Js 文件操作
        /// <summary>
        /// 读取js文件内容并压缩
        /// </summary>
        /// <param name="filePathlist"></param>
        /// <returns></returns>
        public static string ReadJSFile(string[] filePathlist)
        {
            StringBuilder jsStr = new StringBuilder();
            try
            {
                string rootPath = Assembly.GetExecutingAssembly().CodeBase.Replace("/bin/Learun.Util.DLL", "").Replace("file:///", "");
                foreach (var filePath in filePathlist)
                {
                    string path = rootPath + filePath;
                    if (DirFileHelper.IsExistFile(path))
                    {
                        string content = File.ReadAllText(path, Encoding.UTF8);
                        if (Config.GetValue("JsCompressor") == "true")
                        {
                            content = javaScriptCompressor.Compress(content);
                        }
                        jsStr.Append(content);
                    }
                }
                return jsStr.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region Css 文件操作
        /// <summary>
        /// 读取css 文件内容并压缩
        /// </summary>
        /// <param name="filePathlist"></param>
        /// <returns></returns>
        public static string ReadCssFile(string[] filePathlist)
        {
            StringBuilder cssStr = new StringBuilder();
            try
            {
                string rootPath = Assembly.GetExecutingAssembly().CodeBase.Replace("/bin/Learun.Util.DLL", "").Replace("file:///", "");
                foreach (var filePath in filePathlist)
                {
                    string path = rootPath + filePath;
                    if (DirFileHelper.IsExistFile(path))
                    {
                        string content = File.ReadAllText(path, Encoding.UTF8);
                        content = cssCompressor.Compress(content);
                        cssStr.Append(content);
                    }
                }
                return cssStr.ToString();
            }
            catch (Exception)
            {
                return cssStr.ToString();
            }
        }
        #endregion

        #region 读取文件
        /// <summary>
        /// 读取对应文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string Read(string filePath)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                string rootPath = Assembly.GetExecutingAssembly().CodeBase.Replace("/bin/Learun.Util.DLL", "").Replace("file:///", "");
                string path = rootPath + filePath;
                if (DirFileHelper.IsExistFile(path))
                {
                    string content = File.ReadAllText(path, Encoding.UTF8);
                    str.Append(content);
                }
                return str.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 读取js文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadJS(string filePath)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                string rootPath = Assembly.GetExecutingAssembly().CodeBase.Replace("/bin/Learun.Util.DLL", "").Replace("file:///", "");
                string path = rootPath + filePath;
                if (DirFileHelper.IsExistFile(path))
                {
                    string content = File.ReadAllText(path, Encoding.UTF8);
                    if (Config.GetValue("JsCompressor") == "true")
                    {
                        content = javaScriptCompressor.Compress(content);
                    }
                    str.Append(content);
                }
                return str.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 读取css文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadCss(string filePath)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                string rootPath = Assembly.GetExecutingAssembly().CodeBase.Replace("/bin/Learun.Util.DLL", "").Replace("file:///", "");
                string path = rootPath + filePath;
                if (DirFileHelper.IsExistFile(path))
                {
                    string content = File.ReadAllText(path, Encoding.UTF8);
                    content = cssCompressor.Compress(content);
                    str.Append(content);
                }
                return str.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}
