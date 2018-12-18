/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：节点设置	
 */
var layerId = request('layerId');
var isPreview = request('isPreview');

var acceptClick;

var auditors = [];
var authorize = [];
var conditions = [];
var workforms = [];

var bootstrap = function ($, learun) {
    "use strict";
    var currentNode = top[layerId].currentNode;
    var formcomponts = {};

    // 加载表单数据
    function loadformcomponts(formId,formName, type) {// 0 添加 1删除
        if (!!formId) {
            if (type == 0) {
                if (!!formcomponts[formId]) {
                    for (i = 0, l = formcomponts[formId].length; i < l; i++) {
                        authorize.push(formcomponts[formId][i]);
                    }
                    $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                }
                else {
                    formcomponts[formId] = [];
                    $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + formId, function (data) {
                        var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                        for (var i = 0, l = scheme.data.length; i < l; i++) {
                            var componts = scheme.data[i].componts;
                            for (var j = 0, jl = componts.length; j < jl; j++) {
                                var compont = componts[j];
                                if (compont.type == 'gridtable') {
                                    $.each(compont.fieldsData, function (_i, _item) {
                                        if (_item.type != 'guid')
                                        {
                                            var point = { id: learun.newGuid(), formId: formId, formName: formName, fieldName: compont.title + '-' + _item.name, fieldId: compont.id + '|' + _item.id, isLook: '1', isEdit: '1' };
                                            formcomponts[formId].push(point);
                                            authorize.push(point);
                                        }

                                    });
                                }
                                else
                                {
                                    var point = { id: learun.newGuid(), formId: formId, formName: formName, fieldName: compont.title, fieldId: compont.id, isLook: '1', isEdit: '1' };
                                    formcomponts[formId].push(point);
                                    authorize.push(point);
                                }
                            }
                        }
                        $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                    });
                }
            }
            else {
                var _tmpdata = [];
                for (var i = 0, l = authorize.length; i < l; i++) {
                    if (authorize[i].formId != formId) {
                        _tmpdata.push(authorize[i]);
                    }
                }
                authorize = _tmpdata;
                if (!!currentNode.authorizeFields) {
                    currentNode.authorizeFields = authorize;
                }
                $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
            }
        }
    }

    function isRepeat(id) {
        var res = false;
        for (var i = 0, l = auditors.length; i < l; i++) {
            if (auditors[i].auditorId == id) {
                learun.alert.warning('重复添加审核人员信息');
                res = true;
                break;
            }
        }
        return res;
    }


    var page = {
        init: function () {
            page.nodeInit();
            page.bind();
            page.initData();

            if (!!isPreview) {
                $('input,textarea').attr('readonly', 'readonly');
                $('.lr-form-jfgrid-btns').remove();
            }

        },
        nodeInit: function () {
            if (currentNode.type != 'conditionnode') {
                $('#lr_form_tabs li a[data-value="conditionField"]').parent().remove();
                $('#lr_form_tabs li a[data-value="conditionSqlDiv"]').parent().remove();
            }

            switch (currentNode.type) {
                case 'startround':// 开始节点
                    // 去掉审核者设置
                    $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="sqlFailInfo"]').parent().remove();
                    // 超时设置去掉
                    $('#timeoutNotice').parent().remove();
                    $('#timeoutAction').parent().remove();
                    // 去掉会签设置
                    $('#confluenceType').parent().remove();
                    $('#confluenceRate').parent().remove();
                    // 禁止修改节点名称
                    $('#name').attr('readonly', 'readonly');
                    break;
                case 'auditornode':
                    $('#lr_form_tabs li a[data-value="sqlFailInfo"]').parent().remove();
                case 'stepnode':
                    // 去掉会签设置
                    $('#confluenceType').parent().remove();
                    $('#confluenceRate').parent().remove();
                    break;
                case 'confluencenode':
                    // 去掉审核者设置
                    $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
                    // 去掉表单权限设置
                    $('#lr_form_tabs li a[data-value="formAuthorize"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
                    // 禁止修改节点名称
                    $('#name').attr('readonly', 'readonly');
                    // 超时设置去掉
                    $('#timeoutNotice').parent().remove();
                    $('#timeoutAction').parent().remove();
                    break;
                case 'conditionnode':
                    $('#lr_form_tabs li a[data-value="auditor"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="workform"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="formAuthorize"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="methodInfo"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="sqlSuccessInfo"]').parent().remove();
                    $('#lr_form_tabs li a[data-value="sqlFailInfo"]').parent().remove();
                    // 超时设置去掉
                    $('#timeoutNotice').parent().remove();
                    $('#timeoutAction').parent().remove();
                    // 去掉会签设置
                    $('#confluenceType').parent().remove();
                    $('#confluenceRate').parent().remove();
                    break;
            };
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            $('#lr_form_tabs').lrFormTab();
            // 会签设置
            $('#confluenceType').lrselect({//会签类型:1-100%通过，2-一个通过即可，3-按百分比
                placeholder: false,
                data: [{ id: '1', text: '所有步骤通过' }, { id: '2', text: '一个步骤通过即可' }, { id: '3', text: '按百分比计算' }]
            }).lrselectSet('1');
            // 审核者
            $('#auditor_girdtable').jfGrid({
                headData: [
                    {
                        label: "类型", name: "type", width: 100, align: "center",
                        formatter: function (cellvalue) {//审核者类型1.岗位2.角色3.用户
                            switch (cellvalue) {
                                case '1':
                                    return '岗位';
                                    break;
                                case '2':
                                    return '角色';
                                    break;
                                case '3':
                                    return '用户';
                                    break;
                            }
                        }
                    },
                    { label: "名称", name: "auditorName", width: 260, align: "left" },
                    {
                        label: "附加条件", name: "condition", width: 150, align: "left",
                        formatter: function (cellvalue) {// 1.同一个部门2.同一个公司
                            switch (cellvalue) {
                                case '1':
                                    return '同一个部门';
                                    break;
                                case '2':
                                    return '同一个公司';
                                    break;
                            }
                        }
                    }
                ]
            });
            // 岗位添加
            $('#lr_post_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorPostForm',
                    title: '添加审核岗位',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/PostForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {

                            console.log(data);

                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                           
                        });
                    }
                });
            });
            // 角色添加
            $('#lr_role_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorRoleForm',
                    title: '添加审核角色',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/RoleForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 人员添加
            $('#lr_user_auditor').on('click', function () {
                learun.layerForm({
                    id: 'AuditorUserForm',
                    title: '添加审核人员',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/UserForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (!isRepeat(data.auditorId)) {
                                data.id = learun.newGuid();
                                auditors.push(data);
                                $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                            }
                        });
                    }
                });
            });
            // 审核人员移除
            $('#lr_delete_auditor').on('click', function () {
                var _id = $('#auditor_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该审核人员！', function (res,index) {
                        if (res) {
                            for (var i = 0, l = auditors.length; i < l; i++) {
                                if (auditors[i].id == _id) {
                                    auditors.splice(i, 1);
                                    $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
            // 表单添加
            $('#workform_girdtable').jfGrid({
                headData: [
                    { label: "表单名称", name: "name", width: 160, align: "left" },
                    {
                        label: "表单类型", name: "type", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-success \" style=\"cursor: pointer;\">自定义表单</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-warning \" style=\"cursor: pointer;\">系统表单</span>';
                            }
                        }
                    },
                    { label: "表单地址", name: "url", width: 200, align: "left" }
                ]
            });
            $('#lr_add_workform').on('click', function () {
                learun.layerForm({
                    id: 'WorkformForm',
                    title: '添加表单',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/WorkformForm',
                    width: 400,
                    height: 320,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            //需要判断表单的重复性
                            for (var i = 0, l = workforms.length; i < l; i++) {
                                if (data.formId != "") {
                                    if (data.formId == workforms[i].formId) {
                                        learun.alert.error('重复添加表单');
                                        return false;
                                    }
                                }
                                else {
                                    if (data.url == workforms[i].url) {
                                        learun.alert.error('重复添加表单');
                                        return false;
                                    }
                                }
                            }

                            data.id = learun.newGuid();
                            workforms.push(data);
                            $('#workform_girdtable').jfGridSet('refreshdata', workforms);
                            loadformcomponts(data.formId, data.name, 0);
                        });
                    }
                });
            });
            $('#lr_edit_workform').on('click', function () {
                var _id = $('#workform_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'WorkformForm',
                        title: '编辑表单',
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/WorkformForm?id=' + _id,
                        width: 400,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = workforms.length; i < l; i++) {
                                    if (workforms[i].id != _id) {
                                        if (data.formId != "") {
                                            if (data.formId == workforms[i].formId) {
                                                learun.alert.error('重复添加表单');
                                                return false;
                                            }
                                        }
                                        else {
                                            if (data.url == workforms[i].url) {
                                                learun.alert.error('重复添加表单');
                                                return false;
                                            }
                                        }
                                    }
                                }

                                for (var i = 0, l = workforms.length; i < l; i++) {
                                    if (workforms[i].id == _id) {
                                        if (workforms[i].formId != data.formId) {
                                            loadformcomponts(workforms[i].formId, data.name, 1);
                                            loadformcomponts(data.formId, data.name, 0);
                                        }
                                        workforms[i] = data;
                                        $('#workform_girdtable').jfGridSet('refreshdata', workforms);
                                        break;
                                    }
                                }
                            });
                        }
                    });
                }
            });
            $('#lr_delete_workform').on('click', function () {
                var _id = $('#workform_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该表单！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = workforms.length; i < l; i++) {

                                if (workforms[i].id == _id) {
                                    loadformcomponts(workforms[i].formId, '', 1);
                                    workforms.splice(i,1);
                                    $('#workform_girdtable').jfGridSet('refreshdata', workforms);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
            // 表单权限
            $('#authorize_girdtable').jfGrid({
                headData: [
                    { label: "表单名称", name: "formName", width: 160, align: "left" },
                    { label: "字段名称", name: "fieldName", width: 160, align: "left" },
                    { label: "字段ID", name: "fieldId", width: 180, align: "left" },
                    {
                        label: "查看", name: "isLook", width: 70, align: "center",
                        formatter: function (cellvalue, row, dfop, $dcell) {
                            $dcell.on('click', function () {
                                if (row.isLook == 1) {
                                    row.isLook = 0;
                                    $(this).html('<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>');
                                }
                                else {
                                    row.isLook = 1;
                                    $(this).html('<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>');
                                }
                            });
                            if (cellvalue == 1) {
                                return '<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>';
                            }
                        }
                    },
                    {
                        label: "编辑", name: "isEdit", width: 70, align: "center",
                        formatter: function (cellvalue, row, dfop, $dcell) {
                            $dcell.on('click', function () {
                                if (row.isEdit == 1) {
                                    row.isEdit = 0;
                                    $(this).html('<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>');
                                }
                                else {
                                    row.isEdit = 1;
                                    $(this).html('<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>');
                                }
                            });
                            if (cellvalue == 1) {
                                return '<span class=\"label label-success \" style=\"cursor: pointer;\">是</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-default \" style=\"cursor: pointer;\">否</span>';
                            }
                        }
                    }
                ]
            });
            $('#lr_add_authorize').on('click', function () {
                learun.layerForm({
                    id: 'AuthorizeForm',
                    title: '添加表单权限字段',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/AuthorizeForm',
                    width: 400,
                    height: 340,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            data.id = learun.newGuid();
                            authorize.push(data);
                            $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                        });
                    }
                });
            });
            $('#lr_edit_authorize').on('click', function () {
                var _id = $('#authorize_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'AuthorizeForm',
                        title: '编辑表单权限字段',
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/AuthorizeForm?id=' + _id,
                        width: 400,
                        height: 340,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = authorize.length; i < l; i++) {
                                    if (authorize[i].id == _id) {
                                        authorize[i] = data;
                                        $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                                        break;
                                    }
                                }
                            });
                        }
                    });
                }
            });
            $('#lr_delete_authorize').on('click', function () {
                var _id = $('#authorize_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该权限字段！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = authorize.length; i < l; i++) {
                                if (authorize[i].id == _id) {
                                    authorize.splice(i, 1);
                                    $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
            // 成功后执行sql语句方法
            // 数据库表选择
            $('#dbSuccessId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
            // 失败后执行sql语句方法
            $('#dbFailId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
            // 条件节点设置
            $('#dbConditionId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true
            });
            // 条件节点字段条件设置
            $('#condition_girdtable').jfGrid({
                headData: [
                    { label: "字段名称", name: "fieldName", width: 180, align: "left" },
                    { label: "字段ID", name: "fieldId", width: 180, align: "left" },
                    {
                        label: "编辑", name: "compareType", width: 80, align: "center",
                        formatter: function (cellvalue, row) {
                            switch (cellvalue)// 比较类型1.等于2.不等于3.大于4.大于等于5.小于6.小于等于7.包含8.不包含9.包含于10.不包含于
                            {
                                case '1':
                                    return '等于';
                                    break;
                                case '2':
                                    return '不等于';
                                    break;
                                case '3':
                                    return '大于';
                                    break;
                                case '4':
                                    return '大于等于';
                                    break;
                                case '5':
                                    return '小于';
                                    break;
                                case '6':
                                    return '小于等于';
                                    break;
                                case '7':
                                    return '包含';
                                    break;
                                case '8':
                                    return '不包含';
                                    break;
                                case '9':
                                    return '包含于';
                                    break;
                                case '10':
                                    return '不包含于';
                                    break;
                            }
                        }
                    },
                    { label: "数据值", name: "value", width: 200, align: "left" }
                ]
            });
            $('#lr_add_condition').on('click', function () {
                learun.layerForm({
                    id: 'AuthorizeForm',
                    title: '添加条件字段',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/ConditionFieldForm',
                    width: 400,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            data.id = learun.newGuid();
                            conditions.push(data);
                            $('#condition_girdtable').jfGridSet('refreshdata', conditions);
                        });
                    }
                });
            });
            $('#lr_edit_condition').on('click', function () {
                var _id = $('#condition_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'AuthorizeForm',
                        title: '编辑条件字段',
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/ConditionFieldForm?id=' + _id,
                        width: 400,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = conditions.length; i < l; i++) {
                                    if (conditions[i].id == _id) {
                                        conditions[i] = data;
                                        $('#condition_girdtable').jfGridSet('refreshdata', conditions);
                                        break;
                                    }
                                }
                            });
                        }
                    });
                }
            });
            $('#lr_delete_condition').on('click', function () {
                var _id = $('#condition_girdtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该条件字段！', function (res, index) {
                        if (res) {
                            for (var i = 0, l = conditions.length; i < l; i++) {
                                if (conditions[i].id == _id) {
                                    conditions.splice(i, 1);
                                    $('#condition_girdtable').jfGridSet('refreshdata', conditions);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
        },
        /*初始化数据*/
        initData: function () {
            $('#baseInfo').lrSetFormData(currentNode);
            $('#iocName').val(currentNode.iocName || '');
            $('#dbSuccessId').lrselectSet(currentNode.dbSuccessId);
            $('#dbSuccessSql').val(currentNode.dbSuccessSql || '');
            $('#dbFailId').lrselectSet(currentNode.dbFailId);
            $('#dbFailSql').val(currentNode.dbFailSql || '');
            $('#dbConditionId').lrselectSet(currentNode.dbConditionId);
            $('#conditionSql').val(currentNode.conditionSql || '');

            if (!!currentNode.auditors) {
                auditors = currentNode.auditors;
            }
            if (!!currentNode.authorizeFields) {
                authorize = currentNode.authorizeFields;
            }
            if (!!currentNode.conditions) {
                conditions = currentNode.conditions;
            }
            if (!!currentNode.wfForms) {
                workforms = currentNode.wfForms;
            }

            $('#authorize_girdtable').jfGridSet('refreshdata', authorize);
            $('#condition_girdtable').jfGridSet('refreshdata', conditions);
            $('#auditor_girdtable').jfGridSet('refreshdata', auditors);
            $('#workform_girdtable').jfGridSet('refreshdata', workforms);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#baseInfo').lrValidform()) {
            return false;
        }
        var baseInfoData = $('#baseInfo').lrGetFormData();
        switch (currentNode.type) {
            case 'startround':// 开始节点
                currentNode.authorizeFields = authorize;
                currentNode.wfForms = workforms;
                break;
            case 'auditornode':
            case 'stepnode':
                currentNode.name = baseInfoData.name;
                currentNode.auditors = auditors;
                currentNode.authorizeFields = authorize;
                currentNode.wfForms = workforms;

                currentNode.timeoutAction = baseInfoData.timeoutAction;// 超时流转时间
                currentNode.timeoutNotice = baseInfoData.timeoutNotice;// 超时通知时间
                break;
            case 'confluencenode':
                currentNode.confluenceType = baseInfoData.confluenceType;
                currentNode.confluenceRate = baseInfoData.confluenceRate;
                break;
            case 'conditionnode':
                currentNode.name = baseInfoData.name;
                currentNode.conditions = conditions;
                
                currentNode.dbConditionId = $('#dbConditionId').lrselectGet();
                currentNode.conditionSql = $('#conditionSql').val();
                break;
        };
        if (currentNode.type != 'conditionnode') {
            currentNode.iocName = $('#iocName').val();
            currentNode.dbSuccessId = $('#dbSuccessId').lrselectGet();
            currentNode.dbSuccessSql = $('#dbSuccessSql').val();

            if (currentNode.type != 'startround' && currentNode.type != 'auditornode') {
                currentNode.dbFailId = $('#dbFailId').lrselectGet();
                currentNode.dbFailSql = $('#dbFailSql').val();
            }
        }
        callBack();
        return true;
    };
    page.init();
}