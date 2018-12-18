/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.04.28
 * 描 述：App端开发向导
 */
var bootstrap = function ($, learun) {
    "use strict";
    var formData;
    var page = {
        init: function () {
            $('#dataItem').on('click', function () {//数据字典
                top.learun.frameTab.open({ F_ModuleId: '4efd37bf-e3ef-4ced-8248-58eba046d78b', F_FullName: '数据字典', F_UrlAddress: '/LR_SystemModule/DataItem/Index' });
            });
            $('#dataSource').on('click', function () {//数据源
                top.learun.frameTab.open({ F_ModuleId: 'd967ce5c-1bdf-4bbf-967b-876abc3ea245', F_FullName: '数据源', F_UrlAddress: '/LR_SystemModule/DataSource/Index' });
            });

            $('#formDesign').on('click', function () {//表单设计
                top.learun.frameTab.open({ F_ModuleId: 'a57993fa-5a94-44a8-a330-89196515c1d9', F_FullName: '表单设计', F_UrlAddress: '/LR_FormModule/Custmerform/Index' });
            });

            $('#workflow').on('click', function () {//流程设计
                top.learun.frameTab.open({ F_ModuleId: 'f63a252b-975f-4832-a5be-1ce733bc8ece', F_FullName: '流程设计', F_UrlAddress: '/LR_WorkFlowModule/WfScheme/Index' });
            });



            $('#codecreate').on('click', function () {//代码生成器
                learun.layerForm({
                    id: 'CustmerCodeIndex',
                    title: '在线代码生成器 并自动创建代码(移动开发模板)',
                    url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/AppCustmerCodeIndex',
                    width: 1100,
                    height: 700,
                    maxmin: true,
                    btn: null
                });
            });

            $('#formRelease').on('click', function () {//表单功能发布
                top.learun.frameTab.open({ F_ModuleId: 'c32c15a2-8c69-4b96-8462-675a84f25acb', F_FullName: '功能发布', F_UrlAddress: '/AppManager/FunctionManager/Index' });
            });


            $('#desktop').on('click', function () {//首页设置
                top.learun.frameTab.open({ F_ModuleId: '5938418b-de82-4c4e-aca8-2f20ab02a37c', F_FullName: '首页设置', F_UrlAddress: '/LR_Desktop/DTSetting/AppIndex' });
            });

            $('#logosetting').on('click', function () {//logo设置
                top.learun.frameTab.open({ F_ModuleId: '50c1a78f-39d1-431c-875f-075c27d2ba28', F_FullName: 'logo设置', F_UrlAddress: '/LR_SystemModule/LogoImg/AppIndex' });
            });
        }
    };
    page.init();
}