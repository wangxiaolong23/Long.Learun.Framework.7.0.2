var keyVaule = request('keyVaule');
var bootstrap = function ($, learun) {
    "use strict";

    $.lrSetForm(top.$.rootUrl + '/LR_SystemModule/Annexes/GetAnnexesFileList?folderId=' + keyVaule, function (data) {
        for (var i = 0, l = data.length; i < l; i++) {
            $('#lr_form_file_queue .lr-form-file-queue-bg').hide();
            var item = data[i];
            var $item = $('<div class="lr-form-file-queue-item" id="lr_filequeue_' + item.F_Id + '" ></div>');
            $item.append('<div class="lr-file-image"><img src="' + top.$.rootUrl + '/Content/images/filetype/' + item.F_FileType + '.png"></div>');
            $item.append('<span class="lr-file-name">' + item.F_FileName + '(' + learun.countFileSize(item.F_FileSize) + ')</span>');
            $item.append('<div class="lr-tool-bar"><i class="fa fa-cloud-download" title="下载"  data-value="' + item.F_Id + '" ></i></div>');

            $item.find('.lr-tool-bar .fa-cloud-download').on('click', function () {
                var fileId = $(this).attr('data-value');
                DownFile(fileId);
            });

            $('#lr_form_file_queue_list').append($item);
        }
    });
    // 下载文件
    var DownFile = function (fileId) {
        learun.download({ url: top.$.rootUrl + '/LR_SystemModule/Annexes/DownAnnexesFile', param: { fileId: fileId, __RequestVerificationToken: $.lrToken }, method: 'POST' });
    }


    $('#lr_form_file_queue').lrscroll();
}
