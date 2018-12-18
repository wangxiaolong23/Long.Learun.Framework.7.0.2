/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.01
 * 描 述：聊天记录查询	
 */
var userId = request('userId');
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyWord: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_IM/IMMsg/GetMsgPageList?userId=' + userId,
                headData: [
                    {
                        label: '发送人', name: 'F_SendUserId', width: 80, align: 'left',
                        formatterAsync(callback,cellvalue) {
                            var loginInfo = learun.clientdata.get(['userinfo']);
                            if (loginInfo.userId == cellvalue) {
                                callback('我');
                            }
                            else {
                                // 获取人员数据
                                learun.clientdata.getAsync('user', {
                                    key: cellvalue,
                                    callback: function (data, op) {
                                        callback(data.name);
                                    }
                                });
                            }
                        }
                    },
                    {
                        label: '创建时间', name: 'F_CreateDate', width: 130, align: 'left',
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    },
                    { label: '消息内容', name: 'F_Content', width: 200, align: 'left' }
                    

                ],
                mainId: 'F_MsgId',
                sidx: 'F_CreateDate',
                sord: 'DESC',
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            $('#gridtable').jfGridSet('reload', param);
        }
    };
    page.init();
}


