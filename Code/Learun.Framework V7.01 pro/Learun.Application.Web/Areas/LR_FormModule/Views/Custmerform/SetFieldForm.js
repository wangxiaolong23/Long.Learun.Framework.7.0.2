/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：自定义表格字段设置	
 */
var dbId = request('dbId');
var tableName = request('tableName');

var keyValue = request('keyValue'); // 设置列的ID项
var parentId = request('parentId'); // 设置列的ID项


var acceptClick;
var selectFieldData = null;
var bootstrap = function ($, learun) {
    "use strict";

    var dataSourceField = [];
    var dataSourceId = '';
    var itemCode = '';
    var colDatas = top.layer_custmerForm_editgrid_index.colDatas;


    function setGridData(data)
    {
        data = data || [
            { label: '项目名', name: 'F_ItemName', value: '', width: '100', align: 'left', hide: '0', sort: 1 },
            { label: '项目值', name: 'F_ItemValue', value: '', width: '100', align: 'left', hide: '0', sort: 2 }
        ];
        data = data.sort(function (a, b) {
            return parseInt(a.hide) - parseInt(b.hide);
        });
        data = data.sort(function (a, b) {
            return parseInt(a.sort) - parseInt(b.sort);
        });
        $('#gridtable').jfGridSet('refreshdata', data);
    }

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 上级
            $('#parentId').lrselect({
                data: colDatas,
                text: 'name',
                value: 'id'
            }).lrselectSet(parentId);
            // 绑定字段
            $('#field').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true,
                select: function (item) {
                }
            });
            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: dbId, tableName: tableName }, function (data) {
                $('#field').lrselectRefresh({
                    data: data
                });
            });
            // 编辑类型
            $('#type').lrselect({
                placeholder: false,
                data: [{ id: 'input', text: 'input' }, { id: 'label', text: 'label' }, { id: 'select', text: 'select' }, { id: 'guid', text: 'GUID' }],
                select: function (item) {
                    $('.data-edit-select').hide();
                    if (item.id == "select") {
                        $('.data-edit-select').show();
                    }
                }
            }).lrselectSet('input');
            // 对齐方式
            $('#align').lrselect({ placeholder: false }).lrselectSet('left');

            $('#gridtable').jfGrid({
                headData: [
                    { label: "字段名", name: "label", width: 120, align: "left" },
                    { label: "字段ID", name: "name", width: 120, align: "left" },
                    { label: "填充栏位", name: "value", width: 120, align: "left" },
                    { label: "宽度", name: "width", width: 70, align: "center" },
                    { label: "对齐方式", name: "align", width: 60, align: "center" },
                    {
                        label: "是否隐藏", name: "hide", width: 60, align: "center",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-default \" style=\"cursor: pointer;\">是</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-success \" style=\"cursor: pointer;\">否</span>';
                            }
                        }
                    }// 1 隐藏 0 显示
                ]
            });

            // 数据字典选项
            $('#dataItemId').lrselect({
                allowSearch: true,
                maxHeight: 150,
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetClassifyTree',
                type: 'tree',
                select: function (item) {
                    dataSourceId = '';
                    itemCode = '';
                    if (!!item) {
                        itemCode = item.value;
                        dataSourceId = item.id;
                    }
                }
            });
            // 数据源
            $('#dataSourceId').lrselect({
                value: 'F_Code',
                text: 'F_Name',
                allowSearch: true,
                maxHeight: 150,
                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetList',
                select: function (item) {
                    dataSourceField = [];
                    dataSourceId = '';
                    if (!!item) {
                        dataSourceId = item.F_Code;
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: item.F_Code }, function (data) {
                            for (var i = 0, l = data.length; i < l; i++) {
                                if (data[i] != 'rownum') {
                                    var point = { label: '', name: data[i], value: '', width: '100', align: 'left', hide: '1', sort: i };
                                    dataSourceField.push(point);
                                }
                            }
                            setGridData(dataSourceField);
                        });
                    }
                }
            });


            // 数据来源
            $('#dataSource').lrselect({
                data: [{ id: '0', text: '数据字典' }, { id: '1', text: '数据源' }],
                value: 'id',
                text: 'text',
                placeholder: false,
                select: function (item) {
                    if (item.id == '0') {
                        $('#dataSourceId').hide();
                        $('#dataItemId').show();
                        // 如果是数据字典的话
                        setGridData();
                    }
                    else {
                        $('#dataItemId').hide();
                        $('#dataSourceId').show();
                        setGridData(dataSourceField);
                    }
                }
            }).lrselectSet('0');
            
            $('#lr_edit_datasource').on('click', function () {
                selectFieldData = $('#gridtable').jfGridGet('rowdata');
                var _name = $('#gridtable').jfGridValue('name');
                if (learun.checkrow(_name)) {
                    learun.layerForm({
                        id: 'SetSelectFieldForm',
                        title: '设置选择字段',
                        url: top.$.rootUrl + '/LR_FormModule/Custmerform/SetSelectFieldForm?dbId=' + dbId + '&tableName=' + tableName,
                        width: 450,
                        height: 400,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                var rowdatas = $('#gridtable').jfGridGet('rowdatas');
                                for (var i = 0, l = rowdatas.length; i < l; i++) {
                                    if (rowdatas[i].name == data.name) {
                                        rowdatas[i] = data;
                                    }
                                }
                                setGridData(rowdatas);
                            });
                        }
                    });
                }

            });


           
        },
        initData: function () {
            if (!!keyValue) {
                for (var i = 0, l = colDatas.length; i < l; i++) {
                    var item = colDatas[i];
                    if (item.id == keyValue) {
                        item.dataItemId = item.dataSourceId;
                        $('#form').lrSetFormData(item);
                        if (item.type == 'select') {
                            $('#gridtable').jfGridSet('refreshdata', item.selectData);
                        }
                        break;
                    }
                }
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();

        var resdata = {};
        resdata.id = keyValue || learun.newGuid();
        resdata.name = formData.name;
        resdata.field = formData.field;
        resdata.width = formData.width;
        resdata.sort = formData.sort;
        resdata.type = formData.type;
        resdata.align = formData.align;
        resdata.parentId = formData.parentId;
        switch (formData.type) {
            case 'select':
                resdata.dataSource = formData.dataSource;
                resdata.dataSourceId = dataSourceId;
                resdata.dataItemCode = itemCode;
                
                resdata.dataSourceWidth = formData.dataSourceWidth;
                resdata.dataSourceHeight = formData.dataSourceHeight;
                resdata.selectData = $('#gridtable').jfGridGet('rowdatas');
                break;
            case 'fixedInfo':
                resdata.fixedInfoType = formData.fixedInfoType;
                resdata.fixedInfoHide = formData.fixedInfoHide;
                break;
        }
        callBack(resdata);
        return true;
    };
    page.init();
}