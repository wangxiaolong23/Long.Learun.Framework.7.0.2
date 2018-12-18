/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.06.04
 * 描 述：邮件管理	
 */
var bootstrap = function ($, learun) {
    "use strict";
    var datebegin = '';
    var dateend = '';
    var selectedRow = '';
    var currentPage='2';
    var refreshGirdData = function () {
        page.search();
    }

    var page = {
        init: function () {
            page.initleft();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            $('.datetime').each(function () {
                $(this).lrdate({
                    dfdata: [
                        { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                        { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                        { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                        { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    ],
                    // 月
                    mShow: false,
                    premShow: false,
                    // 季度
                    jShow: false,
                    prejShow: false,
                    // 年
                    ysShow: false,
                    yxShow: false,
                    preyShow: false,
                    yShow: false,
                    // 默认
                    dfvalue: '1',
                    selectfn: function (begin, end) {
                        datebegin = begin;
                        dateend = end;
                        page.search();
                    }
                });
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 发邮件
            $('#lr_sendemail').on('click', function () {
                learun.layerForm({
                    id: 'sendform',
                    title: '发送邮件',
                    url: top.$.rootUrl + '/LR_OAModule/Email/Form',
                    width: 800,
                    height: 700,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });

            // 收邮件
            $('#lr_getemail').on('click', function () {
                learun.loading(true);
                learun.httpAsyncGet(top.$.rootUrl + '/LR_OAModule/Email/GetMail', function (res) {
                    console.log(res);
                    learun.loading(false);
                    page.search();
                });
            });

            // 查看
            $('#lr_detail').on('click', function () {
                var keyValue = '';
                if (currentPage == '2') {
                    keyValue = $('#receivetable').jfGridValue('F_Id');
                }
                else {
                    keyValue = $('#sendtable').jfGridValue('F_Id');
                }
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'detailform',
                        title: '查看',
                        url: top.$.rootUrl + '/LR_OAModule/Email/DetailForm?keyValue=' + keyValue + '&type=' + currentPage,
                        width: 800,
                        height: 700,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });

            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = '';
                if (currentPage == '2') {
                    keyValue = $('#receivetable').jfGridValue('F_Id');
                }
                else {
                    keyValue = $('#sendtable').jfGridValue('F_Id');
                }
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/Email/DeleteForm', { keyValue: keyValue, type: currentPage }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initleft: function () {
            $('#lr_sendemail').hide();
            $('#sendtable').css('display', "none");
            $('#lr_left_list li').on('click', function () {
                var $this = $(this);
                var $parent = $this.parent();
                $parent.find('.active').removeClass('active');
                $this.addClass('active');
                var data_value=$this.context.dataset.value;
                switch (data_value) {
                    case "1":
                        $('#sendtable').css('display', "");
                        $('#receivetable').css('display', "none");
                        $('#lr_getemail').hide();
                        $('#lr_sendemail').show();
                        currentPage='1';
                        break;
                    case "2":
                        $('#sendtable').css('display', "none");
                        $('#receivetable').css('display', "");
                        $('#lr_sendemail').hide();
                        $('#lr_getemail').show();
                        currentPage='2';
                        break;
                    case "3":
                        $('#lr_sendemail').hide();
                        $('#lr_getemail').hide();
                        learun.layerForm({
                            id: 'configform',
                            title: '邮件配置',
                            url: top.$.rootUrl + '/LR_OAModule/Email/ConfigForm',
                            width: 400,
                            height: 450,
                            callBack: function (id) {
                                return top[id].acceptClick(refreshGirdData);
                            }
                        });
                        break;
                    default: break;
                }
            });
        },
        initGrid: function () {
            $('#sendtable').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Email/GetSendList',
                headData: [
                    { label: "收件人", name: "F_To", width: 200, align: "left" },
                    { label: "主题", name: "F_Subject", width: 450, align: "left" },
                    {
                        label: "发件时间", name: "F_CreatorTime", width: 135, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                sidx: 'F_CreatorTime',
            });

            $('#receivetable').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Email/GetReceiveList',
                headData: [
                    { label: "发件人", name: "F_SenderName", width: 200, align: "left" },
                    { label: "主题", name: "F_Subject", width: 450, align: "left" },
                    {
                        label: "收件时间", name: "F_Date", width: 135, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    }
                ],
                mainId: 'F_Id',
                isPage: true,
                sidx: 'F_Date',
                sord: 'DESC'
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = datebegin;
            param.EndTime = dateend;
            console.log(param);

            if (currentPage == '1') {
                $('#sendtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
            }
            else {
                $('#receivetable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
            }
        }
    };
    page.init();
}


