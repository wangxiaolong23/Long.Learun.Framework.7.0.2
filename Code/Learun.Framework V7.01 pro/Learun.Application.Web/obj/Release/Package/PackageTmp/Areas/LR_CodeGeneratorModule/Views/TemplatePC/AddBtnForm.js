/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.05.11
 * 描 述：添加扩展按钮	
 */
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();
        if (!!callBack) {
            callBack(formData);
        }

        return true;
    };
}