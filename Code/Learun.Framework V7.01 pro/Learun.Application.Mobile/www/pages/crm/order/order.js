(function () {
    var begin = '';
    var end = '';
    var multipleData = null;

    var page = {
        grid: null,
        init: function ($page) {
            begin = '';
            end = '';
            multipleData = null;

            page.grid = $('#lr_crmorder_list').lrpagination({
                lclass: page.lclass,
                rows: 10,                            // 每页行数
                getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                    param.begin = begin;
                    param.end = end;
                    param.multipleData = multipleData;

                    page.loadData(param, callback, $page);
                },
                renderData: function (_index, _item, _$item) {// 渲染数据模板
                    return page.rowRender(_index, _item, _$item, $page);
                },
                click: function (item, $item, $et) {// 列表行点击事件
                    if ($et.hasClass('lr-btn-danger')) {
                        page.btnClick(item, $item, $page);
                    }
                    else {
                        page.rowClick(item, $item, $page);
                    }
                },
                btns: page.rowBtns
            });
            // 时间搜索
            $page.find('.lr_time_search').searchdate({
                callback: function (_begin, _end) {
                    begin = _begin;
                    end = _end;
                    multipleData = null;
                    page.grid.reload();
                }
            });
            // 多条件查询
            var $multiple = $page.find('.lr_multiple_search').multiplequery({
                callback: function (data) {
                    begin = '';
                    end = '';
                    multipleData = data || {};
                    page.grid.reload();
                }
            });
            // 客户名称
            $multiple.find('#customerId').lrpickerex({
                url: "learun/adms/crm/customer/list",
                ivalue: "F_CustomerId",
                itext: "F_FullName"
            });
            // 销售人员
            $multiple.find('#sellerId').lrselect({
                type: 'user'
            });
            // 客户名称
            $multiple.find('#paymentState').lrpickerex({
                type: 'dataItem',
                code: 'Client_PaymentMode'
            });


            // 新增开票信息
            $page.find('#crm_crmorder_btn').on('tap', function () {
                learun.nav.go({ path: 'crm/order/form', title: '新增订单', type: 'right', param: {} });
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
            if (param.multipleData) {
                _postParam.queryJson = JSON.stringify(multipleData);
            }

            if (param.begin && param.end) {
                _postParam.queryJson = JSON.stringify({ StartTime: param.begin, EndTime: param.end });
            }

            learun.httpget(config.webapi + "learun/adms/crm/order/pagelist", _postParam, (data) => {
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
            _$item.addClass('lr-list-item lr-list-item-multi');
            _$item.append($('<p class="lr-ellipsis"><span>单据日期:</span></p>').dataFormatter({
                type: 'datetime',
                value: _item.F_OrderDate,
                dateformat:'yyyy-MM-dd'
            }));
            _$item.append($('<p class="lr-ellipsis"><span>单据编号:</span></p>').dataFormatter({ value: _item.F_OrderCode}));
            _$item.append($('<p class="lr-ellipsis"><span>客户名称:</span></p>').dataFormatter({
                type: 'dataCustmer',
                value: _item.F_CustomerId,
                url: "learun/adms/crm/customer/list",
                keyId: 'F_CustomerId',
                text:'F_FullName'
            }));
            _$item.append($('<p class="lr-ellipsis"><span>销售人员:</span></p>').dataFormatter({
                type: 'organize',
                value: _item.F_SellerId,
                dataType: "user"
            }));
            _$item.append($('<p class="lr-ellipsis"><span>优惠金额:</span></p>').dataFormatter({ value: _item.F_DiscountSum }));
            _$item.append($('<p class="lr-ellipsis"><span>收款金额:</span></p>').dataFormatter({ value: _item.F_Accounts }));
            _$item.append($('<p class="lr-ellipsis"><span>收款方式:</span></p>').dataFormatter({
                type: 'dataItem',
                value: _item.F_PaymentMode,
                code: "Client_PaymentMode"
            }));
            var _paymentState = '<span class="lr-label lr-badge-danger" >未收款</span>';
            switch (_item.F_PaymentState) {
                case '2':
                    _paymentState = '<span class="lr-label lr-badge-success" >部分收款</span>';
                    break;
                case '3':
                    _paymentState = '<span class="lr-label lr-badge-primary" >全部收款</span>';
                    break;
                    
            }
            _$item.append('<p class="lr-ellipsis"><span>收款状态:</span>' + _paymentState + '</p>');

            _$item.append($('<p class="lr-ellipsis"><span>制单人员:</span></p>').dataFormatter({ value: _item.F_CreateUserName }));
            _$item.append($('<p class="lr-ellipsis"><span>备注:</span></p>').dataFormatter({ value: _item.F_Description }));
            
            return '';
        },
        rowClick: function (item, $item, $page) {// 列表行点击触发方法
            learun.nav.go({ path: 'crm/order/form', title: '订单详情', type: 'right', param: { keyValue: item.F_OrderId } });
        },
        btnClick: function (item, $item, $page) {// 左滑按钮点击事件
            learun.layer.confirm('确定要删除该笔数据吗？', function (_index) {
                if (_index === '1') {
                    learun.layer.loading(true, "正在删除该笔数据");
                    learun.httppost(config.webapi + "learun/adms/crm/order/delete", item.F_OrderId , (data) => {
                        if (data) {// 删除数据成功
                            page.grid.reload();
                        }
                        learun.layer.loading(false);
                    });
                }
            }, '力软提示', ['取消', '确定']);
        },
        rowBtns: ['<a class="lr-btn-danger">删除</a>'] // 列表行左滑按钮
    };
    return page;
})();