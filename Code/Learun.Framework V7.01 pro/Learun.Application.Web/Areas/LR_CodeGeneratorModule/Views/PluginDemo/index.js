/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：力软插件演示页面	
 */
var bootstrap = function ($, learun) {
    "use strict";
   
    var page = {
        init: function () {
            page.bind();
            page.initLeftTree();
        },
        bind: function () {
            $(".lr-tab-scroll-content").lrscroll();
        },
        initLeftTree: function () {
            $('#plugin_list').lrtree({
                data: pluginList,
                nodeClick: function (item) {
                    switch (item.value)
                    {
                        case 'learuntree':
                            $('#title_info').text(item.text);
                            $('#learun_tree_area').parent().find('.showarea-list-item.active').removeClass('active');
                            $('#learun_tree_area').addClass('active');
                            pluginlist.treeinit();
                            break;
                        case 'learunselect':
                            $('#title_info').text(item.text);
                            $('#learun_select_area').parent().find('.showarea-list-item.active').removeClass('active');
                            $('#learun_select_area').addClass('active');
                            pluginlist.selectinit();
                            break;
                        case 'learunuserselect':
                            $('#title_info').text(item.text);
                            $('#learun_selectuser_area').parent().find('.showarea-list-item.active').removeClass('active');
                            $('#learun_selectuser_area').addClass('active');
                            pluginlist.selectUserinit();
                            break;
                        case 'jfGrid':
                            $('#title_info').text(item.text);
                            $('#jfgrid_area').parent().find('.showarea-list-item.active').removeClass('active');
                            $('#jfgrid_area').addClass('active');
                            pluginlist.jfgridinit();
                            break;
                        case 'webUploader':
                            $('#title_info').text(item.text);
                            $('#uploader_area').parent().find('.showarea-list-item.active').removeClass('active');
                            $('#uploader_area').addClass('active');
                            pluginlist.uploaderInit();
                            break;
                    }
                }
            });
        }
    };

    //树插件
    var treeCode = {
        base:
function () {
    $('#tree_show_base').lrtree({
        data: [{
            id: '0',
            text: '父节点',
            value: 'no',
            hasChildren: true,
            isexpand: true,
            complete: true,
            ChildNodes: [
                {
                    id: '1',
                    text: '子节点一',
                    value: 'learuntree',
                    hasChildren: true,
                    isexpand: true,
                    complete: true,
                    ChildNodes: [
                        {
                            id: '2',
                            text: '子节点二',
                            value: 'learuntree',
                            complete: true
                        }
                    ]
                }
            ]
        }]
    });
},
        ajax:
function () {
    $('#tree_show_ajax').lrtree({
        url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetClassifyTree'
    });
},
        checkbox:
function () {
    $('#tree_show_checkbox').lrtree({
        data: [{
            id: '0',
            text: '父节点',
            value: 'no',
            showcheck: true,
            hasChildren: true,
            isexpand: true,
            complete: true,
            ChildNodes: [
                {
                    id: '1',
                    text: '子节点一',
                    value: 'learuntree',
                    hasChildren: true,
                    isexpand: true,
                    complete: true,
                    ChildNodes: [
                        {
                            id: '2',
                            text: '子节点二',
                            value: 'learuntree',
                            showcheck:true,
                            complete: true
                        },
                        {
                            id: '3',
                            text: '子节点三',
                            value: 'learuntree',
                            showcheck: true,
                            complete: true
                        }, {
                            id: '4',
                            text: '子节点四',
                            value: 'learuntree',
                            showcheck: true,
                            complete: true
                        }
                    ]
                },
                {
                    id: '11',
                    text: '子节点一一',
                    value: 'learuntree',
                    showcheck: true,
                    hasChildren: true,
                    isexpand: true,
                    complete: true,
                    ChildNodes: [
                        {
                            id: '12',
                            text: '子节点一二',
                            value: 'learuntree',
                            showcheck: true,
                            complete: true
                        }
                    ]
                }
            ]
        }]
    });
}
    }

    // jfgrid
    var initGrid = function () {
        $('#learun_jfgrid').jfGrid({
            isPage:true,

            isMultiselect: true,

            isSubGrid: true,    // 是否有子表单
            subGridRowExpanded: function () {

            },
            rowdatas: [
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 },
                { A: 'ra1', B1: 'rb11', B21: 'rb211', B22: 'rb221', C: 'rc1', D1: 'rd11', D2: 'rd21', E: 1 }
            ],
            headData: [
                { label: 'A', name: 'A', width: 80, align: 'left', frozen: true },
                {
                    label: 'B', name: 'B', width: 80, align: 'center', frozen: true,
                    children: [
                         { label: 'B1', name: 'B1', width: 80, align: 'center' },
                         {
                             label: 'B2', name: 'B2', width: 80, align: 'center',
                             children: [
                                  { label: 'B21', name: 'B21', width: 80, align: 'center' },
                                  {
                                      label: 'B21', name: 'B22', width: 80, align: 'center'
                                  }
                             ]
                         }
                    ]
                },
                { label: 'C', name: 'C', width: 80, align: 'right' },
                {
                    label: 'D', name: 'D', width: 80, align: 'center',
                    children: [
                        { label: 'D1', name: 'D1', width: 80, align: 'center' },
                        { label: 'D2', name: 'D2', width: 80, align: 'center' }
                    ]
                },
                {
                    label: "E", name: "E", width: 300, align: "left",
                    formatter: function (cellvalue) {
                        return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                    }
                }
            ]
        });
    };


    var pluginlist = {
        treeinit: function () {
            treeCode.base();

            treeCode.ajax();

            treeCode.checkbox();
        },
        selectinit: function () {
            var dfop = {
                type: 'tree',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/LR_OrganizationModule/Company/GetTree',
                // 访问数据接口参数
                param: { parentId: '0' },
            }
            $('#select1').lrselect(dfop);


            var dfop2 = {
                // 字段
                value: "F_AreaCode",
                text: "F_AreaName",
                title: "F_AreaName",
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/LR_SystemModule/Area/Getlist',
                // 访问数据接口参数
                param: { parentId: '' },
            }

            $('#select2').lrselect(dfop2);

            $('#select4').lrselect({
                // 字段
                value: "F_AreaCode",
                text: "F_AreaName",
                title: "F_AreaName",
                type: 'multiple',
                // 展开最大高度
                maxHeight: 200,
                // 是否允许搜索
                allowSearch: true,
                // 访问数据接口地址
                url: top.$.rootUrl + '/LR_SystemModule/Area/Getlist',
                // 访问数据接口参数
                param: { parentId: '' },
            });

            $('#select5').lrGirdSelect({
                // 字段
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',
                param: { itemCode: 'Client_ProductInfo' },
                selectWord: 'F_ItemName',
                value: 'F_ItemValue',
                text: 'F_ItemName',
                headData: [{ label: "商品编号", name: "F_ItemValue", width: 100, align: "left" },
                    { label: "商品名称", name: "F_ItemName", width: 450, align: "left" }],
                select: function (item) {
                }

            });

            $('#select3').lrselect({
                type: 'treemultiple',
                allowSearch: true,
                //type: 'tree',
                select: function (items) {
                },
                data: [{
                    id: '0',
                    text: '父节点',
                    value: 'no',
                    showcheck: true,
                    hasChildren: true,
                    isexpand: true,
                    complete: true,
                    ChildNodes: [
                        {
                            id: '1',
                            text: '子节点一',
                            value: 'learuntree',
                            hasChildren: true,
                            isexpand: true,
                            complete: true,
                            ChildNodes: [
                                {
                                    id: '2',
                                    text: '子节点二',
                                    value: 'learuntree',
                                    showcheck: true,
                                    complete: true
                                },
                                {
                                    id: '3',
                                    text: '子节点三',
                                    value: 'learuntree',
                                    showcheck: true,
                                    complete: true
                                }, {
                                    id: '4',
                                    text: '子节点四',
                                    value: 'learuntree',
                                    showcheck: true,
                                    complete: true
                                }
                            ]
                        },
                        {
                            id: '11',
                            text: '子节点一一',
                            value: 'learuntree',
                            showcheck: true,
                            hasChildren: true,
                            isexpand: true,
                            complete: true,
                            ChildNodes: [
                                {
                                    id: '12',
                                    text: '子节点一二',
                                    value: 'learuntree',
                                    showcheck: true,
                                    complete: true
                                }
                            ]
                        }
                    ]
                }]
            });

            $('#select6').lrlayerselect({
                treeCode: 'dataitemc',
                treeParentId: 'f_parentid',
                treeValueId: 'f_itemid',
                treeTextId: 'f_itemname',
                
                dataCode: 'dataitem',
                dataTreeId: 'f_itemid',
                dataValueId: 'f_itemdetailid',
                dataTextId: 'f_itemname',

                grid: [
                    { label: '项目名', name: 'f_itemname', width: 175, align: 'left' },
                    { label: '项目值', name: 'f_itemvalue', width: 175, align: 'left' },
                    { label: "备注", name: "f_description", width: 200, align: "left" }
                ],
                select: function (values, texts) {
                    //console.log(values, texts);
                }

            });
            //$('#select6').lrlayerselectSet('79e2b36d-73e6-42ff-aca2-e6b6bd885662,ed650fb0-5649-43b3-957d-73ceae26c313');
            //console.log($('#select6').lrlayerselectGet(),'获取数据');
            $('#select7').lrlayerselect({
                treeCode: 'dataitemc',
                treeParentId: 'f_parentid',
                treeValueId: 'f_itemid',
                treeTextId: 'f_itemname',

                dataCode: 'dataitem',
                dataTreeId: 'f_itemid',
                dataValueId: 'f_itemdetailid',
                dataTextId: 'f_itemname',

                grid: [
                    { label: '项目名', name: 'f_itemname', width: 175, align: 'left' },
                    { label: '项目值', name: 'f_itemvalue', width: 175, align: 'left' },
                    { label: "备注", name: "f_description", width: 200, align: "left" }
                ],
                select: function (values, texts) {
                },
                isMultiple: false

            });
        },
        selectUserinit: function () {
            $('#selectuser1').lrformselect({
                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectForm',
                layerUrlW: 800,
                layerUrlH: 520,
                dataUrl:''
            });
        },
        jfgridinit:function(){
            initGrid();
        },
        uploaderInit: function () {
            $('#learun_uploader').lrUploader();
        }
    }

    page.init();
}