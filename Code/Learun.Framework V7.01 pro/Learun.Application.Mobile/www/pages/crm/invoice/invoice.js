(function () {
    var page = {
        grid: null,
        init: function ($page) {
            page.grid = $('#lr_crminvoice_list').lrpagination({
                lclass: page.lclass,
                rows: 10,                            // 每页行数
                getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                    page.loadData(param, callback, $page);
                },
                renderData: function (_index, _item, _$item) {// 渲染数据模板
                    return page.rowRender(_index, _item, _$item, $page);
                },
                click: function (item, $item) {// 列表行点击事件
                    page.click(item, $item, $page);
                }
            });

            // 点击搜索按钮
            $page.find('#lr_crminvoice_search_btn').on('tap', function () {
                learun.nav.go({ path: 'search', title: '', isBack: true, isHead: true, param: 'crm/invoice' });// 告诉搜索页本身所在的地址
            });
            // 新增开票信息
            $page.find('#crm_invoice_addbtn').on('tap', function () {
                learun.nav.go({ path: 'crm/invoice/form', title: '新增开票信息', type: 'right', param: {} });
            });
        },
        lclass: 'lr-list',
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
            learun.httpget(config.webapi + "learun/adms/crm/invoice/list", _postParam, (data) => {
                $page.find('.lr-badge').text('0');
                if (data) {
                    $page.find('.lr-badge').text(data.records);
                    callback(data.rows, parseInt(data.records));
                }
                else {
                    callback([], 0);
                }
            });
        },
        rowRender: function (_index, _item, _$item, $page) {// 渲染列表行数据
            var _html = '<div class="lr-list-item lr-list-item-multi">\
                            <p class="lr-ellipsis"><span>客户名称:</span>'+ _item.F_CustomerName + '</p>\
                            <p class="lr-ellipsis"><span>开票信息:</span>'+ _item.F_InvoiceContent + '</p>\
                        </div >';
            return _html;
        },
        click: function (item, $item, $page) {// 列表行点击触发方法
            learun.nav.go({ path: 'crm/invoice/form', title: '开票信息', type: 'right', param: { data: item, type: 'detail' } });
        },
        rowBtns: [] // 列表行左滑按钮
    };
    return page;
})();