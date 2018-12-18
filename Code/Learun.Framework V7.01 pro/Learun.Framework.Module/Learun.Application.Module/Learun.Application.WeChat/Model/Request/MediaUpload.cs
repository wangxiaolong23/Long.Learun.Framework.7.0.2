
namespace Learun.Application.WeChat
{
    class MediaUpload : OperationRequestBase<MediaUploadResult, HttpPostFileRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token=ACCESS_TOKEN&type={0}";
        protected override string Url()
        {
            return string.Format(url, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string type { get; set; }

           
        /// <summary>
        /// 文件地址
        /// </summary>
        /// <returns></returns>
        public string media { get; set; }

        protected override string HttpSend(IHttpSend httpSend,string url)
        {
            return httpSend.Send(url, media);
        }
    }
}
