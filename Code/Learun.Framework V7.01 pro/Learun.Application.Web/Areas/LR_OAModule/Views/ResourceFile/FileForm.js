/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.06.20
 * 描 述：文件管理	
 */
var keyValue = request('keyValue');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var Extension = "";
    var page = {
        init: function () {
            page.initControl();
        },
        //初始化控件
        initControl: function () {
            console.log(keyValue);
            //获取表单
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + "/LR_OAModule/ResourceFile/GetFileFormJson?keyValue=" + keyValue, function (data) {//
                    console.log(data);
                    $('#form').lrSetFormData(data);
                    Extension = data.F_FileExtensions;
                    var FileName = data.F_FileName.replace(Extension, '');
                    $("#FileName").val(F_FileName).focus().select();
                });
            }
        }
    };
    //保存表单
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $("#form").lrGetFormData(keyValue);
        postData["keyValue"] = keyValue;
        postData["F_FileName"] = $("#F_FileName").val() + Extension;
        $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/SaveFileForm', postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    }
    page.init();
}


