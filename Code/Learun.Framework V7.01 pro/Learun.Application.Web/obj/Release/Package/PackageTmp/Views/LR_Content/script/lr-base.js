/*
 * 版 本 Learun-ADMS V7.0.0 力 软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前 端 开发组
 * 日 期：2017.03.16
 * 描 述：操作类	
 */
top.learun = (function ($) {
    "use strict";
    var learun = {
        // 是否是调试模式
        isDebug: true,
        log: function () {
            if (learun.isDebug) {
                console.log('=====>' + new Date().getTime() + '<=====');
                var len = arguments.length;
                for (var i = 0; i < len; i++) {
                    console.log(arguments[i]);
                }
            }
        },
        // 创建一个GUID
        newGuid: function () {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) guid += "-";
            }
            return guid;
        },
        // 加载提示
        loading: function (isShow, _text) {//加载动画显示与否
            var $loading = top.$("#lr_loading_bar");
            if (!!_text) {
                top.learun.language.get(_text, function (text) {
                    top.$("#lr_loading_bar_message").html(text);
                });
               
            } else {
                top.learun.language.get("正在拼了命为您加载…", function (text) {
                    top.$("#lr_loading_bar_message").html(text);
                });
            }
            if (isShow) {
                $loading.show();
            } else {
                $loading.hide();
            }
        },
        // 动态加载css文件
        loadStyles: function (url) {
            var link = document.createElement("link");
            link.type = "text/css";
            link.rel = "stylesheet";
            link.href = url;
            link.back = "backdl";
            document.getElementsByTagName("head")[0].appendChild(link);
        },
        // 获取iframe窗口
        iframe: function (Id, _frames) {
            if (_frames[Id] != undefined) {
                if (_frames[Id].contentWindow != undefined) {
                    return _frames[Id].contentWindow;
                }
                else {
                    return _frames[Id];
                }
            }
            else {
                return null;
            }
        },
        // 改变url参数值
        changeUrlParam: function (url, key, value) {
            var newUrl = "";
            var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)");
            var tmp = key + "=" + value;
            if (url.match(reg) != null) {
                newUrl = url.replace(eval(reg), tmp);
            } else {
                if (url.match("[\?]")) {
                    newUrl = url + "&" + tmp;
                }
                else {
                    newUrl = url + "?" + tmp;
                }
            }
            return newUrl;
        },
        // 转化成十进制
        toDecimal: function (num) {
            if (num == null) {
                num = "0";
            }
            num = num.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "0";
            var sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            var cents = num % 100;
            num = Math.floor(num / 100).toString();
            if (cents < 10)
                cents = "0" + cents;
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
                num = num.substring(0, num.length - (4 * i + 3)) + '' +
                        num.substring(num.length - (4 * i + 3));
            return (((sign) ? '' : '-') + num + '.' + cents);
        },
        // 文件大小转换
        countFileSize: function (size) {
            if (size < 1024.00)
                return learun.toDecimal(size) + " 字节";
            else if (size >= 1024.00 && size < 1048576)
                return learun.toDecimal(size / 1024.00) + " KB";
            else if (size >= 1048576 && size < 1073741824)
                return learun.toDecimal(size / 1024.00 / 1024.00) + " MB";
            else if (size >= 1073741824)
                return learun.toDecimal(size / 1024.00 / 1024.00 / 1024.00) + " GB";
        },
        // 数组复制
        arrayCopy: function (data) {
            return $.map(data, function (obj) {
                return $.extend(true, {}, obj);
            });
        },
        // 检测数据是否选中
        checkrow: function (id) {
            var isOK = true;
            if (id == undefined || id == "" || id == 'null' || id == 'undefined') {
                isOK = false;
                top.learun.language.get('您没有选中任何数据项,请选中后再操作！', function (text) {
                    learun.alert.warning(text);
                });
               
            }
            return isOK;
        },
        // 提示消息栏
        alert: {
            success: function (msg) {
                top.learun.language.get(msg, function (text) {
                    toastr.success(text);
                });
               
            },
            info: function (msg) {
                top.learun.language.get(msg, function (text) {
                    toastr.info(text);
                });
            },
            warning: function (msg) {
                top.learun.language.get(msg, function (text) {
                    toastr.warning(text);
                });
            },
            error: function (msg) {
                top.learun.language.get(msg, function (text) {
                    toastr.warning(msg);
                });
            }
        },
        //下载文件（she写的扩展）
        download: function (options) {
            var defaults = {
                method: "GET",
                url: "",
                param: []
            };
            var options = $.extend(defaults, options);
            if (options.url && options.param) {
                var $form = $('<form action="' + options.url + '" method="' + (options.method || 'post') + '"></form>');
                for (var key in options.param) {
                    var $input = $('<input type="hidden" />').attr('name', key).val(options.param[key]);
                    $form.append($input);
                }
                $form.appendTo('body').submit().remove();
            };
        },

        // 数字格式转换成千分位
        commafy: function (num) {
            if (num == null) {
                num = "0";
            }
            if (isNaN(num)) {
                return "0";
            }
            num = num + "";
            if (/^.*\..*$/.test(num)) {
                varpointIndex = num.lastIndexOf(".");
                varintPart = num.substring(0, pointIndex);
                varpointPart = num.substring(pointIndex + 1, num.length);
                intPart = intPart + "";
                var re = /(-?\d+)(\d{3})/
                while (re.test(intPart)) {
                    intPart = intPart.replace(re, "$1,$2")
                }
                num = intPart + "." + pointPart;
            } else {
                num = num + "";
                var re = /(-?\d+)(\d{3})/
                while (re.test(num)) {
                    num = num.replace(re, "$1,$2")
                }
            }
            return num;
        },

        // 检测图片是否存在
        isExistImg: function (pathImg) {
            if (!!pathImg) {
                var ImgObj = new Image();
                ImgObj.src = pathImg;
                if (ImgObj.fileSize > 0 || (ImgObj.width > 0 && ImgObj.height > 0)) {
                    return true;
                } else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
    };
    return learun;
})(window.jQuery);