// 有关“空白”模板的简介，请参阅以下文档:
// http://go.microsoft.com/fwlink/?LinkID=397704
// 若要在 cordova-simulate 或 Android 设备/仿真器上在页面加载时调试代码: 启动应用，设置断点，
// 然后在 JavaScript 控制台中运行 "window.location.reload()"。
(function ($, learun) {
    "use strict";
    // 初始化页面
    var tabdata = [
        {
            page: 'workspace',
            text: '首页',
            img: 'images/tab10.png',
            fillimg: 'images/tab11.png'
        },
        {
            page: 'message',
            text: '消息',
            img: 'images/tab20.png',
            fillimg: 'images/tab21.png'
        },
        {
            page: 'contacts',
            text: '通讯录',
            img: 'images/tab30.png',
            fillimg: 'images/tab31.png'
        },
        {
            page: 'my',
            text: '我的',
            img: 'images/tab40.png',
            fillimg: 'images/tab41.png'
        }
    ];

    learun.init(function () {
        // 处理 Cordova 暂停并恢复事件
        document.addEventListener('pause', onPause.bind(this), false);
        document.addEventListener('resume', onResume.bind(this), false);
        learun.tab.init(tabdata);

        var logininfo = learun.storage.get('logininfo');
        if (logininfo) {// 有登录的token
            learun.tab.go('workspace');
        }
        else {
            learun.nav.go({ path: 'login', isBack: false, isHead: false });
        }
        learun.splashscreen.hide();
    });

    function onPause() {
        // TODO: 此应用程序已挂起。在此处保存应用程序状态。
    }

    function onResume() {
        // TODO: 此应用程序已重新激活。在此处还原应用程序状态。
    }

})(window.jQuery, window.lrmui);