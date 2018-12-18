/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：新建表
 */
var databaseLinkId = request('databaseLinkId');
var draftId = '';
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initGrid();
        },
        bind: function () {
            /*复制表*/
            $('#lr_copytable').on('click', function () {
                learun.layerForm({
                    id: 'copyform',
                    title: '复制表',
                    url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/CopyTableForm',
                    width: 800,
                    height: 600,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            $('#gridtable').jfGridSet('refreshdata', []);
                            learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList?databaseLinkId=' + data.databaseLinkId + '&tableName=' + data.tableName, function (res) {
                                learun.clientdata.getAllAsync('dataItem', {
                                    code: 'DbFieldType',
                                    callback: function (dataes) {
                                        var map = {};
                                        $.each(dataes, function (_index, _item) {
                                            map[_item.value] = _item.text;
                                        });
                                        $.each(res.data, function (_index, _item) {
                                            _item.f_datatypename = map[_item.f_datatype];
                                            _item.f_isnullable = _item.f_isnullable || '0';
                                            _item.f_key = _item.f_key || '0';
                                        });
                                        $('#gridtable').jfGridSet('refreshdata', res.data);
                                    }
                                });
                            });
                        });
                    }
                });
            });

            /*复制行*/
            $('#lr_copyrow').on('click', function () {
                var selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (selectedRow.length > 0) {
                    $.each(selectedRow, function (_index, _item) {
                        $('#gridtable').jfGridSet('addRow', JSON.parse(JSON.stringify(_item)));
                    });
                }
                else {
                    top.learun.alert.warning('请选择需要复制的行!');
                }
            });

            /*常用字段*/
            $('#lr_commonfield').on('click', function () {
                learun.layerForm({
                    id: 'commonfieldform',
                    title: '常用字段选择',
                    url: top.$.rootUrl + '/LR_SystemModule/DbField/SelectForm',
                    width: 600,
                    height: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (data) {
                                learun.clientdata.getAllAsync('dataItem', {
                                    code: 'DbFieldType',
                                    callback: function (dataes) {
                                        var map = {};
                                        $.each(dataes, function (_index, _item) {
                                            map[_item.value] = _item.text;
                                        });
                                        if (data.length > 0) {
                                            $.each(data, function (_index, _item) {
                                                var _point = {
                                                    f_column: _item.F_Name,
                                                    f_remark: _item.F_Remark,
                                                    f_datatype: _item.F_DataType,
                                                    f_datatypename: map[_item.F_DataType],
                                                    f_length: _item.F_Length,
                                                    f_key: '0',
                                                    f_isnullable: '1'
                                                }
                                                $('#gridtable').jfGridSet('addRow', _point);
                                            });
                                        }
                                        else {
                                            var _point = {
                                                f_column: data.F_Name,
                                                f_remark: data.F_Remark,
                                                f_datatype: data.F_DataType,
                                                f_datatypename: map[data.F_DataType],
                                                f_length: data.F_Length,
                                                f_key: '0',
                                                f_isnullable: '1'
                                            }
                                            $('#gridtable').jfGridSet('addRow', _point);
                                        }
                                    }
                                });
                            }
                        });
                    }
                });
            });

            /*导入草稿*/
            $('#lr_draft').on('click', function () {
                learun.layerForm({
                    id: 'draftform',
                    title: '导入草稿',
                    url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/DraftForm',
                    width: 800,
                    height: 600,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            if (data) {
                                draftId = data.F_Id;
                                $('#form').lrSetFormData({ tableName: data.F_Name, remark: data.F_Remark});
                                $('#gridtable').jfGridSet('refreshdata', JSON.parse(data.F_Content));
                            }
                        });
                    }
                });
            });


            /*保存草稿*/
            $('#lr_savedraft').on('click', function () {
                if (!$('#form').lrValidform()) {
                    return false;
                }
                var formData = $('#form').lrGetFormData();
                var grid = $('#gridtable').jfGridGet('rowdatas');

                var postData = {
                    F_Name: formData.tableName,
                    F_Remark: formData.remark,
                    F_Content: JSON.stringify(grid),
                    F_DbLinkId: databaseLinkId
                };
                $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/DatabaseTable/SaveDraft?keyValue=' + draftId, postData, function (res) {
                    // 保存成功后才回调
                    console.log(res);
                    if (res.code == 200) {
                        draftId = res.data;
                    }
                }, true);
            });

            /*发布*/
            $('#lr_release').on('click', function () {
                if (!$('#form').lrValidform()) {
                    return false;
                }
                var formData = $('#form').lrGetFormData();
                var grid = $('#gridtable').jfGridGet('rowdatas');
                //判断字段是否合法
                if (!page.checkGrid(grid)) {
                    return false;
                }
                var postData = {
                    databaseLinkId: databaseLinkId,
                    draftId: draftId,
                    tableName: formData.tableName,
                    tableRemark: formData.remark,
                    strColList: JSON.stringify(grid)
                };
                $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/DatabaseTable/SaveTable', postData, function (res) {
                    // 新建成功后刷新页面
                    if (res.code == 200) {
                        draftId = '';
                        $('#form').lrSetFormData({ tableName: '', remark: '' });
                        $('#gridtable').jfGridSet('refreshdata', [{}]);
                    }
                },true);
            });
        },
        //检查列名是否为空或重复
        checkGrid: function (data) {
            // 1.列名不能为空;2.字串类型长度不能为0或不填3.数据类型不能为空4.列名不能重复
            var map = {};
            var flag = true;

            $.each(data, function (_index, _item) {

                if (_item.f_column === undefined || _item.f_column === '') {
                    learun.alert.error('【第' + _index + '列】列名为空');
                    flag = false;
                    return false;
                }
                else if (_item.f_datatype === undefined || _item.f_datatype === '') {
                  

                    learun.alert.error('【第' + _index + '列】数据类型为空');
                    flag = false;
                    return false;
                }
                else if (_item.f_datatype == 'varchar' && (_item.f_length == 0 || _item.f_length == undefined)) {
                    learun.alert.error('【第' + _index + '列】字串长度设置错误');
                    flag = false;
                    return false;
                }
                else if (map[_item.f_column] != undefined ){
                    learun.alert.error('【第' + _index + '列与第' + map[_item.f_column] + '列】列名重复');
                    flag = false;
                    return false;
                }
                map[_item.f_column] = _index;
            });
            return flag;
        },
        /*初始化表格*/
        initGrid: function () {
            // 订单产品信息
            $('#gridtable').jfGrid({
                headData: [
                    {
                        label: "列名", name: "f_column", width: 180, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "数据类型", name: "f_datatype", width: 110, align: "center",
                        formatter: function (cellvalue, row) {
                            return row.f_datatypename;
                        },
                        edit: {
                            type: 'select',
                            op: {
                                value: 'F_ItemValue',
                                text: 'F_ItemName',
                                title: 'F_ItemName',
                                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',
                                param: { itemCode: 'DbFieldType' },
                            },
                            change: function (row, num, selectdata) {// 行数据和行号
                                if (!!selectdata) {
                                    row.f_datatypename = selectdata.F_ItemName;
                                    row.f_datatype = selectdata.F_ItemValue;
                                    if (selectdata.F_ItemValue == 'varchar') {
                                        row.f_length = 50;
                                    }
                                    else {
                                        row.f_length = 0;
                                    }
                                }
                                else {
                                    row.f_length = 0;
                                    row.f_datatype = '';
                                    row.f_datatypename = '';
                                }

                                $('#gridtable').jfGridSet('updateRow', num);
                            }
                        }
                    },
                    {
                        label: "长度", name: "f_length", width: 57, align: "center", edit: {
                            type: 'input'
                        } },
                    {
                        label: "允许空", name: "f_isnullable", width: 80, align: "center",
                        edit: {
                            type: 'radio',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号

                            },
                            data: [
                                { 'id': '0', 'text': '否' },
                                { 'id': '1', 'text': '是' }
                            ],
                            dfvalue: '1'// 默认选中项
                        }
                    },
                    {
                        label: "主键", name: "f_key", width: 80, align: "center",
                        edit: {
                            type: 'radio',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号
                                if (data.f_key == "1") {
                                    data.f_isnullable = '0';
                                    $('#gridtable').jfGridSet('updateRow', num);
                                }
                            },
                            data: [
                                { 'id': '0', 'text': '否' },
                                { 'id': '1', 'text': '是' }
                            ],
                            dfvalue: '0'// 默认选中项
                        }
                    },
                    {
                        label: "说明", name: "f_remark", width: 100, align: "left", edit: {
                            type: 'input'
                        }
                    }
                ],
                onAddRow: function (row, rows) {
                    row.f_key = '0';
                    row.f_isnullable = '1';
                },
                isEdit: true,
                isMultiselect: true
            });
        }
    };
    page.init();
}