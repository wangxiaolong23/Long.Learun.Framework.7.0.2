using Learun.Application.Organization;
using Learun.Util;
using Learun.Util.Operat;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：个人中心
    /// </summary>
    public class UserCenterController : MvcControllerBase
    {
        private UserIBLL userIBLL = new UserBLL();
        private PostIBLL postIBLL = new PostBLL();
        private RoleIBLL roleIBLL = new RoleBLL();


        #region 视图功能
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 联系方式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HeadForm()
        {
            return View();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PassWordForm()
        {
            return View();
        }
        /// <summary>
        /// 个人中心-日志管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogIndex()
        {
            return View();
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
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetUserInfo()
        {
            var data = LoginUserInfo.Get();
            data.password = null;
            data.secretkey = null;

            var jsonData = new {
                baseinfo = data,
                post = postIBLL.GetListByPostIds(data.postIds),
                role = roleIBLL.GetListByRoleIds(data.roleIds)
            };

            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            UserInfo userInfo = LoginUserInfo.Get();

            string FileEextension = Path.GetExtension(files[0].FileName);
            string fileHeadImg =  Config.GetValue("fileHeadImg");
            string fullFileName = string.Format("{0}/{1}{2}", fileHeadImg, userInfo.userId, FileEextension);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);

            UserEntity userEntity = new UserEntity();
            userEntity.F_UserId = userInfo.userId;
            userEntity.F_Account = userInfo.account;
            userEntity.F_HeadIcon = FileEextension;
            userIBLL.SaveEntity(userEntity.F_UserId, userEntity);
            return Success("上传成功。");
        }
        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ValidationOldPassword(string OldPassword)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            OldPassword = Md5Helper.Encrypt(DESEncrypt.Encrypt(OldPassword, userInfo.secretkey).ToLower(), 32).ToLower();
            if (OldPassword != userInfo.password)
            {
                return Fail("原密码错误，请重新输入");
            }
            else
            {
                return Success("通过信息验证");
            }
        }
        /// <summary>
        /// 提交修改密码
        /// </summary>
        /// <param name="password">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="verifyCode">验证码</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitResetPassword(string password, string oldPassword, string verifyCode)
        {
            verifyCode = Md5Helper.Encrypt(verifyCode.ToLower(), 16);
            if (Session["session_verifycode"].IsEmpty() || verifyCode != Session["session_verifycode"].ToString())
            {
                return Fail("验证码错误，请重新输入");
            }
            UserInfo userInfo = LoginUserInfo.Get();

            if (userInfo.isSystem)
            {
                return Fail("当前账户不能修改密码");
            }
            bool res = userIBLL.RevisePassword(password, oldPassword);
            if (!res)
            {
                return Fail("原密码错误，请重新输入");
            }
            Session.Abandon();
            Session.Clear();
            OperatorHelper.Instance.EmptyCurrent();
            return Success("密码修改成功，请牢记新密码。\r 将会自动安全退出。");
        }
        #endregion
    }
}