
namespace Learun.Application.WeChat
{
    public class MediaUploadResult : OperationResultsBase
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）,普通文件(file)
        /// </summary>
        /// <returns></returns>
        public string type { get; set; }

        /// <summary>
        /// 媒体文件上传后获取的唯一标识
        /// </summary>
        /// <returns></returns>
        public string media_id { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        /// <returns></returns>
        public string created_at { get; set; }
    }
}
