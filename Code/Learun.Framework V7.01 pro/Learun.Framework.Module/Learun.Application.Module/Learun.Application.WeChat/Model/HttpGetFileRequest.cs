using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Learun.Util;

namespace Learun.Application.WeChat
{
    class HttpGetFileRequest : IHttpSend
    {
        public string Send(string url, string path)
        {
            Dictionary<string, string> header;

            var bytes = new HttpHelper().GetFile(url, out header);

            if (header["Content-Type"].Contains("application/json"))
            {
                return Encoding.UTF8.GetString(bytes);
            }
            else
            {
                Regex regImg = new Regex("\"(?<fileName>.*)\"", RegexOptions.IgnoreCase);

                MatchCollection matches = regImg.Matches(header["Content-disposition"]);

                string fileName = matches[0].Groups["fileName"].Value;

                string filepath = path.TrimEnd('\\') + "\\" + fileName;

                System.IO.Stream so = new System.IO.FileStream(filepath, System.IO.FileMode.Create);

                so.Write(bytes, 0, bytes.Length);

                so.Close();
            }

            return header.ToJson();
        }
    }
}
