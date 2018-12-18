/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.06.20
 * 描 述：文件管理	
 */

var bootstrap = function ($, learun) {
    "use strict";

    var page = {

        init: function () {
            $.lrSetForm(top.$.rootUrl + '/LR_OAModule/ModuleExport/GetFormData?keyValue=3587a099-980d-4342-864f-a9f48e90e03b', function (data) {//
                $('.price-info').lrSetFormData(data.customerData);
                var html = "";
                for (var item in data.orderProductData) {
                    html += "<tr><td>"+ (parseInt(item)+1) +"</td><td>" + data.orderProductData[item].F_ProductName + "</td><td>定制</td><td>" + data.orderProductData[item].F_Qty + "</td><td>套</td><td>" + data.orderProductData[item].F_Amount + "</td><td>" + data.orderProductData[item].F_Amount * data.orderProductData[item].F_Qty + "</td><td>&nbsp;</td></tr>";
                }
                html += "<tr><td>4</td><td>服务器硬件</td><td>联想</td><td>5</td><td>台</td><td>20000.00</td><td>100000.00</td><td>&nbsp;</td></tr><tr><td>5</td><td>数据库正版</td><td>SQLServer</td><td>5</td><td>套</td><td>9998.00</td><td>49990.00</td><td>&nbsp;</td></tr><tr><td>6</td><td>服务费</td><td>&nbsp;</td><td>1</td><td>年</td><td>80000.00</td><td>80000.00</td><td>&nbsp;</td></tr><tr><td>7</td><td>差旅费用</td><td>&nbsp;</td><td>1</td><td>年</td><td>60000.00</td><td>80000.00</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>;"
                html += '<tr><td colspan="7" style="text-align: right; border-right: none;">币种：人民币&nbsp;&nbsp;&nbsp;&nbsp;总计：323323.00</td><td>&nbsp;</td></tr><tr><td style="height: 80px;">备注</td><td colspan="7"></td></tr>';

                $('.price-table tbody').append(html);
            });

            page.bind();
        },
        bind:function(){
            //导出Excel
            $("#btn_export_Excel").on('click', function () {
                learun.download({ url: top.$.rootUrl + '/LR_OAModule/ModuleExport/ExportExcel', param: { keyValue: "3587a099-980d-4342-864f-a9f48e90e03b", __RequestVerificationToken: $.lrToken }, method: 'POST' });
            });
            //导出Word
            $("#btn_export_Word").on('click', function () {
                learun.download({ url: top.$.rootUrl + '/LR_OAModule/ModuleExport/ExportWord', param: { keyValue: "3587a099-980d-4342-864f-a9f48e90e03b", __RequestVerificationToken: $.lrToken }, method: 'POST' });
            });
            //导出PDF
            $("#btn_export_PDF").on('click', function () {
                learun.download({ url: top.$.rootUrl + '/LR_OAModule/ModuleExport/ExportPDF', param: { __RequestVerificationToken: $.lrToken }, method: 'POST' });
            });
        }
    };
    page.init();
}


