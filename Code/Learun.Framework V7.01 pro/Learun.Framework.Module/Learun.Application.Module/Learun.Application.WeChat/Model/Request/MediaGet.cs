using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Learun.Application.WeChat
{
    class MediaGet : OperationRequestBase<MediaGetResult, HttpGetFileRequest>
    {
        private string url = "https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id={0}";
        protected override string Url()
        {
            return string.Format(url, media_id);
        }

        /// <summary>
        /// 媒体文件id
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string media_id { get; set; }

        /// <summary>
        /// 图片保存路径
        /// </summary>
        /// <returns></returns>
        [IsNotNull]
        public string path { get; set; }

        protected override string HttpSend(IHttpSend httpSend, string url)
        {
            return httpSend.Send(url, path);
        }

        protected override MediaGetResult GetDeserializeObject(string result)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "d:\\";
            }

            try
            {
                var re = base.GetDeserializeObject(result);

                if (re != null && re.errcode != 0)
                {
                    return re;
                }
            }
            catch (Exception)
            {

            }

            var header = result.ToObject<Dictionary<string, string>>();

            Regex regImg = new Regex("\"(?<fileName>.*)\"", RegexOptions.IgnoreCase);

            MatchCollection matches = regImg.Matches(header["Content-disposition"]);

            string fileName = matches[0].Groups["fileName"].Value;

            string filepath = path.TrimEnd('\\') + "\\" + fileName;

            return new MediaGetResult()
            {
                errcode = 0,
                errmsg = "",
                FilePath = filepath,
                FileName = fileName,
                FileType = header["Content-Type"]
            };
        }
    }
}
