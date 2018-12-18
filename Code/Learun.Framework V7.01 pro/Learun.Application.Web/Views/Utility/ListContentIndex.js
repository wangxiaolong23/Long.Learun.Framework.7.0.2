/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.04.28
 * 描 述：桌面消息查看
 */
var id = request('id');
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            var item = top['dtlist' + id];
            $('.title p').text(item.f_title);
            $('.con').html($('<div></div>').html(item.f_content).text());
        }
    };
    page.init();
}