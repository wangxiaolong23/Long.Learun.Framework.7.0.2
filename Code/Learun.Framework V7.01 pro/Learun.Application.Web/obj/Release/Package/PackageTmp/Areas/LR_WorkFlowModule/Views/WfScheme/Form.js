/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：工作流模板设计	
 */
var keyValue = request('keyValue');
var categoryId = request('categoryId');
var shcemeCode = request('shcemeCode');

var currentNode; // 当前设置节点
var currentLine; // 当前设置线条
var schemeAuthorizes = []; // 模板权限人员
var authorizeType = 1;// 模板权限类型

var bootstrap = function ($, learun) {
    "use strict";

    function isRepeat(id) {
        var res = false;
        for (var i = 0, l = schemeAuthorizes.length; i < l; i++) {
            if (schemeAuthorizes[i].F_ObjectId == id) {
                learun.alert.warning('重复添加信息');
                res = true;
                break;
            }
        }
        return res;
    }

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (data.step == 1) {
                        if (!$('#step-1').lrValidform()) {
                            return false;
                        }
                    }
                    else if (data.step == 2)
                    {
                        if (authorizeType != 1) {
                            if (schemeAuthorizes.length == 0) {
                                learun.alert.error('请添加权限人员信息');
                                return false;
                            }
                        }
                        $finish.removeAttr('disabled');
                        $next.attr('disabled', 'disabled');
                    }
                    else {
                        
                        $finish.attr('disabled', 'disabled');
                    }
                } else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });
            $('#F_Category').lrDataItemSelect({ code: 'FlowSort' });
            $('#F_Category').lrselectSet(categoryId);

            // 权限设置
            $('[name="authorizeType"]').on('click', function () {
                var $this = $(this);
                var value = $this.val();
                authorizeType = value;
                if (value == '1') {
                    $('#shcemeAuthorizeBg').show();
                }
                else {
                    $('#shcemeAuthorizeBg').hide();
                }
            });
            $('#authorize_girdtable').jfGrid({
                headData: [
                    {
                        label: "类型", name: "F_ObjectType", width: 100, align: "center",
                        formatter: function (cellvalue) {//审核者类型1.岗位2.角色3.用户
                            switch (parseInt(cellvalue)) {
                                case 1:
                                    return '岗位';
                                    break;
                                case 2:
                                    return '角色';
                                    break;
                                case 3:
                                    return '用户';
                                    break;
                            }
                        }
                    },
                    { label: "名称", name: "F_ObjectName", width: 700, align: "left" }
                ]
            });
            // 岗位添加
            $('#lr_post_authorize').on('click', function () {
                learun.layerForm({
                    id: 'AuthorizePostForm',
                    title: '添加岗位',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/PostForm?flag=1',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                var _data = {};
                                _data.F_Id = learun.newGuid();
                                _data.F_ObjectName = data.auditorName;
                                _data.F_ObjectId = data.auditorId;
                                _data.F_ObjectType = data.type;
                                schemeAuthorizes.push(_data);
                                $('#authorize_girdtable').jfGridSet('refreshdata', { rowdatas: schemeAuthorizes });
                            }
                        });
                    }
                });
            });
            // 角色添加
            $('#lr_role_authorize').on('click', function () {
                learun.layerForm({
                    id: 'AuthorizeRoleForm',
                    title: '添加角色',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/RoleForm?flag=1',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                var _data = {};
                                _data.F_Id = learun.newGuid();
                                _data.F_ObjectName = data.auditorName;
                                _data.F_ObjectId = data.auditorId;
                                _data.F_ObjectType = data.type;
                                schemeAuthorizes.push(_data);
                                $('#authorize_girdtable').jfGridSet('refreshdata', { rowdatas: schemeAuthorizes });
                            }
                        });
                    }
                });
            });
            // 人员添加
            $('#lr_user_authorize').on('click', function () {
                learun.layerForm({
                    id: 'AuthorizeUserForm',
                    title: '添加人员',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/UserForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                var _data = {};
                                _data.F_Id = learun.newGuid();
                                _data.F_ObjectName = data.auditorName;
                                _data.F_ObjectId = data.auditorId;
                                _data.F_ObjectType = data.type;
                                schemeAuthorizes.push(_data);
                                $('#authorize_girdtable').jfGridSet('refreshdata', { rowdatas: schemeAuthorizes });
                            }
                        });
                    }
                });
            });
            // 人员移除
            $('#lr_delete_authorize').on('click', function () {
                var _id = $('#authorize_girdtable').jfGridValue('F_Id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该项！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = schemeAuthorizes.length; i < l; i++) {
                                if (schemeAuthorizes[i].F_Id == _id) {
                                    schemeAuthorizes.splice(i, 1);
                                    $('#authorize_girdtable').jfGridSet('refreshdata', { rowdatas: schemeAuthorizes });
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });


            // 设计页面初始化
            $('#step-3').lrworkflow({
                openNode: function (node) {
                    currentNode = node;
                    if (node.type != 'endround') {
                        learun.layerForm({
                            id: 'NodeForm',
                            title: '节点信息设置【' + node.name + '】',
                            url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/NodeForm?layerId=layer_Form',
                            width: 700,
                            height: 500,
                            callBack: function (id) {
                                return top[id].acceptClick(function () {
                                    $('#step-3').lrworkflowSet('updateNodeName', { nodeId: currentNode.id });
                                });
                            }
                        });
                    }
                },
                openLine: function (line) {
                    currentLine = line;
                    learun.layerForm({
                        id: 'LineForm',
                        title: '线条信息设置',
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/LineForm?layerId=layer_Form',
                        width: 400,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(function () {
                                $('#step-3').lrworkflowSet('updateLineName', { lineId: currentLine.id });
                            });
                        }
                    });
                }
            });
            // 保存草稿
            $("#btn_draft").on('click', page.draftsave);
            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        /*初始化数据*/
        initData: function () {
            if (!!shcemeCode) {
                $.lrSetForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/GetFormData?schemeCode=' + shcemeCode, function (data) {//
                    $('#step-1').lrSetFormData(data.schemeInfoEntity);
                    var shceme = JSON.parse(data.schemeEntity.F_Scheme);
                    $('#step-3').lrworkflowSet('set', { data: shceme });

                    if (data.wfSchemeAuthorizeList.length > 0 && data.wfSchemeAuthorizeList[0].F_ObjectType != 4) {
                        $('#authorizeType2').trigger('click');
                        schemeAuthorizes = data.wfSchemeAuthorizeList;
                        $('#authorize_girdtable').jfGridSet('refreshdata', { rowdatas: schemeAuthorizes });
                        authorizeType = 2;
                    }
                });
            }
        },
        /*保存草稿*/
        draftsave: function () {
            var formdata = $('#step-1').lrGetFormData(keyValue);
            var shcemeData = $('#step-3').lrworkflowGet();

            if (authorizeType == 1) {
                schemeAuthorizes = [];
            }

            var postData = {
                schemeInfo: JSON.stringify(formdata),
                scheme: JSON.stringify(shcemeData),
                shcemeAuthorize: JSON.stringify(schemeAuthorizes),
                type: 2
            };

            $.lrSaveForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调
                learun.frameTab.currentIframe().refreshGirdData(formdata);
            });
        },
        /*保存数据*/
        save: function () {
            if (!$('#step-1').lrValidform()) {
                return false;
            }
            var formdata = $('#step-1').lrGetFormData(keyValue);
            var shcemeData = $('#step-3').lrworkflowGet();

            if (authorizeType == 1) {
                schemeAuthorizes = [];
                schemeAuthorizes.push({
                    F_Id: learun.newGuid(),
                    F_ObjectType: 4
                });
            }
            var postData = {
                schemeInfo: JSON.stringify(formdata),
                scheme: JSON.stringify(shcemeData),
                shcemeAuthorize: JSON.stringify(schemeAuthorizes),
                type: 1
            };

            $.lrSaveForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调
                learun.frameTab.currentIframe().refreshGirdData(formdata);
            });
        }
    };

    page.init();
}