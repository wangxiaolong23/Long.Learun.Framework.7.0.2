/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.29
 * 描 述：功能模块	
 */
var keyValue = request('keyValue');
var tableFields = {}; // 用来缓存数据表字段
var formscheme;
var setFormId = "";
var formFields = [];

var queryData = [];
var colData = [];

var moduleForm = [];

var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initGrid();
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
                        // 加载表单数据
                        var formId = $('#F_FormId').lrselectGet();
                        if (setFormId != formId) {
                            queryData = [];
                            colData = [];
                            setFormId = formId;
                            $('#query_girdtable').jfGridSet('refreshdata', queryData);
                            $('#col_girdtable').jfGridSet('refreshdata', colData);

                            $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + formId, function (data) {//
                                formscheme = JSON.parse(data.schemeEntity.F_Scheme);
                                // 获取表单字段
                                for (var i = 0, l = formscheme.data.length; i < l; i++) {
                                    var componts = formscheme.data[i].componts;
                                    for (var j = 0, jl = componts.length; j < jl; j++) {
                                        var item = componts[j];
                                        if (item.type != "gridtable" && item.type != "label" && item.type != "html" && item.type != "guid" && item.type != "upload") {
                                            formFields.push(item);
                                        }
                                        if (item.type != "gridtable" && item.type != "guid") {
                                            var point = { 'F_ModuleFormId': learun.newGuid(), 'F_EnCode': item.id, 'F_FullName': item.title };
                                            moduleForm.push(point);
                                        }
                                    }
                                }


                                // 获取主表字段
                                // 确定主表信息
                                var mainTable = "";
                                for (var i = 0, l = formscheme.dbTable.length; i < l; i++) {
                                    if (formscheme.dbTable[i].relationName == "") {
                                        mainTable = formscheme.dbTable[i].name;
                                        break;
                                    }
                                }
                                if (!!tableFields[formscheme.dbId + mainTable]) {
                                    $('#queryDatetime').lrselectRefresh({
                                        data: tableFields[formscheme.dbId + mainTable]
                                    });
                                }
                                else {
                                    learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: formscheme.dbId, tableName: mainTable }, function (data) {
                                        tableFields[formscheme.dbId + mainTable] = data;
                                        $('#queryDatetime').lrselectRefresh({
                                            data: data
                                        });
                                    });
                                }

                            });

                        }

                        

                    } else if (data.step == 2) {
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
            // 上级
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/Module/GetExpendModuleTree',
                type: 'tree',
                maxHeight: 180,
                allowSearch: true
            });
            // 选择图标
            $('#selectIcon').on('click', function () {
                learun.layerForm({
                    id: 'iconForm',
                    title: '选择图标',
                    url: top.$.rootUrl + '/Utility/Icon',
                    height: 700,
                    width: 1000,
                    btn: null,
                    maxmin: true,
                    end: function () {
                        if (top._learunSelectIcon != '') {
                            $('#F_Icon').val(top._learunSelectIcon);
                        }
                    }
                });
            });
            // 选在表单
            $('#F_FormId').lrselect({
                text: 'F_Name',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_FormModule/Custmerform/GetSchemeInfoList',
                allowSearch: true
            });
            $('#lr_preview').on('click', function () {
                var formId = $('#F_FormId').lrselectGet();
                if (!!formId) {
                    learun.layerForm({
                        id: 'custmerForm_PreviewForm',
                        title: '预览当前表单',
                        url: top.$.rootUrl + '/LR_FormModule/Custmerform/PreviewForm?schemeInfoId=' + formId,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        btn: null
                    });
                }
                else {
                    learun.alert.warning('请选择表单！');
                }
            });

            // 条件设置
            $('#queryDatetime').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true
            });

            // 新增
            $('#lr_add_query').on('click', function () {
                learun.layerForm({
                    id: 'QueryFieldForm',
                    title: '添加条件字段',
                    url: top.$.rootUrl + '/LR_FormModule/FormRelation/QueryFieldForm',
                    height: 300,
                    width: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            queryData.push(data);
                            queryData = queryData.sort(function (a, b) {
                                return parseInt(a.sort) - parseInt(b.sort);
                            });
                            $('#query_girdtable').jfGridSet('refreshdata', queryData);
                        });
                    }
                });
            });
            // 编辑
            $('#lr_edit_query').on('click', function () {
                var id = $('#query_girdtable').jfGridValue('id');
                if (learun.checkrow(id)) {
                    learun.layerForm({
                        id: 'QueryFieldForm',
                        title: '添加条件字段',
                        url: top.$.rootUrl + '/LR_FormModule/FormRelation/QueryFieldForm?id=' + id,
                        height: 300,
                        width: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = queryData.length; i < l; i++) {
                                    if (queryData[i].id == data.id) {
                                        queryData[i] = data;
                                        break;
                                    }
                                }
                                queryData = queryData.sort(function (a, b) {
                                    return parseInt(a.sort) - parseInt(b.sort);
                                });
                                $('#query_girdtable').jfGridSet('refreshdata', queryData);
                            });
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete_query').on('click', function () {
                var id = $('#query_girdtable').jfGridValue('id');
                if (learun.checkrow(id)) {
                    learun.layerConfirm('是否确认删除该字段', function (res, index) {
                        if (res) {
                            for (var i = 0, l = queryData.length; i < l; i++) {
                                if (queryData[i].id == id) {
                                    queryData.splice(id, 1);
                                    break;
                                }
                            }
                            $('#query_girdtable').jfGridSet('refreshdata', queryData);
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });


            // 列表设置
            // 新增
            $('#lr_add_col').on('click', function () {
                learun.layerForm({
                    id: 'ColFieldForm',
                    title: '添加列表字段',
                    url: top.$.rootUrl + '/LR_FormModule/FormRelation/ColFieldForm',
                    height: 300,
                    width: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            colData.push(data);
                            colData = colData.sort(function (a, b) {
                                return parseInt(a.sort) - parseInt(b.sort);
                            });
                            $('#col_girdtable').jfGridSet('refreshdata', colData);
                        });
                    }
                });
            });
            // 编辑
            $('#lr_edit_col').on('click', function () {
                var id = $('#col_girdtable').jfGridValue('id');
                if (learun.checkrow(id)) {
                    learun.layerForm({
                        id: 'ColFieldForm',
                        title: '添加条件字段',
                        url: top.$.rootUrl + '/LR_FormModule/FormRelation/ColFieldForm?id=' + id,
                        height: 300,
                        width: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = colData.length; i < l; i++) {
                                    if (colData[i].id == data.id) {
                                        colData[i] = data;
                                        break;
                                    }
                                }
                                colData = colData.sort(function (a, b) {
                                    return parseInt(a.sort) - parseInt(b.sort);
                                });
                                $('#col_girdtable').jfGridSet('refreshdata', colData);
                            });
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete_col').on('click', function () {
                var id = $('#col_girdtable').jfGridValue('id');
                if (learun.checkrow(id)) {
                    learun.layerConfirm('是否确认删除该字段', function (res, index) {
                        if (res) {
                            for (var i = 0, l = colData.length; i < l; i++) {
                                if (colData[i].id == id) {
                                    colData.splice(i, 1);
                                    break;
                                }
                            }
                            $('#col_girdtable').jfGridSet('refreshdata', colData);
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });

            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        /*初始化表格*/
        initGrid: function () {
            $('#query_girdtable').jfGrid({
                headData: [
                    { label: "字段项名称", name: "fieldName", width: 260, align: "left" },
                    {
                        label: "所占行比例", name: "portion", width: 150, align: "left",
                        formatter: function (cellvalue, row) {
                            return '1/' + cellvalue;
                        }
                    },
                ],
                mainId: 'id',
            });
            $('#col_girdtable').jfGrid({
                headData: [
                    { label: "字段名称", name: "fieldName", width: 260, align: "left" },
                    { label: "宽度", name: "width", width: 150, align: "left" },
                    { label: "对齐方式", name: "align", width: 150, align: "left" }
                ]
            });
        },
        /*初始化数据*/
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_FormModule/FormRelation/GetFormData?keyValue=' + keyValue, function (data) {//
                    setFormId = data.relation.F_FormId;

                    $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + setFormId, function (data) {//
                        formscheme = JSON.parse(data.schemeEntity.F_Scheme);
                        // 获取表单字段
                        for (var i = 0, l = formscheme.data.length; i < l; i++) {
                            var componts = formscheme.data[i].componts;
                            for (var j = 0, jl = componts.length; j < jl; j++) {
                                var item = componts[j];
                                if (item.type != "gridtable" && item.type != "label" && item.type != "html" && item.type != "guid" && item.type != "upload") {
                                    formFields.push(item);
                                }
                                if (item.type != "gridtable" && item.type != "guid") {
                                    var point = { 'F_ModuleFormId': learun.newGuid(), 'F_EnCode': item.id, 'F_FullName': item.title };
                                    moduleForm.push(point);
                                }
                            }
                        }
                        // 获取主表字段
                        // 确定主表信息
                        var mainTable = "";
                        for (var i = 0, l = formscheme.dbTable.length; i < l; i++) {
                            if (formscheme.dbTable[i].relationName == "") {
                                mainTable = formscheme.dbTable[i].name;
                                break;
                            }
                        }
                        if (!!tableFields[formscheme.dbId + mainTable]) {
                            $('#queryDatetime').lrselectRefresh({
                                data: tableFields[formscheme.dbId + mainTable]
                            });
                        }
                        else {
                            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: formscheme.dbId, tableName: mainTable }, function (data) {
                                tableFields[formscheme.dbId + mainTable] = data;
                                $('#queryDatetime').lrselectRefresh({
                                    data: data
                                });
                            });
                        }

                    });


                    $('#step-1').lrSetFormData(data.module);
                    $('#F_FormId').lrselectSet(data.relation.F_FormId);

                    var settingJson = JSON.parse(data.relation.F_SettingJson);
                    $('[name="formOpenType"][value="' + settingJson.layer.opentype + '"]').attr('checked', 'checked');
                    $('#fromWidth').val(settingJson.layer.width);
                    $('#fromHeight').val(settingJson.layer.height);

                    $('[name="queryDatetime"][value="' + settingJson.query.isDate + '"]').attr('checked', 'checked');
                    $('#queryDatetime').lrselectSet(settingJson.query.DateField);
                    $('#queryWidth').val(settingJson.query.width);
                    $('#queryHeight').val(settingJson.query.height);

                    queryData = settingJson.query.fields;
                    console.log(queryData);
                    $('#query_girdtable').jfGridSet('refreshdata', queryData);



                    $('[name="ispage"][value="' + settingJson.col.isPage + '"]').attr('checked', 'checked');
                    colData = settingJson.col.fields;
                    $('#col_girdtable').jfGridSet('refreshdata', colData);
                });
            }
        },
        /*保存数据*/
        save: function () {
            if (!$('#step-1').lrValidform()) {
                return false;
            }
            var formdata = $('#step-1').lrGetFormData(keyValue);

            formdata.F_ParentId = formdata.F_ParentId || '0';
            // 设置信息
            var settingJson = {
                layer: {
                    width: formdata.fromWidth,
                    height: formdata.fromHeight,
                    opentype: $('[name="formOpenType"]:checked').val() // 1 弹层 2窗口页
                },

                query: {
                    width: $('#queryWidth').val(),
                    height: $('#queryHeight').val(),
                    isDate: $('[name="queryDatetime"]:checked').val(),
                    DateField: $('#queryDatetime').lrselectGet(),
                    fields: queryData
                },
                col: {
                    isPage: $('[name="ispage"]:checked').val(),
                    fields: colData
                }
            }
            // 表单功能设置
            var relation = {
                F_ModuleId: formdata.F_ModuleId,
                F_FormId: formdata.F_FormId,
                F_SettingJson: JSON.stringify(settingJson)
            }
            var tableIndex = 0;
            var tableMap = {};
            var compontsMap = {};
            for (var i = 0, l = formscheme.data.length; i < l; i++) {
                var componts = formscheme.data[i].componts;
                for (var j = 0, jl = componts.length; j < jl; j++) {
                    var item = componts[j];
                    compontsMap[item.id] = item;
                    if (!!item.table && tableMap[item.table] == undefined) {
                        tableMap[item.table] = tableIndex;
                        tableIndex++;
                    }
                }
            }

            // 列表设置
            var moduleColumn = [];
            for (var i = 0, l = colData.length; i < l; i++) {
                var code = colData[i].fieldId + tableMap[compontsMap[colData[i].compontId].table];
                var point = { F_ModuleColumnId: learun.newGuid(), F_FullName: colData[i].fieldName, F_EnCode: code.toLowerCase() };
                moduleColumn.push(point);
            }


            // 提交数据
            var postData = {
                relationJson:JSON.stringify(relation),
                moduleJson: JSON.stringify(formdata),
                moduleColumnJson: JSON.stringify(moduleColumn),
                moduleFormJson: JSON.stringify(moduleForm)
            };
            

            $.lrSaveForm(top.$.rootUrl + '/LR_FormModule/FormRelation/SaveForm?keyValue=' + keyValue, postData, function (res) {
                // 保存成功后才回调
                learun.frameTab.currentIframe().refreshGirdData();
            });
        }
    };

    page.init();
}