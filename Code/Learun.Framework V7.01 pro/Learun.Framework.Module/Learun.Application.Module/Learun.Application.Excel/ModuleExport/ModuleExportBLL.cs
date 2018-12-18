using Learun.Util;
using Pechkin;
using Pechkin.Synchronized;
using System.IO;

namespace Learun.Application.Excel
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：模板导出功能
    /// </summary>
    public class ModuleExportBLL: ModuleExportIBLL
    {
        /// <summary>
        /// 导出PDF
        /// </summary>
        /// <param name="html">html页面字串</param>
        public void ExportPDF(string html)
        {
            SynchronizedPechkin sc = new SynchronizedPechkin(new GlobalConfig().SetMargins(new System.Drawing.Printing.Margins(100, 100, 100, 100)));

            byte[] buf = sc.Convert(new ObjectConfig(), html);

            var ms = new MemoryStream(buf);
            FileDownHelper.DownLoad(ms, "报价单.pdf");
            ms.Close();
        }
    }
}
