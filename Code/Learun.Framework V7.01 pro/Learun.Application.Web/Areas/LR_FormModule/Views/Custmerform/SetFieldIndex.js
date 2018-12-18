/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：表格数据字段编辑	
 */
var keyValue = request('keyValue');
var gridSetting = top[keyValue];
var colDatas;

var bootstrap = function ($, learun) {
    "use strict";
    colDatas = gridSetting.fields;
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 新增
            $('#lr_add').on('click', function () {
                var _Id = $('#gridtable').jfGridValue('id');
                learun.layerForm({
                    id: 'SetFieldForm',
                    title: '添加表格编辑字段',
                    url: top.$.rootUrl + '/LR_FormModule/Custmerform/SetFieldForm?dbId=' + gridSetting.dbId + '&tableName=' + gridSetting.tableName + '&parentId=' + _Id,
                    width: 600,
                    height: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            colDatas.push(data);
                            colDatas = colDatas.sort(function (a, b) {
                                return parseInt(a.sort) - parseInt(b.sort);
                            });
                            $('#gridtable').jfGridSet('refreshdata', colDatas);
                        });

                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var _Id = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(_Id)) {
                    learun.layerForm({
                        id: 'SetFieldForm',
                        title: '编辑表格编辑字段',
                        url: top.$.rootUrl + '/LR_FormModule/Custmerform/SetFieldForm?dbId=' + gridSetting.dbId + '&tableName=' + gridSetting.tableName + '&keyValue=' + _Id,
                        width: 600,
                        height: 500,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                for (var i = 0, l = colDatas.length; i < l; i++) {
                                    var item = colDatas[i];
                                    if (item.id == _Id) {
                                        colDatas[i] = data;
                                        break;
                                    }
                                }
                                colDatas = colDatas.sort(function (a, b) {
                                    return parseInt(a.sort) - parseInt(b.sort);
                                });
                                $('#gridtable').jfGridSet('refreshdata', colDatas);
                            });

                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var _id = $('#gridtable').jfGridValue('id');
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该字段', function (res, index) {
                        if (res) {
                            for (var i = 0, l = colDatas.length; i < l; i++) {
                                var item = colDatas[i];
                                if (item.id == _id) {
                                    colDatas.splice(i, 1);
                                    $('#gridtable').jfGridSet('refreshdata', colDatas);
                                    break;
                                }
                            }
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                headData: [
                    { label: "显示名称", name: "name", width: 200, align: "left" },
                    { label: "绑定字段", name: "field", width: 200, align: "left" },
                    { label: "显示宽度", name: "width", width: 80, align: "center" },
                    { label: "编辑类型", name: "type", width: 80, align: "center" }
                ],
                mainId: 'id',
                reloadSelected: true,
                parentId: 'parentId',
                isTree: true
            });

            $('#gridtable').jfGridSet('refreshdata', colDatas);
        }
    };
    page.init();
}


