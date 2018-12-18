using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WebChatModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：企业号设置
    /// </summary>
    public class TokenController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 企业号管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.CorpId = Config.GetValue("CorpId");
            ViewBag.CorpSecret = Config.GetValue("CorpSecret");
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="CorpId">企业号CorpID</param>
        /// <param name="CorpSecret">管理组凭证密钥</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string CorpId, string CorpSecret)
        {
            Config.SetValue("CorpId", CorpId);
            Config.SetValue("CorpSecret", CorpSecret);
            return Success("操作成功。");
        }
        #endregion
    }
}