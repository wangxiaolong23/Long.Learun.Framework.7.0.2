/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：功能模块	
 */
var objectId = request('objectId');
var objectType = request('objectType');

var bootstrap = function ($, learun) {
    "use strict";

    var selectData;

    var treeData;
    var checkModuleIds = [];

    function setTreeData1() {
        if (!!selectData) {
            $('#step-1').lrtreeSet('setCheck', selectData.modules);
        }
        else {
            setTimeout(setTreeData1,100);
        }
    }
    function setTreeData2() {
        if (!!selectData) {
            $('#step-2').lrtreeSet('setCheck', selectData.buttons);
        }
        else {
            setTimeout(setTreeData2, 100);
        }
    }
    function setTreeData3() {
        if (!!selectData) {
            $('#step-3').lrtreeSet('setCheck', selectData.columns);
        }
        else {
            setTimeout(setTreeData3, 100);
        }
    }
    function setTreeData4() {
        if (!!selectData) {
            $('#step-4').lrtreeSet('setCheck', selectData.forms);
        }
        else {
            setTimeout(setTreeData4, 100);
        }
    }

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/Module/GetCheckTree', function (res) {
                if (res.code == 200) {
                    treeData = res.data;
                    setTimeout(function () {
                        $('#step-1').lrtree({
                            data: treeData.moduleList
                        });
                        if (!!objectId) {
                            setTreeData1();
                        }
                    }, 10);
                    setTimeout(function () {
                        $('#step-2').lrtree({
                            data: treeData.buttonList
                        });
                        if (!!objectId) {
                            setTreeData2();
                        }
                    }, 50);
                    setTimeout(function () {
                        $('#step-3').lrtree({
                            data: treeData.columnList
                        });
                        if (!!objectId) {
                            setTreeData3();
                        }
                    }, 90);
                    setTimeout(function () {
                        $('#step-4').lrtree({
                            data: treeData.formList
                        });
                        if (!!objectId) {
                            setTreeData4();
                        }
                    }, 200);
                }
            });
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (data.step == 1) {
                        checkModuleIds = $('#step-1').lrtreeSet('getCheckNodeIds');
                        $('#step-2 .lr-tree-root [id$="_learun_moduleId"]').parent().hide();
                        $('#step-3 .lr-tree-root [id$="_learun_moduleId"]').parent().hide();
                        $('#step-4 .lr-tree-root [id$="_learun_moduleId"]').parent().hide();
                        $.each(checkModuleIds, function (id, item) {
                            $('#step-2_' + item.replace(/-/g, '_') + '_learun_moduleId').parent().show();
                            $('#step-3_' + item.replace(/-/g, '_') + '_learun_moduleId').parent().show();
                            $('#step-4_' + item.replace(/-/g, '_') + '_learun_moduleId').parent().show();
                        });
                    } else if (data.step == 3) {
                       
                        $finish.removeAttr('disabled');
                        $next.attr('disabled', 'disabled');
                    } else {
                        $finish.attr('disabled', 'disabled');
                    }
                } else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });
            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        /*初始化数据*/
        initData: function () {
            if (!!objectId) {
                $.lrSetForm(top.$.rootUrl + '/LR_AuthorizeModule/Authorize/GetFormData?objectId=' + objectId, function (data) {//
                    selectData = data;
                });
            }
        },
        /*保存数据*/
        save: function () {
            var buttonList = [], columnList = [],formList = [];
            var checkButtonIds = $('#step-2').lrtreeSet('getCheckNodeIds');
            var checkColumnIds = $('#step-3').lrtreeSet('getCheckNodeIds');
            var checkFormIds = $('#step-4').lrtreeSet('getCheckNodeIds');


            $.each(checkButtonIds, function (id, item) {
                if (item.indexOf('_learun_moduleId') == -1) {
                    buttonList.push(item);
                }
            });
            $.each(checkColumnIds, function (id, item) {
                if (item.indexOf('_learun_moduleId') == -1) {
                    columnList.push(item);
                }
            });
            $.each(checkFormIds, function (id, item) {
                if (item.indexOf('_learun_moduleId') == -1) {
                    formList.push(item);
                }
            });


            var postData = {
                objectId: objectId,
                objectType: objectType,
                strModuleId: String(checkModuleIds),
                strModuleButtonId: String(buttonList),
                strModuleColumnId: String(columnList),
                strModuleFormId: String(formList)
            };

            $.lrSaveForm(top.$.rootUrl + '/LR_AuthorizeModule/Authorize/SaveForm', postData, function (res) {});
        }
    };

    page.init();
}