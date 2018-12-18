/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.17
 * 描 述：获取客户端数据
 */
/*
`*******************登录后数据***********************
 *userinfo----------------------用户登录信息

 *modules-----------------------功能模块
 *modulesTree-------------------按照父节点的功能模块
 *modulesMap--------------------主键对应实例数据

 *******************使用时异步获取*******************
 *user--------------------------用户数据
    learun.clientdata.getAsync('user', {
        userId: value,
        callback: function (item) {
            callback(item.F_RealName);
        }
    });
 *department--------------------部门数据
    learun.clientdata.getAsync('department', {
        key: value,
        companyId: row.F_CompanyId,
        callback: function (item) {
            callback(item.F_FullName);
        }
    });
 *company----------------------公司
    learun.clientdata.getAsync('company', {
            key: value,
            callback: function (_data) {
                _data.F_FullName
            }
   });
 *db--------------------------数据库
    learun.clientdata.getAsync('db', {
            key: value,
            callback: function (_data) {
                _data.F_DBName
            }
   });
 *dataItem--------------------数据字典
 learun.clientdata.getAsync('dataItem', {
            key: value,
            itemCode:itemCode,
            callback: function (_data) {
                _data.F_ItemName
            }
   });
 *custmerData-----------------自定义数据
 learun.clientdata.getAsync('custmerData', {
        url: url,
        key: value,
        valueId: valueId,
        callback: function (item) {
            callback(item.F_FullName);
        }
    });
*/

/*
*登录成功后自动去加载基础数据
*公司
*部门
*人员
*数据字典
*数据源数据（数据源数据设置不要过大）
*数据库连接数据（移动端不需要）
*/

(function ($, learun) {
    "use strict";

    var loadSate = {
        no: -1,  // 还未加载
        yes: 1,  // 已经加载成功
        ing: 0,  // 正在加载中
        fail: 2  // 加载失败
    };

    var clientDataFn = {};
    var clientAsyncData = {};

    var clientData = {};


    function initLoad(callback) {
        var res = loadSate.yes;
        for (var id in clientDataFn) {
            var _fn = clientDataFn[id];
            if (_fn.state == loadSate.fail) {
                res = loadSate.fail;
                break;
            }
            else if (_fn.state == loadSate.no) {
                res = loadSate.ing;
                _fn.init();
            }
            else if (_fn.state == loadSate.ing) {
                res = loadSate.ing;
            }
        }
        if (res == loadSate.yes) {
            callback(true);
        } else if (res == loadSate.fail) {
            callback(false);
        }
        else {
            setTimeout(function () {
                initLoad(callback);
            }, 100);
        }
    }
    function get(key, data) {
        var res = "";
        var len = data.length;
        if (len == undefined) {
            res = data[key];
        }
        else {
            for (var i = 0; i < len; i++) {
                if (key(data[i])) {
                    res = data[i];
                    break;
                }
            }
        }
        return res;
    }

    learun.clientdata = {
        init: function (callback) {
            initLoad(function (res) {
                callback(res);
                if (res) {// 开始异步加载数据
                    clientAsyncData.company.init();
                }
            });
        },
        get: function (nameArray) {//[key,function (v) { return v.key == value }]
            var res = "";
            if (!nameArray) {
                return res;
            }
            var len = nameArray.length;
            var data = clientData;
            for (var i = 0; i < len; i++) {
                res = get(nameArray[i], data);
                if (res != "" && res != undefined) {
                    data = res;
                }
                else {
                    break;
                }
            }
            res = res || "";
            return res;
        },
        getAsync: function (name, op) {//
            return clientAsyncData[name].get(op);
        },
        getAllAsync: function (name, op) {//
            return clientAsyncData[name].getAll(op);
        },
        getsAsync: function (name, op) {//
            return clientAsyncData[name].gets(op);
        },
        update: function (name) {
            clientAsyncData[name].update && clientAsyncData[name].update();
        }
    };


    /*******************登录后数据***********************/
    // 注册数据的加载方法
    // 功能模块数据
    clientDataFn.modules = {
        state: loadSate.no,
        init: function () {
            //初始化加载数据
            clientDataFn.modules.state = loadSate.ing;
            learun.httpAsyncGet($.rootUrl + '/LR_SystemModule/Module/GetModuleList', function (res) {
                if (res.code == learun.httpCode.success) {
                    clientData.modules = res.data;
                    clientDataFn.modules.toMap();
                    clientDataFn.modules.state = loadSate.yes;
                }
                else {
                    clientData.modules = [];
                    clientDataFn.modules.toMap();
                    clientDataFn.modules.state = loadSate.fail;
                }
            });
        },
        toMap: function () {
            //转化成树结构 和 转化成字典结构
            var modulesTree = {};
            var modulesMap = {};
            var _len = clientData.modules.length;
            for (var i = 0; i < _len; i++) {
                var _item = clientData.modules[i];
                if (_item.F_EnabledMark == 1) {
                    modulesTree[_item.F_ParentId] = modulesTree[_item.F_ParentId] || [];
                    modulesTree[_item.F_ParentId].push(_item);
                    modulesMap[_item.F_ModuleId] = _item;
                }
            }
            clientData.modulesTree = modulesTree;
            clientData.modulesMap = modulesMap;
        }
    };
    // 登录用户信息
    clientDataFn.userinfo = {
        state: loadSate.no,
        init: function () {
            //初始化加载数据
            clientDataFn.userinfo.state = loadSate.ing;
            learun.httpAsyncGet($.rootUrl + '/Login/GetUserInfo', function (res) {
                if (res.code == learun.httpCode.success) {
                    clientData.userinfo = res.data;
                    clientDataFn.userinfo.state = loadSate.yes;
                }
                else {
                    clientDataFn.userinfo.state = loadSate.fail;
                }
            });
        }
    };

    /*******************使用时异步获取*******************/
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
                clientData[name] = data;
            }
        }
    };
    // 公司信息
    clientAsyncData.company = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.company.states == loadSate.no) {
                clientAsyncData.company.states = loadSate.ing;
                var ver = storage.get("companyData").ver || "";
                learun.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Company/GetMap', { ver: ver }, function (data) {
                    if (!data) {
                        clientAsyncData.company.states = loadSate.fail;
                    } else {
                        if (data.ver) {
                            storage.set("companyData", data);
                        }
                        clientAsyncData.company.states = loadSate.yes;
                        clientAsyncData.department.init();
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.company.init();
            if (clientAsyncData.company.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.company.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("companyData").data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.company.init();
            if (clientAsyncData.company.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.company.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("companyData").data || {};
                op.callback(data, op);
            }
        }
    };
    // 部门信息
    clientAsyncData.department = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.department.states == loadSate.no) {
                clientAsyncData.department.states = loadSate.ing;
                var ver = storage.get("departmentData").ver || "";
                learun.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/Department/GetMap', { ver: ver }, function (data) {
                    if (!data) {
                        clientAsyncData.department.states = loadSate.fail;
                    } else {
                        if (data.ver) {
                            storage.set("departmentData", data);
                        }
                        clientAsyncData.department.states = loadSate.yes;
                        clientAsyncData.user.init();
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.department.init();
            if (clientAsyncData.department.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.department.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("departmentData").data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.department.init();
            if (clientAsyncData.department.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.department.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("departmentData").data || {};
                op.callback(data, op);
            }
        }
    };
    // 人员信息
    clientAsyncData.user = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.user.states == loadSate.no) {
                clientAsyncData.user.states = loadSate.ing;
                var ver = storage.get("userData").ver || "";
                learun.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/User/GetMap', { ver: ver }, function (data) {
                    if (!data) {
                        clientAsyncData.user.states = loadSate.fail;
                    } else {
                        if (data.ver) {
                            storage.set("userData", data);
                        }
                        clientAsyncData.user.states = loadSate.yes;
                        clientAsyncData.dataItem.init();
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.user.init();
            if (clientAsyncData.user.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.user.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("userData").data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.user.init();
            if (clientAsyncData.user.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.user.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("userData").data || {};
                op.callback(data, op);
            }
        }
    };
    // 数据字典
    clientAsyncData.dataItem = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.dataItem.states == loadSate.no) {
                clientAsyncData.dataItem.states = loadSate.ing;
                var ver = storage.get("dataItemData").ver || "";
                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataItem/GetMap', { ver: ver }, function (data) {
                    if (!data) {
                        clientAsyncData.dataItem.states = loadSate.fail;
                    } else {
                        if (data.ver) {
                            storage.set("dataItemData", data);
                        }
                        clientAsyncData.dataItem.states = loadSate.yes;
                        clientAsyncData.db.init();
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.dataItem.init();
            if (clientAsyncData.dataItem.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("dataItemData").data || {};

                // 数据字典翻译
                var _item = clientAsyncData.dataItem.find(op.key, data[op.code] || {});
                if (_item) {
                    top.learun.language.get(_item.text, function (text) {
                        _item.text = text;
                        op.callback(_item, op);
                    });
                }
                else {
                    op.callback({}, op);
                }
            }
        },
        getAll: function (op) {
            clientAsyncData.dataItem.init();
            if (clientAsyncData.dataItem.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("dataItemData").data || {};
                var res = {};
                $.each(data[op.code] || {}, function (_index, _item) {
                    _item.text = top.learun.language.getSyn(_item.text);
                    res[_index] = _item;
                });
                op.callback(res, op);
            }
        },
        gets: function (op) {
            clientAsyncData.dataItem.init();
            if (clientAsyncData.dataItem.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.dataItem.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("dataItemData").data || {};

                var keyList = op.key.split(',');
                var _text = []
                $.each(keyList, function (_index, _item) {
                    var _item = clientAsyncData.dataItem.find(_item, data[op.code] || {});
                    top.learun.language.get(_item.text, function (text) {
                        _text.push(text);
                    });
                });
                op.callback(String(_text), op);
            }
        },
        find: function (key, data) {
            var res = {};
            for (var id in data) {
                if (data[id].value == key) {
                    res = data[id];


                    break;
                }
            }
            return res;
        },
        update: function () {
            clientAsyncData.dataItem.states = loadSate.no;
            clientAsyncData.dataItem.init();
        }
    };
    // 数据库连接数据
    clientAsyncData.db = {
        states: loadSate.no,
        init: function () {
            if (clientAsyncData.db.states == loadSate.no) {
                clientAsyncData.db.states = loadSate.ing;
                var ver = storage.get("dbData").ver || "";
                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetMap', { ver: ver }, function (data) {
                    if (!data) {
                        clientAsyncData.db.states = loadSate.fail;
                    } else {
                        if (data.ver) {
                            storage.set("dbData", data);
                        }
                        clientAsyncData.db.states = loadSate.yes;
                    }
                });
            }
        },
        get: function (op) {
            clientAsyncData.db.init();
            if (clientAsyncData.db.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.db.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else{
                var data = storage.get("dbData").data || {};
                op.callback(data[op.key] || {}, op);
            }
        },
        getAll: function (op) {
            clientAsyncData.db.init();
            if (clientAsyncData.db.states == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.db.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("dbData").data || {};
                op.callback(data, op);
            }
        }
    };
    // 数据源数据
    clientAsyncData.sourceData = {
        states: {},
        get: function (op) {
            if (clientAsyncData.sourceData.states[op.code] == undefined || clientAsyncData.sourceData.states[op.code] == loadSate.no) {
                clientAsyncData.sourceData.states[op.code] = loadSate.ing;
                clientAsyncData.sourceData.load(op.code);
            }

            if (clientAsyncData.sourceData.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.sourceData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = storage.get("sourceData_" + op.code).data || [];
                if (!!data) {
                    op.callback(clientAsyncData.sourceData.find(op.key, op.keyId, data) || {}, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        getAll: function (op) {
            if (clientAsyncData.sourceData.states[op.code] == undefined || clientAsyncData.sourceData.states[op.code] == loadSate.no) {
                clientAsyncData.sourceData.states[op.code] = loadSate.ing;
                clientAsyncData.sourceData.load(op.code);
            }

            if (clientAsyncData.sourceData.states[op.code] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.sourceData.getAll(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else if (clientAsyncData.sourceData.states[op.code] == loadSate.yes) {
                var data = storage.get("sourceData_" + op.code).data || [];

                if (!!data) {
                    op.callback(data, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        load: function (code) {
            var ver = storage.get("sourceData_" + code).ver || "";
            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetMap', { code: code, ver: ver }, function (data) {
                if (!data) {
                    clientAsyncData.sourceData.states[code] = loadSate.fail;
                } else {
                    if (data.ver) {
                        storage.set("sourceData_" + code, data);
                    }
                    clientAsyncData.sourceData.states[code] = loadSate.yes;
                }
            });
        },
        find: function (key, keyId, data) {
            var res = {};
            for (var i = 0, l = data.length; i < l; i++) {
                if (data[i][keyId] == key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        }
    };
    // 获取自定义数据 url key valueId
    clientAsyncData.custmerData = {
        states: {},
        get: function (op) {
            if (clientAsyncData.custmerData.states[op.url] == undefined || clientAsyncData.custmerData.states[op.url] == loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }
            if (clientAsyncData.custmerData.states[op.url] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientData[op.url] || [];
                if (!!data) {
                    op.callback(clientAsyncData.custmerData.find(op.key, op.keyId, data) || {}, op);
                } else {
                    op.callback({}, op);
                }
            }
        },
        gets: function (op) {
            if (clientAsyncData.custmerData.states[op.url] == undefined || clientAsyncData.custmerData.states[op.url] == loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }
            if (clientAsyncData.custmerData.states[op.url] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientData[op.url] || [];
                if (!!data) {
                    var keyList = op.key.split(',');
                    var _text = []
                    $.each(keyList, function (_index, _item) {
                        var _item = clientAsyncData.custmerData.find(op.key, op.keyId, data) || {};
                        if (_item[op.textId]) {
                            _text.push(_item[op.textId]);
                        }
                       
                    });
                    op.callback(String(_text), op); 
                } else {
                    op.callback('', op);
                }
            }
        },
        getAll: function (op) {
            if (clientAsyncData.custmerData.states[op.url] == undefined || clientAsyncData.custmerData.states[op.url] == loadSate.no) {
                clientAsyncData.custmerData.states[op.url] = loadSate.ing;
                clientAsyncData.custmerData.load(op.url);
            }
            if (clientAsyncData.custmerData.states[op.url] == loadSate.ing) {
                setTimeout(function () {
                    clientAsyncData.custmerData.get(op);
                }, 100);// 如果还在加载100ms后再检测
            }
            else {
                var data = clientData[op.url] || [];
                if (!!data) {
                    op.callback(data, op);
                } else {
                    op.callback([], op);
                }
            }
        },
        load: function (url) {
            learun.httpAsync('GET', top.$.rootUrl + url, {}, function (data) {
                if (!!data) {
                    clientData[url] = data;
                }
                clientAsyncData.custmerData.states[url] = loadSate.yes;
            });
        },
        find: function (key, keyId, data) {
            var res = {};
            for (var i = 0, l = data.length; i < l; i++) {
                if (data[i][keyId] == key) {
                    res = data[i];
                    break;
                }
            }
            return res;
        }
    };
})(window.jQuery, top.learun);