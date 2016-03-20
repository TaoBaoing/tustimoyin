function BtnZhiFu() {
    window.location.href = "/Order/CreateOrderByCart";
}

function ClearCart() {
    $.ajax({
        url: '/Order/ClearCart',
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            //                $("#thelist").remove();
            $("#thelist").empty();
            SetTotalCountAndMoney();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });
}

function AppToUi(cart, uifileid) {

    $('#' + uifileid).find('div.jine').text(cart.Money);


    $('#number_' + uifileid).val(cart.Count);

    if (cart.IsDanMian) {
        $("#ddldanshuang_" + uifileid + " option[value='设置单面']").attr("selected", true);
    } else {
        $("#ddldanshuang_" + uifileid + " option[value='设置双面']").attr("selected", true);
    }

    if (cart.IsCaiDa) {
        $("#ddlheibai_" + uifileid + " option[value='彩色打']").attr("selected", true);
    } else {
        $("#ddlheibai_" + uifileid + " option[value='黑白打']").attr("selected", true);

    }
    if (cart.PrintSize === 1) {
        $("#ddlsize_" + uifileid + " option[value='A4']").attr("selected", true);
    }
    else if (cart.PrintSize === 10) {
        $("#ddlsize_" + uifileid + " option[value='A3']").attr("selected", true);
    }
    else if (cart.PrintSize === 20) {
        $("#ddlsize_" + uifileid + " option[value='A2']").attr("selected", true);
    }
    else if (cart.PrintSize === 30) {
        $("#ddlsize_" + uifileid + " option[value='A1']").attr("selected", true);
    }

    if (cart.BaoZhuang === 1) {
        $("#ddlbaozhuang_" + uifileid + " option[value='无']").attr("selected", true);
    } else if (cart.BaoZhuang === 10) {
        $("#ddlbaozhuang_" + uifileid + " option[value='胶装']").attr("selected", true);
    }
    else if (cart.BaoZhuang === 20) {
        $("#ddlbaozhuang_" + uifileid + " option[value='打孔装']").attr("selected", true);
    }

    SetTotalCountAndMoney();
}

function HeiBaiChange(sel, uifileid) {
    var p1 = $(sel).children('option:selected').val();//设置黑白彩色打印
    var heibai = "";
    if (p1 === "黑白打") {
        heibai = "heibai";
    }
    var cartid = $(uifileid).attr("cartid");//购物车id
    $.ajax({
        url: '/Order/HeiBaiChange?cartid=' + cartid + '&heibai=' + heibai,
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            AppToUi(data, uifileid.id);
            //$(deltr).closest('tr').remove();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });
}

function isPositiveNum(s) {//是否为正整数
    var re = /^[0-9]*[1-9][0-9]*$/;
    return re.test(s);
}

function DelImgOnClick(uifileid) {
    var cartid = $(uifileid).attr("cartid");//购物车id
    $.ajax({
        url: '/Order/DelCart?cartid=' + cartid,
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            $(uifileid).remove();
            SetTotalCountAndMoney();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });

}

function DelImgOnMouseOver(uifileid) {
    $('#delimg_' + uifileid.id).attr("src", "/Images/delmousein.jpg");
}

function DelImgOnMouseOut(uifileid) {

    $('#delimg_' + uifileid.id).attr("src", "/Images/delmouseout.jpg");
}

function CountChange(uifileid) {
    var cartid = $(uifileid).attr("cartid");//购物车id
    var count = $('#number_' + uifileid.id).val();
    if (!isPositiveNum(count)) {
        alert('请输入正整数');
        $('#number_' + uifileid.id).val("1");
        return;
    }

    $.ajax({
        url: '/Order/CountChange?cartid=' + cartid + '&count=' + count,
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            AppToUi(data, uifileid.id);
            //$(deltr).closest('tr').remove();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });
}

function PrintSizeChange(sel, uifileid) {
    var p1 = $(sel).children('option:selected').val();//设置黑白彩色打印
    var psize = p1;

    var cartid = $(uifileid).attr("cartid");//购物车id
    $.ajax({
        url: '/Order/PrintSizeChange?cartid=' + cartid + '&psize=' + psize,
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            AppToUi(data, uifileid.id);
            //$(deltr).closest('tr').remove();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });
}

function BaoZhuangChange(sel, uifileid) {
    var p1 = $(sel).children('option:selected').val();//设置包装
    var baozhuang = "";
    if (p1 === "无") {
        baozhuang = "wu";
    }
    else if (p1 === "胶装") {
        baozhuang = "jiaozhuang";
    }
    else if (p1 === "打孔装") {
        baozhuang = "dakongzhuang";
    }

    var cartid = $(uifileid).attr("cartid");//购物车id
    $.ajax({
        url: '/Order/BaoZhuangChange?cartid=' + cartid + '&baozhuang=' + baozhuang,
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            AppToUi(data, uifileid.id);
            //$(deltr).closest('tr').remove();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });
}

function DanShuangChange(sel, uifileid) {
    var p1 = $(sel).children('option:selected').val();//设置单面 设置双面
    var danshuang = "";
    if (p1 === "设置单面") {
        danshuang = "dan";
    }
    var cartid = $(uifileid).attr("cartid");//购物车id
    $.ajax({
        url: '/Order/ChangeCartByDanShuang?cartid=' + cartid + '&danshuang=' + danshuang,
        sync: true,
        cache: false,
        dataType: "json",
        success: function (data) {
            AppToUi(data, uifileid.id);
            //$(deltr).closest('tr').remove();
        },
        error: function (rrr, textStatus, errorThrown) {
            alert(rrr.responseText);
        }
    });

}

var totalmoney = parseFloat("0");
function SetTotalCountAndMoney() {

    var count = $("div[class='maindiv']").length;
    $("#TotalCount").text(count);
    if (count === 0) {
        $("#PayDiv").hide();
    } else {
        $("#PayDiv").show();
    }
    totalmoney = parseFloat("0");
    var jines = $("div[class='jine']");
    jines.each(function () {
        var money = $(this).text();
        //        alert(money);
        totalmoney += parseFloat(money);
    });

    if (totalmoney.toString() !== "NaN") {
        $("#TotalMoney").text("￥" + totalmoney);
    }

};

$(function () {
    SetTotalCountAndMoney();
});

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


    uploader.on('uploadError', function (file, response) {
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


