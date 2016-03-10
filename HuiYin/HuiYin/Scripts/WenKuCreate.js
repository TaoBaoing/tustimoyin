
var applicationPath = window.applicationPath === "" ? "" : window.applicationPath || "../../";
// 文件上传
jQuery(function () {
    var $ = jQuery,
        $list = $('#thelist'),
        $btn = $('#ctlBtn'),
        state = 'pending',
        uploader;

    uploader = WebUploader.create({

        //自动上传
        auto: true,
        // 不压缩image
        resize: false,

        // swf文件路径
        swf: applicationPath + '/Script/Uploader.swf',

        // 文件接收服务端。
        server: applicationPath + 'Order/UpLoadProcess',

        //文件大小限制
        fileSizeLimit: 10 * 1024 * 1024,    // 200 M
        fileSingleSizeLimit: 5 * 1024 * 1024,    // 50 M

        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: '#picker'
    });

    // 当有文件添加进来的时候
    uploader.on('fileQueued', function (file) {
        $list.append('' +
             '<div id=' + file.id + ' class="maindiv" cartid=""  fileid="">' +
                 '<br/>' +
        '<div class="row">' +
           '<div class="col-md-6">' +
                '<div class="fname">' + file.name + '</div>' +
           '</div>' +
            '<div class="col-md-2">' +
                '份数<input id="number_' + file.id + '" onblur="CountChange(' + file.id + ')" type="text" style="width: 50px;"/>' +
            '</div>' +
            '<div class="col-md-3">' +
                '文件共<span  class="totalye"></span>页' +
            '</div>' +
            '<div class="col-md-1">' +
                '<img id="delimg_' + file.id + '" onmouseover="DelImgOnMouseOver(' + file.id + ')" onmouseout="DelImgOnMouseOut(' + file.id + ')" onclick="DelImgOnClick(' + file.id + ')"  src="/Images/delmouseout.jpg"/>' +
            '</div>' +
        '</div>' +
        '<br/>' +
        '<div class="row">' +
            '<div class="col-md-8">' +
                '<div>' +
                    '<span>' +
                        '<select id="ddldanshuang_' + file.id + '" onchange="DanShuangChange(this,' + file.id + ')">' +
                            '<option value="设置单面">设置单面</option>' +
                           '<option value="设置双面">设置双面</option>' +
                        '</select>' +
                    '</span>' +
                    '<span>' +
                       ' <select id="ddlheibai_' + file.id + '" onchange="HeiBaiChange(this,' + file.id + ')">' +
                            '<option>黑白打</option>' +
                            '<option>彩色打</option>' +
                        '</select>' +
                    '</span>' +
                      '<span>' +
                       ' <select id="ddlsize_' + file.id + '" onchange="PrintSizeChange(this,' + file.id + ')">' +
                            '<option>A4</option>' +
                            '<option>A3</option>' +
                            '<option>A2</option>' +
                            '<option>A1</option>' +
                        '</select>' +
                    '</span>' +
                    '<span>' +
                       ' <select id="ddlbaozhuang_' + file.id + '" onchange="BaoZhuangChange(this,' + file.id + ')">' +
                            '<option>无</option>' +
                            '<option>胶装</option>' +
                            '<option>打孔装</option>' +
                        '</select>' +
                    '</span>' +
                    '<span style="padding-left:50px;" class="state">等待上传...</span>' +
                '</div>' +
            '</div>' +

            '<div class="col-md-3">' +
            '</div>' +
            '<div class="col-md-1">' +
                '<div class="jine"></div>' +
            '</div>' +
        '</div>' +
         '<hr/>' +
    '</div>' +

            '');
    });

    // 文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id),
            $percent = $li.find('.progress .progress-bar');

        // 避免重复创建
        if (!$percent.length) {
            $percent = $('<div class="progress progress-striped active">' +
                '<div class="progress-bar" role="progressbar" style="width: 0%">' +
                '</div>' +
                '</div>').appendTo($li).find('.progress-bar');
        }
        $li.find('span.state').text('上传中');

        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {
        $('#' + file.id).find('span.state').text('已上传');

        var fenshu = response.fenshu;
        //            $('#' + file.id).find('input.txtNumber').val(fenshu);
        $('#number_' + file.id).val(fenshu);

        var cartid = response.cartid;
        $("#" + file.id).attr("cartid", cartid);

        var fileid = response.fileid;
        $("#" + file.id).attr("fileid", fileid);

        var totalyeshu = response.totalyeshu;
        $('#' + file.id).find('span.totalye').text(totalyeshu);

        var jine = response.jine;
        $('#' + file.id).find('div.jine').text(jine);


        SetTotalCountAndMoney();

    });


    uploader.on('uploadError', function (file) {
        $('#' + file.id).find('span.state').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });

    uploader.on('all', function (type) {
        if (type === 'startUpload') {
            state = 'uploading';
        } else if (type === 'stopUpload') {
            state = 'paused';
        } else if (type === 'uploadFinished') {
            state = 'done';
        }

        if (state === 'uploading') {
            $btn.text('暂停上传');
        } else {
            $btn.text('开始上传');
        }
    });

    $btn.on('click', function () {
        if (state === 'uploading') {
            uploader.stop();
        } else {
            uploader.upload();
        }
    });
});