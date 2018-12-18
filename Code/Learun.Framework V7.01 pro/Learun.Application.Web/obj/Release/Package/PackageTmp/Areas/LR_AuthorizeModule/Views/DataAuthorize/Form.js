/* * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软框架开发组
 * 日  期：2017-06-21 16:30
 * 描  述：数据权限
 */
var interfaceId = request('interfaceId');
var objectId = request('objectId');
var objectType = request('objectType');
var keyValue = request('keyValue');

var acceptClick;
var queryDataList = [];
var bootstrap = function ($, learun) {
    "use strict";

    var refreshData = function (label, data, rowid) {// 刷新条件
        if (rowid != "") {
            queryDataList[rowid] = data;
        }
        else {
            rowid = queryDataList.length;
            queryDataList.push(data);
            var $item = $('<div class="lr-query-item" id="lr_query_item_' + rowid + '"><div class="lr-query-item-text"></div><div class="btn-group btn-group-sm"><a class="btn btn-default btn-edit">编辑</a><a class="btn btn-default btn-delete">删除</a></div></div>');
            $item.find('.btn-edit')[0].rowid = rowid;
            $item.find('.btn-delete')[0].rowid = rowid;
            $('#querylist').append($item);
        }
        $('#lr_query_item_' + rowid).find('.lr-query-item-text').html('<div class="lr-query-item-num">' + (rowid + 1) + '</div>' + label);
    };
    var loadData = function () {
        $('#querylist').html("");
        for (var i = 0, l = queryDataList.length; i < l; i++) {
            var _item = queryDataList[i];
            var $item = $('<div class="lr-query-item" id="lr_query_item_' + i + '"><div class="lr-query-item-text"></div><div class="btn-group btn-group-sm"><a class="btn btn-default btn-edit">编辑</a><a class="btn btn-default btn-delete">删除</a></div></div>');
            $item.find('.btn-edit')[0].rowid = i;
            $item.find('.btn-delete')[0].rowid = i;
            $('#querylist').append($item);
            $('#lr_query_item_' + i).find('.lr-query-item-text').html('<div class="lr-query-item-num">' + (i + 1) + '</div>' + "【" + _item.F_FieldName + "】 " + _item.F_SymbolName + " " + _item.F_FiledValue);
        }
    }

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 条件行
            $('#querylist').on('click', function (e) {
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('btn-edit')) {
                    var _rowid = $et[0].rowid;
                    learun.layerForm({
                        id: 'QueryForm',
                        title: '添加自定义查询条件',
                        url: top.$.rootUrl + '/LR_AuthorizeModule/DataAuthorize/QueryForm?interfaceId=' + interfaceId + '&rowid=' + _rowid,
                        width: 500,
                        height: 300,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshData);
                        }
                    });
                }
                else if ($et.hasClass('btn-delete')) {
                    var _rowid = $et[0].rowid;
                    queryDataList.splice(_rowid, 1);
                    loadData();
                }
            });

            // 添加条件
            $('#lr_query_add').on('click', function () {
                learun.layerForm({
                    id: 'QueryForm',
                    title: '添加自定义查询条件',
                    url: top.$.rootUrl + '/LR_AuthorizeModule/DataAuthorize/QueryForm?interfaceId=' + interfaceId,
                    width: 500,
                    height: 300,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshData);
                    }
                });
            });

            // 公式提示
            $('#lr-info').hover(function () { $('#lr-message').show(); }, function () { $('#lr-message').hide(); });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_AuthorizeModule/DataAuthorize/GetDataAuthorizeEntity?keyValue=' + keyValue, function (data) {
                    $('#form').lrSetFormData(data.relationEntity);
                    queryDataList = data.conditionEntity;

                    interfaceId = data.relationEntity.F_InterfaceId;
                    objectId = data.relationEntity.F_ObjectId;
                    objectType = data.relationEntity.F_ObjectType;

                    loadData();
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData(keyValue);
        formData.F_InterfaceId = interfaceId;
        formData.F_ObjectId = objectId;
        formData.F_ObjectType = objectType;

        if (queryDataList.length == 0) {
            learun.alert.error('请设置条件！');
            return false;
        }
        var postData = {
            conditions: JSON.stringify(queryDataList),
            relation: JSON.stringify(formData)
        };

        $.lrSaveForm(top.$.rootUrl + '/LR_AuthorizeModule/DataAuthorize/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
