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
            $('#psw_change').css({
                'background': 'url(' + $.rootUrl + '/Content/images/Login/psw0.png) no-repeat center center'
            });

            var error = request('error');
            if (error == "ip") {
                lrPage.tip("登录IP限制");
            }
            else if (error == "time"){
                lrPage.tip("登录时间限制");
            }

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
            $('.lr-login-input input').on('focus', function () {
                var src = $(this).prev().attr('src');
                $(this).prev().attr('src', src.replace(/0.png$/, '1.png'));
            }).on('blur', function () {
                var src = $(this).prev().attr('src');
                $(this).prev().attr('src', src.replace(/1.png$/, '0.png'));
            });

            // 点击切换验证码
            $("#lr_verifycode_img").click(function () {
                $("#lr_verifycode_input").val('');
                $("#lr_verifycode_img").attr("src", $.rootUrl + "/Login/VerifyCode?time=" + Math.random());
            });
            var errornum = $('#errornum').val();
            if (errornum >= 3) {

                $('.lr-login-bypsw').removeClass('noreg');
                $("#lr_verifycode_img").trigger('click');
            }

            //点击密码icon  显示／隐藏
            $('#psw_change').click(function (event) {
                var event = event || window.event;
                event.stopPropagation();
                var $this = $(this);
                $this.toggleClass('psw_show');
                //如果当前隐藏  变显示
                if ($this.hasClass('psw_show')) {
                    $this.css({
                        'background': 'url(' + $.rootUrl +'/Content/images/Login/psw1.png) no-repeat center center'
                    });
                    $this.prev().attr('type', 'text');
                } else {
                    $this.css(
                        'background', 'url(/Content/images/Login/psw0.png) no-repeat center center'
                    );
                    $this.prev().attr('type', 'password');
                }
            });

            //登录方式点击
            $('.lr-login-toCode').click(function () {
                var _this = $(this);
                if (_this.attr('login-access') == 'psw') {
                    $('.lr-login-bycode').show();
                    $('.lr-login-bypsw').hide();

                } else {
                    $('.lr-login-bypsw').show();
                    $('.lr-login-bycode').hide();

                }
            })

            // 登录按钮事件
            $("#lr_login_btn").on('click', function () {
                lrPage.login();
            });
        },
        login: function () {
            lrPage.tip();

            var $username = $("#lr_username"), $password = $("#lr_password"), $verifycode = $("#lr_verifycode_input");
            var username = $.trim($username.val()), password = $.trim($password.val()), verifycode = $.trim($verifycode.val());

            if (username == "") {
                lrPage.tip('请输入账户');
                $username.focus();
                return false;
            }
            if (password == "") {
                lrPage.tip('请输入密码');
                $password.focus();
                return false;
            }

            if ($("#lr_verifycode_input").is(":visible") && verifycode == "") {
                lrPage.tip('请输入验证码');
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
                $("#lr_login_btn").css('background', '#eeecec url(/Content/images/Login/loading.gif) no-repeat center 10px');

            }
            else {
                $('input').removeAttr('disabled');
                $("#lr_login_btn").removeClass('active').removeAttr('disabled').find('span').show();
                $("#lr_login_btn").css('background', '#268fe2');

            }
        },
        tip: function (msg) {
            var $tip = $('.error_info');
            $tip.hide();
            if (!!msg) {
                $tip.find('span').html(msg);
                $tip.show();
            }
        }
    };
    $(function () {
        lrPage.init();
    });
})(window.jQuery)