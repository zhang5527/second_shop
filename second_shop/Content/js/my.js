//分页方法
function page(cur, total, pagesize,url) {
    var html = "";
    var pageleft;
    var pageright;
    var size = Math.ceil(1.0 * total / pagesize);
    if (size == 0) size = 1;
    console.log(size);
    if (cur == 1) {
        html += '<li class="disabled"><span>«</span></li>';
    }
    else {
        html += ' <li><a href="' + url + '?page=' + (cur - 1) + '">«</a></li>'
    }
    if (size <= 5) {
        for (var i = 1; i <= size; i++) {
            if (cur == i) {
                html += "<li class='active'><span>" + i + "</span></li>";
                continue;
            }
            html += "<li><a href='" + url + "?page=" + i + "'>" + i + "</a></li>";
        }
    }
    else {
        if (cur <= 3) {
            for (var i = 1; i <= 5; i++) {
                if (cur == i) {
                    html += "<li class='active'><span>" + i + "</span></li>";
                    continue;
                }
                html += "<li><a href='" + url+"?page=" + i + "'>" + i + "</a></li>";
            }
        }
        else {
            pageleft = cur - 2;
            if (cur + 2 > size)
                pageright = size;
            else
                pageright = cur + 2;
            for (var i = pageleft; i <= pageright; i++) {
                if (cur == i) {
                    html += "<li class='active'><span>" + i + "</span></li>";
                    continue;
                }
                html += "<li><a href='" + url + "?page=" + i + "'>" + i + "</a></li>";
            }
        }
    }
    if (cur == size) {
        html += '<li class="disabled"><span>»</span></li>';
    }
    else {
        html += ' <li><a href="' + url + '?page=' + (cur + 1) + '">»</a></li>'
    }
    console.log(html);
    $('.pagination').html(html);
}
//根据id和url删除
function banuser(id, e, url,text) {
    $.confirm({
        title: '提示',
        content: '您确定' + text + '所选吗',
        type: 'orange',
        typeAnimated: false,
        buttons: {
            omg: {
                text: '确定',
                btnClass: 'btn-orange',
                action: function () {
                    $.ajax({
                        url: url,
                        type: 'post',
                        dataType: 'text',
                        data: "id=" + id,
                        success: function (data) {
                            console.log(data);
                            if (data == '1') {
                                lightyear.notify(text + '成功', 'success', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
                                $(e).not('.header').parents('tr').remove();
                            } else {
                                lightyear.notify(text + '失败', 'warning', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
                            }
                        },
                        'error': function () {
                            lightyear.notify('系统错误', 'warning', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
                        }
                    })
                }
            },
            close: {
                text: '关闭',
            }
        }
    });
}
//根据id和url改变其状态
function changestate(state, id, text, url, e) {
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'text',
        data: "id=" + id,
        success: function (data) {
            console.log(data);
            if (data == '1') {
                if (state == 0) {
                    $(e).text('禁用');
                    $(e).removeClass("text-success");
                    $(e).addClass("text-danger");
                    $(e).attr("title", "点击启用");
                    $(e).removeAttr("onclick");
                    $(e).attr("onclick", 'changestate(1, ' + id + ', "启用",' +"'"+ url +"'"+ ', this)')
                }
                else {
                    $(e).text('正常');
                    $(e).removeClass("text-danger");
                    $(e).addClass("text-success");
                    $(e).attr("title", "点击禁用");
                    $(e).removeAttr("onclick");
                    $(e).attr("onclick", 'changestate(0, ' + id + ', "停用", ' + "'" + url + "'" + ',this)')
                }
                lightyear.notify(text + '成功', 'success', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
                console.log(state)
            } else {
                lightyear.notify(text + '失败', 'warning', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
            }
        },
        'error': function () {
            lightyear.notify('系统错误', 'warning', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
        }
    })
}
function GetObjFromForm(jsonArray) {
    var o = {};
    $.each(jsonArray, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
}

function SubmitObj(obj,url,callback) {
    $.ajax({
        type: "POST",
        url: url,
        data: { data: JSON.stringify(obj) },
        success: function (data) {
            if (data == '1') {
                lightyear.loading('hide');
                $('#exampleModal').modal('hide');
                lightyear.notify('成功', 'success', 2000, 'mdi mdi-emoticon-happy', 'top', 'center');
                //setTimeout(function () { location.reload(); }, 2000);
            }
        }
    })
    callback();
}
function GetObj(id,url,callback){
    var obj;
    $.ajax({
        type: "POST",
        url: url,
        data: { data: id },
        success: function (data) {
            console.log(data)
            if (data != '') {                
                obj = JSON.parse(data)
                callback(obj);
            }
        }
    })
}
function deleteobj(id,url,callback) {
    $.ajax({
        type: "POST",
        url: url,
        data: { id: id },
        success: function (data) {
            if (data == 1) callback();
        }
    })
}
function openmodal(id,callback) {
    $("#exampleModal").find("input").each(function (e, el) {
        $(this).val("");
    })
    callback();
    $(id).modal('show');
}