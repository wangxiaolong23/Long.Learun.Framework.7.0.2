(function () {
    var $header = null;
    var page = {
        isScroll: true,
        init: function ($page, param) {
            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-cancel" style="display:block;" >取消</div>\
                <div class="lr-form-header-submit" style="display:block;" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            // 添加头部按钮事件
            // 取消
            $header.find('.lr-form-header-cancel').on('tap', function () {
                learun.nav.closeCurrent();
            });
            // 提交
            $header.find('.lr-form-header-submit').on('tap', function () {
                if (!$page.find('.lr-form-container').lrformValid()) {
                    return false;
                }


                var formdata = $page.find('.lr-form-container').lrformGet();
                formdata.auditorName = $page.find('#auditorId').lrselectGet('text');
                var prepage = learun.nav.getpage("workflow/audit");
                prepage.sign(formdata);
            });

            $page.find('#auditorId').lrselect({
                type: 'user'
            });
        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    return page;
})();