/*!
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.08
 * 描 述：登录页面前端脚本
 */
(function ($) {
    "use strict";

    var lrPage = {
        init: function () {
            if (window.location.href != top.window.location.href) {
                top.window.location.href = window.location.href;
            }
            var isIE = !!window.ActiveXObject;
            var isIE6 = isIE && !window.XMLHttpRequest;
            if (isIE6) {
                window.location.href = $.rootUrl + "/Error/ErrorBrowser";
            }          
            lrPage.bind();
        },
        bind: function () {
            // 回车键
            document.onkeydown = function (e) {
                e = e || window.event;
                if ((e.keyCode || e.which) == 13) {
                    $('#lr_login_btn').trigger('click');
                }
            }
            //输入框获取焦点
            $('.lr-input-item input').on('focus', function () {
                var $item = $(this).parent();
                $item.addClass('focus');
            }).on('blur', function () {
                var $item = $(this).parent();
                $item.removeClass('focus');
            });
            
            // 点击切换验证码
            $("#lr_verifycode_img").click(function () {
                $("#lr_verifycode_input").val('');
                $("#lr_verifycode_img").attr("src", $.rootUrl + "/Login/VerifyCode?time=" + Math.random());
            });
            var errornum = $('#errornum').val();
            if (errornum >= 3) {
                $('#lr_verifycode_input').parent().show();
                $("#lr_verifycode_img").trigger('click');
            }

            // 登录按钮事件
            $("#lr_login_btn").on('click',function () {
                lrPage.login();
            });
        },
        login: function () {
            lrPage.tip();

            var $username = $("#lr_username"), $password = $("#lr_password"), $verifycode = $("#lr_verifycode_input");
            var username = $.trim($username.val()), password = $.trim($password.val()), verifycode = $.trim($verifycode.val());

            if (username == "" ) {
                lrPage.tip('请输入账户。');
                $username.focus();
                return false;
            }
            if (password == "") {
                lrPage.tip('请输入密码。');
                $password.focus();
                return false;
            }

            if ($("#lr_verifycode_input").is(":visible") && verifycode == "") {
                lrPage.tip('请输入验证码。');
                $verifycode.focus();
                return false;
            }
            password = $.md5(password);
            lrPage.logining(true);
            $.ajax({
                url: $.rootUrl + "/Login/CheckLogin",
                headers: { __RequestVerificationToken: $.lrToken },
                data: { username: username, password: password, verifycode: verifycode },
                type: "post",
                dataType: "json",
                success: function (res) {
                    if (res.code == 200) {
                        window.location.href = $.rootUrl + '/Home/Index';
                    }
                    else if (res.code == 400) {
                        lrPage.logining(false);
                        lrPage.tip(res.info, true);
                        $('#errornum').val(res.data);
                        if (res.data >= 3) {
                            $('#lr_verifycode_input').parent().show();
                            $("#lr_verifycode_img").trigger('click');
                        }
                    }
                    else if (res.code == 500) {
                        console.error(res.info);
                        lrPage.logining(false);
                        lrPage.tip('服务端异常，请联系管理员', true);
                    }
                }
            });
        },
        logining: function (isShow) {
            if (isShow) {
                $('input').attr('disabled', 'disabled');
                $("#lr_login_btn").addClass('active').attr('disabled', 'disabled').find('span').hide();
            }
            else {
                $('input').removeAttr('disabled');
                $("#lr_login_btn").removeClass('active').removeAttr('disabled').find('span').show();
            }
        },
        tip: function (msg) {
            var $tip = $('#lr_tips');
            $tip.hide();
            if (!!msg) {
                $tip.html('<b></b>' + msg);
                $tip.show();
            }            
        }
    };
    $(function () {
        lrPage.init();
    });
})(window.jQuery)