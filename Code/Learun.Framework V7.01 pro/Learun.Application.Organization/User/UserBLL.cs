using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Learun.Application.WeChat;

namespace Learun.Application.Organization
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.06
    /// 描 述：用户模块业务类
    /// </summary>
    public class UserBLL : UserIBLL
    {
        #region 属性
        private UserService userService = new UserService();
        private DepartmentIBLL departmentIBLL = new DepartmentBLL();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_user_";       // +公司主键
        private string cacheKeyAccount = "learun_adms_user_account_";// +用户账号（账号不允许改动）
        private string cacheKeyId = "learun_adms_user_Id_";// +用户账号（账号不允许改动）
        #endregion

        #region 获取数据
        /// <summary>
        /// 用户列表(根据公司主键)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public List<UserEntity> GetList(string companyId)
        {
            try
            {
                if (string.IsNullOrEmpty(companyId))
                {
                    return new List<UserEntity>();
                }

                List<UserEntity> list = cache.Read<List<UserEntity>>(cacheKey + companyId, CacheId.user);
                if (list == null)
                {
                    list = (List<UserEntity>)userService.GetList(companyId);
                    cache.Write<List<UserEntity>>(cacheKey + companyId, list, CacheId.user);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 用户列表(根据公司主键,部门主键)
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        public List<UserEntity> GetList(string companyId, string departmentId, string keyword)
        {
            try
            {
               

                List<UserEntity> list = GetList(companyId);
                if (!string.IsNullOrEmpty(departmentId))
                {
                    list = list.FindAll(t => t.F_DepartmentId.ContainsEx(departmentId));
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_RealName.ContainsEx(keyword) || t.F_Account.ContainsEx(keyword));
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 用户列表(全部)
        /// </summary>
        /// <returns></returns>
        public List<UserEntity> GetAllList()
        {
            try
            {
                return (List<UserEntity>)userService.GetAllList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 用户列表(根据部门主键)
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <returns></returns>
        public List<UserEntity> GetListByDepartmentId(string departmentId)
        {
            try
            {
                if (string.IsNullOrEmpty(departmentId))
                {
                    return new List<UserEntity>();
                }
                DepartmentEntity departmentEntity = departmentIBLL.GetEntity(departmentId);
                if (departmentEntity == null)
                {
                    return new List<UserEntity>();
                }
                return GetList(departmentEntity.F_CompanyId, departmentId,"");
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        public List<UserEntity> GetPageList(string companyId, string departmentId, Pagination pagination, string keyword)
        {
            try
            {
                return (List<UserEntity>)userService.GetPageList(companyId, departmentId, pagination, keyword);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 用户列表（导出Excel）
        /// </summary>
        /// <returns></returns>
        public void GetExportList()
        {
            try
            {
                //取出数据源
                DataTable exportTable = userService.GetExportList();
                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = "测试用户导出";
                excelconfig.TitleFont = "微软雅黑";
                excelconfig.TitlePoint = 25;
                excelconfig.FileName = "用户导出.xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                excelconfig.ColumnEntity = new List<ColumnModel>();
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_account", ExcelColumn = "账户" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_realname", ExcelColumn = "姓名" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_gender", ExcelColumn = "性别" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_birthday", ExcelColumn = "生日" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_mobile", ExcelColumn = "手机", Background = Color.Red });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_telephone", ExcelColumn = "电话", Background = Color.Red });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_wechat", ExcelColumn = "微信" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_organize", ExcelColumn = "公司" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_department", ExcelColumn = "部门" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_description", ExcelColumn = "说明" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_createdate", ExcelColumn = "创建日期" });
                excelconfig.ColumnEntity.Add(new ColumnModel() { Column = "f_createusername", ExcelColumn = "创建人" });
                //调用导出方法
                ExcelHelper.ExcelDownload(exportTable, excelconfig);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取实体,通过用户账号
        /// </summary>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public UserEntity GetEntityByAccount(string account)
        {
            try
            {
                string userId = cache.Read<string>(cacheKeyAccount + account, CacheId.user);
                UserEntity userEntity;
                if (string.IsNullOrEmpty(userId))
                {
                    userEntity = userService.GetEntityByAccount(account);
                    if (userEntity != null)
                    {
                        cache.Write<string>(cacheKeyAccount + userEntity.F_Account, userEntity.F_UserId, CacheId.user);
                        cache.Write<UserEntity>(cacheKeyId + userEntity.F_UserId, userEntity, CacheId.user);
                    }
                }
                else
                {
                    userEntity = cache.Read<UserEntity>(cacheKeyId + userId, CacheId.user);
                    if (userEntity == null)
                    {
                        userEntity = userService.GetEntityByAccount(account);
                        if (userEntity != null)
                        {
                            cache.Write<UserEntity>(cacheKeyId + userEntity.F_UserId, userEntity, CacheId.user);
                        }
                    }
                }
                return userEntity;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public UserEntity GetEntityByUserId(string userId)
        {
            try
            {
                UserEntity userEntity = cache.Read<UserEntity>(cacheKeyId + userId, CacheId.user);
                if (userEntity == null)
                {
                    userEntity = userService.GetEntity(userId);
                    if (userEntity != null)
                    {
                        cache.Write<string>(cacheKeyAccount + userEntity.F_Account, userId, CacheId.user);
                        cache.Write<UserEntity>(cacheKeyId + userId, userEntity, CacheId.user);
                    }
                }
                return userEntity;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取用户列表数据
        /// </summary>
        /// <param name="userIds">用户主键串</param>
        /// <returns></returns>
        public List<UserEntity> GetListByUserIds(string userIds)
        {
            try
            {
                if (string.IsNullOrEmpty(userIds))
                {
                    return null;
                }
                List<UserEntity> list = new List<UserEntity>();
                string[] userList = userIds.Split(',');
                foreach (string userId in userList)
                {
                    UserEntity userEntity = GetEntityByUserId(userId);
                    if (userEntity != null) {
                        list.Add(userEntity);
                    }
                }


                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取映射数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, UserModel> GetModelMap()
        {
            try
            {
                Dictionary<string, UserModel> dic = cache.Read<Dictionary<string, UserModel>>(cacheKey + "dic", CacheId.user);
                if (dic == null)
                {
                    dic = new Dictionary<string, UserModel>();
                    var list = userService.GetAllList();
                    foreach (var item in list)
                    {
                        UserModel model = new UserModel()
                        {
                            companyId = item.F_CompanyId,
                            departmentId = item.F_DepartmentId,
                            name = item.F_RealName,
                        };
                        string img = "";
                        if (!string.IsNullOrEmpty(item.F_HeadIcon))
                        {
                            string fileHeadImg = Config.GetValue("fileHeadImg");
                            string fileImg = string.Format("{0}/{1}{2}", fileHeadImg, item.F_UserId, item.F_HeadIcon);
                            if (DirFileHelper.IsExistFile(fileImg))
                            {
                                img = item.F_HeadIcon;
                            }
                        }
                        if (string.IsNullOrEmpty(img))
                        {
                            if (item.F_Gender == 0)
                            {
                                img = "0";
                            }
                            else
                            {
                                img = "1";
                            }
                        }
                        model.img = img;
                        dic.Add(item.F_UserId, model);
                        cache.Write(cacheKey + "dic", dic, CacheId.user);
                    }
                }
                return dic;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                UserEntity userEntity = GetEntityByUserId(keyValue);
                cache.Remove(cacheKey + userEntity.F_CompanyId, CacheId.user);
                cache.Remove(cacheKeyId + keyValue, CacheId.user);
                cache.Remove(cacheKeyAccount + userEntity.F_Account, CacheId.user);

                Dictionary<string, UserModel>dic = GetModelMap();
                dic.Remove(keyValue);
                cache.Write(cacheKey + "dic", dic, CacheId.department);


                userService.VirtualDelete(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, UserEntity userEntity)
        {
            try
            {
                cache.Remove(cacheKey + userEntity.F_CompanyId, CacheId.user);
                cache.Remove(cacheKeyId + keyValue, CacheId.user);
                cache.Remove(cacheKeyAccount + userEntity.F_Account, CacheId.user);
                cache.Remove(cacheKey +"dic", CacheId.user);


                if (!string.IsNullOrEmpty(keyValue))
                {
                    userEntity.F_Account = null;// 账号不允许改动
                }

                userService.SaveEntity(keyValue, userEntity);


            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="newPassword">新密码（MD5 小写）</param>
        /// <param name="oldPassword">旧密码（MD5 小写）</param>
        public bool RevisePassword(string newPassword, string oldPassword)
        {
            try
            {
                UserInfo userInfo = LoginUserInfo.Get();
                cache.Remove(cacheKeyId + userInfo.userId, CacheId.user);
                cache.Remove(cacheKeyAccount + userInfo.account, CacheId.user);

                string oldPasswordByEncrypt = Md5Helper.Encrypt(DESEncrypt.Encrypt(oldPassword, userInfo.secretkey).ToLower(), 32).ToLower();
                if (oldPasswordByEncrypt == userInfo.password)
                {
                    userService.RevisePassword(userInfo.userId, newPassword);
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 重置密码(000000)
        /// </summary>
        /// <param name="keyValue">账号主键</param>
        public void ResetPassword(string keyValue)
        {
            try
            {
                cache.Remove(cacheKeyId + keyValue, CacheId.user);
                string password = Md5Helper.Encrypt("000000", 32).ToLower();
                userService.RevisePassword(keyValue, password);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态：1-启动；0-禁用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                UserEntity userEntity = GetEntityByUserId(keyValue);
                cache.Remove(cacheKey + userEntity.F_CompanyId, CacheId.user);
                cache.Remove(cacheKeyId + keyValue, CacheId.user);
                cache.Remove(cacheKeyAccount + userEntity.F_Account, CacheId.user);
                cache.Remove(cacheKey + "dic", CacheId.user);
                userService.UpdateState(keyValue, state);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户值</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistAccount(string account, string keyValue)
        {
            try
            {
                return userService.ExistAccount(account, keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码 MD5 32位 小写</param>
        /// <returns></returns>
        public UserEntity CheckLogin(string account, string password)
        {
            ////调用微信开放平台接口获得Token、OpenId
            //string appid = Config.GetValue("AppId");
            //string appsecret = Config.GetValue("AppSecret");
            //OpenTokenGet openTokenGet = new OpenTokenGet();
            //openTokenGet.appid = appid;
            //openTokenGet.secret = appsecret;
            //openTokenGet.code = "0815LTNN0EEei42rURNN0z5QNN05LTNS";
            //OpenTokenGetResult openInfo = openTokenGet.OpenSend();
            //string openid = openInfo.openid;
            //string token = openInfo.access_token;
            ////调用微信开放平台接口获得登录用户个人信息
            //OpenUserGet openuser = new OpenUserGet();
            //openuser.openid = openid;
            //openuser.access_token = token;
            //OpenUserGetResult userinfo = openuser.OpenSend();
            try
            {
                UserEntity userEntity = GetEntityByAccount(account);
                if (userEntity == null)
                {
                    userEntity = new UserEntity()
                    {
                        LoginMsg = "账户不存在!",
                        LoginOk = false
                    };
                    return userEntity;
                }
                userEntity.LoginOk = false;
                if (userEntity.F_EnabledMark == 1)
                {
                    string dbPassword = Md5Helper.Encrypt(DESEncrypt.Encrypt(password.ToLower(), userEntity.F_Secretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userEntity.F_Password)
                    {
                        userEntity.LoginOk = true;
                    }
                    else
                    {
                        userEntity.LoginMsg = "密码和账户名不匹配!";
                    }
                }
                else
                {
                    userEntity.LoginMsg = "账户被系统锁定,请联系管理员!";
                }
                return userEntity;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="userId">用户ID</param>
        public void GetImg(string userId)
        {
            UserEntity entity = GetEntityByUserId(userId);
            string img = "";
            string fileHeadImg = Config.GetValue("fileHeadImg");
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.F_HeadIcon)) {
                    string fileImg = string.Format("{0}/{1}{2}", fileHeadImg, entity.F_UserId, entity.F_HeadIcon);
                    if (DirFileHelper.IsExistFile(fileImg)) {
                        img = fileImg;
                    }
                }
            }
            else {
                img  = string.Format("{0}/{1}", fileHeadImg, "on-boy.jpg");
            }
            if (string.IsNullOrEmpty(img))
            {
                if (entity.F_Gender == 0)
                {
                    img = string.Format("{0}/{1}", fileHeadImg, "on-girl.jpg");
                }
                else {
                    img = string.Format("{0}/{1}", fileHeadImg, "on-boy.jpg");
                }
            }
            FileDownHelper.DownLoadnew(img);
        }
        #endregion
    }
}
