/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：部门管理	
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var companyId = '';
    var departmentId = '';
    var page = {
        init: function () {
            page.inittree();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 部门选择
            $('#department_select').lrselect({
                type: 'tree',
                placeholder: '请选择部门',
                // 展开最大高度
                maxHeight: 300,
                // 是否允许搜索
                allowSearch: true,
                select: function (item) {

                    if (!item || item.value == '-1') {
                        departmentId = '';
                    }
                    else {
                        departmentId = item.value;
                    }
                    page.search();
                }
            });

            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                if (!companyId) {
                    learun.alert.warning('请选择公司！');
                    return false;
                }
                selectedRow = null;
                learun.layerForm({
                    id: 'form',
                    title: '添加岗位',
                    url: top.$.rootUrl + '/LR_OrganizationModule/Post/Form?companyId=' + companyId,
                    width: 500,
                    height: 379,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PostId');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑岗位',
                        url: top.$.rootUrl + '/LR_OrganizationModule/Post/Form?companyId=' + companyId,
                        width: 500,
                        height: 379,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PostId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OrganizationModule/Post/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 添加岗位成员
            $('#lr_memberadd').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PostId');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '添加岗位成员',
                        url: top.$.rootUrl + '/LR_AuthorizeModule/UserRelation/SelectForm?objectId=' + keyValue + '&companyId=' + companyId + '&departmentId=' + selectedRow.F_DepartmentId + '&category=2',
                        width: 800,
                        height: 520,
                        callBack: function (id) {
                            return top[id].acceptClick();
                        }
                    });
                }
            });
            // 产看成员
            $('#lr_memberlook').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_PostId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '查看岗位成员',
                        url: top.$.rootUrl + '/LR_AuthorizeModule/UserRelation/LookForm?objectId=' + keyValue,
                        width: 800,
                        height: 520,
                        btn: null
                    });
                }
            });
        },
        inittree: function () {
            $('#companyTree').lrtree({
                url: top.$.rootUrl + '/LR_OrganizationModule/Company/GetTree',
                param: { parentId: '0' },
                nodeClick: page.treeNodeClick
            });
            $('#companyTree').lrtreeSet('setValue', '53298b7a-404c-4337-aa7f-80b2a4ca6681');
        },
        treeNodeClick: function (item) {
            companyId = item.id;
            $('#titleinfo').text(item.text);

            $('#department_select').lrselectRefresh({
                // 访问数据接口地址
                url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',
                // 访问数据接口参数
                param: { companyId: companyId, parentId: '0' },
            });
            departmentId = '';
            page.search();
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_OrganizationModule/Post/GetList',
                headData: [
                        { label: "岗位名称", name: "F_Name", width: 300, align: "left" },
                        { label: "岗位编号", name: "F_EnCode", width: 100, align: "left" },
                        {
                            label: "所属部门", name: "F_DepartmentId", width: 120, align: "left",
                            formatterAsync: function (callback, value) {
                                learun.clientdata.getAsync('department', {
                                    key: value,
                                    companyId: companyId,
                                    callback: function (item) {
                                        callback(item.F_FullName);
                                    }
                                });
                            }
                        },
                        { label: "备注", name: "F_Description", width: 200, align: "left" },
                        { label: "创建人", name: "F_CreateUserName", width: 100, align: "left" },
                        {
                            label: "创建时间", name: "F_CreateDate", width: 100, align: "left",
                            formatter: function (cellvalue) {
                                return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                            }
                        }
                ],

                isTree: true,
                mainId: 'F_PostId',
                parentId: 'F_ParentId',
            });
        },
        search: function (param) {
            param = param || {};
            param.companyId = companyId;
            param.departmentId = departmentId;

            $('#gridtable').jfGridSet('reload', param);
        }
    };

    refreshGirdData = function () {
        page.search();
    };

    page.init();
}


