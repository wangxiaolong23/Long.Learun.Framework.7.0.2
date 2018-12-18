/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.7.12
 * 描 述：力软移动端框架(ADMS) 自定义表单发布功能-表单页
 */
(function () {
    var keyValue = '';
    var formSchemeId = '';

    var $header = null;
    var titleText = '';

    var page = {
        init: function ($page, param) {
            keyValue = param.keyValue;
            formSchemeId = param.formSchemeId;

            // 添加头部按钮列表
            var _html = '\
                <div class="lr-form-header-cancel" >取消</div>\
                <div class="lr-form-header-btnlist" >\
                    <div class="lr-form-header-more" ><i class="iconfont icon-more" ></i></div>\
                    <div class="lr-form-header-edit" ><i class="iconfont icon-edit" ></i></div>\
                </div>\
                <div class="lr-form-header-submit" >提交</div>';
            $header = $page.parents('.f-page').find('.f-page-header');
            $header.append(_html);
            // 添加头部按钮事件
            // 取消
            $header.find('.lr-form-header-cancel').on('tap', function () {
                learun.layer.confirm('确定要退出当前编辑？', function (_index) {
                    if (_index === '1') {
                        if (keyValue) {// 如果是编辑状态
                            learun.formblur();
                            $header.find('.lr-form-header-cancel').hide();
                            $header.find('.lr-form-header-submit').hide();
                            $header.find('.lr-form-header-btnlist').show();
                            $header.find('.f-page-title').text(titleText);
                            $('#custmerpage_container').setFormRead();

                        }
                        else {// 如果是新增状态 关闭当前页面
                            learun.nav.closeCurrent();
                        }
                    }
                }, '力软提示', ['取消', '确定']);
            });
            // 编辑
            $header.find('.lr-form-header-edit').on('tap', function () {
                $header.find('.lr-form-header-btnlist').hide();
                $header.find('.lr-form-header-cancel').show();
                $header.find('.lr-form-header-submit').show();
                titleText = $header.find('.f-page-title').text();
                $header.find('.f-page-title').text('编辑');
                $('#custmerpage_container').setFormWrite();
            });
            // 更多
            $header.find('.lr-form-header-more').on('tap', function () {
                learun.actionsheet({
                    id: 'more',
                    data: [
                        {
                            text: '删除',
                            mark: true,
                            event: function () {// 删除当前条信息
                                learun.layer.confirm('确定要删除该笔数据吗？', function (_index) {
                                    if (_index === '1') {
                                        learun.layer.loading(true, "正在删除该笔数据");
                                        learun.httppost(config.webapi + "learun/adms/form/delete", { schemeInfoId: formSchemeId, keyValue: keyValue }, (data) => {
                                            if (data) {// 删除数据成功
                                                learun.nav.closeCurrent();
                                                var prepage = learun.nav.getpage('custmerform');
                                                prepage.gird.reload();
                                            }
                                            learun.layer.loading(false);
                                        });
                                    }
                                }, '力软提示', ['取消', '确定']);
                            }
                        }
                    ],
                    cancel: function () {
                    }
                });
            });
            // 提交
            $header.find('.lr-form-header-submit').on('tap', function () {
                // 保存数据到后台
                var formData = $('#custmerpage_container').custmerformGet();
                if (formData == null) {
                    return false;
                }
                learun.layer.loading(true, "正在提交数据");
                var formreq = [];
                var point = {
                    schemeInfoId: formSchemeId,
                    keyValue: keyValue,
                    formData: JSON.stringify(formData[formSchemeId])
                }
                formreq.push(point);
                learun.httppost(config.webapi + "learun/adms/form/save", formreq, (data) => {
                    if (data) {// 表单数据保存成功
                        if (keyValue) {
                            learun.layer.toast('保存数据成功!');
                            learun.formblur();
                            $header.find('.lr-form-header-cancel').hide();
                            $header.find('.lr-form-header-submit').hide();
                            $header.find('.lr-form-header-btnlist').show();
                            $header.find('.f-page-title').text(titleText);
                            $('#custmerpage_container').setFormRead();
                        }
                        else {// 如果是
                            learun.nav.closeCurrent();
                        }
                        var prepage = learun.nav.getpage('custmerform');
                        prepage.gird.reload();
                    }
                    learun.layer.loading(false);
                });
            });

            // 初始化表单
            var data = {};
            data[formSchemeId] = param.formScheme;
            $('#custmerpage_container').custmerform(data);

            // 判断是详情还是新增
            if (keyValue) {// 详情
                $('#custmerpage_container').setFormRead();
                $header.find('.lr-form-header-btnlist').show();
                // 获取表单数据
                learun.layer.loading(true, '获取表单数据');
                learun.httpget(config.webapi + "learun/adms/form/data", [{ schemeInfoId: formSchemeId, keyValue: keyValue }], (data) => {
                    if (data) {
                        setTimeout(function () {
                            $('#custmerpage_container').custmerformSet(data);
                        },100);
                    }
                    learun.layer.loading(false);
                });
            }
            else {
                $header.find('.lr-form-header-cancel').show();
                $header.find('.lr-form-header-submit').show();
            }
        },
        destroy: function (pageinfo) {
            $header = null;
        }
    };
    return page;
})();