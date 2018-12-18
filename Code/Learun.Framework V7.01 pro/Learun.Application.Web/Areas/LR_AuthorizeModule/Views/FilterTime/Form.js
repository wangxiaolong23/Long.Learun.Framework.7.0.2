/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：时间段设置管理	
 */
var objectId = request('objectId');
var objectType = request('objectType');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            var _html = '<ul class="sys_spec_text">';
            _html += '<li><a data-value="00:00">00:00</a></li>';
            _html += '<li><a data-value="01:00">01:00</a></li>';
            _html += '<li><a data-value="02:00">02:00</a></li>';
            _html += '<li><a data-value="03:00">03:00</a></li>';
            _html += '<li><a data-value="04:00">04:00</a></li>';
            _html += '<li><a data-value="05:00">05:00</a></li>';
            _html += '<li><a data-value="06:00">06:00</a></li>';
            _html += '<li><a data-value="07:00">07:00</a></li>';
            _html += '<li><a data-value="08:00">08:00</a></li>';
            _html += '<li><a data-value="09:00">09:00</a></li>';
            _html += '<li><a data-value="10:00">10:00</a></li>';
            _html += '<li><a data-value="11:00">11:00</a></li>';
            _html += '<li><a data-value="12:00">12:00</a></li>';
            _html += '<li><a data-value="13:00">13:00</a></li>';
            _html += '<li><a data-value="14:00">14:00</a></li>';
            _html += '<li><a data-value="15:00">15:00</a></li>';
            _html += '<li><a data-value="16:00">16:00</a></li>';
            _html += '<li><a data-value="17:00">17:00</a></li>';
            _html += '<li><a data-value="18:00">18:00</a></li>';
            _html += '<li><a data-value="19:00">19:00</a></li>';
            _html += '<li><a data-value="20:00">20:00</a></li>';
            _html += '<li><a data-value="21:00">21:00</a></li>';
            _html += '<li><a data-value="22:00">22:00</a></li>';
            _html += '<li><a data-value="23:00">23:00</a></li></ul>';
            $('#WeekDay1').html(_html);
            $('#WeekDay2').html(_html);
            $('#WeekDay3').html(_html);
            $('#WeekDay4').html(_html);
            $('#WeekDay5').html(_html);
            $('#WeekDay6').html(_html);
            $('#WeekDay7').html(_html);


            $('#nav_tabs').lrFormTabEx();

            $(".tab-content li").click(function () {
                if (!!$(this).hasClass("active")) {
                    $(this).removeClass("active");
                } else {
                    $(this).addClass("active").siblings("li");
                }
            });

        },
        initData: function () {
            $.lrSetForm(top.$.rootUrl + '/LR_AuthorizeModule/FilterTime/GetFormData?keyValue=' + objectId, function (data) {
                var formdata = data || {};
                function setTime(id, data) {
                    if (!!data) {
                        var array = data.split(',');
                        for (var i = 0, l = array.length; i < l; i++) {
                            var value = array[i];
                            $("#" + id).find('li').find('[data-value="' + value + '"]').parent().addClass('active');
                        }
                    }
                }
                setTime('WeekDay1', formdata.F_WeekDay1);
                setTime('WeekDay2', formdata.F_WeekDay2);
                setTime('WeekDay3', formdata.F_WeekDay3);
                setTime('WeekDay4', formdata.F_WeekDay4);
                setTime('WeekDay5', formdata.F_WeekDay5);
                setTime('WeekDay6', formdata.F_WeekDay6);
                setTime('WeekDay7', formdata.F_WeekDay7);
            });
        }
    };
    // 保存数据
    acceptClick = function () {
        var selectedTime = [];
        $('.tab-pane').each(function () {
            var time = [];
            $(this).find('ul li.active').each(function () {
                var value = $(this).find('a').html();
                time.push(value);
            });
            selectedTime.push(String(time))
        });

        var postData = {
            'F_FilterTimeId': objectId,
            'F_ObjectType': objectType
        };
        postData["F_WeekDay1"] = selectedTime[0];
        postData["F_WeekDay2"] = selectedTime[1];
        postData["F_WeekDay3"] = selectedTime[2];
        postData["F_WeekDay4"] = selectedTime[3];
        postData["F_WeekDay5"] = selectedTime[4];
        postData["F_WeekDay6"] = selectedTime[5];
        postData["F_WeekDay7"] = selectedTime[6];
        $.lrSaveForm(top.$.rootUrl + '/LR_AuthorizeModule/FilterTime/SaveForm', postData, function (res) { });

        return true;
    };
    page.init();
}