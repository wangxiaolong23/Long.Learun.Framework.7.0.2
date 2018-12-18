
/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.03.16
 * 描 述：功能模块	
 */
var keyValue = request('keyValue');
var type = request('type');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var lrcomponts = [];

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            // 列表显示
            $('#listTitle').lrselect({
                type: 'multiple'
            });
            $('#listContent1').lrselect({ maxHeight: 160 });
            $('#listContent2').lrselect({ maxHeight: 160 });
            $('#listContent3').lrselect({ maxHeight: 160 });


            // 选择图标
            $('#selectIcon').on('click', function () {
                learun.layerForm({
                    id: 'iconForm',
                    title: '选择图标',
                    url: top.$.rootUrl + '/Utility/AppIcon',
                    height: 700,
                    width: 1000,
                    btn: null,
                    maxmin: true,
                    end: function () {
                        if (top._learunSelectIcon != '') {
                            $('#F_Icon').val(top._learunSelectIcon);
                        }
                    }
                });
            });
            // 分类
            $('#F_Type').lrDataItemSelect({ code: 'function' });
            $('#F_Type').lrselectSet(type);
            // 选在表单
            $('#F_FormId').lrselect({
                text: 'F_Name',
                value: 'F_Id',
                url: top.$.rootUrl + '/LR_FormModule/Custmerform/GetSchemeInfoList',
                maxHeight: 180,
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        lrcomponts = [];
                        $.lrSetForm(top.$.rootUrl + '/LR_FormModule/Custmerform/GetFormData?keyValue=' + item.F_Id, function (data) {
                            var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                            for (var i = 0, l = scheme.data.length; i < l; i++) {
                                var componts = scheme.data[i].componts;
                                for (var j = 0, jl = componts.length; j < jl; j++) {
                                    var compont = componts[j];
                                    if (compont.type == 'gridtable') {
                                    }
                                    else {
                                        var point = { text: compont.title, id: compont.id };
                                        lrcomponts.push(point);
                                    }
                                }
                            }

                            $('#listTitle').lrselectRefresh({
                                data: lrcomponts
                            });
                            $('#listContent1').lrselectRefresh({
                                data: lrcomponts
                            });
                            $('#listContent2').lrselectRefresh({
                                data: lrcomponts
                            });
                            $('#listContent3').lrselectRefresh({
                                data: lrcomponts
                            });
                        });
                    } else {
                        lrcomponts = [];
                        $('#listTitle').lrselectRefresh({
                            data: lrcomponts
                        });
                        $('#listContent1').lrselectRefresh({
                            data: lrcomponts
                        });
                        $('#listContent2').lrselectRefresh({
                            data: lrcomponts
                        });
                        $('#listContent3').lrselectRefresh({
                            data: lrcomponts
                        });
                    }
                }
            });
            $('#lr_preview').on('click', function () {
                var formId = $('#F_FormId').lrselectGet();
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

            // 功能类型
            $('[name="F_IsSystem"]').on('click', function () {
                var value = $(this).val();
                if (value == 1) {
                    $('.system').show();
                    $('.nosystem').hide();
                } else {
                    $('.system').hide();
                    $('.nosystem').show();
                }
            });


        },
        /*初始化数据*/
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/AppManager/FunctionManager/GetForm?keyValue=' + keyValue, function (data) {//
                    $('#form').lrSetFormData(data.entity);
                    if (data.entity.F_IsSystem != 1 && data.schemeEntity) {
                        var scheme = JSON.parse(data.schemeEntity.F_Scheme);
                        $('#listTitle').lrselectSet(scheme.title);
                        $('#listContent1').lrselectSet(scheme.content[0]);
                        $('#listContent2').lrselectSet(scheme.content[1]);
                        $('#listContent3').lrselectSet(scheme.content[2]);
                    }
                });
            }
        }
    };


    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData(keyValue);
        formData.F_IsSystem = $('[name="F_IsSystem"]:checked').val();
        var entity = {
            F_Name: formData.F_Name,
            F_Icon: formData.F_Icon,
            F_Type: formData.F_Type,
            F_SortCode: formData.F_SortCode,
            F_FormId: formData.F_FormId,
            F_SchemeId: formData.F_SchemeId,
            F_Url: formData.F_Url,
            F_IsSystem: formData.F_IsSystem,
        };
        if (formData.F_IsSystem == 1) {
            if ($.trim(formData.F_Url || '') == '') {
                learun.alert.error('请填写功能地址!');
                return false;
            }
        }
        else {
            if ($.trim(formData.listTitle || '') == '') {
                learun.alert.error('请设置列表展示!');
                return false;
            }
        }

        var scheme = {
            title: formData.listTitle,
            content:['','','']
        };
        scheme.content[0] = formData.listContent1;
        scheme.content[1] = formData.listContent2;
        scheme.content[2] = formData.listContent3;

        var schemeEntity = {
            F_Scheme: JSON.stringify(scheme)
        }


        // 提交数据
        var postData = {
            strEntity: JSON.stringify(entity),
            strSchemeEntity: JSON.stringify(schemeEntity),
        };


        $.lrSaveForm(top.$.rootUrl + '/AppManager/FunctionManager/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };


    page.init();
}