using Learun.Application.Organization;
using Learun.Application.WeChat.WeChat;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WebChatModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：企业号部门同步
    /// </summary>
    public class OrganizeController : MvcControllerBase
    {
        private UserIBLL userIBLL = new UserBLL();
        private static Access_tokenWithTime mode = new Access_tokenWithTime();
        private DepartmentIBLL departmentIBLL = new DepartmentBLL();
        private CompanyIBLL companyIBLL = new CompanyBLL();
        //List表单数据
        private static List<UserEntity> Userlist;

        #region 视图功能

        /// <summary>
        /// 部门主界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 同步员工
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberForm()
        {
            return View();
        }
        #endregion

        #region 获取数据 
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTreeList(string keyword)
        {
            //获取微信部门数据
            WXDepartmentListEntity wXDepartmentList = GetDepartmentList("");
            if (wXDepartmentList.errcode != 0)
            {
                return Fail("微信接口错误码" + wXDepartmentList.errcode + ",错误信息" + wXDepartmentList.errmsg);
            }
            else
            {
                //转换成dic数据
                Dictionary<string, string> dir = new Dictionary<string, string>();
                foreach (var item in wXDepartmentList.department)
                {
                    dir.Add(item.id.ToString(), item.name);
                }
                //获取内部系统公司部门列表
                var data = companyIBLL.GetWeChatList(keyword);
                //判断是否同步过
                foreach (var item in data)
                {
                    if (dir.ContainsKey(item.F_EnCode) && dir[item.F_EnCode] == item.F_FullName + "1")
                    {
                        item.F_Fax = "已同步";
                    }
                    else
                    {
                        item.F_Fax = "未同步";
                    }
                }
                return Success(data);
            }
        }

        /// <summary>
        /// 同步部门
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult Sync()
        {
            //获取微信部门数据
            WXDepartmentListEntity wXDepartmentList = GetDepartmentList("");
            if (wXDepartmentList.errcode != 0)
            {
                return Fail("微信接口错误码" + wXDepartmentList.errcode + ",错误信息" + wXDepartmentList.errmsg);
            }
            else
            {
                Dictionary<string, string> dir = new Dictionary<string, string>();
                foreach (var item in wXDepartmentList.department)
                {
                    dir.Add(item.id.ToString(), item.name);
                }
                var data = companyIBLL.GetWeChatList("");
                foreach (var item in data)
                {
                    WX_DepartmentEntity entity = new WX_DepartmentEntity();
                    if (dir.ContainsKey(item.F_EnCode))
                    {
                        //在微信中修改部门
                        entity.F_WXId = item.F_EnCode.ToInt();
                        entity.F_Name = item.F_FullName;
                        var parentEntity = data.Find(i => i.F_CompanyId == item.F_ParentId);
                        if (parentEntity != null)
                        {
                            entity.F_ParentId = parentEntity.F_EnCode.ToInt();
                        }
                        else
                        {
                            entity.F_ParentId = 1;
                        }
                        var res = UpdateDepartment(entity);
                        if (res.errcode != 0)
                        {
                            item.F_Description = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                            companyIBLL.SaveEntity(item.F_CompanyId, item);
                            //return Fail("微信接口错误码" + res.errcode + ",错误信息" + res.errmsg);
                            continue;
                        }
                    }
                    else
                    {
                        entity.F_WXId = item.F_EnCode.ToInt();
                        entity.F_Name = item.F_FullName + "1";
                        var parentEntity = data.Find(i => i.F_CompanyId == item.F_ParentId);
                        if (parentEntity != null)
                        {
                            entity.F_ParentId = parentEntity.F_EnCode.ToInt();
                        }
                        else
                        {
                            entity.F_ParentId = 1;
                        }
                        //在微信中创建部门
                        var res = CreateDepartment(entity);
                        if (res.errcode != 0)
                        {
                            item.F_Description = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                            companyIBLL.SaveEntity(item.F_CompanyId, item);
                            //return Fail("微信接口错误码" + res.errcode + ",错误信息" + res.errmsg);
                            continue;
                        }
                    }
                }
                return Success(data);
            }
        }

        /// <summary>
        /// 获取微信人员同步左侧部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetLeftTree(string parentId)
        {
            var data = companyIBLL.GetWeChatTree(parentId);
            return Success(data);
        }

        /// <summary>
        /// 获取微信人员同步右侧同步信息
        /// </summary>
        /// <param name="companyId">微信公司Id</param>
        /// <param name="keyword">查询关键字</param>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        public ActionResult GetUserPageList(string pagination, string keyword, string companyId, string departmentId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            //获取内部系统人员列表
            var data = userIBLL.GetPageList(companyId, departmentId, paginationobj, keyword);
            //获取微信员工列表
            var wxData = GetUserList("1", 1);
            if (wxData.errcode != 0)
            {
                return Fail("微信接口错误码" + wxData.errcode + ",错误信息" + wxData.errmsg);
            }
            #region 判断数据是否相同
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            foreach (var i in wxData.userlist)
            {
                dic.Add(i.userid, new List<string> { i.name, i.mobile });
            }
            foreach (var item in data)
            {
                if (dic.ContainsKey(item.F_Account))
                {
                    if (item.F_RealName != dic[item.F_Account][0])
                    {
                        item.F_AnswerQuestion = "未同步";
                    }
                    else
                    {
                        item.F_AnswerQuestion = "已同步";
                    }
                }
                else
                {
                    item.F_AnswerQuestion = "未同步";
                }
            }
            #endregion
            Userlist = data;
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 同步员工
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult SyncMember()
        {
            //获取微信部门数据
            UserListEntity wXUserList = GetSimpleUserList("1", 1);
            if (wXUserList.errcode != 0)
            {
                return Fail("微信接口错误码" + wXUserList.errcode + ",错误信息" + wXUserList.errmsg);
            }
            else
            {
                Dictionary<string, string> dir = new Dictionary<string, string>();
                foreach (var item in wXUserList.userlist)
                {
                    dir.Add(item.userid, item.name);
                }
                //获取当前
                var data = userIBLL.GetAllList();
                foreach (var item in data)
                {
                    WeChatUserEntity userEntity = new WeChatUserEntity();
                    if (string.IsNullOrEmpty(item.F_DepartmentId) || string.IsNullOrEmpty(item.F_Mobile)) continue;
                    if (dir.ContainsKey(item.F_Account))
                    {
                        if (dir[item.F_Account] != item.F_RealName)
                        {
                            userEntity.name = item.F_RealName;
                            userEntity.mobile = item.F_Mobile;
                            userEntity.userid = item.F_Account;
                            userEntity.enable = 1;
                            if (item.F_DepartmentId != null)
                            {
                                var departmentEntity = departmentIBLL.GetEntity(item.F_DepartmentId);
                                userEntity.department = new List<int> { departmentEntity == null ? 0 : departmentEntity.F_EnCode.ToInt() };
                            }
                            else
                            {
                                var companyEntity = companyIBLL.GetEntity(item.F_CompanyId);
                                userEntity.department = new List<int> { companyEntity == null ? 0 : companyEntity.F_EnCode.ToInt() };
                            }
                            //更新微信部门成员
                            //var res = UpdateWXUser(userEntity);
                            //if (res.errcode != 0)
                            //{
                            //    item.F_Description = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                            //    userIBLL.SaveEntity(item.F_UserId, item);
                            //    return Fail("微信接口错误码" + res.errcode + ",错误信息" + res.errmsg);
                            //    //continue;
                            //}
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        userEntity.name = item.F_RealName;
                        userEntity.mobile = item.F_Mobile;
                        userEntity.userid = item.F_Account;
                        userEntity.enable = 1;
                        if (item.F_DepartmentId != null)
                        {
                            var departmentEntity = departmentIBLL.GetEntity(item.F_DepartmentId);
                            userEntity.department = new List<int> { departmentEntity == null ? 0 : departmentEntity.F_EnCode.ToInt() };
                        }
                        else
                        {
                            var companyEntity = companyIBLL.GetEntity(item.F_CompanyId);
                            userEntity.department = new List<int> { companyEntity == null ? 0 : companyEntity.F_EnCode.ToInt() };
                        }
                        //创建微部门成员
                        var res = CreateWXUser(userEntity);
                        if (res.errcode != 0)
                        {
                            item.F_Description = "微信接口错误码" + res.errcode + ",错误信息" + res.errmsg;
                            userIBLL.SaveEntity(item.F_UserId, item);
                            //return Fail("微信接口错误码" + res.errcode + ",错误信息" + res.errmsg);
                            continue;
                        }
                    }
                }
                return Success("");
            }
        }
        #endregion

        #region 微信接口方法

        /// <summary>
        /// 判断token是否过期
        /// </summary>
        /// <returns></returns>
        public Access_TokenEntity IsExistAccess_Token()
        {
            if (mode.token != null)
            {
                TimeSpan st1 = new TimeSpan(mode.time.Ticks); //最后刷新的时间
                TimeSpan st2 = new TimeSpan(DateTime.Now.Ticks); //当前时间
                TimeSpan st = st2 - st1; //两者相差时间
                if (st.TotalSeconds > mode.token.expires_in)
                {
                    mode.token = GetToken();
                    mode.time = DateTime.Now;
                }
            }
            else
            {
                mode.token = GetToken();
                mode.time = DateTime.Now;
            }
            return mode.token;
        }
        /// <summary>
        /// 获取微信token
        /// </summary>
        /// <returns></returns>
        public Access_TokenEntity GetToken()
        {
            var corpId = Config.GetValue("CorpId");
            var corpSecret = Config.GetValue("CorpSecret");
            string url = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid=" + corpId + "&corpsecret=" + corpSecret;
            Access_TokenEntity token = new Access_TokenEntity();
            token = HttpGet(url).ToObject<Access_TokenEntity>();
            return token;
        }
        /// <summary>
        /// 获取微信账户
        /// </summary>
        /// <param name="userId">用户账户</param>
        public GetUserEntity GetWeChatUser(string userId)
        {
            var token = IsExistAccess_Token();
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token=" + token.access_token + "&userid=" + userId;
            GetUserEntity account = new GetUserEntity();
            account = HttpGet(url).ToObject<GetUserEntity>();
            return account;
        }
        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="fetchchild"></param>
        /// <returns></returns>
        public UserListEntity GetSimpleUserList(string departmentId, int fetchchild)
        {
            var token = IsExistAccess_Token();
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token=" + token.access_token + "&department_id=" + departmentId + "&fetch_child=" + fetchchild;
            UserListEntity account = new UserListEntity();
            account = HttpGet(url).ToObject<UserListEntity>();
            return account;
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="fetchchild"></param>
        /// <returns></returns>
        public WXDepartmentListEntity GetDepartmentList(string departmentId)
        {
            var token = IsExistAccess_Token();
            string url = " https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token=" + token.access_token + "&id=" + departmentId;
            WXDepartmentListEntity res = new WXDepartmentListEntity();
            res = HttpGet(url).ToObject<WXDepartmentListEntity>();
            return res;
        }
        /// <summary>
        /// 获取部门成员详情
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="fetchchild"></param>
        /// <returns></returns>
        public UserDetailListEntity GetUserList(string departmentId, int fetchchild)
        {
            var token = IsExistAccess_Token();
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token=" + token.access_token + "&department_id=" + departmentId + "&fetch_child=" + fetchchild;
            UserDetailListEntity account = new UserDetailListEntity();
            account = HttpGet(url).ToObject<UserDetailListEntity>();
            return account;
        }

        /// <summary>
        /// 创建微信账户
        /// </summary>
        /// <param name="entity"></param>
        public ReturnMessageEntity CreateWXUser(WeChatUserEntity account)
        {
            var token = IsExistAccess_Token();
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token=" + token.access_token;
            ReturnMessageEntity res = new ReturnMessageEntity();
            res = HttpPost(url, account.ToJson()).ToObject<ReturnMessageEntity>();
            return res;
        }

        /// <summary>
        /// 更新微信账户
        /// </summary>
        /// <param name="entity"></param>
        public ReturnMessageEntity UpdateWXUser(WeChatUserEntity account)
        {
            var token = IsExistAccess_Token();
            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token=" + token.access_token;
            ReturnMessageEntity res = new ReturnMessageEntity();
            res = HttpPost(url, account.ToJson()).ToObject<ReturnMessageEntity>();
            return res;
        }
        /// <summary>
        /// 删除微信账户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ReturnMessageEntity DeleteWeChatUser(string userId)
        {
            var token = IsExistAccess_Token();
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token=" + token.access_token + "&userid=" + userId;
            ReturnMessageEntity res = new ReturnMessageEntity();
            res = HttpGet(url).ToObject<ReturnMessageEntity>();
            return res;
        }
        /// <summary>
        /// 批量删除微信账户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ReturnMessageEntity BatchDeleteWeChatUser(List<string> userId)
        {
            var token = IsExistAccess_Token();
            BatchDelete list = new BatchDelete();
            list.useridlist = userId;
            string url = "https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token=" + token.access_token;
            ReturnMessageEntity res = new ReturnMessageEntity();
            res = HttpPost(url, list.ToString()).ToObject<ReturnMessageEntity>();
            return res;
        }
        /// <summary>
        /// 创建微信部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXDepartmentReMsgEntity CreateDepartment(WX_DepartmentEntity entity)
        {
            WXDepartmentEntity dep = new WXDepartmentEntity();
            dep.name = entity.F_Name;
            dep.id = entity.F_WXId;
            dep.parentid = entity.F_ParentId;
            dep.order = entity.F_Order;
            var token = IsExistAccess_Token();
            var url = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token=" + token.access_token;
            WXDepartmentReMsgEntity res = new WXDepartmentReMsgEntity();
            res = HttpPost(url, dep.ToJson()).ToObject<WXDepartmentReMsgEntity>();
            return res;
        }
        /// <summary>
        /// 更新微信部门
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public WXDepartmentReMsgEntity UpdateDepartment(WX_DepartmentEntity entity)
        {
            WXDepartmentEntity dep = new WXDepartmentEntity();
            dep.name = entity.F_Name;
            dep.id = entity.F_WXId;
            dep.parentid = entity.F_ParentId;
            dep.order = entity.F_Order;
            var token = IsExistAccess_Token();
            var url = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token=" + token.access_token;
            WXDepartmentReMsgEntity res = new WXDepartmentReMsgEntity();
            res = HttpPost(url, dep.ToJson()).ToObject<WXDepartmentReMsgEntity>();
            return res;
        }

        /// <summary>
        /// 删除微信部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public ReturnMessageEntity DeleteDepartment(string departmentId)
        {
            var token = IsExistAccess_Token();
            string url = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token=" + token.access_token + "&id=" + departmentId;
            ReturnMessageEntity res = new ReturnMessageEntity();
            res = HttpGet(url).ToObject<ReturnMessageEntity>();
            return res;
        }
        #endregion

        #region 实体类
        /// <summary>
        /// token-time类
        /// </summary>
        public class Access_tokenWithTime
        {
            public DateTime time { get; set; }

            public Access_TokenEntity token { get; set; }
        }

        public class BatchDelete
        {
            public List<string> useridlist { get; set; }
        }
        #endregion

        #region HTTP操作
        public string HttpGet(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public string HttpPost(string Url, string postDataStr)
        {
            //创建一个HTTP请求  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            //Post请求方式  
            request.Method = "POST";
            //内容类型
            request.ContentType = "application/x-www-form-urlencoded";
            //设置参数，并进行URL编码 
            string paraUrlCoded = postDataStr;//System.Web.HttpUtility.UrlEncode(jsonParas);   
            byte[] payload;
            //将Json字符串转化为字节  
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            //设置请求的ContentLength   
            request.ContentLength = payload.Length;
            //发送请求，获得请求流 
            Stream writer;
            try
            {
                writer = request.GetRequestStream();//获取用于写入请求数据的Stream对象
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("连接服务器失败!");
            }
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            writer.Close();//关闭请求流
            HttpWebResponse response;
            try
            {
                //获得响应流
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            Stream postData = Request.InputStream;
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            return postContent;//返回Json数据

        }
        #endregion
    }
}