using Learun.Application.Base.SystemModule;
using Learun.Application.Organization;
using Learun.Util;
using Learun.Util.Operat;
using Nancy;

namespace Learun.Application.WebApi
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.05.12
    /// 描 述：用户信息
    /// </summary>
    public class UserApi : BaseApi
    {
        /// <summary>
        /// 注册接口
        /// </summary>
        public UserApi()
            : base("/learun/adms/user")
        {
            Post["/login"] = Login;
            Post["/modifypw"] = ModifyPassword;

            Get["/info"] = Info;
            Get["/map"] = GetMap;
            Get["/img"] = GetImg;
        }
        private UserIBLL userIBLL = new UserBLL();
        private PostIBLL postIBLL = new PostBLL();
        private RoleIBLL roleIBLL = new RoleBLL();




        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response Login(dynamic _)
        {
            LoginModel loginModel = this.GetReqData<LoginModel>();

            #region 内部账户验证
            UserEntity userEntity = userIBLL.CheckLogin(loginModel.username, loginModel.password);

            #region 写入日志
            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 1;
            logEntity.F_OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.F_OperateAccount = loginModel.username + "(" + userEntity.F_RealName + ")";
            logEntity.F_OperateUserId = !string.IsNullOrEmpty(userEntity.F_UserId) ? userEntity.F_UserId : loginModel.username;
            logEntity.F_Module = Config.GetValue("SoftName");
            #endregion

            if (!userEntity.LoginOk)//登录失败
            {
                //写入日志
                logEntity.F_ExecuteResult = 0;
                logEntity.F_ExecuteResultJson = "登录失败:" + userEntity.LoginMsg;
                logEntity.WriteLog();
                return Fail(userEntity.LoginMsg);
            }
            else
            {
                string token = OperatorHelper.Instance.AddLoginUser(userEntity.F_Account, "Learun_ADMS_6.1_App", this.loginMark, false);//写入缓存信息
                //写入日志
                logEntity.F_ExecuteResult = 1;
                logEntity.F_ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                OperatorResult res = OperatorHelper.Instance.IsOnLine(token, this.loginMark);
                res.userInfo.password = null;
                res.userInfo.secretkey = null;

                var jsonData = new
                {
                    baseinfo = res.userInfo,
                    post = postIBLL.GetListByPostIds(res.userInfo.postIds),
                    role = roleIBLL.GetListByRoleIds(res.userInfo.roleIds)
                };
                return Success(jsonData);
            }
            #endregion
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns> 
        private Response Info(dynamic _)
        {
            var data = userInfo;
            data.password = null;
            data.secretkey = null;

            var jsonData = new
            {
                baseinfo = data,
                post = postIBLL.GetListByPostIds(data.postIds),
                role = roleIBLL.GetListByRoleIds(data.roleIds)
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response ModifyPassword(dynamic _)
        {
            ModifyModel modifyModel = this.GetReqData<ModifyModel>();
            if (userInfo.isSystem)
            {
                return Fail("当前账户不能修改密码");
            }
            else
            {
                bool res = userIBLL.RevisePassword(modifyModel.newpassword, modifyModel.oldpassword);
                if (!res)
                {
                    return Fail("原密码错误，请重新输入");
                }
                else
                {
                    return Success("密码修改成功");
                }
            }
        }


        /// <summary>
        /// 获取所有员工账号列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetList(dynamic _)
        {
            var data = userInfo;
            data.password = null;
            data.secretkey = null;
            var jsonData = new
            {
                baseinfo = data,
                post = postIBLL.GetListByPostIds(data.postIds),
                role = roleIBLL.GetListByRoleIds(data.roleIds)
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取用户映射表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetMap(dynamic _)
        {
            string ver = this.GetReqData();// 获取模板请求数据
            var data = userIBLL.GetModelMap();
            string md5 = Md5Helper.Encrypt(data.ToJson(), 32);
            if (md5 == ver)
            {
                return Success("no update");
            }
            else
            {
                var jsondata = new
                {
                    data = data,
                    ver = md5
                };
                return Success(jsondata);
            }
        }
        /// <summary>
        /// 获取人员头像图标
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetImg(dynamic _)
        {
            string userId = this.GetReqData();// 获取模板请求数据
            userIBLL.GetImg(userId);
            return Success("获取成功");
        }
    }

    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
    /// <summary>
    /// 修改密码
    /// </summary>
    public class ModifyModel
    {
        /// <summary>
        /// 新密码
        /// </summary>
        public string newpassword { get; set; }
        /// <summary>
        /// 旧密码
        /// </summary>
        public string oldpassword { get; set; }
    }
}