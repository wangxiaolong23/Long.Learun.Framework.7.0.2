/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.16
 * 描 述：表单数据验证完整性
 */
(function ($, learun) {
    "use strict";
    
    $.lrValidformMessage = function ($this, errormsg) {
        /*错误处理*/
        $this.addClass('lr-field-error');
        $this.parent().append('<div class="lr-field-error-info" title="' + errormsg + '！"><i class="fa fa-info-circle"></i></div>');
        var validatemsg = $this.parent().find('.form-item-title').text() + ' ' + errormsg;
        learun.alert.error('表单信息输入有误,请检查！</br>' + validatemsg);
        if ($this.attr('type') == 'lrselect') {
            $this.on('change', function () {
                removeErrorMessage($(this));
            });
        }
        else if ($this.attr('type') == 'formselect') {
            $this.on('change', function () {
                removeErrorMessage($(this));
            });
        }
        else if ($this.hasClass('lr-input-wdatepicker')) {
            $this.on('change', function () {
                var $input = $(this);
                if ($input.val()) {
                    removeErrorMessage($input);
                }
            });
        }
        else {
            $this.on('input propertychange', function () {
                var $input = $(this);
                if ($input.val()) {
                    removeErrorMessage($input);
                }
            });
        }
    };

    $.fn.lrRemoveValidMessage = function () {
        removeErrorMessage($(this));
    }

    $.fn.lrValidform = function () {
        var validateflag = true;
        var validHelper = learun.validator;
        var formdata = $(this).lrGetFormData();

        $(this).find("[isvalid=yes]").each(function () {
            var $this = $(this);

            if ($this.parent().find('.lr-field-error-info').length > 0) {
                validateflag = false;
                return true;
            }

            var checkexpession = $(this).attr("checkexpession");
            var checkfn = validHelper['is' + checkexpession];
            if (!checkexpession || !checkfn) { return false; }
            var errormsg = $(this).attr("errormsg") || "";

            var id = $this.attr('id');
            var value = formdata[id];

            //var type = $this.attr('type');
            //if (type == 'lrselect') {
            //    value = $this.lrselectGet();
            //}
            //else if (type == 'formselect') {
            //    value = $this.lrformselectGet();
            //}
            //else {
            //    value = $this.val();
            //}
            var r = { code: true, msg: '' };
            if (checkexpession == 'LenNum' || checkexpession == 'LenNumOrNull' || checkexpession == 'LenStr' || checkexpession == 'LenStrOrNull') {
                var len = $this.attr("length");
                r = checkfn(value, len);
            } else {
                r = checkfn(value);
            }
            if (!r.code) {
                validateflag = false;
                $.lrValidformMessage($this, errormsg + r.msg);
            }
        });
        return validateflag;
    }

    function removeErrorMessage($obj) {
        $obj.removeClass('lr-field-error');
        $obj.parent().find('.lr-field-error-info').remove();
    }

})(window.jQuery, top.learun);