/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.06.20
 * 描 述：文件管理	
 */

var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";

    var _url = "/LR_OAModule/ResourceFile/GetListJson";
    var fileId = "";

    var page = {
        init: function () {
            page.initleft();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            //返回上一级、返回所有文件
            $(".crumb-path span").click(function () {
                var value = $(this).attr('data-folderId');
                var folderId = $(".crumb-path span:last").attr('data-folderId');
                console.log(value);
                if (value == "back") {
                    if (folderId == 0) {
                        $(".crumb-path .back").hide();
                    }
                    $.lrSetForm(top.$.rootUrl + _url + '?folderId=' + folderId, function (data) {//
                        $('#gridTable').jfGridSet('refreshdata', data);
                    });
                    $(".crumb-path span:last").remove();
                } else {
                    $.lrSetForm(top.$.rootUrl + _url + '?folderId=0', function (data) {//
                        $('#gridTable').jfGridSet('refreshdata', data);
                    });
                    $(".crumb-path .back").hide();
                    $(".crumb-path .add").remove();
                }
            });
            //上传文件
            $('#lr-uploadify').on('click', function () {
                learun.layerForm({
                    id: 'UploadifyForm',
                    title: '上传文件',
                    url: top.$.rootUrl + '/LR_OAModule/ResourceFile/UploadifyForm?folderId=' + fileId,
                    width: 600,
                    height: 400,
                    btn: null,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            //新建文件夹
            $('#lr-addfolder').on('click', function () {
                learun.layerForm({
                    id: 'FolderForm',
                    title: '添加文件夹',
                    url: top.$.rootUrl + '/LR_OAModule/ResourceFile/FolderForm?parentId=' + fileId,
                    width: 400,
                    height: 200,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            //文件下载
            $('#lr-download').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    if (fileType != 'folder') {
                        learun.download({ url: top.$.rootUrl + '/LR_OAModule/ResourceFile/DownloadFile', param: { keyValue: keyValue }, method: 'POST' });
                    } else {
                        learun.alert.error('目前不支持文件夹下载');
                    }
                } else {
                    learun.alert.warning('请选择要下载的文件！');
                }
            });
            //文件预览
            $('#lr-preview').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                var rowData = $("#gridTable").jfGridGet('rowdata');
                if (keyValue) {
                    if (fileType != 'folder') {
                        learun.layerForm({
                            id: 'PreviewForm',
                            title: '文件预览',
                            url: top.$.rootUrl + '/LR_OAModule/ResourceFile/PreviewFile?fileId=' + rowData.F_FileId,
                            width: 1080,
                            height: 850,
                            btn: null,
                            callBack: function (id) {
                                return top[id].acceptClick(refreshGirdData);
                            }
                        });
                    } else {
                        learun.alert.error('请选择文件');
                    }
                } else {
                    learun.alert.warning('请选择要预览的文件！');
                }
            });
            //文件（夹）删除
            $('#lr-delete').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerConfirm("注：您确定要删除此" + (fileType == "folder" ? "文件夹" : "文件") + "吗？可在回收站还原！", function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/RemoveForm', { keyValue: keyValue, fileType: fileType }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                } else {
                    learun.alert.warning('请选择要删除的文件夹或文件！');
                }
            });
            //文件共享
            $('#lr-share').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerConfirm("注：您确定要共享此" + (fileType == "folder" ? "文件夹" : "文件") + "吗？", function (res) {
                        if (res) {
                            learun.warning(top.$.rootUrl + '/LR_OAModule/ResourceFile/ShareFile', { keyValue: keyValue, IsShare: 1, fileType: fileType }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                } else {
                    learun.alert.warning('请选择要共享的文件夹或文件！');
                }
            });
            //取消共享
            $('#lr-cancelshare').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerConfirm("注：您确定要取消共享此" + (fileType == "folder" ? "文件夹" : "文件") + "吗？", function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/ShareFile', { keyValue: keyValue, IsShare: 0, fileType: fileType }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                } else {
                    learun.alert.warning('请选择要取消共享的文件夹或文件！');
                }
            });
            //文件（夹）重命名
            $('#lr-rename').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerForm({
                        id: (fileType == "folder" ? "FolderForm" : "FileForm"),
                        title: (fileType == "folder" ? "文件夹" : "文件") + '重命名',
                        url: top.$.rootUrl + '/LR_OAModule/ResourceFile/' + (fileType == "folder" ? "FolderForm" : "FileForm") + '?keyValue=' + keyValue,
                        width: 400,
                        height: 200,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                } else {
                    learun.alert.warning('请选择要重命名的文件夹或文件！');
                }
            });
            //文件（夹）移动
            $('#lr-move').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerForm({
                        id: 'MoveLocation',
                        title: (fileType == "folder" ? "文件夹" : "文件") + '移动',
                        url: top.$.rootUrl + '/LR_OAModule/ResourceFile/MoveForm?keyValue=' + keyValue + "&fileType=" + fileType,
                        width: 400,
                        height: 350,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                } else {
                    learun.alert.warning('请选择要移动的文件夹或文件！');
                }
            });
            //文件（夹）还原
            $('#lr-restoreFile').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerConfirm("注：您确定要还原此" + (fileType == "folder" ? "文件夹" : "文件") + "吗？", function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/RestoreFile', { keyValue: keyValue, fileType: fileType }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                } else {
                    learun.alert.warning('请选择要还原的文件夹或文件！');
                }
            });
            //文件（夹）彻底删除
            $('#lr-thoroughDelete').on('click', function () {
                var keyValue = $("#gridTable").jfGridValue("F_FileId");
                var fileType = $("#gridTable").jfGridValue("F_FileType");
                if (keyValue) {
                    learun.layerConfirm("注：您确定要删除此" + (fileType == "folder" ? "文件夹" : "文件") + "吗？该操作将无法恢复！", function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/ThoroughRemoveForm', { keyValue: keyValue, fileType: fileType }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                } else {
                    learun.alert.warning('请选择要删除的文件夹或文件！');
                }
            });
            //清空回收站
            $('#lr-emptyRecycled').on('click', function () {
                learun.layerConfirm("注：您确定要清空回收站吗？该操作将无法恢复！", function (res) {
                    if (res) {
                        learun.deleteForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/EmptyRecycledForm', {}, function () {
                            refreshGirdData();
                        });
                    }
                });
            });
        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                $("#txt_Keyword").val('');
                $("#lr-uploadify").hide();
                $("#lr-addfolder").hide();
                $("#lr-download").hide();
                $("#lr-delete").hide();
                $("#lr-share").hide();
                $("#lr-cancelshare").hide();
                $("#lr-more").hide();
                $("#lr-restoreFile").hide();
                $("#lr-thoroughDelete").hide();
                $("#lr-emptyRecycled").hide();
                $(".crumb-path .back").hide();
                $(".crumb-path .add").remove();


                var $this = $(this);
                var $parent = $this.parent();
                $parent.find('.active').removeClass('active');
                $this.addClass('active');
                var data_value = $this.context.dataset.value;

                
                switch (data_value) {
                    case "allFile":
                        _url = "/LR_OAModule/ResourceFile/GetListJson";
                        $(".crumb-path").find('[data-folderid=allfile]').html('所有文件');
                        $("#lr-uploadify").show();
                        $("#lr-addfolder").show();
                        $("#lr-download").show();
                        $("#lr-delete").show();
                        $("#lr-share").show();
                        $("#lr-more").show();
                        break;
                    case "allDocument":
                        _url = "/LR_OAModule/ResourceFile/GetDocumentListJson";
                        $(".crumb-path").find('[data-folderid=allfile]').html('所有文档');
                        $("#lr-download").show();
                        $("#lr-delete").show();
                        $("#lr-share").show();
                        break;
                    case "allImage":
                        _url = "/LR_OAModule/ResourceFile/GetImageListJson";
                        $(".crumb-path").find('[data-folderid=allfile]').html('所有图片');
                        $("#lr-download").show();
                        $("#lr-delete").show();
                        $("#lr-share").show();
                        break;
                    case "recycledFile":
                        _url = "/LR_OAModule/ResourceFile/GetRecycledListJson";
                        $(".crumb-path").find('[data-folderid=allfile]').html('回收站');
                        $("#lr-restoreFile").show();
                        $("#lr-thoroughDelete").show();
                        $("#lr-emptyRecycled").show();
                        break;
                    case "myShare":
                        _url = "/LR_OAModule/ResourceFile/GetMyShareListJson";
                        $("#lr-cancelshare").show();
                        break;
                    case "othersShare":
                        _url = "/LR_OAModule/ResourceFile/GetOthersShareListJson";
                        //$gridTable.setGridParam().showCol("CreateUserName");
                        $("#lr-download").show();
                        break;
                    default:
                        break;
                }
                //$gridTable.setGridParam().hideCol("F_CreateUserName");
                $.lrSetForm(top.$.rootUrl + _url, function (res) {//
                    if (res.length != 0) {
                        $('#gridTable').jfGridSet('refreshdata', res);
                    }
                    else {
                        $('#gridTable').jfGridSet('refreshdata', {});
                    }
                });
            });
        },
        //加载表格
        initGrid: function () {
            var $gridTable = $("#gridTable");
            $('#gridTable').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/ResourceFile/GetListJson',
                headData: [
                    {
                        label: '文件名', name: 'F_FileName', width: 520, align: 'left',
                        formatter: function (cellvalue, options, rowObject) {
                            if (options.length != 0) {
                                return "<div style='cursor:pointer;'><div style='float: left;'><img src='" + top.$.rootUrl + "/Content/images/filetype/" + options.F_FileType + ".png' style='width:30px;height:30px;padding:5px;margin-left:-5px;margin-right:5px;' /></div><div style='float: left;line-height:35px;'>" + options.F_FileName + "</div></div>";
                            }
                        }   
                    },
                    {
                        label: '大小', name: 'F_FileSize', index: 'F_FileSize', width: 100, align: 'center',
                        formatter: function (cellvalue, options, rowObject) {
                            return page.CountFileSize(cellvalue);
                        }
                    },
                    {
                        label: "修改时间", name: "F_ModifyDate", index: "F_ModifyDate", width: 120, align: "center",
                        //formatter: function (cellvalue, options, rowObject) {
                        //    return formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        //}
                    },
                ],
                mainId: 'F_FileId',
                isPage: false,
                sidx: 'F_CreatorTime',
                dblclick: function (id) {
                    var rowData = $gridTable.jfGridGet('rowdata');
                    if (rowData.F_FileType == "folder") {
                        fileId = rowData.F_FileId;
                        $.lrSetForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/GetListJson?folderId=' + fileId, function (data) {//
                            $('#gridTable').jfGridSet('refreshdata', data);
                        });

                        $(".crumb-path").append('<span class="path-item add" data-fileId=' + rowData.F_FileId + ' data-folderId=' + rowData.F_FolderId + ' style="cursor:pointer;text-decoration:underline">' + rowData.F_FileName + '</span>');
                        $(".crumb-path .back").show();
                        $(".crumb-path span.add").unbind('click');
                        $(".crumb-path span.add").click(function () {
                            $(this).nextAll().remove();
                            $.lrSetForm(top.$.rootUrl + '/LR_OAModule/ResourceFile/GetListJson?folderId=' + $(this).attr('data-fileId'), function (data) {//
                                $('#gridTable').jfGridSet('refreshdata', data);
                            });
                        });
                    }
                    else {
                        //在线预览
                        learun.layerForm({
                            id: 'PreviewForm',
                            title: '文件预览',
                            url: top.$.rootUrl + '/LR_OAModule/ResourceFile/PreviewFile?fileId=' + rowData.F_FileId,
                            width: 1080,
                            height: 850,
                            btn: null,
                            callBack: function (id) {
                                return top[id].acceptClick(refreshGirdData);
                            }
                        });
                    }
                }
            });
            $('#gridTable').jfGridSet('reload', {});
        },
        search: function (param) {
            param = param || {};
            $('#gridTable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        },
        //初始化页面
        InitialPage: function () {
            //layout布局
            $('#layout').layout({
                applyDemoStyles: true,
                west__resizable: false,
                west__size: 220,
                spacing_open: 0,
                onresize: function () {
                    $(window).resize()
                }
            });
            $('.profile-nav').height($(window).height() - 20);
            $('.profile-content').height($(window).height() - 20);
            //resize重设(表格、树形)宽高
            $(window).resize(function (e) {
                window.setTimeout(function () {
                    $('#gridTable').setGridWidth(($('#gridPanel').width() - 15));
                    $("#gridTable").setGridHeight($(window).height() - 141.5);
                    $('.profile-nav').height($(window).height() - 20);
                    $('.profile-content').height($(window).height() - 20);
                }, 200);
                e.stopPropagation();
            });
        },
        //计算文件大小函数(保留两位小数),Size为字节大小
        CountFileSize: function (Size) {
            var m_strSize = "";
            var FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = page.ToDecimal(FactSize) + " 字节";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = page.ToDecimal(FactSize / 1024.00) + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = page.ToDecimal(FactSize / 1024.00 / 1024.00) + " MB";
            else if (FactSize >= 1073741824)
                m_strSize = page.ToDecimal(FactSize / 1024.00 / 1024.00 / 1024.00) + " GB";
            return m_strSize;
        },
        //保留两位小数
        //功能：将浮点数四舍五入，取小数点后2位
        ToDecimal: function (x) {
            var f = parseFloat(x);
            if (isNaN(f)) {
                return 0;
            }
            f = Math.round(x * 100) / 100;
            return f;
        },
    };

    refreshGirdData = function () {
        page.search();
    };

    page.init();
}


