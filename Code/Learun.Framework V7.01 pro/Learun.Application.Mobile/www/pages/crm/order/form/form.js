(function () {
    var keyValue = '';

    var $header = null;
    var titleText = '';

    var page = {
        isScroll: true,
        init: function ($page, param) {
            keyValue = param.keyValue;

            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-cancel" >取消</div>\
                <div class="lr-form-header-btnlist" >\
                    <div class="lr-form-header-more" ><i class="iconfont icon-more" ></i></div>\
                    <div class="lr-form-header-edit" ><i class="iconfont icon-edit" ></i></div>\
                </div>\
                <div class="lr-form-header-submit" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);

            // 取消
            $header.find('.lr-form-header-cancel').on('tap', function () {
                learun.layer.confirm('确定要退出当前编辑？', function (_index) {
                    if (_index === '1') {
                        if (keyValue) {// 如果是编辑状态
                            learun.formblur();
                            $header.find('.lr-form-header-cancel').hide();
                            $header.find('.lr-form-header-submit').hide();
                            $header.find('.lr-form-header-btnlist').show();
                            $header.find('.f-page-title').text(titleText);
                            $page.find('.lr-form-container').setFormRead();
                        }
                        else {// 如果是新增状态 关闭当前页面
                            learun.nav.closeCurrent();
                        }
                    }
                }, '力软提示', ['取消', '确定']);
            });
            // 编辑
            $header.find('.lr-form-header-edit').on('tap', function () {
                $header.find('.lr-form-header-btnlist').hide();
                $header.find('.lr-form-header-cancel').show();
                $header.find('.lr-form-header-submit').show();
                titleText = $header.find('.f-page-title').text();
                $header.find('.f-page-title').text('编辑');
                $page.find('.lr-form-container').setFormWrite();
            });
            // 更多
            $header.find('.lr-form-header-more').on('tap', function () {
                learun.actionsheet({
                    id: 'more',
                    data: [
                        {
                            text: '删除',
                            mark: true,
                            event: function () {// 删除当前条信息
                                learun.layer.confirm('确定要删除该笔数据吗？', function (_index) {
                                    if (_index === '1') {
                                        learun.layer.loading(true, "正在删除该笔数据");
                                        learun.httppost(config.webapi + "learun/adms/crm/order/delete", keyValue, (data) => {
                                            if (data) {// 删除数据成功
                                                learun.nav.closeCurrent();
                                                var prepage = learun.nav.getpage('crm/order');
                                                prepage.grid.reload();
                                            }
                                            learun.layer.loading(false);
                                        });
                                    }
                                }, '力软提示', ['取消', '确定']);
                            }
                        }
                    ],
                    cancel: function () {
                    }
                });
            });
            // 提交
            $header.find('.lr-form-header-submit').on('tap', function () {
                // 获取表单数据
                if (!$page.find('.lr-form-container').lrformValid()) {
                    return false;
                }
                var formData = $page.find('.lr-form-container').lrformGet();
                // 获取表格数据
                var girdData = $page.find('#productgird').lrgridGet();
                learun.layer.loading(true, "正在提交数据");

                var _postData = {
                    keyValue: keyValue,
                    crmOrderJson: JSON.stringify(formData),
                    crmOrderProductJson: JSON.stringify(girdData)
                };
                learun.httppost(config.webapi + "learun/adms/crm/order/save", _postData, (data) => {
                    learun.layer.loading(false);
                    if (data) {// 表单数据保存成功
                        if (keyValue) {
                            learun.layer.toast('保存数据成功!');
                            learun.formblur();
                            $header.find('.lr-form-header-cancel').hide();
                            $header.find('.lr-form-header-submit').hide();
                            $header.find('.lr-form-header-btnlist').show();
                            $header.find('.f-page-title').text(titleText);
                            $page.find('.lr-form-container').setFormRead();
                        }
                        else {// 如果是
                            learun.nav.closeCurrent();
                        }
                        var prepage = learun.nav.getpage('crm/order');
                        prepage.grid.reload();
                    }
                });
            });

            page.bind($page, param);

            if (keyValue) {
                // 添加编辑按钮
                $page.find('.lr-form-container').setFormRead();
                $header.find('.lr-form-header-btnlist').show();

                // 获取表单数据
                learun.layer.loading(true, '获取表单数据');
                learun.httpget(config.webapi + "learun/adms/crm/order/form", keyValue, (data) => {
                    if (data) {
                        $page.find('.lr-form-container').lrformSet(data.orderData);
                        $page.find('#productgird').lrgridSet(data.orderProductData);
                    }
                    learun.layer.loading(false);
                });
            }
            else {
                $header.find('.lr-form-header-cancel').show();
                $header.find('.lr-form-header-submit').show();
            }
        },
        bind: function ($page, param) {
            // 客户名称
            $page.find('#F_CustomerId').lrpickerex({
                url: "learun/adms/crm/customer/list",
                ivalue: "F_CustomerId",
                itext: "F_FullName"
            });
            // 销售人员
            $page.find('#F_SellerId').lrselect({
                type: 'user',
            });
            // 单据日期
            $page.find('#F_OrderDate').lrdate({
                type: 'date'
            });
            // 单据编号和制单人员
            if (!keyValue) {
                learun.getRuleCode('10000', function (data) {
                    $page.find('#F_OrderCode').val(data);
                });
                var userinfo = learun.storage.get('userinfo');
                $page.find('#F_CreateUserName').val(userinfo.baseinfo.realName);
            }
            // 初始化表格
            $page.find('#productgird').lrgrid({
                title: '产品明细',
                componts: [
                    {
                        name: '商品名称', field: 'F_ProductName', type: 'layer', code: 'Client_ProductInfo', datatype: 'dataItem',
                        layerData: [
                            { label: '商品编号', name: "F_ItemValue", value:'F_ProductCode'},
                            { label: '商品名称', name: "F_ItemName", value: 'F_ProductName' }
                        ],
                        change: function (data, $block) {
                            $block.find('#F_Qty').val('1');
                            $block.find('#F_Price').val('0');
                            $block.find('#F_Amount').val('0');
                            $block.find('#F_TaxRate').val('0');
                            $block.find('#F_Taxprice').val('0');
                            $block.find('#F_Tax').val('0');
                            $block.find('#F_TaxAmount').val('0');

                        }
                    },
                    {
                        name: '商品编号', field: 'F_ProductCode', type: 'label'
                    },
                    {
                        name: '单位', field: 'F_UnitId', type: 'input'
                    },
                    {
                        name: '数量', field: 'F_Qty', type: 'input',
                        change: function (v, $row) {
                            var _F_Price = parseFloat($row.find('#F_Price').val() || '0');
                            var _F_Qty = parseFloat(v || '0');
                            var _F_TaxRate = parseFloat($row.find('#F_TaxRate').val() || '0');

                            var _F_Amount = parseInt(_F_Price * _F_Qty);
                            var _F_TaxAmount = parseInt(_F_Price * (1 + _F_TaxRate / 100) * _F_Qty);

                            $row.find('#F_Amount').val(_F_Amount);
                            $row.find('#F_TaxAmount').val(_F_TaxAmount);
                            $row.find('#F_Tax').val(_F_TaxAmount - _F_Amount);
                        }
                    },
                    {
                        name: '单价', field: 'F_Price', type: 'input',
                        change: function (v, $row) {
                            var _F_Price = parseFloat(v|| '0');
                            var _F_Qty = parseFloat($row.find('#F_Qty').val() || '0');
                            var _F_TaxRate = parseFloat($row.find('#F_TaxRate').val() || '0');

                            var _F_Amount = parseInt(_F_Price * _F_Qty);
                            var _F_TaxAmount = parseInt(_F_Price * (1 + _F_TaxRate / 100) * _F_Qty);
                            var _F_Taxprice = parseInt(_F_Price * (1 + _F_TaxRate / 100));

                            $row.find('#F_Amount').val(_F_Amount);
                            $row.find('#F_TaxAmount').val(_F_TaxAmount);
                            $row.find('#F_Tax').val(_F_TaxAmount - _F_Amount);

                            $row.find('#F_Taxprice').val(_F_Taxprice);
                        }

                    },
                    { name: '金额', field: 'F_Amount', type: 'label' },
                    {
                        name: '税率(%)', field: 'F_TaxRate', type: 'input',
                        change: function (v, $row) {
                            var _F_Price = parseFloat($row.find('#F_Price').val() || '0');
                            var _F_Qty = parseFloat($row.find('#F_Qty').val() || '0');
                            var _F_TaxRate = parseFloat(v || '0');

                            var _F_Amount = parseInt(_F_Price * _F_Qty);
                            var _F_TaxAmount = parseInt(_F_Price * (1 + _F_TaxRate / 100) * _F_Qty);
                            var _F_Taxprice = parseInt(_F_Price * (1 + _F_TaxRate / 100));

                            $row.find('#F_Amount').val(_F_Amount);
                            $row.find('#F_TaxAmount').val(_F_TaxAmount);
                            $row.find('#F_Tax').val(_F_TaxAmount - _F_Amount);

                            $row.find('#F_Taxprice').val(_F_Taxprice);
                        }
                    },
                    { name: '含税单价', field: 'F_Taxprice', type: 'label' },
                    { name: '税额', field: 'F_Tax', type: 'label' },
                    { name: '含税金额', field: 'F_TaxAmount', type: 'label' },
                    {
                        name: "说明信息", field: "F_UnitId", type: 'input'
                    }
                ]
            });

            // 收款日期
            $page.find('#F_PaymentDate').lrdate({
                type: 'date'
            });

            // 收款方式
            $page.find('#F_PaymentMode').lrpickerex({
                code: 'Client_PaymentMode',
                type: 'dataItem'
            });
        },
        destroy: function (pageinfo) {
            $header = null;
            keyValue = '';
        }
    };
    return page;
})();