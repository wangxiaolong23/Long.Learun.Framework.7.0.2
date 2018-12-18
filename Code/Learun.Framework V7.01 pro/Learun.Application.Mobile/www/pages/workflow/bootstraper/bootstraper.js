(function () {
    var processId = '';
    var schemeCode = '';
    var fieldMap = {};
    var formDataes;
    var formAllData;
    var formreq;

    var $header = null;
    var headText = '';

    var page = {
        isScroll: false,
        init: function ($page, param) {
            schemeCode = param.schemeCode;

            var _html = '<div class="lr-form-header-submit" style="display:block;" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            headText = $header.find('.f-page-title').text();

            processId = learun.guid('-');;
            bootstraper($page);

            // 发起流程
            $header.find('.lr-form-header-submit').on('tap', function () {
                formDataes = $page.find('#flowBootstraper').custmerformGet();
                if (formDataes === null) {
                    return false;
                }
                formAllData = {};
                formreq = [];
                var flag = true;
                $.each(formDataes, function (id, formData) {
                    if (!fieldMap[id]) {
                        learun.layer.warning('未设置流程表单关联字段,请在流程设计中配置！', function () { }, '力软提示', '关闭');
                        flag = false;
                        return false;
                    }
                    $.extend(formAllData, formData);
                    formData[fieldMap[id]] = processId;

                    var point = {
                        schemeInfoId: id,
                        processIdName: fieldMap[id],
                        keyValue: '',
                        formData: JSON.stringify(formData)
                    }
                    formreq.push(point);
                });
                if (flag) {
                    learun.nav.go({
                        path: 'workflow/bootstraper/form', title: headText, type: 'right', param: {
                            formData: JSON.stringify(formAllData),
                            schemeCode: schemeCode
                        }
                    });
                }
            });
        },
        create: function (info, auditers) {// 提交创建流程
            var flowreq = {
                isNew: true,
                processId: processId,
                schemeCode: schemeCode,
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
                learun.nav.closeCurrent();
            });
        }
    };
    // 流程发起初始化
    function bootstraper($page) {
        var req = {
            isNew: true,
            schemeCode: schemeCode
        };

        learun.layer.loading(true, "获取流程模板信息");
        learun.httpget(config.webapi + "learun/adms/workflow/bootstraper", req, (flowdata) => {
            learun.layer.loading(false);
            if (flowdata) {
                if (flowdata.status === 1) {// 流程数据加载成功
                    var wfForms = flowdata.data.currentNode.wfForms;// 表单数据
                    var schemeIds = [];

                    // 获取下关联字段
                    $.each(wfForms, function (_index, _item) {
                        if (_item.formId) {
                            fieldMap[_item.formId] = _item.field;
                            schemeIds.push(_item.formId);
                        }
                    });
                    learun.custmerform.loadScheme(schemeIds, function (scheme) {
                        $page.find('#flowBootstraper').custmerform(scheme);
                        // 设置表单的可查看权限
                        $.each(flowdata.data.currentNode.authorizeFields || [], function (_index, _item) {
                            if (_item.isLook === 0) {
                                $('#auditcontainer').find('#' + _item.fieldId).parents('.lr-form-row').remove();
                            }
                            else if (_item.isEdit === 0) {
                                $('#auditcontainer').find('#' + _item.fieldId).parents('.lr-form-row').attr('readonly', 'readonly');
                            }
                        });
                    });
                }               
            }
        });
    }

    return page;
})();