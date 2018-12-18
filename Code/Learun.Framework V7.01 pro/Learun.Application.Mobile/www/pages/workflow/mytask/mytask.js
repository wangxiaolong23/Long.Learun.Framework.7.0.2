/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.7.12
 * 描 述：力软移动端框架(ADMS) 流程我的任务
 */
(function () {
    var begin = '';
    var end = '';

    var $header = null;

    var page = {
        currentPage: 0,
        grid:[],
        init: function ($page) {
            page.currentPage = 0;
            page.grid = [];
            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-btnlist" style="display:block;" >\
                    <div class="lr-form-header-more" ><i class="iconfont icon-searchlist" ></i></div>\
                    <div class="lr-form-header-edit" ><i class="iconfont icon-time" ></i></div>\
                </div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);

            // 设置查询条件
            $header.find('.lr-form-header-edit').searchdate({
                callback: function (_begin, _end) {
                    begin = _begin;
                    end = _end;
                    page.grid[page.currentPage].reload();
                }
            });
            // 点击搜索按钮
            $header.find('.lr-form-header-more').on('tap', function () {
                learun.nav.go({ path: 'search', title: '', isBack: true, isHead: true, param: 'workflow/mytask' });// 告诉搜索页本身所在的地址
            });
            $page.find('#mytask_tab').toptab(['我的', '待办', '已办'], function (_index) {
                page.currentPage = parseInt(_index);
                begin = "";
                end = "";
                page.grid[page.currentPage].reload();
            }).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        page.grid[index] = $this.lrpagination({
                            lclass: "lr-list lr-flow-list",
                            rows: 10,                            // 每页行数
                            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                                param.begin = begin;
                                param.end = end;
                                page.loadData(param, callback, $page);
                            },
                            renderData: function (_index, _item, _$item) {// 渲染数据模板
                                return page.rowRender(_index, _item, _$item, $page);
                            },
                            click: function (item, $item) {// 列表行点击事件
                                page.click(item, $item, $page);
                            }
                        });
                        break;
                    case 1:
                        page.grid[index] = $this.lrpagination({
                            lclass: "lr-list lr-flow-list",
                            rows: 10,                            // 每页行数
                            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                                param.begin = begin;
                                param.end = end;
                                page.loadData(param, callback, $page);
                            },
                            renderData: function (_index, _item, _$item) {// 渲染数据模板
                                return page.rowRender(_index, _item, _$item, $page);
                            },
                            click: function (item, $item) {// 列表行点击事件
                                page.click(item, $item, $page);
                            }
                        });
                        break;
                    case 2:
                        page.grid[index] = $this.lrpagination({
                            lclass: "lr-list lr-flow-list",
                            rows: 10,                            // 每页行数
                            getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                                param.begin = begin;
                                param.end = end;
                                page.loadData(param, callback, $page);
                            },
                            renderData: function (_index, _item, _$item) {// 渲染数据模板
                                return page.rowRender(_index, _item, _$item, $page);
                            },
                            click: function (item, $item) {// 列表行点击事件
                                page.click(item, $item, $page);
                            }
                        });
                        break;
                }
                $this = null;
            });
        },
        lclass: 'lr-list lr-flow-list',
        loadData: function (param, callback, $page) {// 列表加载后台数据
            var _postParam = {
                pagination: {
                    rows: param.rows,
                    page: param.page,
                    sidx: 'F_CreateDate',
                    sord: 'DESC'
                },
                queryJson: '{}'
            };
            if (param.keyword) {
                _postParam.queryJson = JSON.stringify({ keyword: param.keyword });
            }
            if (param.begin && param.end) {
                _postParam.queryJson = JSON.stringify({ StartTime: param.begin, EndTime: param.end });
            }
            var url = '';
            var mypage = learun.nav.getpage('workflow/mytask');
            switch (mypage.currentPage) {
                case 0:
                    url = config.webapi + "learun/adms/workflow/mylist";
                    break;
                case 1:
                    url = config.webapi + "learun/adms/workflow/mytask";
                    break;
                case 2:
                    url = config.webapi + "learun/adms/workflow/mytaskmaked";
                    break;
            }

            learun.httpget(url, _postParam, (data) => {
                if (data) {
                    callback(data.rows, parseInt(data.records));
                }
                else {
                    callback([], 0);
                }
            });
        },
        rowRender: function (_index, _item, _$item, $page) {// 渲染列表行数据
            var levelText = '';
            var levelbg = '';
            switch (_item.F_ProcessLevel) {
                case 0:
                    levelText = '普通';
                    levelbg = 'bgcblue1';
                    break;
                case 1:
                    levelText = '重要';
                    levelbg = 'bgcyellow';
                    break;
                case 2:
                    levelText = '紧急';
                    levelbg = 'bgcpink';
                    break;
            }
            statusText = '待审批';
            if (_item.F_TaskName) {
                statusText = '【' + _item.F_TaskName + '】' + statusText;
            }

            if (_item.F_IsFinished === 1) {
                statusText = '结束';
            }
            else if (_item.F_EnabledMark !== 1) {
                statusText = '暂停';
            }

            if (_item.F_IsAgain === 1) {
                statusText = '<span style="color:red;" >重新发起</span>';
            }

            var _html = '';
            var mypage = learun.nav.getpage('workflow/mytask');
            if (mypage.currentPage === 0) {
                _html = '<div class="lr-list-item">\
                    <div class="left" >\
                        <span class="circle '+ levelbg + '">' + levelText + '</span>\
                    </div >\
                    <div class="middle">\
                        <div class="title">'+ _item.F_ProcessName + '</div>\
                        <div class="text">'+ _item.F_SchemeName + '</div>\
                        <div class="status">'+ statusText + '</div>\
                    </div>\
                    <div class="right">'+ learun.date.format(_item.F_CreateDate, 'yyyy-MM-dd') + '</div>\
                </div>';
            }
            else {
                _html = '<div class="lr-list-item">\
                    <div class="left" >\
                        <span class="circle '+ levelbg + '">' + levelText + '</span>\
                    </div >\
                    <div class="middle">\
                        <div class="title">'+ _item.F_ProcessName + '</div>\
                        <div class="text">'+ _item.F_CreateUserName + '/' + _item.F_SchemeName + '</div>\
                        <div class="status">'+ statusText + '</div>\
                    </div>\
                    <div class="right">'+ learun.date.format(_item.F_CreateDate, 'yyyy-MM-dd') + '</div>\
                </div>';
            }
            return _html;
        },
        click: function (item, $item, $page) {// 列表行点击触发方法
            if (item.F_IsAgain === 1) {// 重新发起流程
                learun.nav.go({ path: 'workflow/againbootstraper', title: item.F_ProcessName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                return false;
            }


            var mypage = learun.nav.getpage('workflow/mytask');
            switch (mypage.currentPage) {
                case 0:
                    learun.nav.go({ path: 'workflow/processInfo', title: item.F_ProcessName, type: 'right', param: { processId: item.F_Id } });
                    break;
                case 1:
                    learun.nav.go({ path: 'workflow/audit', title: item.F_ProcessName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                    break;
                case 2:
                    learun.nav.go({ path: 'workflow/processInfo', title: item.F_ProcessName, type: 'right', param: { processId: item.F_Id, taskId: item.F_TaskId } });
                    break;
            }
            return false;
        },
        destroy: function (pageinfo) {
            page.currentPage = 0;
            page.grid = [];
        }
    };
    return page;
})();