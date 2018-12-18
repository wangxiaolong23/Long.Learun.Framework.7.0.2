/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：公司管理	
 */

var acceptClick;
var keyValue = '';
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 商机类别
            $('#F_ChanceTypeId').lrDataItemSelect({ code: 'Client_ChanceSort', maxHeight: 230 });
            // 商机来源
            $('#F_SourceId').lrDataItemSelect({ code: 'Client_ChanceSource', maxHeight: 230 });
            // 商机阶段
            $('#F_StageId').lrDataItemSelect({ code: 'Client_ChancePhase', maxHeight: 230 });
            //跟进人员
            $('#F_TraceUserId').lrformselect({
                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectForm',
                layerUrlW: 800,
                layerUrlH: 520,
                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'
            });
            //公司行业
            $('#F_CompanyNatureId').lrDataItemSelect({ code: 'Client_Trade', maxHeight: 230 });


        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_ChanceId;
                $('#form').lrSetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        $.lrSaveForm(top.$.rootUrl + '/LR_CRMModule/Chance/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}