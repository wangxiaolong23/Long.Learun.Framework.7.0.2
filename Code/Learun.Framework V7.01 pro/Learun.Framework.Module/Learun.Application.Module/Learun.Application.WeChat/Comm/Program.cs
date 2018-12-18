using Learun.Util;
using System;
namespace Learun.Application.WeChat
{
    class Program
    {
        static void Main(string[] args)
        {
            //         //公众平台上开发者设置的token, corpID, EncodingAESKey
            //         string sToken = "QDG6eK";
            //         string sCorpID = "wx5823bf96d3bd56c7";
            //         string sEncodingAESKey = "jWmYm7qr5nMoAUwZRjGtBxmz3KA1tkAj3ykkR6q2B2C";

            //         /*
            //------------使用示例一：验证回调URL---------------
            //*企业开启回调模式时，企业号会向验证url发送一个get请求 
            //假设点击验证时，企业收到类似请求：
            //* GET /cgi-bin/wxpush?msg_signature=5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3&timestamp=1409659589&nonce=263014780&echostr=P9nAzCzyDtyTWESHep1vC5X9xho%2FqYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp%2B4RPcs8TgAE7OaBO%2BFZXvnaqQ%3D%3D 
            //* HTTP/1.1 Host: qy.weixin.qq.com

            //* 接收到该请求时，企业应			1.解析出Get请求的参数，包括消息体签名(msg_signature)，时间戳(timestamp)，随机数字串(nonce)以及公众平台推送过来的随机加密字符串(echostr),
            //这一步注意作URL解码。
            //2.验证消息体签名的正确性 
            //3.解密出echostr原文，将原文当作Get请求的response，返回给公众平台
            //第2，3步可以用公众平台提供的库函数VerifyURL来实现。
            //*/

            //         Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            //         // string sVerifyMsgSig = HttpUtils.ParseUrl("msg_signature");
            //         string sVerifyMsgSig = "5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3";
            //         // string sVerifyTimeStamp = HttpUtils.ParseUrl("timestamp");
            //         string sVerifyTimeStamp = "1409659589";
            //         // string sVerifyNonce = HttpUtils.ParseUrl("nonce");
            //         string sVerifyNonce = "263014780";
            //         // string sVerifyEchoStr = HttpUtils.ParseUrl("echostr");
            //         string sVerifyEchoStr = "P9nAzCzyDtyTWESHep1vC5X9xho/qYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp+4RPcs8TgAE7OaBO+FZXvnaqQ==";
            //         int ret = 0;
            //         string sEchoStr = "";
            //         ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            //         if (ret != 0)
            //         {
            //             System.Console.WriteLine("ERR: VerifyURL fail, ret: " + ret);
            //             return;
            //         }
            //         //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业号。
            //         // HttpUtils.SetResponse(sEchoStr);

            //string strulr = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

            //string corpID = "wx362a967bd6dbbda6";
            //string Secret = "9oxdOKBnkfzPC7zvmnjXwQDftRmGyaOZAB9Z70oNKMXymmFsjYyriZMQmOrKOIoA";


            //HttpHelper http = new HttpHelper();

            //string respone = http.Get(string.Format(strulr, corpID, Secret), Encoding.UTF8);

            //string respone ="{\"access_token\":\"IFxMCIhQI7UmVcmxeZoKHippbU47teUFCGt0za2gmBVtP2cAjfx_0O3vOPS_YrSy\",\"expires_in\":7200}";

            //var token = JsonConvert.DeserializeObject<Token>(respone);

            //Console.WriteLine(respone);

            //OperationRequestBase b = new DepartmentCreate() {name="22"};

            //string str = "";

            // Console.WriteLine(b.Verify(out str));


            //var a = new UserSimplelist() {department_id = "1"}.Send();

            //Console.WriteLine(JsonConvert.SerializeObject(a));





            var o = new Oauth2Authorize()
            {
                appid = "wx362a967bd6dbbda6",
                redirect_uri = "http://115.28.86.11/test.php",
                state = "ping"

            };

            var b = new SendText()
            {
                agentid = "2",
                touser = "@all",
                text = new SendText.SendItem()
                {
                    content = o.GetAuthorizeUrl()
                }
            };
            //var c = new SendNews()
            //{
            //    agentid = "2",
            //    touser = "@all",
            //    news = new SendNews.SendItemLoist
            //    {
            //        articles = new List<SendNews.SendItem> { 
            //        new SendNews.SendItem
            //        {
            //        description="测试新闻",
            //        picurl="http://www.learun.cn/images/banner3.jpg",
            //        url="http://www.learun.cn/fdms/index.html",
            //        title="力软在线"
            //        },
            //                            new SendNews.SendItem
            //        {
            //        description="测试新闻二",
            //        picurl="http://www.learun.cn/images/banner3.jpg",
            //        url="http://www.learun.cn",
            //        title="力软在线"
            //        }
                    
            //        }
            //    }
            //};
            // var b = new MediaUpload() {filePath = "d:\\1014_eb4ee167ea304b4dba06692d17464320.f20.mp4", type = "video"};

            //var m = b.Send();
            //c.Send();
            UserGet cc = new UserGet(); cc.userid = "liu"; 
            var m=cc.Send();

            Console.WriteLine(m.ToJson());

            //string str = new HttpHelper().PostFile(
            //    @"https://mp.weixin.qq.com/debug/cgi-bin/apiagent?url=http%3A%2F%2Ffile.api.weixin.qq.com%2Fcgi-bin%2Fmedia%2Fupload%3Faccess_token%3D123123123%26type%3Dimage&method=POST&body=0"
            //    ,  Encoding.UTF8);

            Console.ReadLine();
        }
    }

}
