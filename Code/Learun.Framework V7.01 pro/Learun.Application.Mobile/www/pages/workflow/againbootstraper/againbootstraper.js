(function () {
    var processId = '';
    var taskId = '';

    var $header = null;
    var headText = '';

    var fieldMap = {};
    var formMap = {};
    var formDataes;
    var formAllData;
    var formreq;

    var getFormData = function ($page) {
        formDataes = $page.find('#auditcontainer').custmerformGet();
        if (formDataes == null) {
            return false;
        }
        formreq = [];
        formAllData = {};
        for (var id in formDataes) {
            if (!fieldMap[id]) {
                learun.layer.warning('未设置流程表单关联字段！', function () { }, '力软提示', '关闭');
                return false;
            }
            $.extend(formAllData, formDataes[id]);
            if (!formMap[id]) {
                formDataes[id][fieldMap[id]] = processId;
            }
            var point = {
                schemeInfoId: id,
                processIdName: fieldMap[id],
                formData: JSON.stringify(formDataes[id])
            }

            if (formMap[id]) {
                point.keyValue = processId;
            }
            formreq.push(point);
        }

        return true;
    };

    var page = {
        init: function ($page, param) {
            var _html = '<div class="lr-form-header-submit" style="display:block;" >发起</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            headText = $header.find('.f-page-title').text();

            $page.find('.lr-againbootstraper-page').toptab(['表单信息', '流程信息']).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        $this.html('<div class="container" id="againbootstrapercontainer1"></div>');
                        break;
                    case 1:
                        $this.html('<div class="container" id="againbootstrapercontainer2"></div>');
                        break;
                }
                $this = null;
            });
            processId = param.processId;
            taskId = param.taskId;
            taskinfo(param);

            // 审核
            $header.find('.lr-form-header-submit').on('tap', function () {
                if (!getFormData($page)) {
                    return false;
                }
                learun.nav.go({ path: 'workflow/againbootstraper/form', title: headText + "【发起】", type: 'right', param: { processId: processId, formData: JSON.stringify(formAllData) } });
            });
        },
        create: function (info, auditers) {// info审核信息
            var flowreq = {
                isNew: false,
                processId: processId,
                processName: info.processName,
                processLevel: info.processLevel,
                description: info.description,
                formData: JSON.stringify(formAllData),
                auditers: JSON.stringify(auditers),

                formreq: JSON.stringify(formreq)
            };
            learun.layer.loading(true, "创建流程,请等待！");
            learun.httppost(config.webapi + "learun/adms/workflow/create", flowreq, (data) => {
                learun.layer.loading(false);
                var prepage = learun.nav.getpage('workflow/mytask');
                prepage.grid[prepage.currentPage].reload();
                learun.nav.closeCurrent();
            });
        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    // 流程发起初始化
    function taskinfo(_param) {
        fieldMap = {};
        var req = {
            processId: _param.processId,
            isNew: false
        };
        learun.layer.loading(true, "获取流程信息");
        learun.httpget(config.webapi + "learun/adms/workflow/bootstraper", req, (data) => {
            learun.layer.loading(false);
            if (data) {
                var flowdata = data;
                if (flowdata.status == 1) {// 流程数据加载成功
                    var wfForms = flowdata.data.currentNode.wfForms;// 表单数据
                    // 获取下关联字段
                    var formreq = [];
                    var schemeIds = [];
                    $.each(wfForms, function (_index, _item) {
                        fieldMap[_item.formId] = _item.field;
                        schemeIds.push(_item.formId);
                        var point = {
                            schemeInfoId: _item.formId,
                            processIdName: _item.field,
                            keyValue: _param.processId,
                        }
                        formreq.push(point);
                    });

                    learun.custmerform.loadScheme(schemeIds, function (scheme) {
                        $('#againbootstrapercontainer1').custmerform(scheme);
                        // 设置表单的可查看权限
                        $.each(flowdata.data.currentNode.authorizeFields || [], function (_index, _item) {
                            if (_item.isLook === 0) {
                                $('#againbootstrapercontainer1').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                            }
                            else if (_item.isEdit === 0) {
                                $('#againbootstrapercontainer1').find('#' + _item.fieldId).parents('.lr-form-row').attr('readonly', 'readonly');
                            }
                        });
                    });
                    formMap = {};
                    // 获取下自定义表单数据
                    learun.httpget(config.webapi + "learun/adms/form/data", formreq, (data) => {
                        if (data) {// 加载表单
                            // 设置自定义表单数据
                            $.each(data, function (_id, _item) {
                                $.each(_item, function (_j, _jitem) {
                                    if (_jitem.length > 0) {
                                        formMap[_id] = true;
                                    }
                                });
                            });
                            $('#againbootstrapercontainer1').custmerformSet(data);

                        }
                    });

                    // 加载流程信息
                    initTimeLine(flowdata.data.history);
                }
            }
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
        $('#againbootstrapercontainer2').ftimeline(nodelist);
    }
    return page;
})();