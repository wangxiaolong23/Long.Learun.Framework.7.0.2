using ClosedXML.Excel;
using Learun.Application.CRM;
using Learun.Application.Excel;
using NPOI.XWPF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OAModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.17
    /// 描 述：模板导出
    /// </summary>
    public class ModuleExportController : MvcControllerBase
    {

        private CrmOrderIBLL crmOrderIBLL = new CrmOrderBLL();
        private CrmCustomerIBLL crmCustomerIBLL = new CrmCustomerBLL();
        private ModuleExportIBLL moduleExportIBLL = new ModuleExportBLL();

        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件函数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var orderData = crmOrderIBLL.GetCrmOrderEntity(keyValue);
            var customerData = crmCustomerIBLL.GetEntity(orderData.F_CustomerId);
            var orderProductData = crmOrderIBLL.GetCrmOrderProductEntity(keyValue);
            var jsonData = new
            {
                orderData = orderData,
                orderProductData = orderProductData,
                customerData = customerData
            };
            return Success(jsonData);
        }

        #region 扩展：导出
        /// <summary>
        /// 导出报价单(Word)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="requestInfo">数据</param>
        public void ExportWord(string keyValue, string orderInfo)
        {
            XWPFDocument doc = new XWPFDocument();//创建新的word文档

            XWPFParagraph p1 = doc.CreateParagraph();//向新文档中添加段落
            p1.Alignment = ParagraphAlignment.CENTER;//段落对其方式为居中
            XWPFRun r1 = p1.CreateRun();                //向该段落中添加文字
            r1.SetText("上海力软信息技术有限公司");
            r1.IsBold = true;//设置粗体
            r1.FontSize = 28;//设置字体大小

            XWPFParagraph p2 = doc.CreateParagraph();
            p2.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r2 = p2.CreateRun();
            r2.SetText("上海市浦东新区世纪大道108号");
            r2.FontSize = 12;//设置字体大小
            r2.FontFamily = "宋体";

            XWPFParagraph p3 = doc.CreateParagraph();
            p3.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r3 = p3.CreateRun();
            r3.SetText("电话：021-8888888  传真：021-8888888");
            r3.FontSize = 12;//设置字体大小
            r3.FontFamily = "宋体";

            XWPFParagraph p4 = doc.CreateParagraph();
            p4.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r4 = p4.CreateRun();
            r4.SetText("报  价  单");
            r4.IsBold = true;//设置粗体
            r4.FontSize = 28;//设置字体大小

            XWPFParagraph p5 = doc.CreateParagraph();
            XWPFRun r5_1 = p5.CreateRun();
            r5_1.SetText("公    司：");
            r5_1.FontSize = 12;//设置字体大小
            r5_1.FontFamily = "宋体";

            XWPFRun r5_2 = p5.CreateRun();
            r5_2.SetText("捷敏电子          ");
            r5_2.FontSize = 12;//设置字体大小
            r5_2.SetUnderline(UnderlinePatterns.Single);
            r5_2.FontFamily = "宋体";

            XWPFRun r5_3 = p5.CreateRun();
            r5_3.SetText("联系人：");
            r5_3.FontSize = 12;//设置字体大小
            r5_3.FontFamily = "宋体";

            XWPFRun r5_4 = p5.CreateRun();
            r5_4.SetText("陈齐               ");
            r5_4.FontSize = 12;//设置字体大小
            r5_4.SetUnderline(UnderlinePatterns.Single);
            r5_4.FontFamily = "宋体";

            XWPFParagraph p6 = doc.CreateParagraph();
            XWPFRun r6_1 = p6.CreateRun();
            r6_1.SetText("电    话：");
            r6_1.FontSize = 12;//设置字体大小
            r6_1.FontFamily = "宋体";

            XWPFRun r6_2 = p6.CreateRun();
            r6_2.SetText("0592-88888886");
            r6_2.FontSize = 12;//设置字体大小
            r6_2.SetUnderline(UnderlinePatterns.Single);
            r6_2.FontFamily = "宋体";

            XWPFRun r6_3 = p6.CreateRun();
            r6_3.SetText("传    真：");
            r6_3.FontSize = 12;//设置字体大小
            r6_3.FontFamily = "宋体";

            XWPFRun r6_4 = p6.CreateRun();
            r6_4.SetText("0592-88888886     ");
            r6_4.FontSize = 12;//设置字体大小
            r6_4.SetUnderline(UnderlinePatterns.Single);
            r6_4.FontFamily = "宋体";

            XWPFParagraph p7 = doc.CreateParagraph();
            XWPFRun r7_1 = p7.CreateRun();
            r7_1.SetText("邮    箱：");
            r7_1.FontSize = 12;//设置字体大小
            r7_1.FontFamily = "宋体";

            XWPFRun r7_2 = p7.CreateRun();
            r7_2.SetText("821727758@163.com ");
            r7_2.FontSize = 12;//设置字体大小
            r7_2.SetUnderline(UnderlinePatterns.Single);
            r7_2.FontFamily = "宋体";

            XWPFRun r7_3 = p7.CreateRun();
            r7_3.SetText("地    址：");
            r7_3.FontSize = 12;//设置字体大小
            r7_3.FontFamily = "宋体";

            XWPFRun r7_4 = p7.CreateRun();
            r7_4.SetText("陈齐               ");
            r7_4.FontSize = 12;//设置字体大小
            r7_4.SetUnderline(UnderlinePatterns.Single);
            r7_4.FontFamily = "宋体";

            XWPFParagraph p8 = doc.CreateParagraph();
            XWPFRun r8_1 = p8.CreateRun();
            r8_1.SetText("报价人：");
            r8_1.FontSize = 12;//设置字体大小
            r8_1.FontFamily = "宋体";

            XWPFRun r8_2 = p8.CreateRun();
            r8_2.SetText("李阳华               ");
            r8_2.FontSize = 12;//设置字体大小
            r8_2.SetUnderline(UnderlinePatterns.Single);
            r8_2.FontFamily = "宋体";

            XWPFRun r8_3 = p8.CreateRun();
            r8_3.SetText("日    期：");
            r8_3.FontSize = 12;//设置字体大小
            r8_3.FontFamily = "宋体";

            XWPFRun r8_4 = p8.CreateRun();
            r8_4.SetText("2016-05-23 11:18:29");
            r8_4.FontSize = 12;//设置字体大小
            r8_4.SetUnderline(UnderlinePatterns.Single);
            r8_4.FontFamily = "宋体";

            XWPFTable table = doc.CreateTable(11, 8);//创建表格
            table.Width = 600 * 8;
            table.SetColumnWidth(0, 400);/* 设置列宽 */
            table.SetColumnWidth(1, 1100);/* 设置列宽 */
            table.SetColumnWidth(2, 500);/* 设置列宽 */
            table.SetColumnWidth(3, 500);/* 设置列宽 */
            table.SetColumnWidth(4, 500);/* 设置列宽 */
            table.SetColumnWidth(5, 500);/* 设置列宽 */
            table.SetColumnWidth(6, 500);/* 设置列宽 */
            table.SetColumnWidth(7, 800);/* 设置列宽 */

            table.GetRow(0).GetCell(0).SetText("编号");
            table.GetRow(0).GetCell(1).SetText("品名");
            table.GetRow(0).GetCell(2).SetText("规格");
            table.GetRow(0).GetCell(3).SetText("数量");
            table.GetRow(0).GetCell(4).SetText("单位");
            table.GetRow(0).GetCell(5).SetText("单价");
            table.GetRow(0).GetCell(6).SetText("金额");
            table.GetRow(0).GetCell(7).SetText("备注");

            table.GetRow(1).GetCell(0).SetText("1");
            table.GetRow(1).GetCell(1).SetText("敏捷开发框架-个人开发版");
            table.GetRow(1).GetCell(2).SetText("定制");
            table.GetRow(1).GetCell(3).SetText("1");
            table.GetRow(1).GetCell(4).SetText("套");
            table.GetRow(1).GetCell(5).SetText("11111");
            table.GetRow(1).GetCell(6).SetText("11111");
            table.GetRow(1).GetCell(7).SetText("");

            table.GetRow(2).GetCell(0).SetText("2");
            table.GetRow(2).GetCell(1).SetText("敏捷开发框架-个人尊享版");
            table.GetRow(2).GetCell(2).SetText("定制");
            table.GetRow(2).GetCell(3).SetText("1");
            table.GetRow(2).GetCell(4).SetText("套");
            table.GetRow(2).GetCell(5).SetText("22222");
            table.GetRow(2).GetCell(6).SetText("22222");
            table.GetRow(2).GetCell(7).SetText("");

            table.GetRow(3).GetCell(0).SetText("3");
            table.GetRow(3).GetCell(1).SetText("敏捷开发框架-企业旗舰版");
            table.GetRow(3).GetCell(2).SetText("定制");
            table.GetRow(3).GetCell(3).SetText("1");
            table.GetRow(3).GetCell(4).SetText("套");
            table.GetRow(3).GetCell(5).SetText("100000");
            table.GetRow(3).GetCell(6).SetText("100000");
            table.GetRow(3).GetCell(7).SetText("");

            table.GetRow(4).GetCell(0).SetText("4");
            table.GetRow(4).GetCell(1).SetText("服务器硬件");
            table.GetRow(4).GetCell(2).SetText("联想");
            table.GetRow(4).GetCell(3).SetText("5");
            table.GetRow(4).GetCell(4).SetText("台");
            table.GetRow(4).GetCell(5).SetText("20000.00");
            table.GetRow(4).GetCell(6).SetText("100000.00");
            table.GetRow(4).GetCell(7).SetText("");

            table.GetRow(5).GetCell(0).SetText("5");
            table.GetRow(5).GetCell(1).SetText("数据库正版");
            table.GetRow(5).GetCell(2).SetText("SQLServer");
            table.GetRow(5).GetCell(3).SetText("5");
            table.GetRow(5).GetCell(4).SetText("套");
            table.GetRow(5).GetCell(5).SetText("9998.00");
            table.GetRow(5).GetCell(6).SetText("49990.00");
            table.GetRow(5).GetCell(7).SetText("");

            table.GetRow(6).GetCell(0).SetText("6");
            table.GetRow(6).GetCell(1).SetText("服务费");
            table.GetRow(6).GetCell(2).SetText("");
            table.GetRow(6).GetCell(3).SetText("1");
            table.GetRow(6).GetCell(4).SetText("年");
            table.GetRow(6).GetCell(5).SetText("80000.00");
            table.GetRow(6).GetCell(6).SetText("80000.00");
            table.GetRow(6).GetCell(7).SetText("");

            table.GetRow(7).GetCell(0).SetText("7");
            table.GetRow(7).GetCell(1).SetText("差旅费用");
            table.GetRow(7).GetCell(2).SetText("");
            table.GetRow(7).GetCell(3).SetText("1");
            table.GetRow(7).GetCell(4).SetText("年");
            table.GetRow(7).GetCell(5).SetText("60000.00");
            table.GetRow(7).GetCell(6).SetText("80000.00");
            table.GetRow(7).GetCell(7).SetText("");

            table.GetRow(8).GetCell(0).SetText("");
            table.GetRow(8).GetCell(1).SetText("");
            table.GetRow(8).GetCell(2).SetText("");
            table.GetRow(8).GetCell(3).SetText("");
            table.GetRow(8).GetCell(4).SetText("");
            table.GetRow(8).GetCell(5).SetText("");
            table.GetRow(8).GetCell(6).SetText("");
            table.GetRow(8).GetCell(7).SetText("");

            table.GetRow(9).GetCell(0).SetText("             币种：人民币    总计：323323.00	 ");
            table.GetRow(9).MergeCells(0, 7);

            table.GetRow(10).GetCell(0).SetText("备注");
            table.GetRow(10).GetCell(1).SetText("");
            table.GetRow(10).MergeCells(1, 7);


            XWPFParagraph p9 = doc.CreateParagraph();
            XWPFRun r9_1 = p9.CreateRun();
            r9_1.SetText("日    期：");
            r9_1.FontSize = 12;//设置字体大小
            r9_1.FontFamily = "宋体";

            XWPFRun r9_2 = p9.CreateRun();
            r9_2.SetText("                     ");
            r9_2.FontSize = 12;//设置字体大小
            r9_2.SetUnderline(UnderlinePatterns.Single);
            r9_2.FontFamily = "宋体";

            XWPFParagraph p10 = doc.CreateParagraph();
            XWPFRun r10_1 = p10.CreateRun();
            r10_1.SetText("签    字：	");
            r10_1.FontSize = 12;//设置字体大小
            r10_1.FontFamily = "宋体";

            XWPFRun r10_2 = p10.CreateRun();
            r10_2.SetText("              ");
            r10_2.FontSize = 12;//设置字体大小
            r10_2.SetUnderline(UnderlinePatterns.Single);
            r10_2.FontFamily = "宋体";


            MemoryStream ms = new MemoryStream();
            doc.Write(ms);
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.doc", "报价单"));
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            // 指定返回的是一个不能被客户端读取的流，必须被下载   
            Response.ContentType = "application/ms-word";
            // 把文件流发送到客户端 
            Response.BinaryWrite(ms.ToArray());
            doc = null;
            ms.Close();
            ms.Dispose();
        }


        /// <summary>
        /// 导出报价单(Excel)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="requestInfo">数据</param>
        public void ExportExcel(string keyValue, string orderInfo)
        {
            var requestData = crmOrderIBLL.GetCrmOrderEntity(keyValue);
            var customerData = crmCustomerIBLL.GetEntity(requestData.F_CustomerId);//获取采购申请单信息
            var goodsList = crmOrderIBLL.GetCrmOrderProductEntity(keyValue);//获取相应的商品信息
            List<CrmOrderProductEntity> list = goodsList.ToList();
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("报价单");//shift名称
            ws.Cell("A1").Value = "上海力软信息技术有限公司";//表格标题A1代表表格第一行的内容
            ws.Cell("A2").Value = "上海市浦东新区世纪大道108号";//表格标题A2代表表格第一行的内容
            ws.Range(2, 1, 2, 12).Merge();
            ws.Range(2, 1, 2, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell("A2").Style.Font.FontSize = 10;
            ws.Cell("A3").Value = "电话：021-8888888  传真：021-8888888";//表格标题A3代表表格第一行的内容
            ws.Range(3, 1, 3, 12).Merge();
            ws.Range(3, 1, 3, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell("A3").Style.Font.FontSize = 10;
            ws.Cell("A4").Value = "报  价  单";//表格标题A4代表表格第一行的内容
            ws.Range(4, 1, 4, 12).Merge();
            ws.Range(4, 1, 4, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range(4, 1, 4, 12).Style.Font.Bold = true;
            ////报价单信息
            ws.Cell("A5").Value = "公    司";
            ws.Cell("B5").Value = customerData.F_FullName;//公司名称，合并5个单元格
            ws.Cell("B5").DataType = 0;
            ws.Range(5, 2, 5, 6).Merge();
            ws.Range(5, 2, 5, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell("G5").Value = "联系人";
            ws.Cell("H5").Value = customerData.F_Contact;//联系人，合并5个单元格
            ws.Range(5, 8, 5, 12).Merge();
            ws.Range(5, 8, 5, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell("A6").Value = "电    话";
            ws.Cell("B6").Value = customerData.F_Tel;//供货单位，合并5个单元格
            ws.Range(6, 2, 6, 6).Merge();
            ws.Range(6, 2, 6, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell("G6").Value = "传    真";
            ws.Cell("H6").Value = customerData.F_Fax;//采购类型，合并5个单元格
            ws.Range(6, 8, 6, 12).Merge();
            ws.Range(6, 8, 6, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell("A7").Value = "邮    箱";
            ws.Cell("B7").Value = customerData.F_Email;//到货日期，合并5个单元格
            ws.Range(7, 2, 7, 6).Merge();
            ws.Range(7, 2, 7, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell("G7").Value = "地    址";
            ws.Cell("H7").Value = customerData.F_CompanyAddress;//采购总额，合并3个单元格
            ws.Range(7, 8, 7, 12).Merge();
            ws.Range(7, 8, 7, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell("A8").Value = "报价人";
            ws.Cell("B8").Value = customerData.F_CreateUserName;//付款方式，合并3个单元格
            ws.Range(8, 2, 8, 6).Merge();
            ws.Range(8, 2, 8, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell("G8").Value = "日    期";
            ws.Cell("H8").Value = customerData.F_CreateDate;//付款期限，合并3个单元格
            ws.Range(8, 8, 8, 12).Merge();
            ws.Range(8, 8, 8, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ////表格表头处理
            ws.Cell("A9").Value = "编号";
            ws.Cell("B9").Value = "品名";
            ws.Range(9, 2, 9, 4).Merge();
            ws.Range(9, 2, 9, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell("E9").Value = "规格";
            ws.Cell("F9").Value = "数量";
            ws.Cell("G9").Value = "单位";
            ws.Cell("H9").Value = "单价";
            ws.Cell("I9").Value = "金额";
            ws.Cell("J9").Value = "备注";
            ws.Range(9, 10, 9, 12).Merge();
            ws.Range(9, 10, 9, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ////循环添加表格内容
            for (int i = 0; i < list.Count; i++)
            {
                ws.Cell(i + 10, 1).Value = i + 1;
                ws.Cell(i + 10, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(i + 10, 2).Value = list[i].F_ProductName;
                ws.Range(i + 10, 2, i + 10, 4).Merge();
                ws.Range(i + 10, 2, i + 10, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Cell(i + 10, 5).Value = "定制";
                ws.Cell(i + 10, 6).Value = list[i].F_Qty;
                ws.Cell(i + 10, 7).Value = "套";
                ws.Cell(i + 10, 8).Value = list[i].F_Amount;
                ws.Cell(i + 10, 9).Value = list[i].F_Amount * list[i].F_Qty;
                ws.Cell(i + 10, 10).Value = "";//备注
                ws.Range(i + 10, 10, i + 10, 12).Merge();
                ws.Range(i + 10, 10, i + 10, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            }
            ws.Cell(13, 1).Value = list.Count + 1;
            ws.Cell(13, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(13, 2).Value = "服务器硬件";
            ws.Range(13, 2, 13, 4).Merge();
            ws.Range(13, 2, 13, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell(13, 5).Value = "联想";
            ws.Cell(13, 6).Value = 5;
            ws.Cell(13, 7).Value = "台";
            ws.Cell(13, 8).Value = 20000.00;
            ws.Cell(13, 9).Value = 100000.00;
            ws.Cell(13, 10).Value = "";//备注
            ws.Range(13, 10, 13, 12).Merge();
            ws.Range(13, 10, 13, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell(14, 1).Value = list.Count + 2;
            ws.Cell(14, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(14, 2).Value = "数据库正版";
            ws.Range(14, 2, 14, 4).Merge();
            ws.Range(14, 2, 14, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell(14, 5).Value = "SQLServer";
            ws.Cell(14, 6).Value = 5;
            ws.Cell(14, 7).Value = "套";
            ws.Cell(14, 8).Value = 9998.00;
            ws.Cell(14, 9).Value = 49990.00;
            ws.Cell(14, 10).Value = "";//备注
            ws.Range(14, 10, 14, 12).Merge();
            ws.Range(14, 10, 14, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell(15, 1).Value = list.Count + 3;
            ws.Cell(15, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(15, 2).Value = "服务费";
            ws.Range(15, 2, 15, 4).Merge();
            ws.Range(15, 2, 15, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell(15, 5).Value = "";
            ws.Cell(15, 6).Value = 1;
            ws.Cell(15, 7).Value = "年";
            ws.Cell(15, 8).Value = 80000.00;
            ws.Cell(15, 9).Value = 80000.00;
            ws.Cell(15, 10).Value = "";//备注
            ws.Range(15, 10, 15, 12).Merge();
            ws.Range(15, 10, 15, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell(16, 1).Value = list.Count + 4;
            ws.Cell(16, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(16, 2).Value = "差旅费用";
            ws.Range(16, 2, 16, 4).Merge();
            ws.Range(16, 2, 16, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell(16, 5).Value = "";
            ws.Cell(16, 6).Value = 1;
            ws.Cell(16, 7).Value = "年";
            ws.Cell(16, 8).Value = 60000.00;
            ws.Cell(16, 9).Value = 60000.00;
            ws.Cell(16, 10).Value = "";//备注
            ws.Range(16, 10, 16, 12).Merge();
            ws.Range(16, 10, 16, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Cell("A17").Value = "币种:";
            ws.Cell("B17").Value = "人民币";
            ws.Cell("C17").Value = "总计:";
            ws.Cell("D17").Value = "343323.00";
            ws.Cell("A18").Value = "备注";
            ws.Cell("B18").Value = "";
            ws.Range(18, 2, 18, 12).Merge();
            ws.Range(18, 2, 18, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            //表格添加边框
            //列
            //ws.Range(9, 1,16,1).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 1, 16, 1).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 2, 16, 2).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 2, 16, 2).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 5, 16, 5).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 5, 16, 5).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 6, 16, 6).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 6, 16, 6).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 7, 16, 7).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 7, 16, 7).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 8, 16, 8).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 8, 16, 8).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 9, 16, 9).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 9, 16, 9).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 10, 16, 10).Style.Border.LeftBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 10, 16, 10).Style.Border.LeftBorderColor = XLColor.Black;

            //ws.Range(9, 12, 16, 12).Style.Border.RightBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 12, 16, 12).Style.Border.RightBorderColor = XLColor.Black;

            ////行
            //ws.Range(9, 1, 9, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(9, 1, 9, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(10, 1, 10, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(10, 1, 10, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(11, 1, 11, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(11, 1, 11, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(12, 1, 12, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(12, 1, 12, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(13, 1, 13, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(13, 1, 13, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(14, 1, 14, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(14, 1, 14, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(15, 1, 15, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(15, 1, 15, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(16, 1, 16, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(16, 1, 16, 12).Style.Border.TopBorderColor = XLColor.Black;

            //ws.Range(17, 1, 17, 12).Style.Border.TopBorder = XLBorderStyleValues.Hair;
            //ws.Range(17, 1, 17, 12).Style.Border.TopBorderColor = XLColor.Black;
            /////////
            var rngTable = ws.Range("A1:L1");
            var rngHeaders = rngTable.Range("A1:L1");
            ws.Row(1).Height = 20;
            rngHeaders.FirstCell().Style
             .Font.SetBold()
             .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            rngHeaders.FirstRow().Merge();
            ws.Columns().AdjustToContents();
            var col1 = ws.Column("D");
            col1.Width = 15;
            MemoryStream ms = new MemoryStream();
            wb.SaveAs(ms);
            ms.Flush();
            ms.Position = 0;

            byte[] oByte = null;
            oByte = ms.ToArray();

            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + customerData.F_FullName + "报价单" + ".xlsx");
            System.Web.HttpContext.Current.Response.BinaryWrite(oByte);
            //清除缓存  
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
            //关闭缓冲区  
            ms.Close();
        }

        /// <summary>
        /// 导出pdf
        /// </summary>
        public void ExportPDF()
        {
            moduleExportIBLL.ExportPDF("<!DOCTYPE html><html><head></head><body><h1 align=\"center\">上海力软信息技术有限公司</h1><p align=\"center\">上海市浦东新区世纪大道108号</p><p align=\"center\">电话：021-8888888 传真：021-8888888 </p><h1 align=\"center\"> 报 价 单 </h1><p align=\"center\"> 公 司：捷敏电子 联系人：陈齐 </p><p align=\"center\">电 话：0592 - 88888886 传 真：0592 - 88888886 </p><p align=\"center\"> 邮 箱：捷敏电子 地 址：北京市海淀区西直门北大街42号 </p><p align=\"center\"> 报价人：捷敏电子 日 期：2016-05-23 11:18:29 </p><table align=\"center\" border=\"1\"  cellspacing=\"0\" cellpadding=\"0\"><tr><th>编号</th><th>品名</th><th>规格</th><th>数量</th><th>单位</th><th>单价</th><th>金额</th><th>备注</th></tr><tr align=\"center\"><td>1</td><td>敏捷开发框架-个人开发版</td><td>定制</td><td>1</td><td>套</td><td>11111</td><td>11111</td><td></td></tr><tr align=\"center\"><td>2</td><td>敏捷开发框架-个人尊享版</td><td>定制</td><td>1</td><td>套</td><td>22222</td><td>22222</td><td></td></tr><tr align=\"center\"><td>3</td><td>敏捷开发框架-企业旗舰版</td><td>定制</td><td>1</td><td>套</td><td>100000</td><td>100000</td><td></td></tr><tr align=\"center\"><td>4</td><td>服务器硬件</td><td>联想</td><td>5</td><td>台</td><td>20000.00</td><td>100000.00</td><td></td></tr><tr align=\"center\"><td>5</td><td>数据库正版</td><td>SQLServer</td><td>5</td><td>套</td><td>9998.00</td><td>49990.00</td><td></td></tr><tr align=\"center\"><td>6</td><td>服务费</td><td></td><td>1</td><td>年</td><td>80000.00</td><td>80000.00</td><td></td></tr><tr align=\"center\"><td>7</td><td>差旅费用</td><td></td><td>1</td><td>年</td><td>60000.00</td><td>80000.00</td><td></td></tr><tr align=\"center\"><td colspan=\"8\">币种：人民币    总计：323323.00</td></tr><tr><td>备注</td><td colspan=\"7\"></td></tr></table><p>日 期：</p><p>签 字：</p></body></html>");
        }

        #endregion
    }

}