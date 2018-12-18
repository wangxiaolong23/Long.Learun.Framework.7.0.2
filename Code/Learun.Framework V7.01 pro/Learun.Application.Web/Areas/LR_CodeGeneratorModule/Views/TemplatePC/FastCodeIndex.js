/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：自定义开发模板	
 */
var bootstrap = function ($, learun) {
    "use strict";
    var databaseLinkId = '';

    var rootDirectory = $('#rootDirectory').val();
    var postData = {};
    var page = {
        init: function () {
            page.bind();
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (data.step == 1) {
                        var dbTable = $('#dbtablegird').jfGridValue('name');
                        if (dbTable == '') {
                            learun.alert.error('请选择数据表');
                            return false;
                        }
                    }
                    else if (data.step == 2) {
                        if (!$('#step-2').lrValidform()) {
                            return false;
                        }
                        var formData = $('#step-2').lrGetFormData();
                        console.log(formData);
                        postData = {
                            databaseLinkId: databaseLinkId,
                            tableName: $('#dbtablegird').jfGridValue('name'),
                            name: formData.name,
                            describe: formData.describe,
                            backArea: formData.outputArea,
                            area: formData.outputArea,
                            mappingDirectory: $('#mappingDirectory').val(),
                            serviceDirectory: $('#serviceDirectory').val(),
                            webDirectory: $('#webDirectory').val()
                        };
                        learun.httpAsyncPost(top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/LookFastCode', postData, function (res) {
                            if (res.code == 200)
                            {
                                $.each(res.data, function (id, item) {
                                    $('#' + id).html('<textarea name="SyntaxHighlighter" class="brush: c-sharp;"></textarea>');
                                    $('#' + id + ' [name="SyntaxHighlighter"]').text(item);
                                });
                                SyntaxHighlighter.highlight();
                            }
                        });
                    }
                    else if (data.step == 3) {
                        $finish.removeAttr('disabled');
                        $next.attr('disabled', 'disabled');
                    }
                    else {
                        $finish.attr('disabled', 'disabled');
                    }
                } else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });

            // 数据表选择
            $('#dbId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
                allowSearch: true,
                select: function (item) {
                    if (item.hasChildren) {
                        databaseLinkId = '';
                        $('#dbtablegird').jfGridSet('refreshdata', []);
                    }
                    else if (dbId != item.id) {
                        databaseLinkId = item.id;
                        page.dbTableSearch();
                    }
                }
            });
            // 查询按钮
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.dbTableSearch({ tableName: keyword });
            });

            $('#dbtablegird').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetList',
                headData: [
                    { label: "表名", name: "name", width: 300, align: "left" },
                    {
                        label: "记录数", name: "sumrows", width: 100, align: "center",
                        formatter: function (cellvalue) {
                            return cellvalue + "条";
                        }
                    },
                    { label: "使用大小", name: "reserved", width: 100, align: "center" },
                    { label: "索引使用大小", name: "index_size", width: 120, align: "center" },
                    { label: "说明", name: "tdescription", width: 350, align: "left" }
                ],
                mainId: 'name',
                isSubGrid: true,             // 是否有子表
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList',
                        headData: [
                            { label: "列名", name: "f_column", width: 300, align: "left" },
                            { label: "数据类型", name: "f_datatype", width: 80, align: "center" },
                            { label: "长度", name: "f_length", width: 57, align: "center" },
                            {
                                label: "允许空", name: "f_isnullable", width: 50, align: "center",
                                formatter: function (cellvalue) {
                                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                                }
                            },
                            {
                                label: "标识", name: "f_identity", width: 50, align: "center",
                                formatter: function (cellvalue) {
                                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                                }
                            },
                            {
                                label: "主键", name: "f_key", width: 50, align: "center",
                                formatter: function (cellvalue) {
                                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                                }
                            },
                            { label: "说明", name: "f_remark", width: 200, align: "left" }
                        ]
                    });
                    $('#' + subid).jfGridSet('reload', { databaseLinkId: databaseLinkId, tableName: rowdata.name });
                }// 子表展开后调用函数
            });
            // 基础信息配置
            var loginInfo = learun.clientdata.get(['userinfo']);
            $('#createUser').val(loginInfo.realName);
            $('#outputArea').lrDataItemSelect({ code: 'outputArea' });

            $('#mappingDirectory').val(rootDirectory + $('#_mappingDirectory').val());
            $('#serviceDirectory').val(rootDirectory + $('#_serviceDirectory').val());
            $('#webDirectory').val(rootDirectory + $('#_webDirectory').val());
          
            // 代码查看
            $('#nav_tabs').lrFormTabEx();
            //$('#tab_content>div').lrscroll();
            // 发布功能
            // 上级
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/Module/GetExpendModuleTree',
                type: 'tree',
                maxHeight: 280,
                allowSearch: true
            });
            // 选择图标
            $('#selectIcon').on('click', function () {
                learun.layerForm({
                    id: 'iconForm',
                    title: '选择图标',
                    url: top.$.rootUrl + '/Utility/Icon',
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
            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        dbTableSearch: function (param) {
            param = param || {};
            param.databaseLinkId = databaseLinkId;
            $('#dbtablegird').jfGridSet('reload', param);
        },
        /*保存数据*/
        save: function () {
            var moduleData = $('#step-4').lrGetFormData();
            moduleData.F_EnabledMark = 1;
            postData.moduleEntityJson = JSON.stringify(moduleData);
            $.lrSaveForm(top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/CreateFastCode', postData, function (res) {

            });
        }
    };

    page.init();
}