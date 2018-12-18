/*
 * 版 本 Learun-ADMS V7.0.0 力软 敏捷 开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力 软- 前端开发组
 * 日 期：2018.04.11
 * 描 述：框架开启js
 */
(function (window) {
    "use strict";
    var plugins = [
        { name: 'jquery', ver: '1.10.2' },
        { name: 'cookie', ver: '1.0.0' },
        { name: 'md5', ver: '1.0.0' },
        { name: 'scrollbar', ver: '1.0.0' },
        { name: 'toastr', ver: '1.0.0' },
        { name: 'bootstrap', ver: '1.0.0' },
        { name: 'layer', ver: '1.0.0' },
        { name: 'jqprint', ver: '1.0.0' },
        { name: 'wdatePicker', ver: '1.0.0' },
        { name: 'syntaxhighlighter', ver: '1.0.0' },

        { name: 'fontAwesome', ver: '1.0.0' },
        { name: 'iconfont', ver:'1.0.0'},
        //'signalR',等即时好了再添加

        { name: 'common', ver: '1.0.0', isIframe: true },
        { name: 'base', ver: '1.0.0' },
        { name: 'tabs', ver: '1.0.0' },
        { name: 'date', ver: '1.0.0' },
        { name: 'validator-helper', ver: '1.0.0' },
        { name: 'lrlayer', ver: '1.0.0' },
        { name: 'ajax', ver: '1.0.0' },
        { name: 'clientdata', ver: '1.0.0' },

        { name: 'iframe', ver: '1.0.0', isIframe: true },
        { name: 'validator', ver: '1.0.0', isIframe: true },
        { name: 'layout', ver: '1.0.0', isIframe: true  },
        { name: 'tree', ver: '1.0.0', isIframe: true  },
        { name: 'select', ver: '1.0.0', isIframe: true  },
        { name: 'formselect', ver: '1.0.0', isIframe: true  },
        { name: 'layerselect', ver: '1.0.0', isIframe: true  },
        { name: 'jfgrid', ver: '1.0.0', isIframe: true  },
        { name: 'wizard', ver: '1.0.0', isIframe: true  },
        { name: 'timeline', ver: '1.0.0', isIframe: true  },
        { name: 'datepicker', ver: '1.0.0', isIframe: true },
        { name: 'uploader', ver: '1.0.0', isIframe: true  },
        { name: 'excel', ver: '1.0.0', isIframe: true },
        { name: 'authorize', ver: '1.0.0', isIframe: true  },
        { name: 'custmerform', ver: '1.0.0', isIframe: true  },
        { name: 'workflow', ver: '1.0.0', isIframe: true  },
        { name: 'form', ver: '1.0.0', isIframe: true  },
    ];

    var iframePlugins = [];

    // 原生ajax方法实现
    function ajax() {
        var ajaxData = {
            type: arguments[0].type || "GET",
            url: arguments[0].url || "",
            async: arguments[0].async || "true",
            data: arguments[0].data || null,
            dataType: arguments[0].dataType || "text",
            contentType: arguments[0].contentType || "application/x-www-form-urlencoded",
            beforeSend: arguments[0].beforeSend || function () { },
            success: arguments[0].success || function () { },
            error: arguments[0].error || function () { }
        }
        ajaxData.beforeSend()
        var xhr = createxmlHttpRequest();
        xhr.responseType = ajaxData.dataType;
        xhr.open(ajaxData.type, ajaxData.url, ajaxData.async);
        xhr.setRequestHeader("Content-Type", ajaxData.contentType);
        xhr.send(convertData(ajaxData.data));
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    ajaxData.success(xhr.response)
                } else {
                    ajaxData.error()
                }
            }
        }
    }
    function createxmlHttpRequest() {
        if (window.ActiveXObject) {
            return new ActiveXObject("Microsoft.XMLHTTP");
        } else if (window.XMLHttpRequest) {
            return new XMLHttpRequest();
        }
    }
    function convertData(data) {
        if (typeof data === 'object') {
            var convertResult = "";
            for (var c in data) {
                convertResult += c + "=" + data[c] + "&";
            }
            convertResult = convertResult.substring(0, convertResult.length - 1)
            return convertResult;
        } else {
            return data;
        }
    }

    // 浏览器本地存储方法
    var storage = {
        get: function (name) {
            if (localStorage) {
                return JSON.parse(localStorage.getItem(name)) || {};
            }
            else {
                return clientData[name] || {};
            }
        },
        set: function (name, data) {
            if (localStorage) {
                localStorage.setItem(name, JSON.stringify(data));
            }
            else {
                return clientData[name] = data;
            }
        }
    };

    // 加载框架需要的js和css代码
    var loadPlugins = [];

    function loadPlugin() {
        for (var i = 0, l = plugins.length; i < l; i++) {
            var item = plugins[i];
            var plugin = storage.get(item.name);
            if (plugin.ver != item.ver) {
                loadPlugins.push(item.name);
            }
            if (item.isIframe) {
                loadPlugins.push(item.name);
            }
        }
        // 从服务端加载js和css;
        ajax({
            url: "ajax.php",
            dataType: "json",
            data: { "plugins": String(loadPlugins) },
            success: function (msg) {
                console.log(msg);
            },
            error: function () {
            }
        });
    }
})(window);