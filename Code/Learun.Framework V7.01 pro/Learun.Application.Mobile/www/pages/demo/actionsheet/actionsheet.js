(function () {
    var page = {
        init: function ($page) {
            $page.find('#actionsheet1').on('tap', function () {
                learun.actionsheet({
                    id: 'test',
                    data: [
                        {
                            text: '测试1',
                            event: function () {
                                learun.layer.toast('点击了测试1');
                            }
                        },
                        {
                            text: '测试2',
                            mark: true,
                            event: function () {
                                learun.layer.toast('点击了测试2');
                            }
                        },
                        {
                            text: '测试3',
                            group: '123',
                            event: function () {
                                learun.layer.toast('点击了测试3');
                            }
                        },
                        {
                            text: '测试4',
                            group: '123',
                            event: function () {
                                learun.layer.toast('点击了测试4');
                            }
                        }
                    ],
                    cancel: function () {
                        learun.layer.toast('点击了取消按钮');
                    }
                });

            });
        }
    };
    return page;
})();