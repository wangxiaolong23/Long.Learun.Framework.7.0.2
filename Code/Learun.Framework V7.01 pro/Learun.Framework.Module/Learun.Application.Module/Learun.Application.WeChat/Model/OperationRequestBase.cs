using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Learun.Util;

namespace Learun.Application.WeChat
{
    public abstract class OperationRequestBase<T, THttp> : ISend<T>
        where T : OperationResultsBase, new()
        where THttp : IHttpSend, new()
    {
        protected abstract string Url();

        /// <summary>
        /// 视同attribute进行简单校验
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool Verify(out string message)
        {
            message = "";
            foreach (var pro in this.GetType().GetProperties())
            {
                var v = pro.GetCustomAttributes(typeof(IVerifyAttribute), true);


                foreach (IVerifyAttribute verify in pro.GetCustomAttributes(typeof(IVerifyAttribute), true))
                {
                    if (!verify.Verify(pro.PropertyType, pro.GetValue(this), out message))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 格式化URL，替换Token
        /// </summary>
        /// <returns></returns>
        protected string GetUrl()
        {
            if (Token.IsTimeOut())
            {
                Token.GetNewToken();
            }

            string url = Url();

            if (url.Contains("=ACCESS_TOKEN"))
            {
                url = url.Replace("=ACCESS_TOKEN", "=" + Token.GetToken());
            }

            return url;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <returns></returns>
        public T Send()
        {
            string message = "";
            if (!Verify(out message))
            {
                throw new Exception(message);
            }
            
            //string result = new HttpHelper().Post(url, JsonConvert.SerializeObject(this), Encoding.UTF8, Encoding.UTF8);

            IHttpSend httpSend = new THttp();

            string result = HttpSend(httpSend, GetUrl());

            return GetDeserializeObject(result);
        }
        /// <summary>
        /// 开放平台发送
        /// </summary>
        /// <returns></returns>
        public T OpenSend()
        {
            string message = "";
            if (!Verify(out message))
            {
                throw new Exception(message);
            }

            //string result = new HttpHelper().Post(url, JsonConvert.SerializeObject(this), Encoding.UTF8, Encoding.UTF8);

            IHttpSend httpSend = new THttp();

            string result = HttpSend(httpSend, Url());

            return GetDeserializeObject(result);
        }

        /// <summary>
        /// 处理返回结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual T GetDeserializeObject(string result)
        {
            return result.ToObject<T>();
        }

        /// <summary>
        /// 处理发送请求
        /// </summary>
        /// <param name="httpSend"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        protected virtual string HttpSend(IHttpSend httpSend, string url)
        {
            return httpSend.Send(url,this.ToJson());
        }

    }
}
