/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.7.12
 * 描 述：力软移动端框架(ADMS) 力软移动框架方法扩展
 */
(function ($, learun) {
    "use strict";

    // http 方法（加了登录信息）
    learun.httpget = function (url, data, callback) {
        var param = {};
        var logininfo = learun.storage.get('logininfo');
        if (!logininfo) {
            callback(null);
            return false;
        }

        param.token = logininfo.token;
        param.loginMark = learun.deviceId();
        var type = learun.type(data);
        if (type === 'object' || type === 'array') {
            param.data = JSON.stringify(data);
        }
        else if (type === 'string') {
            param.data = data;
        }

        learun.http.get(url, param, function (res) {
            if (res === null) {
                learun.layer.toast('无法连接服务器,请检测网络！');
                callback(null);
            }
            else if (res.code === 410) {
                callback(null);
                if (!learun.isOutLogin) {
                    learun.isOutLogin = true;
                    learun.layer.toast('登录过期,请重新登录!');
                    learun.storage.set('logininfo', null);
                    learun.nav.go({ path: 'login', isBack: false, isHead: false });
                }
            } else {
                if (res.code === 200) {
                    callback(res.data);
                } else {
                    callback(null);
                    learun.layer.toast(res.info);
                }
            }
        });
    };
    learun.httppost = function (url, data, callback) {
        var param = {};
        var logininfo = learun.storage.get('logininfo');
        if (!logininfo) {
            callback(null);
            return false;
        }
        param.token = logininfo.token;
        param.loginMark = learun.deviceId();
        var type = learun.type(data);
        if (type === 'object' || type === 'array') {
            param.data = JSON.stringify(data);
        }
        else if (type === 'string') {
            param.data = data;
        }

        learun.http.post(url, param, function (res) {
            if (res === null) {
                learun.layer.toast('无法连接服务器,请检测网络！');
                callback(null);
            }
            else if (res.code === 410) {
                callback(null);
                if (!learun.isOutLogin) {
                    learun.isOutLogin = true;
                    learun.layer.toast('登录过期,请重新登录!');
                    learun.storage.set('logininfo', null);
                    learun.nav.go({ path: 'login', isBack: false, isHead: false });
                }
               
            } else {
                if (res.code === 200) {
                    callback(res.data, res.info);
                } else {
                    callback(null);
                    learun.layer.toast(res.info);
                }
            }
        });
    };

    // 获取后台信息（基础信息）
    var loadSate = {
        no: -1,  // 还未加载
        yes: 1,  // 已经加载成功
        ing: 0,  // 正在加载中
        fail: 2  // 加载失败
    };
    var clientAsyncData = {};
    learun.clientdata = {
        init: function () {
            clientAsyncData.company.init();
        },
        get: function (name, op) {//
            return clientAsyncData[name].get(op);
        },
        getAll: function (name, op) {//
            return clientAsyncData[name].getAll(op);
        },
        clear: function (name) {
            clientAsyncData[name].states = loadSate.no;
        }
    };

    // 公司信息
    clientAsyncData.company = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.company.states === loadSate.no) {
                clientAsyncData.company.states = loadSate.ing;
                var data = learun.storage.get("companyData") || {};
                var ver = data.ver || "";
                learun.httpget(config.webapi + "learun/adms/company/map", { ver: ver }, function (data) {
                    if (data) {
                        if (data.ver) {
                            learun.storage.set("companyData", data);
                        }
                        clientAsyncData.company.states = loadSate.yes;
                        clientAsyncData.department.init();
                    }
                    else {
                        clientAsyncData.company.states = loadSate.fail;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.company.init();
            if (clientAsyncData.company.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.company.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("companyData") || {};
                var data = _dataTmp.data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.company.init();
            if (clientAsyncData.company.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.company.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("companyData") || {};
                var data = _dataTmp.data || {};
                op.callback(data, op);
            }
        }
    };
    // 部门信息
    clientAsyncData.department = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.department.states === loadSate.no) {
                clientAsyncData.department.states = loadSate.ing;
                var data = learun.storage.get("departmentData") || {};
                var ver = data.ver || "";
                learun.httpget(config.webapi + "learun/adms/department/map", { ver: ver }, function (data) {
                    if (data) {
                        if (data.ver) {
                            learun.storage.set("departmentData", data);
                        }
                        clientAsyncData.department.states = loadSate.yes;
                        clientAsyncData.user.init();
                    }
                    else {
                        clientAsyncData.department.states = loadSate.fail;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.department.init();
            if (clientAsyncData.department.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.department.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("departmentData") || {};
                var data = _dataTmp.data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.department.init();
            if (clientAsyncData.department.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.department.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("departmentData") || {};
                var data = _dataTmp.data || {};
                op.callback(data, op);
            }
        }
    };
    // 人员信息
    clientAsyncData.user = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.user.states === loadSate.no) {
                clientAsyncData.user.states = loadSate.ing;
                var data = learun.storage.get("userData") || {};
                var ver = data.ver || "";

                learun.httpget(config.webapi + "learun/adms/user/map", { ver: ver }, function (data) {
                    if (data) {
                        if (data.ver) {
                            learun.storage.set("userData", data);
                        }
                        clientAsyncData.user.states = loadSate.yes;
                        clientAsyncData.dataItem.init();
                    }
                    else {
                        clientAsyncData.user.states = loadSate.fail;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.user.init();
            if (clientAsyncData.user.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.user.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("userData") || {};
                var data = _dataTmp.data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.user.init();
            if (clientAsyncData.user.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.user.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("userData") || {};
                var data = _dataTmp.data || {};
                op.callback(data, op);
            }
        }
    };
    // 数据字典
    clientAsyncData.dataItem = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.dataItem.states === loadSate.no) {
                clientAsyncData.dataItem.states = loadSate.ing;
                var data = learun.storage.get("dataItemData") || {};
                var ver = data.ver || "";
                learun.httpget(config.webapi + "learun/adms/dataitem/map", { ver: ver }, function (data) {
                    if (data) {
                        if (data.ver) {
                            learun.storage.set("dataItemData", data);
                        }
                        clientAsyncData.dataItem.states = loadSate.yes;
                    }
                    else {
                        clientAsyncData.dataItem.states = loadSate.fail;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.dataItem.init();
            if (clientAsyncData.dataItem.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("dataItemData") || {};
                var data = _dataTmp.data || {};
                op.callback(clientAsyncData.dataItem.find(op.key, data[op.code] || {}) || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.dataItem.init();
            if (clientAsyncData.dataItem.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _dataTmp = learun.storage.get("dataItemData") || {};
                var data = _dataTmp.data || {};
                op.callback(data[op.code] || {}, op);
            }
        },
        find: function (key, data) {
            var res = {};
            for (var id in data) {
                if (data[id].value === key) {
                    res = data[id];
                    break;
                }
            }
            return res;
        }
    };
    // 数据源数据
    clientAsyncData.sourceData = {
        states: {},
        get: function (op) {
            if (clientAsyncData.sourceData.states[op.code] === undefined || clientAsyncData.sourceData.states[op.code] === loadSate.no) {
                clientAsyncData.sourceData.states[op.code] = loadSate.ing;
                clientAsyncData.sourceData.load(op.code);
            }

            if (clientAsyncData.sourceData.states[op.code] === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.sourceData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _tmpData = learun.storage.get("sourceData_" + op.code) || {};
                var data = _tmpData.data || [];
                if (data) {
                    op.callback(clientAsyncData.sourceData.find(op.key, op.keyId, data) || {}, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        getAll: function (op) {
            if (clientAsyncData.sourceData.states[op.code] === undefined || clientAsyncData.sourceData.states[op.code] === loadSate.no) {
                clientAsyncData.sourceData.states[op.code] = loadSate.ing;
                clientAsyncData.sourceData.load(op.code);
            }

            if (clientAsyncData.sourceData.states[op.code] === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.sourceData.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _tmpData = learun.storage.get("sourceData_" + op.code) || {};
                var data = _tmpData.data || [];
                if (data) {
                    op.callback(data, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        load: function (code) {
            var data = learun.storage.get("sourceData_" + code) || {};
            var ver = data.ver || "";
            learun.httpget(config.webapi + "learun/adms/datasource/map", { code: code, ver: ver }, function (data) {
                if (data) {
                    if (data.ver) {
                        learun.storage.set("sourceData_" + code, data);
                    }
                    clientAsyncData.sourceData.states[code] = loadSate.yes;
                }
                else {
                    clientAsyncData.sourceData.states[code] = loadSate.fail;
                }
            });
        },
        find: function (key, keyId, data) {
            var res = {};
            for (var i = 0, l = data.length; i < l; i++) {
                if (data[i][keyId] === key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        }
    };
    // 自定义数据
    var custmerData = {};
    clientAsyncData.custmerData = {
        states: {},
        get: function (op) {
            if (clientAsyncData.custmerData.states[op.url] === undefined || clientAsyncData.custmerData.states[op.url] === loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }

            if (clientAsyncData.custmerData.states[op.url] === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else  {
                var data = custmerData[op.url];
                if (data) {
                    op.callback(clientAsyncData.custmerData.find(op.key, op.keyId, data) || {}, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        getAll: function (op) {
            if (clientAsyncData.custmerData.states[op.url] === undefined || clientAsyncData.custmerData.states[op.url] === loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }

            if (clientAsyncData.custmerData.states[op.url] === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = custmerData[op.url];
                op.callback(data || [], op);
            }
        },
        load: function (url) {
            learun.httpget(config.webapi + url, null, function (data) {
                if (data) {
                    custmerData[url] = data;
                    clientAsyncData.custmerData.states[url] = loadSate.yes;
                }
                else {
                    clientAsyncData.custmerData.states[url] = loadSate.fail;
                }
            });
        },
        find: function (key, keyId, data) {
            var res = {};
            for (var i = 0, l = data.length; i < l; i++) {
                if (data[i][keyId] === key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        }
    };

    // 获取功能列表
    clientAsyncData.module = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.module.states === loadSate.no) {
                clientAsyncData.module.states = loadSate.ing;
                var data = learun.storage.get("moduleData") || {};
                var ver = data.ver || "";
                learun.httpget(config.webapi + "learun/adms/function/list", { ver: ver }, function (data) {
                    if (data) {
                        if (data.ver) {
                            learun.storage.set("moduleData", data);
                        }
                        clientAsyncData.module.states = loadSate.yes;
                    }
                    else {
                        clientAsyncData.module.states = loadSate.fail;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.module.init();
            if (clientAsyncData.module.states === loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.module.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var _tmpData = learun.storage.get("moduleData");
                var data = _tmpData.data || {};
                op.callback(data, op);
            }
        }
    };
    // 获取我的应用列表
    learun.myModule = {
        states: loadSate.no,
        init: function (modules) {
            if (learun.myModule.states === loadSate.no) {
                learun.myModule.states = loadSate.ing;
                var mymoduleData = learun.storage.get("mymoduleData");
                if (mymoduleData === null) {
                    var len = 7;
                    if (len > modules.length) {
                        len = modules.length;
                    }
                    var list = [];
                    for (var i = 0; i < len; i++) {
                        var item = modules[i];
                        list.push(item.F_Id);
                    }
                    learun.storage.set("mymoduleData", list);
                    learun.httppost(config.webapi + "learun/adms/function/mylist/update", String(list), (res) => {
                    });
                    learun.myModule.states = loadSate.yes;
                }
                else {
                    learun.httpget(config.webapi + "learun/adms/function/mylist", null, function (data) {
                        if (data) {
                            learun.storage.set("mymoduleData", data);
                        }
                        learun.myModule.states = loadSate.yes;
                    });
                }
            }
        },
        get: function (modules, callback) {
            learun.myModule.init(modules);
            if (learun.myModule.states === loadSate.ing) {
                setTimeout(function () {
                    learun.myModule.get(modules, callback);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                callback(learun.storage.get("mymoduleData"));
            }
        }
    };
    // 获取单据编码
    learun.getRuleCode = function (code, callback) {
        learun.httpget(config.webapi + "learun/adms/coderule/code", code, function (data) {
            if (data) {
                callback(data);
            }
            else {
                callback('');
            }
        });
    };

    /*选择框方法扩展(包含单选框)*/
    // 选择框扩展(url（后台数据地址）,数据字典,数据源),
    $.fn.lrpickerex = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        if (!op.data) {
            op.data = [];
            switch (op.type) {
                case 'dataItem':
                    $this.lrpicker(op);
                    learun.clientdata.getAll('dataItem', {
                        code: op.code,
                        callback: function (data) {
                            var _tmpdata = [];
                            $.each(data, function (_index, _item) {
                                _tmpdata.push({ value: _item.value, text: _item.text });
                            });

                            $this.lrpickerSetData(_tmpdata);
                        }
                    });
                    break;
                case 'sourceData':
                    $this.lrpicker(op);
                    learun.clientdata.getAll('sourceData', {
                        code: op.code,
                        callback: function (data) {
                            $this.lrpickerSetData(data);
                        }
                    });
                    break;
                default:
                    $this.lrpicker(op);
                    if (op.url) {// 如果有连接地址
                        learun.clientdata.getAll('custmerData', {
                            url: op.url,
                            callback: function (data) {
                                $this.lrpickerSetData(data);
                            }
                        });
                    }
                    break;
            }
        } else {
            $this.lrpicker(op);
        }
        return $this;
    };
    // 组织单位选择
    $.fn.lrselect = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        var dfop = {
            placeholder: '请选择',
            type: 'company',
            companyId: '',
            departmentId: '',
            change: false
        };
        $.extend(dfop, op || {});
        $this.attr('type', 'lrselect');
        $this.addClass('lr-select');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');
        dfop.id = $this.attr('id');
        $this[0].dfop = dfop;

        $this.on('tap', function () {
            var $this = $(this);
            if ($this.attr('readonly') || $this.parents('.lr-form-row').attr('readonly')) {
                return false;
            }
            learun.formblur();

            var op = $this[0].dfop;

            if (op.needPre) {
                learun.layer.toast('请先选择上一级!');
                return false;
            }

            var name = '';
            switch (op.type) {
                case 'company':
                    name = '选择公司';
                    break;
                case 'department':
                    name = '选择部门';
                    break;
                case 'user':
                    name = '选择人员';
                    break;
            }
            learun.nav.go({
                path: 'lrselect/' + op.type, title: name, type: 'right', param: {
                    callback: function (data, _op, $this) {
                        $this.find('.text').text(data.name);
                        _op.value = data.id;
                        _op.text = data.name;
                        _op.change && _op.change(data);
                        $this.trigger('change');
                    },
                    op: op,
                    $this: $this
                }
            });
        });

        return $this;
    };
    $.fn.lrselectGet = function (type) {
        var $this = $(this);
        if ($this.length === 0) {
            return '';
        }
        var op = $this[0].dfop;
        $this = null;
        if (type === 'text') {
            return op.text;
        }
        else {
            return op.value;
        }
    };
    $.fn.lrselectSet = function (value) {
        var $this = $(this);
        if ($this.length === 0) {
            return '';
        }
        var op = $this[0].dfop;
        op.value = value;
        $this = null;
        learun.clientdata.get(op.type, {
            key: value,
            set: op,
            callback: function (data, _op) {
                $('#' + _op.set.id).find('.text').text(data.name || '');
                _op.set.text = data.name || '';
                data.id = _op.key;
                _op.change && _op.change(data);
                $('#' + _op.set.id).trigger('change');
            }
        });
    };
    $.fn.lrselectUpdate = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        $.extend($this[0].dfop, op || {});
        $this[0].dfop.value = '';
        $this[0].dfop.text = '';
        $this.find('.text').text($this[0].dfop.placeholder);
        $this = null;
    };

    // 弹层选择框（数据字典和数据源）
    $.fn.layerSelect = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        var dfop = {
            placeholder: '请选择',
            type: 'dataItem',// sourceData
            layerData:[],
            callback: false
        };
        $.extend(dfop, op || {});
        $this.attr('type', 'lrlayerSelect');
        $this.addClass('lr-layerSelect');
        $this.html('<div class="text">' + dfop.placeholder + '</div>');
        dfop.id = $this.attr('id');
        $this[0].dfop = dfop;

        $this.on('tap', function () {
            var $this = $(this);
            if ($this.attr('readonly') || $this.parents('.lr-form-row').attr('readonly')) {
                return false;
            }

            learun.formblur();
            var op = $this[0].dfop;
            learun.nav.go({
                path: 'lrselect/layer', title: op.placeholder, type: 'right', param: {
                    callback: function (data, _op, _$this) {
                        _op.callback && _op.callback(data, _op.layerData, _$this);
                        _$this = null;
                    },
                    op: op,
                    $this: $this
                }
            });
            $this = null;
            return false;
        });

        return $this;

    };
    $.fn.layerSelectSet = function (value) {
        var $this = $(this);
        var op = $(this)[0].dfop;
        if (value) {
            $this.find('.text').text(value);
            op.value = value;
        }
        else {
            op.value = '';
            $(this).find('.text').text(op.placeholder);
        }
    };
    $.fn.layerSelectGet = function () {
        return $(this)[0].dfop.value;
    };

    /*多选框方法扩展*/
    $.fn.lrcheckboxex = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        if (!op.data) {
            op.data = [];
            switch (op.type) {
                case 'dataItem':
                    $this.lrcheckbox(op);
                    learun.clientdata.getAll('dataItem', {
                        code: op.code,
                        callback: function (data) {
                            var _tmpdata = [];
                            $.each(data, function (_index, _item) {
                                _tmpdata.push({ value: _item.value, text: _item.text });
                            });

                            $this.lrcheckboxSetData(_tmpdata);
                        }
                    });
                    break;
                case 'sourceData':
                    $this.lrcheckbox(op);
                    learun.clientdata('sourceData', {
                        code: op.code,
                        callback: function (data) {
                            $this.lrcheckboxSetData(data);
                        }
                    });
                    break;
                default:
                    $this.lrcheckbox(op);
                    if (op.url) {// 如果有连接地址
                        learun.clientdata('custmerData', {
                            url: op.url,
                            callback: function (data) {
                                $this.lrcheckboxSetData(data);
                            }
                        });
                    }
                    break;
            }
        } else {
            $this.lrcheckbox(op);
        }
        return $this;
    };

    /*编辑表格方法扩展*/
    function setGridRowValue($block, id, type, value) {// 设置编辑表格每个块的值
        switch (type) {
            case 'label':
            case 'input':
                $block.find('#' + id).val(value);
                break;
            case 'radio':
            case 'select':
                $block.find('#' + id).lrpickerSet(value);
                break;
            case 'checkbox':
                $block.find('#' + id).lrcheckboxSet(value);
                break;
            case 'layer':
                $block.find('#' + id).layerSelectSet(value);
                break;
            case 'datetime':
                $block.find('#' + id).lrdateSet(value);
                break;
        }
        $block = null;
    }
    function getGridRowValue($block, id, type) {// 设置编辑表格每个块的值
        var v = '';
        switch (type) {
            case 'label':
            case 'input':
                v = $block.find('#' + id).val();
                break;
            case 'radio':
            case 'select':
                v = $block.find('#' + id).lrpickerGet();
                break;
            case 'checkbox':
                v = $block.find('#' + id).lrcheckboxGet();
                break;
            case 'layer':
                v = $block.find('#' + id).layerSelectGet();
                break;
            case 'datetime':
                v = $block.find('#' + id).lrdateGet();
                break;
        }
        $block = null;
        return v;
    }
    function addGridRow($this, op) {
        // 添加编辑集合块
        var $html = $('<div class="lr-edit-grid-block" ></div>');
        if (op.hasBtn) {
            $this.find('.lr-edit-grid-btn').before($html);
        }
        else {
            $this.append($html);
        }
        //  添加标题栏
        var $title = $('<div class="lr-edit-grid-title" >' + op.title + '(<span>' + op._index + '</span>)</div>');
        if (op._index > 1) {
            $title.append('<div class="lr-edit-grid-deletebtn" data-value="' + op._index + '">删除</div>');
        }
        $html.html($title);

        var _compontHtml = '';
        op.compontsMap = op.compontsMap || {};
        // 编辑表格-组件初始化
        $.each(op.componts, function (_index, _item) {
            if (op._index === 1) {
                op.compontsMap[_item.field] = _item;
            }

            switch (_item.type) {
                case 'label':
                    _compontHtml = '<div class="lr-edit-grid-row" ><label>' + _item.name + '</label><input type="text" readonly id="' + _item.field + '" ></div>';
                    $html.append(_compontHtml);
                    break;
                case 'input':
                    _compontHtml = '<div class="lr-edit-grid-row" ><label>' + _item.name + '</label><input type="text" id="' + _item.field + '" ></div>';
                    $html.append(_compontHtml);
                    // 值改变
                    if (_item.change) {
                        $html.find('#' + _item.field).on('input propertychange', { change: _item.change }, function (e) {
                            e = e || window.event;
                            var _change = e.data.change;
                            var $this = $(this);
                            var _$block = $this.parents('.lr-edit-grid-block');
                            var v = $this.val();
                            _change(v, _$block);

                            $this = null;
                            _$block = null;
                        });
                    }
                    break;
                case 'radio':
                case 'select':
                    _compontHtml = '<div class="lr-edit-grid-row" ><label>' + _item.name + '</label><div id="' + _item.field + '" ></div></div>';
                    $html.append(_compontHtml);
                    $html.find('#' + _item.field).lrpickerex({
                        code: _item.code,
                        type: _item.datatype,
                        ivalue: _item.ivalue,
                        itext: _item.itext,
                        data: _item.data,
                        url: _item.url,
                        change: function (value, text, datalist, $self) {
                            var _$block = $self.parents('.lr-edit-grid-block');
                            _item.change && _item.change(value, text, datalist, _$block);
                            $self = null;
                            _$block = null;
                        }
                    });
                    break;
                case 'checkbox':
                    _compontHtml = '<div class="lr-edit-grid-row" ><label>' + _item.name + '</label><div id="' + _item.field + '" ></div></div>';
                    $html.append(_compontHtml);
                    $html.find('#' + _item.field).lrcheckboxex({
                        code: _item.code,
                        type: _item.datatype,
                        ivalue: _item.ivalue,
                        itext: _item.itext,
                        data: _item.data,
                        url: _item.url,
                        change: function (value, text, datalist, $self) {
                            var _$block = $self.parents('.lr-edit-grid-block');
                            _item.change && _item.change(value, text, datalist, _$block);
                            $self = null;
                            _$block = null;
                        }
                    });
                    break;
                case 'layer':
                    _compontHtml = '<div class="lr-edit-grid-row" ><label>' + _item.name + '</label><div id="' + _item.field + '" ></div></div>';
                    $html.append(_compontHtml);
                    $html.find('#' + _item.field).layerSelect({
                        code: _item.code,
                        type: _item.datatype,
                        layerData: _item.layerData,
                        callback: function (data, layerData, _$this) {
                            var $block = _$this.parents('.lr-edit-grid-block');
                            $.each(layerData, function (_jindex, _jitem) {
                                setGridRowValue($block, _jitem.value, op.compontsMap[_jitem.value].type, data[_jitem.name]);
                            });
                            _item.change && _item.change(data, $block);
                            $block = null;
                            _$this = null;
                        }

                    });
                    break;
                case 'datetime':
                    _compontHtml = '<div class="lr-edit-grid-row" ><label>' + _item.name + '</label><div id="' + _item.field + '" ></div></div>';
                    $html.append(_compontHtml);
                    $html.find('#' + _item.field).lrdate({
                        type: _item.datetime === 'date' ? 'date' : 'datetime',
                        change: function (v, _$this) {
                            var $block = _$this.parents('.lr-edit-grid-block');
                            _item.change && _item.change(v, $block);

                            $block = null;
                            _$this = null;
                        }
                    });
                    break;
                default:
                    break;
            }
        });

        $title = null;
        $this = null;

        return $html; 
    }
   
    $.fn.lrgrid = function (op) {
        var $this = $(this);
        if ($this.length === 0) {
            return $this;
        }
        if ($this[0].dfop) {
            return $this;
        }

        var dfop = {
            title: '编辑表格',
            componts: [],
            hasBtn: true,
            _index: 1
        };
        $.extend(dfop, op || {});

        $this[0].dfop = dfop;

        $this.addClass('lr-edit-gird');

        if (dfop.hasBtn) {
            // 添加增加按钮
            var $btn = $('<div class="lr-edit-grid-btn" ><i class="iconfont icon-add1" ></i>增加' + (dfop.title || '') + '</div>');
            $btn.on('tap', { op: dfop }, function (e) {// 添加一块编辑集合
                e = e || window.event;
                var _op = e.data.op;
                var $grid = $(this).parents('.lr-edit-gird');
                _op._index++;// 增加块数
                learun.formblur();
                addGridRow($grid, _op);
            });
            $this.html($btn);

            // 注册删除按钮事件
            $this.delegate('.lr-edit-grid-deletebtn', 'tap', { op: dfop }, function (e) {
                e = e || window.event;
                var _op = e.data.op;
                var $my = $(this);
                var myIndex = $my.attr('data-value');
                learun.formblur();
                learun.layer.confirm('你确定要删除' + _op.title + myIndex + '吗？', function (isOk) {
                    if (isOk === '1') {
                        _op._index--;// 减少块数
                       
                        // 排在后面的块需要重新调整序号
                        $my.parents('.lr-edit-gird').find('.lr-edit-grid-block:gt(' + (parseInt(myIndex) - 1) + ')').each(function () {
                            var $this = $(this);
                            var $deletebtn = $this.find('.lr-edit-grid-deletebtn');
                            var _index = parseInt($deletebtn.attr('data-value')) - 1;
                            $deletebtn.attr('data-value', _index);
                            $this.find('.lr-edit-grid-title span').text(_index);
                            $deletebtn = null;
                            $this = null;
                        });
                        // 移除绑定在自己身上的弹层，选择框，单选框，多选框，日期
                        var $block = $my.parents('.lr-edit-grid-block');
                        $block.find('.lr-date,.lr-picker,.lr-checkbox').each(function () {
                            var $this = $(this);
                            var _op = $this[0].fop;
                            $('#pop_' + _op.id).remove();
                            $('#dt_' + _op.id).remove();
                        });
                        // 移除自己
                        $block.remove();
                        $block = null;
                    }
                    $my = null;
                }, '', ['取消', '确定']);
            });
        }
        addGridRow($this, dfop);
        return $this;
    };

    $.fn.lrgridSet = function (data) {
        var $this = $(this);
        var op = $this[0].dfop;
        $.each(data, function (_index, _item) {
            if (_index === 0) {
                // 如果是第一行数据
                $.each(op.componts, function (_jindex, jitem) {
                    var _id = jitem.field;
                    if (op.isToLowerCase) {
                        _id = _id.toLowerCase();
                    }
                    setGridRowValue($this.find('.lr-edit-grid-block'), jitem.field, jitem.type, _item[_id]);
                });
            } else {
                // 如果不是第一行数据就先添加一行
                op._index++;// 增加块数
                var $block = addGridRow($this, op);
                $.each(op.componts, function (_jindex, jitem) {
                    var _id = jitem.field;
                    if (op.isToLowerCase) {
                        _id = _id.toLowerCase();
                    }
                    setGridRowValue($block, jitem.field, jitem.type, _item[_id]);
                });
            }
        });

        $this = null;
    };

    $.fn.lrgridGet = function () {
        var $this = $(this);
        var op = $this[0].dfop;
        var data = [];
        $this.find('.lr-edit-grid-block').each(function () {
            var $block = $(this);
            var point = {};
            $.each(op.componts, function (_jindex, jitem) {
                point[jitem.field] = getGridRowValue($block, jitem.field, jitem.type);
            });
            data.push(point);
        });

        $this = null;
        return data;
    };

    // 设置表单组件为只读
    $.fn.setFormRead = function () {
        $(this).find('.lr-form-row').each(function () {
            $(this).attr('readonly', 'readonly');
        });
    }

    $.fn.setFormWrite = function () {
        $(this).find('.lr-form-row').each(function () {
            $(this).removeAttr('readonly');
        });
    }

    // 数据格式化
    $.fn.dataFormatter = function (op) {
        var $this = $(this);
        var _v = '';
        if (op.value === null || op.value === undefined || op.value === 'null' || op.value === 'undefined') {
            op.value = '';
        }
        op.value = op.value + '';
        switch (op.type) {
            case 'datetime':
                $this.append(learun.date.format(op.value, op.dateformat));
                break;
            case 'dataItem':
                _v = op.value.split(',');
                $.each(_v || [], function (_index, _item) {
                    if (_item) {
                        learun.clientdata.get('dataItem', {
                            key: _item,
                            code: op.code,
                            $this: $this,
                            callback: function (_data, _op) {
                                _op.$this.append(_data.text);
                            }
                        });
                    }
                });
                break;
            case 'dataSource':
                _v = op.value.split(',');
                $.each(_v || [], function (_index, _item) {
                    if (_item) {
                        learun.clientdata.get('sourceData', {
                            key: _item,
                            keyId: op.keyId,
                            code: op.code,
                            set: op,
                            $this: $this,
                            callback: function (_data, _op) {
                                _op.$this.append(_data[_op.set.text]);
                            }
                        });
                    }
                });
                break;
            case 'dataCustmer':
                _v = op.value.split(',');
                $.each(_v || [], function (_index, _item) {
                    if (_item) {
                        learun.clientdata.get('custmerData', {
                            key: _item,
                            keyId: op.keyId,
                            url: op.url,
                            set: op,
                            $this: $this,
                            callback: function (_data, _op) {
                                _op.$this.append(_data[_op.set.text]);
                            }
                        });
                    }
                });
                break;
            case 'organize':
                learun.clientdata.get(op.dataType, {
                    key: op.value,
                    $this: $this,
                    callback: function (_data,_op) {
                        _op.$this.append(_data.name);
                    }
                });
                break;
            default:
                $this.append(op.value);
                break;
        }
        return $this;
    }

    // 图片选择，上传，下载
    $.fn.lrImagepicker = function (op) {
        var dfop = {
            maxCount: 9,
            isOnlyCamera: false,
            uploadUrl: config.webapi + 'learun/adms/annexes/upload',
            downUrl: config.webapi + 'learun/adms/annexes/down?data=',
            getParams: function () {
                var param = {};
                var logininfo = learun.storage.get('logininfo');
                param.token = logininfo.token;
                param.loginMark = learun.deviceId();
                return param;
            },
            deleteImg: function (fileId) {
                learun.httppost(config.webapi + 'learun/adms/annexes/delete', fileId, function () { });
            },
            downFile: function (value, callback) {
                learun.httpget(config.webapi + 'learun/adms/annexes/list', value, function (data) {
                    if (data) {
                        var _data = [];
                        $.each(data, function (_index, _item) {
                            if (_item.F_FileType === 'jpg' || _item.F_FileType === 'png') {
                                var _point = {
                                    id: _item.F_Id,
                                    name: _item.F_Id + '.' + _item.F_FileType
                                };
                                _data.push(_point);
                            }
                        });
                        callback(_data);
                    }
                });
            }
        };
        $.extend(dfop, op || {});
        
        return $(this).imagepicker(dfop);
    }

})(window.jQuery, window.lrmui);