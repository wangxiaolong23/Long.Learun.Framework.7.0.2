using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Learun.Application.WeChat
{
    public class HttpHelper
    {
        public bool Debug { get; set; }
        public CookieCollection Cookies
        {
            get { return _cookies; }
        }

        public void ClearCookies()
        {
            _cookies = new CookieCollection();
        }

        CookieCollection _cookies = new CookieCollection();

        private static readonly string DefaultUserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public HttpWebResponse CreateGetHttpResponse(string url, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            if (Debug)
            {
                Console.Write("Start Get Url:{0}    ", url);
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            HttpWebRequest request;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.Method = "GET";
            request.Headers["Pragma"] = "no-cache";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.Headers["Accept-Language"] = "en-US,en;q=0.5";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = DefaultUserAgent;
            request.Referer = Referer;

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }


            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value * 1000;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(Cookies);
            }
            var v = request.GetResponse() as HttpWebResponse;

            Cookies.Add(request.CookieContainer.GetCookies(new Uri("http://" + new Uri(url).Host)));
            Cookies.Add(request.CookieContainer.GetCookies(new Uri("https://" + new Uri(url).Host)));
            Cookies.Add(v.Cookies);

            if (Debug)
            {
                Console.WriteLine("OK");
            }

            return v;
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, Encoding requestEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            if (Debug)
            {
                Console.Write("Start Post Url:{0}  ", url);

                foreach (KeyValuePair<string, string> keyValuePair in parameters)
                {
                    Console.Write(",{0}:{1}", keyValuePair.Key, keyValuePair.Value);
                }
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, application/json, text/javascript, */*; q=0.01";
            request.Referer = Referer;
            request.Headers["Accept-Language"] = "en-US,en;q=0.5";
            request.UserAgent = DefaultUserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Pragma"] = "no-cache";

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(Cookies);
            }


            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value * 1000;
            }

            request.Expect = string.Empty;

            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                var buffer = CraeteParameter(parameters);
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            var v = request.GetResponse() as HttpWebResponse;

            Cookies.Add(request.CookieContainer.GetCookies(new Uri("http://" + new Uri(url).Host)));
            Cookies.Add(request.CookieContainer.GetCookies(new Uri("https://" + new Uri(url).Host)));

            Cookies.Add(v.Cookies);

            if (Debug)
            {
                Console.WriteLine("OK");
            }

            return v;
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public HttpWebResponse CreatePostHttpResponse(string url, string parameters, Encoding requestEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            if (Debug)
            {
                Console.Write("Start Post Url:{0} ,parameters:{1}  ", url, parameters);


            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.Method = "POST";
            request.Headers.Add("Accept-Language", "zh-CN,en-GB;q=0.5");
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.Referer = Referer;
            request.Headers["Accept-Language"] = "en-US,en;q=0.5";
            request.UserAgent = DefaultUserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Pragma"] = "no-cache";

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(Cookies);
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }


            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value * 1000;
            }

            request.Expect = string.Empty;

            //如果需要POST数据  
            if (!string.IsNullOrEmpty(parameters))
            {
                byte[] data = requestEncoding.GetBytes(parameters);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            var v = request.GetResponse() as HttpWebResponse;

            Cookies.Add(request.CookieContainer.GetCookies(new Uri("http://" + new Uri(url).Host)));
            Cookies.Add(request.CookieContainer.GetCookies(new Uri("https://" + new Uri(url).Host)));
            Cookies.Add(v.Cookies);

            if (Debug)
            {
                Console.WriteLine("OK");
            }


            return v;
        }

        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public HttpWebResponse CreatePostFileHttpResponse(string url, string filePath, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            if (Debug)
            {
                Console.Write("Start Post Url:{0}  ", url);

            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, application/json, text/javascript, */*; q=0.01";
            request.Referer = Referer;
            request.Headers["Accept-Language"] = "en-US,en;q=0.5";
            request.UserAgent = DefaultUserAgent;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers["Pragma"] = "no-cache";

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            else
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(Cookies);
            }


            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value * 1000;
            }

            request.Expect = string.Empty;

            //如果需要POST数据  
            if (!string.IsNullOrEmpty(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader r = new BinaryReader(fs);

                    //时间戳 
                    string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
                    byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");

                    //请求头部信息 
                    StringBuilder sb = new StringBuilder();
                    sb.Append("--");
                    sb.Append(strBoundary);
                    sb.Append("\r\n");
                    sb.Append("Content-Disposition: form-data; name=\"");
                    sb.Append("file");
                    sb.Append("\"; filename=\"");
                    sb.Append(fs.Name);
                    sb.Append("\"");
                    sb.Append("\r\n");
                    sb.Append("Content-Type: ");
                    sb.Append("application/octet-stream");
                    sb.Append("\r\n");
                    sb.Append("\r\n");
                    string strPostHeader = sb.ToString();
                    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);


                    request.ContentType = "multipart/form-data; boundary=" + strBoundary;
                    long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;

                    request.ContentLength = length;

                    //开始上传时间 
                    DateTime startTime = DateTime.Now;

                    byte[] filecontent = new byte[fs.Length];

                    fs.Read(filecontent, 0, filecontent.Length);

                    using (Stream stream = request.GetRequestStream())
                    {

                        //发送请求头部消息 
                        stream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                        stream.Write(filecontent, 0, filecontent.Length);

                        //添加尾部的时间戳 
                        stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    }

                }
            }
            var v = request.GetResponse() as HttpWebResponse;

            Cookies.Add(request.CookieContainer.GetCookies(new Uri("http://" + new Uri(url).Host)));
            Cookies.Add(request.CookieContainer.GetCookies(new Uri("https://" + new Uri(url).Host)));

            Cookies.Add(v.Cookies);

            if (Debug)
            {
                Console.WriteLine("OK");
            }

            return v;
        }


        public static string CraeteParameter(IDictionary<string, string> parameters)
        {
            StringBuilder buffer = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                buffer.AppendFormat("&{0}={1}", key, parameters[key]);
            }
            return buffer.ToString().TrimStart('&');
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }



        public string Post(string url, IDictionary<string, string> parameters, Encoding requestEncoding, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, requestEncoding, timeout, userAgent, cookies, Referer, headers);

            try
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), responseEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string Post(string url, string parameters, Encoding requestEncoding, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            HttpWebResponse response = CreatePostHttpResponse(url, parameters, requestEncoding, timeout, userAgent, cookies, Referer, headers);

            try
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), responseEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string PostFile(string url, string filePath, Encoding responseEncoding,
            int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "",
            Dictionary<string, string> headers = null)
        {
            HttpWebResponse response = CreatePostFileHttpResponse(url, filePath, timeout, userAgent, cookies, Referer,
                headers);

            try
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), responseEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string Get(string url, Encoding responseEncoding, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            HttpWebResponse response = CreateGetHttpResponse(url, timeout, userAgent, cookies, Referer, headers);

            try
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), responseEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public byte[] GetFile(string url, out Dictionary<string, string> header, int? timeout = 300, string userAgent = "", CookieCollection cookies = null, string Referer = "", Dictionary<string, string> headers = null)
        {
            HttpWebResponse response = CreateGetHttpResponse(url, timeout, userAgent, cookies, Referer, headers);

            header = new Dictionary<string, string>();

            foreach (string key in response.Headers.AllKeys)
            {
                header.Add(key, response.Headers[key]);
            }

            try
            {
                System.IO.Stream st = response.GetResponseStream();

                byte[] by = new byte[response.ContentLength];

                st.Read(by, 0, by.Length);

                st.Close();

                return by;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
