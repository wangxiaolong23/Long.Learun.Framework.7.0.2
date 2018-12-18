using System.IO;

namespace Learun.Application.OA.File.FilePreview
{
    public interface FilePreviewIBLL
    {
        /// <summary>
        /// excel文档
        /// <summary>
        /// <returns></returns>
        void GetExcelData(string path);
        /// <summary>
        /// word文档
        /// <summary>
        /// <returns></returns>
        void GetWordData(string path);
    }
}