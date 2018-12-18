(function () {
    var processId = '';
    var taskId = '';

    var page = {
        init: function ($page, param) {
            processId = param.processId || '';
            taskId = param.taskId || '';

            $page.find('.lr-processInfo-page').toptab(['表单信息', '流程信息']).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        $this.html('<div class="container" id="processInfocontainer1"></div>');
                        break;
                    case 1:
                        $this.html('<div class="container" id="processInfocontainer2"></div>');
                        break;
                }
                $this = null;
            });
            processinfo(param);
        }
    };
    // 流程发起初始化
    function processinfo(_param) {
        var req = {
            processId: _param.processId,
            taskId: _param.taskId
        };
        learun.layer.loading(true, "获取流程信息");
        learun.httpget(config.webapi + "learun/adms/workflow/processinfo", req, (data) => {
            if (data) {
                var flowdata = data;

                if (flowdata.status == 1) {// 流程数据加载成功
                    var wfForms = flowdata.data.currentNode.wfForms;// 表单数据
                    var schemeIds = [];
                    var formreq = [];
                    $.each(wfForms, function (_index, _item) {
                        if (_item.formId) {
                            schemeIds.push(_item.formId);
                            var point = {
                                schemeInfoId: _item.formId,
                                processIdName: _item.field,
                                keyValue: _param.processId,
                            }
                            formreq.push(point);
                        }
                    });
                    learun.custmerform.loadScheme(schemeIds, function (scheme) {
                        $('#processInfocontainer1').custmerform(scheme);

                        // 设置表单的可查看权限
                        $.each(flowdata.data.currentNode.authorizeFields || [], function (_index, _item) {
                            if (_item.isLook === 0) {
                                $('#processInfocontainer1').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                            }
                        });

                        $('#processInfocontainer1').setFormRead();
                    });
                    // 获取下自定义表单数据
                    learun.httpget(config.webapi + "learun/adms/form/data", formreq, (data) => {
                        if (data) {
                            // 设置自定义表单数据
                            $('#processInfocontainer1').custmerformSet(data);
                        }
                    });
                   
                    // 加载流程信息
                    initTimeLine(flowdata.data.history);

                }
            }
            learun.layer.loading(false);
        });
    }
    function initTimeLine(flowHistory) {
        var nodelist = [];
        for (var i = 0, l = flowHistory.length; i < l; i++) {
            var item = flowHistory[i];

            var content = '';
            if (item.F_Result == 1) {
                content += '通过';
            }
            else {
                content += '不通过';
            }
            if (item.F_Description) {
                content += '【备注】' + item.F_Description;
            }

            var point = {
                title: item.F_NodeName,
                people: item.F_CreateUserName + ':',
                content: content,
                time: item.F_CreateDate
            };
            nodelist.push(point);
        }
        $('#processInfocontainer2').ftimeline(nodelist);
    }
    return page;
})();