using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Base.SystemModule;
using Learun.Application.Organization;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Web;

namespace Learun.Util.Operat
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：当前连接用户信息处理类
    /// </summary>
    public class OperatorHelper
    {
        #region 基础数据类
        private UserIBLL userIBLL = new UserBLL();
        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();

        private CompanyIBLL companyIBLL = new CompanyBLL();
        private DepartmentIBLL departmentIBLL = new DepartmentBLL();


        #endregion

        /// <summary>
        /// 缓存操作类
        /// </summary>
        private ICache redisCache = CacheFactory.CaChe();
        private string cacheKeyOperator = "learun_adms_operator_";// +登录者token
        private string cacheKeyToken = "learun_adms_token_";// +登录者token
        private string cacheKeyError = "learun_adms_error_";// + Mark
        /// <summary>
        /// 秘钥
        /// </summary>
        private string LoginUserToken = "Learun_ADMS_V7_Token";
        /// <summary>
        /// 标记登录的浏览器
        /// </summary>
        private string LoginUserMarkKey = "Learun_ADMS_V7_Mark";
        /// <summary>
        /// 获取实例
        /// </summary>
        public static OperatorHelper Instance
        {
            get { return new OperatorHelper(); }
        }

        /// <summary>
        /// 获取浏览器设配号
        /// </summary>
        /// <returns></returns>
        public string GetMark()
        {
            string cookieMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
            if (string.IsNullOrEmpty(cookieMark))
            {
                cookieMark = Guid.NewGuid().ToString();
                WebHelper.WriteCookie(LoginUserMarkKey, cookieMark);
            }
            return cookieMark;
        }
        /// <summary>
        /// 登录者信息添加到缓存中
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="appId">应用id</param>
        /// <param name="loginMark">设备标识</param>
        /// <param name="cookie">是否保存cookie，默认是</param>
        /// <returns></returns>
        public string AddLoginUser(string account, string mobileCode, string appId, string loginMark, bool cookie = true)
        {
            string token = Guid.NewGuid().ToString();
            try
            {
                // 填写登录信息
                Operator operatorInfo = new Operator();
                operatorInfo.appId = appId;
                operatorInfo.account = account;
                operatorInfo.logTime = DateTime.Now;
                operatorInfo.iPAddress = Net.Ip;
                operatorInfo.browser = Net.Browser;
                operatorInfo.token = token;
                if (cookie)
                {
                    string cookieMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                    if (string.IsNullOrEmpty(cookieMark))
                    {
                        operatorInfo.loginMark = Guid.NewGuid().ToString();
                        WebHelper.WriteCookie(LoginUserMarkKey, operatorInfo.loginMark);
                    }
                    else
                    {
                        operatorInfo.loginMark = cookieMark;
                    }
                    WebHelper.WriteCookie(LoginUserToken, token);
                }
                else
                {
                    operatorInfo.loginMark = loginMark;
                }
                Dictionary<string, string> tokenMarkList = redisCache.Read<Dictionary<string, string>>(cacheKeyToken + account, CacheId.loginInfo);
                if (tokenMarkList == null)// 此账号第一次登录
                {
                    tokenMarkList = new Dictionary<string, string>();
                    tokenMarkList.Add(operatorInfo.loginMark, token);
                }
                else
                {
                    if (tokenMarkList.ContainsKey(operatorInfo.loginMark))
                    {
                        tokenMarkList[operatorInfo.loginMark] = token;
                    }
                    else
                    {
                        tokenMarkList.Add(operatorInfo.loginMark, token);
                    }
                }
                redisCache.Write<Dictionary<string, string>>(cacheKeyToken + account, tokenMarkList, CacheId.loginInfo);
                redisCache.Write<Operator>(cacheKeyOperator + operatorInfo.loginMark, operatorInfo, CacheId.loginInfo);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 登录者信息添加到缓存中
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="appId">应用id</param>
        /// <param name="loginMark">设备标识</param>
        /// <param name="cookie">是否保存cookie，默认是</param>
        /// <returns></returns>
        public string AddLoginUser(string account, string appId, string loginMark, bool cookie = true)
        {
            string token = Guid.NewGuid().ToString();
            try
            {
                // 填写登录信息
                Operator operatorInfo = new Operator();
                operatorInfo.appId = appId;
                operatorInfo.account = account;
                operatorInfo.logTime = DateTime.Now;
                operatorInfo.iPAddress = Net.Ip;
                operatorInfo.browser = Net.Browser;
                operatorInfo.token = token;
                if (cookie)
                {
                    string cookieMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                    if (string.IsNullOrEmpty(cookieMark))
                    {
                        operatorInfo.loginMark = Guid.NewGuid().ToString();
                        WebHelper.WriteCookie(LoginUserMarkKey, operatorInfo.loginMark);
                    }
                    else
                    {
                        operatorInfo.loginMark = cookieMark;
                    }
                    WebHelper.WriteCookie(LoginUserToken, token);
                }
                else
                {
                    operatorInfo.loginMark = loginMark;
                }
                Dictionary<string, string> tokenMarkList = redisCache.Read<Dictionary<string, string>>(cacheKeyToken + account, CacheId.loginInfo);
                if (tokenMarkList == null)// 此账号第一次登录
                {
                    tokenMarkList = new Dictionary<string, string>();
                    tokenMarkList.Add(operatorInfo.loginMark, token);
                }
                else
                {
                    if (tokenMarkList.ContainsKey(operatorInfo.loginMark))
                    {
                        tokenMarkList[operatorInfo.loginMark] = token;
                    }
                    else
                    {
                        tokenMarkList.Add(operatorInfo.loginMark, token);
                    }
                }
                redisCache.Write<Dictionary<string, string>>(cacheKeyToken + account, tokenMarkList, CacheId.loginInfo);
                redisCache.Write<Operator>(cacheKeyOperator + operatorInfo.loginMark, operatorInfo, CacheId.loginInfo);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 清空当前登录信息
        /// </summary>
        public void EmptyCurrent()
        {
            try
            {
                string token = WebHelper.GetCookie(LoginUserToken).ToString();
                string loginMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                EmptyCurrent(token, loginMark);
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 清空当前登录信息
        /// </summary>
        /// <param name="token">登录票据</param>
        /// <param name="loginMark">登录设备标识</param>
        public void EmptyCurrent(string token, string loginMark)
        {
            try
            {
                Operator operatorInfo = redisCache.Read<Operator>(cacheKeyOperator + loginMark, CacheId.loginInfo);
                if (operatorInfo != null && operatorInfo.token == token)
                {
                    Dictionary<string, string> tokenMarkList = redisCache.Read<Dictionary<string, string>>(cacheKeyToken + operatorInfo.account, CacheId.loginInfo);
                    tokenMarkList.Remove(loginMark);
                    redisCache.Remove(cacheKeyOperator + loginMark, CacheId.loginInfo);
                    redisCache.Write<Dictionary<string, string>>(cacheKeyToken + operatorInfo.account, tokenMarkList, CacheId.loginInfo);
                }
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 判断登录状态
        /// </summary>
        /// <returns>-1未登录,1登录成功,0登录过期</returns>
        public OperatorResult IsOnLine()
        {
            try
            {
                string token = WebHelper.GetCookie(LoginUserToken).ToString();
                string loginMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                return IsOnLine(token, loginMark);
            }
            catch (Exception)
            {
                return new OperatorResult { stateCode = -1 };
            }
        }
        /// <summary>
        /// 判断登录状态
        /// </summary>
        /// <param name="token">登录票据</param>
        /// <param name="loginMark">登录设备标识</param>
        /// <returns>-1未登录,1登录成功,0登录过期</returns>
        public OperatorResult IsOnLine(string token, string loginMark)
        {
            OperatorResult operatorResult = new OperatorResult();
            operatorResult.stateCode = -1; // -1未登录,1登录成功,0登录过期
            try
            {
                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(loginMark))
                {
                    return operatorResult;
                }
                Operator operatorInfo = redisCache.Read<Operator>(cacheKeyOperator + loginMark, CacheId.loginInfo);
                if (operatorInfo != null && operatorInfo.token == token)
                {
                    TimeSpan span = (TimeSpan)(DateTime.Now - operatorInfo.logTime);
                    if (span.TotalHours >= 12)// 登录操作过12小时移除
                    {
                        operatorResult.stateCode = 0;
                        Dictionary<string, string> tokenMarkList = redisCache.Read<Dictionary<string, string>>(cacheKeyToken + operatorInfo.account, CacheId.loginInfo);
                        tokenMarkList.Remove(loginMark);
                        redisCache.Write<Dictionary<string, string>>(cacheKeyToken + operatorInfo.account, tokenMarkList, CacheId.loginInfo);
                        redisCache.Remove(cacheKeyOperator + loginMark, CacheId.loginInfo);
                    }
                    else
                    {
                        UserInfo userInfo = new UserInfo();
                        userInfo.appId = operatorInfo.appId;
                        userInfo.logTime = operatorInfo.logTime;
                        userInfo.iPAddress = operatorInfo.iPAddress;
                        userInfo.browser = operatorInfo.browser;
                        userInfo.loginMark = operatorInfo.loginMark;
                        userInfo.token = operatorInfo.token;
                        userInfo.account = operatorInfo.account;

                        UserEntity userEntity = userIBLL.GetEntityByAccount(operatorInfo.account);
                        if (userEntity != null)
                        {
                            userInfo.userId = userEntity.F_UserId;
                            userInfo.enCode = userEntity.F_EnCode;
                            userInfo.password = userEntity.F_Password;
                            userInfo.secretkey = userEntity.F_Secretkey;
                            userInfo.realName = userEntity.F_RealName;
                            userInfo.nickName = userEntity.F_NickName;
                            userInfo.headIcon = userEntity.F_HeadIcon;
                            userInfo.gender = userEntity.F_Gender;
                            userInfo.mobile = userEntity.F_Mobile;
                            userInfo.telephone = userEntity.F_Telephone;
                            userInfo.email = userEntity.F_Email;
                            userInfo.oICQ = userEntity.F_OICQ;
                            userInfo.weChat = userEntity.F_WeChat;
                            userInfo.companyId = userEntity.F_CompanyId;
                            userInfo.departmentId = userEntity.F_DepartmentId;
                            userInfo.openId = userEntity.F_OpenId;
                            userInfo.isSystem = userEntity.F_SecurityLevel == 1 ? true : false;

                            userInfo.roleIds = userRelationIBLL.GetObjectIds(userEntity.F_UserId, 1);
                            userInfo.postIds = userRelationIBLL.GetObjectIds(userEntity.F_UserId, 2);

                            userInfo.companyIds = companyIBLL.GetSubNodes(userEntity.F_CompanyId);
                            userInfo.departmentIds = departmentIBLL.GetSubNodes(userEntity.F_CompanyId, userEntity.F_DepartmentId);

                            if (HttpContext.Current != null)
                            {
                                HttpContext.Current.Items.Add("LoginUserInfo", userInfo);
                            }
                            operatorResult.userInfo = userInfo;
                            operatorResult.stateCode = 1;
                        }
                        else
                        {
                            operatorResult.stateCode = 0;
                        }
                    }
                }
                return operatorResult;
            }
            catch (Exception)
            {
                return operatorResult;
            }
        }


        #region 登录错误次数记录
        /// <summary>
        /// 获取当前登录错误次数
        /// </summary>
        /// <returns></returns>
        public int GetCurrentErrorNum()
        {
            int res = 0;
            try
            {
                string cookieMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                if (string.IsNullOrEmpty(cookieMark))
                {
                    cookieMark = Guid.NewGuid().ToString();
                    WebHelper.WriteCookie(LoginUserMarkKey, cookieMark);
                }
                string num = redisCache.Read<string>(cacheKeyError + cookieMark, CacheId.loginInfo);
                if (!string.IsNullOrEmpty(num))
                {
                    res = Convert.ToInt32(num);
                }
            }
            catch (Exception)
            {
            }
            return res;
        }
        /// <summary>
        /// 增加错误次数
        /// </summary>
        /// <returns></returns>
        public int AddCurrentErrorNum()
        {
            int res = 0;
            try
            {
                string cookieMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                if (string.IsNullOrEmpty(cookieMark))
                {
                    cookieMark = Guid.NewGuid().ToString();
                    WebHelper.WriteCookie(LoginUserMarkKey, cookieMark);
                }
                string num = redisCache.Read<string>(cacheKeyError + cookieMark, CacheId.loginInfo);
                if (!string.IsNullOrEmpty(num))
                {
                    res = Convert.ToInt32(num);
                }
                res++;
                num = res + "";
                redisCache.Write<string>(cacheKeyError + cookieMark, num, CacheId.loginInfo);
            }
            catch (Exception)
            {
            }
            return res;
        }
        /// <summary>
        /// 清除当前登录错误次数
        /// </summary>
        public void ClearCurrentErrorNum()
        {
            try
            {
                string cookieMark = WebHelper.GetCookie(LoginUserMarkKey).ToString();
                if (string.IsNullOrEmpty(cookieMark))
                {
                    cookieMark = Guid.NewGuid().ToString();
                    WebHelper.WriteCookie(LoginUserMarkKey, cookieMark);
                }
                redisCache.Remove(cacheKeyError + cookieMark, CacheId.loginInfo);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 写入操作日志
        /// <summary>
        /// 写操作日志
        /// </summary>
        public void WriteOperateLog(OperateLogModel operateLogModel)
        {
            try
            {
                if (operateLogModel.userInfo == null)
                {
                    operateLogModel.userInfo = LoginUserInfo.Get();
                }
                LogEntity logEntity = new LogEntity();
                logEntity.F_CategoryId = 3;
                logEntity.F_OperateTypeId = ((int)operateLogModel.type).ToString();
                logEntity.F_OperateType = EnumAttribute.GetDescription(operateLogModel.type);
                logEntity.F_OperateAccount = operateLogModel.userInfo.account;
                logEntity.F_OperateUserId = operateLogModel.userInfo.userId;
                logEntity.F_Module = operateLogModel.title;
                logEntity.F_ExecuteResult = 1;
                logEntity.F_ExecuteResultJson = "访问地址：" + operateLogModel.url;
                logEntity.F_SourceObjectId = operateLogModel.sourceObjectId;
                logEntity.F_SourceContentJson = operateLogModel.sourceContentJson;
                logEntity.WriteLog();
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
