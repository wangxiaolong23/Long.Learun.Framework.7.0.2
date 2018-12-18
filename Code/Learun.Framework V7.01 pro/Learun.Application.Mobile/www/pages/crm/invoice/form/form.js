(function () {
    var keyValue = '';

    var $header = null;
    var titleText = '';

    var page = {
        isScroll: true,
        init: function ($page, param) {
            keyValue = '';

            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-cancel" >取消</div>\
                <div class="lr-form-header-btnlist" >\
                    <div class="lr-form-header-edit" ><i class="iconfont icon-edit" ></i></div>\
                </div>\
                <div class="lr-form-header-submit" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            // 添加头部按钮事件
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
            // 提交
            $header.find('.lr-form-header-submit').on('tap', function () {
                // 保存数据到后台
                learun.layer.loading(true, "正在保存数据");
                var data = $page.find('.lr-form-container').lrformGet();
                data.F_CustomerName = $page.find('#F_CustomerId').lrpickerGet('text');
                learun.httppost(config.webapi + "learun/adms/crm/invoice/save", { keyValue: keyValue, entity: data }, (data) => {
                    if (data) {// 表单数据保存成功，发起流程
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

                        var prepage = learun.nav.getpage('crm/invoice');
                        prepage.grid.reload();
                    }
                    learun.layer.loading(false);
                });
            });

            $page.find('#F_CustomerId').lrpickerex({
                url: "learun/adms/crm/customer/list",
                ivalue: "F_CustomerId",
                itext: "F_FullName"
            });

            if (param && param.data) {
                // 添加编辑按钮
                $page.find('.lr-form-container').setFormRead();
                $header.find('.lr-form-header-btnlist').show();
                keyValue = param.data.F_InvoiceId;

                $page.find('.lr-form-container').lrformSet(param.data);
            }
            else {
                $header.find('.lr-form-header-cancel').show();
                $header.find('.lr-form-header-submit').show();
            }
        },
        destroy: function (pageinfo) {
            $header = null;
            keyValue = '';
        }
    };
    return page;
})();