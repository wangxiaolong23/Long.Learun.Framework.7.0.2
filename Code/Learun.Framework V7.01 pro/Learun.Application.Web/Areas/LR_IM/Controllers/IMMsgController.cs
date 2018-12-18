using Learun.Application.IM;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_IM.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-05-24 10:00
    /// 描 述：即时通讯
    /// </summary>
    public class IMMsgController : MvcControllerBase
    {
        private IMMsgIBLL iMMsgIBLL = new IMMsgBLL();
        private IMContactsIBLL iMContactsIBLL = new IMContactsBLL();

        #region 视图功能
        /// <summary>
        /// 聊天记录
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion


        #region 获取数据
        /// <summary>
        /// 获取列表数据(消息的最近10条数据)
        /// <summary>
        /// <param name="userId">对方用户ID</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMsgList(string userId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            var data = iMMsgIBLL.GetList(userInfo.userId, userId);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据(消息列表)
        /// <summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">对方用户ID</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMsgPageList(string pagination,string userId, string keyWord)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            UserInfo userInfo = LoginUserInfo.Get();
            var data = iMMsgIBLL.GetPageList(paginationobj, userInfo.userId, userId, keyWord);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }


        /// <summary>
        /// 获取最近联系人列表
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetContactsList()
        {
            UserInfo userInfo = LoginUserInfo.Get();
            var data = iMContactsIBLL.GetList(userInfo.userId);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="userId">接收方用户id</param>
        /// <param name="content">消息内容</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SendMsg(string userId,string content)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            IMMsgEntity entity = new IMMsgEntity();
            entity.F_SendUserId = userInfo.userId;
            entity.F_RecvUserId = userId;
            entity.F_Content = content;
            iMMsgIBLL.SaveEntity(entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 添加一条最近的联系人
        /// </summary>
        /// <param name="otherUserId">对方用户Id</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AddContact(string otherUserId)
        {
            IMContactsEntity entity = new IMContactsEntity();
            UserInfo userInfo = LoginUserInfo.Get();
            entity.F_MyUserId = userInfo.userId;
            entity.F_OtherUserId = otherUserId;
            iMContactsIBLL.SaveEntity(entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 更新联系人消息读取状态
        /// </summary>
        /// <param name="otherUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateContactState(string otherUserId)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            iMContactsIBLL.UpdateState(userInfo.userId,otherUserId);
            return Success("保存成功！");
        }
        #endregion
    }
}