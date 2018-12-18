/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：工作流操作界面
 */
var tabIframeId = request('tabIframeId');  // 当前窗口ID
var shcemeCode = request('shcemeCode');    // 流程模板编号
var type = request('type');                // 操作类型 0.创建 1.审批 2.重新创建 3.确认阅读 4.加签 100 流程进度查看

var processId = request('processId');      // 流程实例主键
var taskId = request('taskId');            // 任务主键

var currentNode;
var flowScheme;
var flowHistory = [];
var currentIds = [];

var allFormDatas = {};                     // 表单数据 

var bootstrap = function ($, learun) {
    "use strict";

    // 表单页面对象集合
    var formIframes = [];
    var formIframesData = {};
    var formIframesHave = {};

   
    var flow = {
        release: function (isNew) { // 发起流程方法
            // 验证表单数据完整性和获取表单数据
            for (var i = 0, l = formIframes.length; i < l; i++) {
                if (!formIframes[i].validForm()) {
                    return false;
                }
                var data = (!!formIframes[i].getFormData ? formIframes[i].getFormData() : {});
                $.extend(allFormDatas, data || {});
            }

            learun.layerForm({
                id: 'ReleaseFlowForm',
                title: '发起流程',
                url: top.$.rootUrl + '/LR_WorkFlowModule/WfMyTask/ReleaseFlowForm',
                width: 600,
                height: 400,
                callBack: function (id) {
                    return top[id].acceptClick(function (formData, auditers) {
                        // 保存数据
                        for (var i = 0, l = formIframes.length; i < l; i++) {
                            if (formIframesHave[i] != 1) {
                                formIframes[i].save(processId, function (res, data, _index) {
                                    if (res.code == 200) {
                                        formIframesHave[_index] = 1;
                                        $.extend(allFormDatas, data || {});
                                    }
                                    else {
                                        formIframesHave[_index] = 0;
                                    }
                                }, i);
                            }
                        }
                        createProcess();
                        function createProcess() {
                            var num = 0;
                            var flag = true;
                            for (var i = 0, l = formIframes.length; i < l; i++) {
                                if (formIframesHave[i] == 0) {
                                    num++;
                                    flag = false;
                                }
                                else if (formIframesHave[i] == 1) {
                                    num++;
                                }
                            }
                            if (num == formIframes.length) {
                                if (flag) {
                                    // 发起流程
                                    learun.workflowapi.create({
                                        isNew: isNew,
                                        schemeCode: shcemeCode,
                                        processId: processId,
                                        processName: formData.processName,
                                        processLevel: formData.processLevel,
                                        description: formData.description,
                                        auditers: JSON.stringify(auditers),
                                        formData: JSON.stringify(allFormDatas),
                                        callback: function (res, data) {
                                            if (res) {
                                                learun.frameTab.parentIframe().refreshGirdData();
                                                learun.frameTab.close(tabIframeId);
                                            }                                            
                                        }
                                    });
                                }
                                else {
                                    learun.alert.error('表单数据保存失败');
                                }
                            }
                            else {
                                setTimeout(function () {
                                    createProcess();
                                }, 100);
                            }
                        }
                    });
                }
            });
        },
        audit: function () { // 审核流程
            for (var i = 0, l = formIframes.length; i < l; i++) {
                if (!formIframes[i].validForm()) {
                    return false;
                }
                var data = (!!formIframes[i].getFormData ? formIframes[i].getFormData() : {});
                $.extend(allFormDatas, data || {});
            }


            learun.layerForm({
                id: 'AuditFlowForm',
                title: '审核流程',
                url: top.$.rootUrl + '/LR_WorkFlowModule/WfMyTask/AuditFlowForm',
                width: 600,
                height: 400,
                callBack: function (id) {
                    return top[id].acceptClick(function (formData, auditers) {
                        // 保存数据
                        for (var i = 0, l = formIframes.length; i < l; i++) {
                            if (formIframesHave[i] != 1) {
                                formIframes[i].save(processId, function (res, data, _index) {
                                    if (res.code == 200) {
                                        formIframesHave[_index] = 1;
                                        $.extend(allFormDatas, data || {});
                                    }
                                    else {
                                        formIframesHave[_index] = 0;
                                    }
                                }, i);
                            }
                        }
                        auditProcess();
                        function auditProcess() {
                            var num = 0;
                            var flag = true;
                            for (var i = 0, l = formIframes.length; i < l; i++) {
                                if (formIframesHave[i] == 0) {
                                    num++;
                                    flag = false;
                                }
                                else if (formIframesHave[i] == 1) {
                                    num++;
                                }
                            }
                            if (num == formIframes.length) {
                                if (flag) {
                                    // 审核流程
                                    var verifyType = formData.verifyType;
                                    if (type == 4) {// 加签的情况
                                        verifyType = parseInt(verifyType) + 3;
                                    }
                                    learun.workflowapi.audit({
                                        taskId: taskId,
                                        verifyType: verifyType,
                                        description: formData.description,
                                        formData: JSON.stringify(allFormDatas),
                                        auditers: JSON.stringify(auditers),
                                        callback: function (res, data) {
                                            if (res) {
                                                learun.frameTab.parentIframe().refreshGirdData();
                                                learun.frameTab.close(tabIframeId);
                                            }                                            
                                        }
                                    });
                                }
                                else {
                                    learun.alert.error('表单数据保存失败');
                                }
                            }
                            else {
                                setTimeout(function () {
                                    auditProcess();
                                }, 100);
                            }
                        }
                    });
                }
            });
        },
        sign: function () { // 加签流程
            learun.layerForm({
                id: 'AuditFlowForm',
                title: '加签审核',
                url: top.$.rootUrl + '/LR_WorkFlowModule/WfMyTask/SignFlowForm',
                width: 600,
                height: 400,
                callBack: function (id) {
                    return top[id].acceptClick(function (formData) {
                        // 验证表单数据完整性
                        for (var i = 0, l = formIframes.length; i < l; i++) {
                            if (!formIframes[i].validForm()) {
                                return false;
                            }
                        }
                        // 保存数据
                        for (var i = 0, l = formIframes.length; i < l; i++) {
                            if (formIframesHave[i] != 1) {
                                formIframes[i].save(processId, function (res, data, _index) {
                                    if (res.code == 200) {
                                        formIframesHave[_index] = 1;
                                        $.extend(formData, data || {});
                                    }
                                    else {
                                        formIframesHave[_index] = 0;
                                    }
                                }, i);
                            }
                        }
                        signProcess();
                        function signProcess() {
                            var num = 0;
                            var flag = true;
                            for (var i = 0, l = formIframes.length; i < l; i++) {
                                if (formIframesHave[i] == 0) {
                                    num++;
                                    flag = false;
                                }
                                else if (formIframesHave[i] == 1) {
                                    num++;
                                }
                            }
                            if (num == formIframes.length) {
                                if (flag) {
                                    // 审核流程
                                    learun.workflowapi.audit({
                                        taskId: taskId,
                                        verifyType: 3,
                                        auditorId: formData.auditorId,
                                        auditorName: formData.auditorName,
                                        description: formData.description,
                                        formData: JSON.stringify(allFormDatas),
                                        callback: function (res, data) {
                                            if (res) {
                                                learun.frameTab.parentIframe().refreshGirdData();
                                                learun.frameTab.close(tabIframeId);
                                            }
                                        }
                                    });
                                }
                                else {
                                    learun.alert.error('表单数据保存失败');
                                }
                            }
                            else {
                                setTimeout(function () {
                                    signProcess();
                                }, 100);
                            }
                        }
                    });
                }
            });
        },
    };

    var page = {
        init: function () {
            page.bind();
            page.initflow();
        },
        bind: function () {
            // 显示信息选项卡
            $('#tablist').lrFormTabEx(function (id) {
                if (id == 'workflowshcemeinfo') {
                    $('#print').hide();
                }
                else
                {
                    $('#print').show();
                }
            });

            // 表单选项卡点击事件
            $('#form_list_tabs').delegate('a', 'click', function () {
                var $this = $(this);
                if (!$this.hasClass('active')) {
                    $this.parents('ul').find('.active').removeClass('active');
                    $this.parent().addClass('active');

                    var value = $this.attr('data-value');
                    var $iframes = $('#form_list_iframes');
                    $iframes.find('.active').removeClass('active');
                    $iframes.find('[data-value="' + value + '"]').addClass('active');                   
                }
            });

            $('#flow').lrworkflow({
                isPreview: true,
                openNode: function (node) {
                    currentNode = node;
                    
                    if (!!node.history) {
                        learun.layerForm({
                            id: 'WfNodeForm',
                            title: '审核记录查看【' + node.name + '】',
                            url: top.$.rootUrl + '/LR_WorkFlowModule/WfMyTask/WfNodeForm',
                            width: 600,
                            height: 400,
                            btn: null
                        });
                    }
                }
            });

            // 打印表单
            $('#print').on('click', function () {
                var $iframes = $('#form_list_iframes');
                var iframeId = $iframes.find('.form-list-iframe.active').attr('id');
                var $iframe = learun.iframe(iframeId, frames);
                $iframe.$('.lr-form-wrap:visible').jqprint();
            });
            $('#print').show();
        },
        // 初始化流程信息
        initflow: function () {
            switch (type) {
                case '0':
                    // 生成流程实例ID
                    processId = learun.newGuid();
                case '2':
                    learun.workflowapi.bootstraper({
                        isNew: type == 0 ? true : false,
                        processId:processId,
                        schemeCode: shcemeCode,
                        callback: function (res, data) {
                            if (res) {
                                // 初始化页面组件
                                $('#release').on('click', function () {
                                    if (type == '0') {
                                        flow.release(true);
                                    }
                                    else {
                                        flow.release(false);
                                    }
                                    
                                });
                                $('#release').show();
                                //$('#savedraft').show();草稿按钮暂时注释掉

                                // 初始化表单信息
                                var startnode = data.currentNode;
                                currentNode = startnode;

                                var $ul = $('#form_list_tabs');
                                var $iframes = $('#form_list_iframes');
                                for (var i = 0, l = startnode.wfForms.length; i < l; i++) {
                                    var forminfo = startnode.wfForms[i];
                                    $ul.append('<li><a data-value="' + i + '" >' + forminfo.name + '</a></li>');
                                    $iframes.append('<iframe id="wfFormIframe' + i + '" class="form-list-iframe" data-value="' + i + '" frameborder="0" ></iframe>');
                                    if (i == 0) {
                                        $ul.find('a').trigger('click');
                                    }
                                    formIframesData[i] =forminfo ;
                                    page.iframeLoad("wfFormIframe" + i, forminfo.url, function (iframeObj, _index) {
                                        // 设置字段权限
                                        iframeObj.setAuthorize(startnode.authorizeFields);
                                        if (!!formIframesData[_index].field) {
                                            iframeObj.processIdName = formIframesData[_index].field;
                                        }
                                        iframeObj.setFormData(processId);
                                    }, i);
                                }
                                // 优化表单选项卡滚动条
                                $('#form_list_tabs_warp').lrscroll();
                                // 初始化流程信息和审核记录信息
                                flowScheme = JSON.parse(data.scheme);
                                flowHistory = data.history || [];
                                currentIds = data.currentNodeIds || [];
                                initScheme();
                                initTimeLine(flowHistory);
                            }
                            else {
                                learun.frameTab.close(tabIframeId);
                            }
                        }
                    });
                    break;
                case '4':
                case '1':
                    learun.workflowapi.taskinfo({
                        processId: processId,
                        taskId:taskId,
                        callback: function (res, data) {
                            if (res) {
                                // 初始化页面组件
                                // 审核
                                $('#verify').on('click', function () {
                                    flow.audit();

                                });
                                $('#verify').show();
                                // 加签
                                $('#sign').on('click', function () {
                                    flow.sign();
                                });
                                $('#sign').show();
                                //$('#savedraft').show();草稿按钮暂时注释掉

                                // 初始化表单信息
                                currentNode = data.currentNode;

                                var $ul = $('#form_list_tabs');
                                var $iframes = $('#form_list_iframes');
                                for (var i = 0, l = currentNode.wfForms.length; i < l; i++) {
                                    var forminfo = currentNode.wfForms[i];
                                    $ul.append('<li><a data-value="' + i + '" >' + forminfo.name + '</a></li>');
                                    $iframes.append('<iframe id="wfFormIframe' + i + '" class="form-list-iframe" data-value="' + i + '" frameborder="0" ></iframe>');
                                    if (i == 0) {
                                        $ul.find('a').trigger('click');
                                    }
                                    formIframesData[i] = forminfo;
                                    page.iframeLoad("wfFormIframe" + i, forminfo.url, function (iframeObj, _index) {
                                        // 设置字段权限
                                        iframeObj.setAuthorize(currentNode.authorizeFields);
                                        if (!!formIframesData[_index].field) {
                                            iframeObj.processIdName = formIframesData[_index].field;
                                        }
                                        iframeObj.setFormData(processId);
                                    }, i);
                                }

                                // 优化表单选项卡滚动条
                                $('#form_list_tabs_warp').lrscroll();

                                // 初始化流程信息
                                flowScheme = JSON.parse(data.scheme);
                                flowHistory = data.history || [];
                                currentIds = data.currentNodeIds || [];
                                initScheme();
                                initTimeLine(flowHistory);
                            }
                            else {
                                learun.frameTab.close(tabIframeId);
                            }
                        }
                    });
                    break;
                case '3':
                    learun.workflowapi.taskinfo({
                        processId: processId,
                        taskId: taskId,
                        callback: function (res, data) {
                            if (res) {
                                $('#confirm').show();
                                $('#confirm').on('click', function () {
                                    learun.layerConfirm('是否确认阅读！', function (res, index) {
                                        if (res) {
                                            learun.workflowapi.audit({
                                                taskId: taskId,
                                                verifyType: '6',
                                                description: '',
                                                formData: JSON.stringify({}),
                                                auditers: JSON.stringify({}),
                                                callback: function (res, data) {
                                                    if (res) {
                                                        learun.frameTab.parentIframe().refreshGirdData();
                                                        learun.frameTab.close(tabIframeId);
                                                    }

                                                    learun.layerClose('', index); //再执行关闭  
                                                }
                                            });

                                        }
                                    });
                                });

                                // 初始化表单信息
                                currentNode = data.currentNode;

                                var $ul = $('#form_list_tabs');
                                var $iframes = $('#form_list_iframes');
                                for (var i = 0, l = currentNode.wfForms.length; i < l; i++) {
                                    var forminfo = currentNode.wfForms[i];
                                    $ul.append('<li><a data-value="' + i + '" >' + forminfo.name + '</a></li>');
                                    $iframes.append('<iframe id="wfFormIframe' + i + '" class="form-list-iframe" data-value="' + i + '" frameborder="0" ></iframe>');
                                    if (i == 0) {
                                        $ul.find('a').trigger('click');
                                    }
                                    formIframesData[i] = forminfo;
                                    page.iframeLoad("wfFormIframe" + i, forminfo.url, function (iframeObj, _index) {
                                        // 设置字段权限
                                        iframeObj.setAuthorize(currentNode.authorizeFields);
                                        if (!!formIframesData[_index].field) {
                                            iframeObj.processIdName = formIframesData[_index].field;
                                        }
                                        iframeObj.setFormData(processId);
                                    }, i);
                                }

                                // 优化表单选项卡滚动条
                                $('#form_list_tabs_warp').lrscroll();

                                // 初始化流程信息
                                flowScheme = JSON.parse(data.scheme);
                                flowHistory = data.history || [];
                                currentIds = data.currentNodeIds || [];
                                initScheme();
                                initTimeLine(flowHistory);

                            }
                            else {
                                learun.frameTab.close(tabIframeId);
                            }
                        }
                    });
                    break;
                case '100':
                    learun.workflowapi.processinfo({
                        processId: processId,
                        taskId: taskId,
                        callback: function (res, data) {
                            if (res) {
                                // 初始化表单信息
                                currentNode = data.currentNode;

                                var $ul = $('#form_list_tabs');
                                var $iframes = $('#form_list_iframes');
                                for (var i = 0, l = currentNode.wfForms.length; i < l; i++) {
                                    var forminfo = currentNode.wfForms[i];
                                    $ul.append('<li><a data-value="' + i + '" >' + forminfo.name + '</a></li>');
                                    $iframes.append('<iframe id="wfFormIframe' + i + '" class="form-list-iframe" data-value="' + i + '" frameborder="0" ></iframe>');
                                    if (i == 0) {
                                        $ul.find('a').trigger('click');
                                    }
                                    formIframesData[i] = forminfo;
                                    page.iframeLoad("wfFormIframe" + i, forminfo.url, function (iframeObj, _index) {
                                        // 设置字段权限
                                        iframeObj.setAuthorize(currentNode.authorizeFields);
                                        if (!!formIframesData[_index].field) {
                                            iframeObj.processIdName = formIframesData[_index].field;
                                        }
                                        iframeObj.setFormData(processId);
                                    }, i);
                                }

                                // 优化表单选项卡滚动条
                                $('#form_list_tabs_warp').lrscroll();

                                // 初始化流程信息
                                flowScheme = JSON.parse(data.scheme);
                                flowHistory = data.history || [];
                                currentIds = data.currentNodeIds || [];
                                initScheme();
                                initTimeLine(flowHistory);
                            }
                            else {
                                learun.frameTab.close(tabIframeId);
                            }
                        }
                    });
                    break;
                case '101':
                    learun.workflowapi.processinfoByMonitor({
                        processId: processId,
                        taskId: taskId,
                        callback: function (res, data) {
                            if (res) {
                                // 初始化表单信息
                                currentNode = data.currentNode;

                                var $ul = $('#form_list_tabs');
                                var $iframes = $('#form_list_iframes');
                                for (var i = 0, l = currentNode.wfForms.length; i < l; i++) {
                                    var forminfo = currentNode.wfForms[i];
                                    $ul.append('<li><a data-value="' + i + '" >' + forminfo.name + '</a></li>');
                                    $iframes.append('<iframe id="wfFormIframe' + i + '" class="form-list-iframe" data-value="' + i + '" frameborder="0" ></iframe>');
                                    if (i == 0) {
                                        $ul.find('a').trigger('click');
                                    }
                                    formIframesData[i] = forminfo;
                                    page.iframeLoad("wfFormIframe" + i, forminfo.url, function (iframeObj, _index) {
                                        // 设置字段权限
                                        iframeObj.setAuthorize(currentNode.authorizeFields);
                                        if (!!formIframesData[_index].field) {
                                            iframeObj.processIdName = formIframesData[_index].field;
                                        }
                                        iframeObj.setFormData(processId);
                                    }, i);
                                }

                                // 优化表单选项卡滚动条
                                $('#form_list_tabs_warp').lrscroll();

                                // 初始化流程信息
                                flowScheme = JSON.parse(data.scheme);
                                flowHistory = data.history || [];
                                currentIds = data.currentNodeIds || [];
                                initScheme();
                                initTimeLine(flowHistory);
                            }
                            else {
                                learun.frameTab.close(tabIframeId);
                            }
                        }
                    });
                    break;
            }
        },
        // iframe 加载
        iframeLoad: function (iframeId, url, callback, i) {
            var _iframe = document.getElementById(iframeId);
            var _iframeLoaded = function () {
                var iframeObj = learun.iframe(iframeId, frames);
                formIframes.push(iframeObj);
                if (!!iframeObj.$) {
                    callback(iframeObj,i);
                }
            };

            if (_iframe.attachEvent) {
                _iframe.attachEvent("onload", _iframeLoaded);
            } else {
                _iframe.onload = _iframeLoaded;
            }
            setTimeout(function () {
                $('#' + iframeId).attr('src', top.$.rootUrl + url);
            }, i * 500);
          
        }
    };


    function initScheme() {
        // 初始化工作流节点历史处理信息
        var nodeInfoes = {};
        $.each(flowHistory, function (id, item) {
            nodeInfoes[item.F_NodeId] = nodeInfoes[item.F_NodeId] || [];
            nodeInfoes[item.F_NodeId].push(item);
        });
        var strcurrentIds = String(currentIds);
        // 初始化节点状态
        for (var i = 0, l = flowScheme.nodes.length; i < l; i++) {
            var node = flowScheme.nodes[i];
            node.state = '3';
            if (!!nodeInfoes[node.id]) {
                node.history = nodeInfoes[node.id];
                if (nodeInfoes[node.id][0].F_Result == 1) {
                    node.state = '1';
                }
                else {
                    node.state = '2';
                }
            }
            if (strcurrentIds.indexOf(node.id) > -1) {
                node.state = '0';
            }
            if (currentNode.id == node.id) {
                node.state = '4';
            }
            //if (type != '101') {
            //    if (currentNode.id == node.id) {
            //        node.state = '4';
            //    }
            //}
        }
        $('#flow').lrworkflowSet('set', { data: flowScheme });
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


        $('#auditinfo').lrtimeline(nodelist);
    }

    page.init();


   
}