/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.16
 * 描 述：数据验证完整性
 */
(function ($, learun) {
    "use strict";

    // 数据验证方法
    learun.validator = {
        validReg: function (obj, reg, msg) {
            var res = { code: true, msg: '' };
            if (!reg.test(obj)) {
                res.code = false;
                res.msg = msg;
            }
            return res;
        },
        validRegOrNull: function (obj, reg, msg) {
            var res = { code: true, msg: '' };
            if (obj == null || obj == undefined || obj.length == 0) {
                return res;
            }
            if (!reg.test(obj)) {
                res.code = false;
                res.msg = msg;
            }
            return res;
        },
        isNotNull: function (obj) {// 验证不为空
            var res = { code: true, msg: '' };
            obj = $.trim(obj);
            if (obj == null || obj == undefined || obj.length == 0) {
                res.code = false;
                res.msg = '不能为空';
            }
            return res;
        },
        isNum: function (obj) {// 验证数字
            return learun.validator.validReg(obj, /^[-+]?\d+$/, '必须为数字');
        },
        isNumOrNull: function (obj) {// 验证数字 或者空
            return learun.validator.validRegOrNull(obj, /^[-+]?\d+$/, '数字或空');
        },
        isEmail: function (obj) {//Email验证 email
            return learun.validator.validReg(obj, /^\w{3,}@\w+(\.\w+)+$/, '必须为E-mail格式');
        },
        isEmailOrNull: function (obj) {//Email验证 email   或者null,空
            return learun.validator.validRegOrNull(obj, /^\w{3,}@\w+(\.\w+)+$/, '必须为E-mail格式或空');
        },
        isEnglishStr: function (obj) {//验证只能输入英文字符串 echar
            return learun.validator.validReg(obj, /^[a-z,A-Z]+$/, '必须为英文字符串');
        },
        isEnglishStrOrNull: function (obj) {//验证只能输入英文字符串 echar 或者null,空
            return learun.validator.validRegOrNull(obj, /^[a-z,A-Z]+$/, '必须为英文字符串或空');
        },
        isTelephone: function (obj) { //验证是否电话号码 phone
            return learun.validator.validReg(obj, /^(\d{3,4}\-)?[1-9]\d{6,7}$/, '必须为电话格式');
        },
        isTelephoneOrNull: function (obj) {//验证是否电话号码 phone或者null,空
            return learun.validator.validRegOrNull(obj, /^(\d{3,4}\-)?[1-9]\d{6,7}$/, '必须为电话格式或空');
        },
        isMobile: function (obj) {//验证是否手机号 mobile
            return learun.validator.validReg(obj, /^(\+\d{2,3}\-)?\d{11}$/, '必须为手机格式');
        },
        isMobileOrnull: function (obj) {//验证是否手机号 mobile或者null,空
            return learun.validator.validRegOrNull(obj, /^(\+\d{2,3}\-)?\d{11}$/, '必须为手机格式或空');
        },
        isMobileOrPhone: function (obj) {//验证是否手机号或电话号码 mobile phone 
            var res = { code: true, msg: '' };
            if (!learun.validator.isTelephone(obj).code && !learun.validator.isMobile(obj).code) {
                res.code = false;
                res.msg = '为电话格式或手机格式';
            }
            return res;
        },
        isMobileOrPhoneOrNull: function (obj) {//验证是否手机号或电话号码 mobile phone或者null,空
            var res = { code: true, msg: '' };
            if (learun.validator.isNotNull(obj).code && !learun.validator.isTelephone(obj).code && !learun.validator.isMobile(obj).code) {
                res.code = false;
                res.msg = '为电话格式或手机格式或空';
            }
            return res;
        },
        isUri: function (obj) {//验证网址 uri
            return learun.validator.validReg(obj, /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/, '必须为网址格式');
        },
        isUriOrNull: function (obj) {//验证网址 uri或者null,空
            return learun.validator.validRegOrNull(obj, /^http:\/\/[a-zA-Z0-9]+\.[a-zA-Z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/, '必须为网址格式或空');
        },
        isDate: function (obj) {//判断日期类型是否为YYYY-MM-DD格式的类型 date
            return learun.validator.validReg(obj, /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/, '必须为日期格式');
        },
        isDateOrNull: function (obj) {//判断日期类型是否为YYYY-MM-DD格式的类型 date或者null,空
            return learun.validator.validRegOrNull(obj, /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/, '必须为日期格式或空');
        },
        isDateTime: function (obj) {//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime
            return learun.validator.validReg(obj, /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/, '必须为日期时间格式');
        },
        isDateTimeOrNull: function (obj) {//判断日期类型是否为YYYY-MM-DD hh:mm:ss格式的类型 datetime或者null,空
            return learun.validator.validRegOrNull(obj, /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/, '必须为日期时间格式');
        },
        isTime: function (obj) {//判断日期类型是否为hh:mm:ss格式的类型 time
            return learun.validator.validReg(obj, /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/, '必须为时间格式');
        },
        isTimeOrNull: function (obj) {//判断日期类型是否为hh:mm:ss格式的类型 time或者null,空
            return learun.validator.validRegOrNull(obj, /^((20|21|22|23|[0-1]\d)\:[0-5][0-9])(\:[0-5][0-9])?$/, '必须为时间格式或空');
        },
        isChinese: function (obj) {//判断输入的字符是否为中文 cchar 
            return learun.validator.validReg(obj, /^[\u0391-\uFFE5]+$/, '必须为中文');
        },
        isChineseOrNull: function (obj) {//判断输入的字符是否为中文 cchar或者null,空
            return learun.validator.validRegOrNull(obj, /^[\u0391-\uFFE5]+$/, '必须为中文或空');
        },
        isZip: function (obj) {//判断输入的邮编(只能为六位)是否正确 zip
            return learun.validator.validReg(obj, /^\d{6}$/, '必须为邮编格式');
        },
        isZipOrNull: function (obj) {//判断输入的邮编(只能为六位)是否正确 zip或者null,空
            return learun.validator.validRegOrNull(obj, /^\d{6}$/, '必须为邮编格式或空');
        },
        isDouble: function (obj) {//判断输入的字符是否为双精度 double
            return learun.validator.validReg(obj, /^[-\+]?\d+(\.\d+)?$/, '必须为小数');
        },
        isDoubleOrNull: function (obj) {//判断输入的字符是否为双精度 double或者null,空
            return learun.validator.validRegOrNull(obj, /^[-\+]?\d+(\.\d+)?$/, '必须为小数或空');
        },
        isIDCard: function (obj) {//判断是否为身份证 idcard
            return learun.validator.validReg(obj, /^\d{15}(\d{2}[A-Za-z0-9;])?$/, '必须为身份证格式');
        },
        isIDCardOrNull: function (obj) {//判断是否为身份证 idcard或者null,空
            return learun.validator.validRegOrNull(obj, /^\d{15}(\d{2}[A-Za-z0-9;])?$/, '必须为身份证格式或空');
        },
        isIP: function (obj) {//判断是否为IP地址格式
            var res = { code: true, msg: '' };
            var reg = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
            var flag = false;
            if (reg.test(obj)) {
                if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) { flag = true };
            }
            if (!flag) {
                res.code = false;
                res.msg = '必须为IP格式';
            }
            return res;
        },
        isIPOrNull: function (obj) {//判断是否为IP地址格式 或者null,空
            var res = { code: true, msg: '' };
            if (learun.validator.isNotNull(obj) && !learun.validator.isIP(obj).code) {
                res.code = false;
                res.msg = '必须为IP格式或空';
            }
            return res;
        },

        isLenNum: function (obj, n) {//验证是否是n位数字字符串编号 nnum
            var res = { code: true, msg: '' };
            var reg = /^[0-9]+$/;
            obj = $.trim(obj);
            if (obj.length > n || !reg.test(obj)) {
                res.code = false;
                res.msg = '必须为' + n + '位数字';
            }
            return res;
        },
        isLenNumOrNull: function (obj, n) {//验证是否是n位数字字符串编号 nnum或者null,空
            var res = { code: true, msg: '' };
            if (learun.validator.isNotNull(obj).code && !learun.validator.isLenNum(obj)) {
                res.code = false;
                res.msg = '必须为' + n + '位数字或空';
            }
            return res;
        },
        isLenStr: function (obj, n) {//验证是否小于等于n位数的字符串 nchar
            var res = { code: true, msg: '' };
            obj = $.trim(obj);
            if (!learun.validator.isNotNull(obj).code || obj.length > n) {
                res.code = false;
                res.msg = '必须小于等于' + n + '位字符';
            }
            return res;
        },
        isLenStrOrNull: function (obj, n) {//验证是否小于等于n位数的字符串 nchar或者null,空
            var res = { code: true, msg: '' };
            obj = $.trim(obj);
            if (learun.validator.isNotNull(obj).code && obj.length > n) {
                res.code = false;
                res.msg = '必须小于等于' + n + '位字符或空';
            }
            return res;
        }
    };

})(window.jQuery, top.learun);