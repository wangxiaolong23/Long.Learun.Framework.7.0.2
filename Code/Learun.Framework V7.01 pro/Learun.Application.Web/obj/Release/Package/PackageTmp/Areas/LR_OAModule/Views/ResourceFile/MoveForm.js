/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.06.20
 * 描 述：文件管理	
 */
var keyValue = request('keyValue');
var fileType = request('fileType');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var moveFolderId = "";
    var page = {
        init: function () {
            page.GetTree();
        },
        //初始化控件
        GetTree: function () {
            $('#ItemsTree').lrtree({
                url: top.$.rootUrl + "/LR_OAModule/ResourceFile/GetTreeJson",
                nodeClick: function (item) {
                    moveFolderId = item.id;
                }
            });
            $("#ItemsTree_" + keyValue.replace(/-/g, '_')).parent('li').remove();
        }
    };
    //保存表单
    acceptClick = function (callBack) {
        if (moveFolderId == "") {
            learun.alert.error('请选择要移动到的位置');
            return false;
        }
        var postData = $("#form").lrGetFormData(keyValue);
        postData["keyValue"] = keyValue;
        postData["moveFolderId"] = moveFolderId;
        postData["fileType"] = fileType;
        $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/SaveMoveForm', postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });


    }
    page.init();
}