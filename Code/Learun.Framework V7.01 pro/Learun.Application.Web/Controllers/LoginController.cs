using Learun.Application.Organization;
using Learun.Application.Base.SystemModule;
using Learun.Util;
using Learun.Util.Operat;
using System.Web.Mvc;

namespace Learun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：登录控制器
    /// </summary>
    [HandlerLogin(FilterMode.Ignore)]
    public class LoginController : MvcControllerBase
    {
        #region 模块对象
        private UserIBLL userBll = new UserBLL();
        #endregion

        #region 视图功能
        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Default()
        {
            return RedirectToAction("Index", "Login");
        }
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.errornum = OperatorHelper.Instance.GetCurrentErrorNum();
            string learn_UItheme = WebHelper.GetCookie("Learn_ADMS_V6.1_UItheme");
            switch (learn_UItheme)
            {
                case "1":
                    return View("Default");      // 经典版本
                case "2":
                    return View("Accordion");    // 手风琴版本
                case "3":
                    return View("Window");       // Windos版本
                case "4":
                    return View("Top");          // 顶部菜单版本
                default:
                    return View("Default");      // 经典版本
            }
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        [HandlerLogin(FilterMode.Enforce)]
        public ActionResult GetUserInfo()
        {
            var data = LoginUserInfo.Get();
            data.imUrl = Config.GetValue("IMUrl");
            data.password = null;
            data.secretkey = null;
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerLogin(FilterMode.Enforce)]
        public ActionResult OutLogin()
        {
            var userInfo = LoginUserInfo.Get();
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 1;
            logEntity.F_OperateTypeId = ((int)OperationType.Exit).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exit);
            logEntity.F_OperateAccount = userInfo.account + "(" + userInfo.realName + ")";
            logEntity.F_OperateUserId = userInfo.userId;
            logEntity.F_ExecuteResult = 1;
            logEntity.F_ExecuteResultJson = "退出系统";
            logEntity.F_Module = Config.GetValue("SoftName");
            logEntity.WriteLog();
            Session.Abandon();                                          //清除当前会话
            Session.Clear();                                            //清除当前浏览器所有Session
            OperatorHelper.Instance.EmptyCurrent();
            return Success("退出系统");
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerValidateAntiForgeryToken]
        public ActionResult CheckLogin(string username, string password, string verifycode)
        {

            int error = OperatorHelper.Instance.GetCurrentErrorNum();
            if (error >= 3)
            {
                #region 验证码验证
                verifycode = Md5Helper.Encrypt(verifycode.ToLower(), 16);
                if (Session["session_verifycode"].IsEmpty() || verifycode != Session["session_verifycode"].ToString())
                {
                    return Fail("验证码错误");
                }
                #endregion
            }
            
            #region 内部账户验证
            UserEntity userEntity = userBll.CheckLogin(username, password);

            #region 写入日志
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 1;
            logEntity.F_OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.F_OperateAccount = username + "(" + userEntity.F_RealName + ")";
            logEntity.F_OperateUserId = !string.IsNullOrEmpty(userEntity.F_UserId) ? userEntity.F_UserId : username;
            logEntity.F_Module = Config.GetValue("SoftName");
            #endregion

            if (!userEntity.LoginOk)//登录失败
            {
                //写入日志
                logEntity.F_ExecuteResult = 0;
                logEntity.F_ExecuteResultJson = "登录失败:" + userEntity.LoginMsg;
                logEntity.WriteLog();
                int num = OperatorHelper.Instance.AddCurrentErrorNum();
                return Fail(userEntity.LoginMsg, num);
            }
            else
            {
                OperatorHelper.Instance.AddLoginUser(userEntity.F_Account, "Learun_ADMS_6.1_PC", null);//写入缓存信息
                //写入日志
                logEntity.F_ExecuteResult = 1;
                logEntity.F_ExecuteResultJson = "登录成功";
                logEntity.WriteLog();
                OperatorHelper.Instance.ClearCurrentErrorNum();
                return Success("登录成功");
            }
            #endregion
        }
        #endregion
    }
}