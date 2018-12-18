using Spire.Xls;
using System;
using Learun.Util;
using Spire.Doc;
using System.IO;

namespace Learun.Application.OA.File.FilePreview
{
    public class FilePreviewBLL:FilePreviewIBLL
    {

        /// <summary>
        /// 获取EXCEL数据
        /// <summary>
        /// <returns></returns>
        public void GetExcelData(string path)
        {
            try
            {
                // load Excel file
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(path);
                workbook.SaveToFile(path.Substring(0, path.LastIndexOf(".")) + ".pdf", Spire.Xls.FileFormat.PDF);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }


        /// <summary>
        /// 获取Word数据
        /// <summary>
        /// <returns></returns>
        public void GetWordData(string path)
        {
            try
            {
                Document document = new Document();
                document.LoadFromFile(path);
                document.SaveToFile(path.Substring(0, path.LastIndexOf(".")) + ".pdf", Spire.Doc.FileFormat.PDF);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
    }
}