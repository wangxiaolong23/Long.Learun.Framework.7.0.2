/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：功能模块	
 */
var keyValue = request('keyValue');
var moduleId = request('moduleId');

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
            // 目标
            $('#F_Target').lrselect().on('change', function () {
                // 目标改变
                var value = $(this).lrselectGet();
                var $next = $("#btn_next");
                var $finish = $("#btn_finish");
                if (value == 'expand') {
                    $next.attr('disabled', 'disabled');
                    $finish.removeAttr('disabled');
                }
                else {
                    $next.removeAttr('disabled');
                    $finish.attr('disabled', 'disabled');
                }
            });
            // 上级
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/Module/GetExpendModuleTree',
                type: 'tree',
                maxHeight:180,
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
                        if (top._learunSelectIcon != '')
                        {
                            $('#F_Icon').val(top._learunSelectIcon);
                        }
                    }
                });
            });
            
            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        /*初始化表格*/
        initGrid: function () {
            $('#btns_girdtable').jfGrid({
                headData: [
                    
                    {
                        label: "名称", name: "F_FullName", width: 200, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "编号", name: "F_EnCode", width: 160, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "上级按钮", name: "F_ParentId", width: 160, align: "left",
                        formatter: function (value, row, op, $cell) {
                            if (value == '0' || value == '') {
                                row.F_ParentId = '';
                                return '';
                            }
                            var res = '';
                            $.each(op.rowdatas, function (_index, _item) {
                                if (value == _item.F_ModuleButtonId) {
                                    res = _item.F_FullName;
                                    return false;
                                }
                            });
                            return res;
                        },
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                // 获取当前列表数据
                                var rowdatas = $('#btns_girdtable').jfGridGet('rowdatas');
                                var res = [];
                                $.each(rowdatas, function (_index, _item) {
                                    if (row.F_ModuleButtonId != _item.F_ModuleButtonId) {
                                        res.push(_item);
                                    }
                                });

                                $self.lrselectRefresh({
                                    data: res
                                });
                            },
                            op: {
                                value: 'F_ModuleButtonId',
                                text: 'F_FullName',
                                title: 'F_FullName'
                            },
                            change: function (rowData, rowIndex, item) {
                                setTimeout(function () {
                                    $('#btns_girdtable').jfGridSet('refreshdata');
                                }, 300);
                            }
                        }
                    },
                    {
                        label: "", name: "btn1", width: 52, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#btns_girdtable').jfGridSet('moveUp', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-info\" style=\"cursor: pointer;\">上移</span>';
                        }
                    },
                    {
                        label: "", name: "btn2", width: 52, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#btns_girdtable').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-success\" style=\"cursor: pointer;\">下移</span>';
                        }
                    },
                ],
                isTree: true,
                mainId: 'F_ModuleButtonId',
                parentId: 'F_ParentId',
                isMultiselect: true,
                isEdit: true,
                onAddRow: function (row, rows) {
                    row.F_ModuleButtonId = learun.newGuid();
                    row.F_ParentId = '';
                }
            });
            $('#view_girdtable').jfGrid({
                headData: [
                    {
                        label: "名称", name: "F_FullName", width: 260, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "编号", name: "F_EnCode", width: 260, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "", name: "btn1", width: 52, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#view_girdtable').jfGridSet('moveUp', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-info\" style=\"cursor: pointer;\">上移</span>';
                        }
                    },
                    {
                        label: "", name: "btn2", width: 52, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#view_girdtable').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-success\" style=\"cursor: pointer;\">下移</span>';
                        }
                    }
                ],
                mainId: 'F_ModuleColumnId',
                isMultiselect: true,
                isEdit: true,
                onAddRow: function (row, rows) {
                    row.F_ModuleColumnId = learun.newGuid();
                    row.F_ParentId = '0';
                }
            });
            $('#form_girdtable').jfGrid({
                headData: [
                    {
                        label: "名称", name: "F_FullName", width: 260, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "编号", name: "F_EnCode", width: 260, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "", name: "btn1", width: 52, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#form_girdtable').jfGridSet('moveUp', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-info\" style=\"cursor: pointer;\">上移</span>';
                        }
                    },
                    {
                        label: "", name: "btn2", width: 52, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#form_girdtable').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-success\" style=\"cursor: pointer;\">下移</span>';
                        }
                    }
                ],
                mainId: 'F_ModuleFormId',
                isMultiselect: true,
                isEdit: true,
                onAddRow: function (row, rows) {
                    row.F_ModuleFormId = learun.newGuid();
                }
            });
        },
        /*初始化数据*/
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_SystemModule/Module/GetFormData?keyValue=' + keyValue, function (data) {//
                    $('#step-1').lrSetFormData(data.moduleEntity);
                    $('#btns_girdtable').jfGridSet('refreshdata', data.moduleButtons);
                    $('#view_girdtable').jfGridSet('refreshdata', data.moduleColumns);
                    $('#form_girdtable').jfGridSet('refreshdata', data.moduleFields);
                });
            }
            else if (!!moduleId) {
                $('#F_ParentId').lrselectSet(moduleId);
            }
        },
        /*保存数据*/
        save: function () {
            if (!$('#step-1').lrValidform()) {
                return false;
            }
            var postData = {};
            var formdata = $('#step-1').lrGetFormData(keyValue);
            if (formdata["F_ParentId"] == '' || formdata["F_ParentId"] == '&nbsp;') {
                formdata["F_ParentId"] = '0';
            }
            postData.keyValue = keyValue;
            postData.moduleEntityJson = JSON.stringify(formdata);
            if (formdata.F_Target != 'expand') {
                // 当为窗口和弹层时需要获取按钮和视图设置信息
                var _btns = $('#btns_girdtable').jfGridGet('rowdatas');
                var _cols = $('#view_girdtable').jfGridGet('rowdatas');
                var _fields = $('#form_girdtable').jfGridGet('rowdatas');


                var btns = [];
                $.each(_btns, function (_index, _item) {
                    if (_item.F_EnCode && _item.F_FullName) {
                        var point = {
                            F_ModuleButtonId: _item.F_ModuleButtonId,
                            F_ParentId: _item.F_ParentId || '0',
                            F_EnCode: _item.F_EnCode,
                            F_FullName: _item.F_FullName,
                            F_SortCode: _index
                        };
                        btns.push(point);
                    }
                });
                var cols = [];
                $.each(_cols, function (_index, _item) {
                    if (_item.F_EnCode && _item.F_FullName) {
                        var point = {
                            F_ModuleColumnId: _item.F_ModuleColumnId,
                            F_ParentId: '0',
                            F_EnCode: _item.F_EnCode,
                            F_FullName: _item.F_FullName,
                            F_SortCode: _index
                        };
                        cols.push(point);
                    }
                });
                var fields = [];
                $.each(_fields, function (_index, _item) {
                    if (_item.F_EnCode && _item.F_FullName) {
                        var point = {
                            F_ModuleFormId: _item.F_ModuleFormId,
                            F_EnCode: _item.F_EnCode,
                            F_FullName: _item.F_FullName,
                            F_SortCode: _index
                        };
                        fields.push(point);
                    }
                });

                postData.moduleColumnListJson = JSON.stringify(cols);
                postData.moduleButtonListJson = JSON.stringify(btns);
                postData.moduleFormListJson = JSON.stringify(fields);
            }
            
            $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/Module/SaveForm', postData, function (res) {
                // 保存成功后才回调
                learun.frameTab.currentIframe().refreshGirdData(formdata);
            });
        }
    };

    page.init();
}