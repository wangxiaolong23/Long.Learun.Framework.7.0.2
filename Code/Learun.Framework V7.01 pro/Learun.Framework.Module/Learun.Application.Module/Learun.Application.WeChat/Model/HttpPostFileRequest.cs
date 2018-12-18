using System.Text;

namespace Learun.Application.WeChat
{
    class HttpPostFileRequest : IHttpSend
    {
        public string Send(string url, string data)
        {
            return new HttpHelper().PostFile(url, data,Encoding.UTF8);
        }
    }
}
