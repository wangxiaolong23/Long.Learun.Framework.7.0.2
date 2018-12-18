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
                // 获取审核人员
                var auditers = {};
                $page.find('.nodeId').each(function () {
                    var $this = $(this);
                    var id = $this.attr('id');
                    var type = $this.attr('type');


                    if (formdata[id] && formdata[id] !== 'undefined' && formdata[id] !== undefined) {
                        var point = {
                            userId: formdata[id],
                        };
                        if (type === 'lrpicker') {
                            point.userName = $this.lrpickerGet('text');
                        }
                        else {
                            point.userName = $this.lrselectGet('text');
                        }
                        auditers[id] = point;
                    }
                });

                learun.nav.closeCurrent();
                setTimeout(function () {
                    var prepage = learun.nav.getpage("workflow/againbootstraper");
                    prepage.create(formdata, auditers);
                }, 300);
            });

            $page.find('#processLevel').lrpicker({
                placeholder: '请选择(必填)',
                data: [
                    { value: '0', text: '普通' },
                    { value: '1', text: '重要' },
                    { value: '2', text: '紧急' }
                ]
            });

            //加载下一节点审核者
            var req = {
                isNew: false,
                processId: param.processId,
                formData: param.formData
            }
            learun.httpget(config.webapi + "learun/adms/workflow/auditer", req, (data) => {
                if (data) {
                    var $des = $page.find('.lr-form-row-multi');
                    $.each(data, function (_index, _item) {
                        $des.before('<div class="lr-form-row"><label>' + _item.name + '</label><div id="' + _item.nodeId + '"  class="nodeId" ></div></div>');

                        if (_item.all || _item.list.length === 0) {
                            $page.find('#' + _item.nodeId).lrselect({ type: 'user' });
                        }
                        else {
                            $page.find('#' + _item.nodeId).lrpicker({ data: _item.list, itext: 'name', ivalue: 'id' });
                        }
                    });
                }
            });

        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    return page;
})();