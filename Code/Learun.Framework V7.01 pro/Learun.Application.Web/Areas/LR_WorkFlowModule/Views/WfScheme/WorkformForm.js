/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：表单权限字段导入
 */
var id = request('id');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
           

            $('#field').lrselect({
                placeholder: '请选择字段',
                maxHeight: 80,
                allowSearch: true
            });
            $('#formId').lrselect({
                placeholder: '请选择表单',
                text: 'F_Name',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_FormModule/Custmerform/GetSchemeInfoList',
                maxHeight: 140,
                allowSearch: true,
                select: function (item) {
                    if (!!item) {
                        var formName = item.F_Name;
                        var url = '/LR_FormModule/Custmerform/WorkflowInstanceForm?id=' + item.F_Id;
                        $('#name').val(formName);
                        $('#url').val(url);
                        $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + item.F_Id, function (data) {
                            var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                            var fields = [];
                            for (var i = 0, l = scheme.data.length; i < l; i++) {
                                var componts = scheme.data[i].componts;
                                for (var j = 0, jl = componts.length; j < jl; j++) {
                                    var compont = componts[j];
                                    if (!!compont.title && !!compont.field) {
                                        var point = { text: compont.title, id: compont.id };
                                        fields.push(point);
                                    }
                                }
                            }
                            $('#field').lrselectRefresh({ data: fields });
                        });
                    }
                    else {
                        $('#field').lrselectRefresh({ data: [] });
                    }
                }
            });
            $('#lr_preview').on('click', function () {
                var formId = $('#formId').lrselectGet();
                if (!!formId) {
                    learun.layerForm({
                        id: 'custmerForm_PreviewForm',
                        title: '预览当前表单',
                        url: top.$.rootUrl + '/LR_FormModule/Custmerform/PreviewForm?schemeInfoId=' + formId,
                        width: 800,
                        height: 600,
                        maxmin: true,
                        btn: null
                    });
                }
                else {
                    learun.alert.warning('请选择表单！');
                }
            });


            $('#type').lrselect({
                data: [{ id: '1', text: '自定义表单' }, { id: '0', text: '系统表单' }],
                placeholder: false,
                maxHeight: 80,
                select: function (item) {
                    if (item.id != '1') {
                        $('.custmer-form').hide();
                    }
                    else {
                        $('.custmer-form').show();
                    }
                }
            }).lrselectSet('1');
        },
        initData: function () {
            if (!!id) {
                var workforms = top.layer_NodeForm.workforms;
                for (var i = 0, l = workforms.length; i < l; i++) {
                    if (workforms[i].id == id) {
                        $('#form').lrSetFormData(workforms[i]);
                        break;
                    }
                }
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formdata = $('#form').lrGetFormData();
        formdata.id = id;
        if (formdata.type != '1') {
            formdata.formId = '';
        }
        callBack(formdata);
        return true;
    };
    page.init();
}