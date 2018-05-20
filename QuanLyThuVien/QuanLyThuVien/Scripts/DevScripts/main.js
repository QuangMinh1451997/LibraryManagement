//Read file 
function ReadFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        console.log(reader);
        reader.onload = function (event) {
            $(".avatar").attr('src', event.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}
function sentAjax(url, method, dataType, data, func) {
    $.ajax({
        url: url,
        type: method,
        dataType: dataType,
        data: data,
        success: func,
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Vui lòng thử lại!!");
        }

    });
}
$(document).ready(function () {
    // Click button id = btn-save
    $("#btn-save").click(function (event) {
        event.preventDefault();
        var btnSave = $(this);
        console.log(btnSave);
        var result = confirm("Bạn muốn lưu ?");
        if (result) {
            console.log(result);
            $(".form").submit();
        }
    });
    // Click btn-delete
    $(".btn-delete").click(function (event) {
        var url = $(this).attr('data-url');
        var row = $(this).closest("tr");
        var userResult = confirm("Xóa bản ghi sẽ xóa các thông tin liên quan. Bạn có muốn xóa ?");
        if (userResult) {
            sentAjax(url, 'post', 'text', {}, function (serverResult) {
                serverResult = parseInt(serverResult);
                if (serverResult === -1) {
                    alert("Bản ghi này không tồn tại !!!");
                }
                else if (serverResult === -2) {
                    alert("Có lỗi khi xóa bản ghi. Vui lòng thữ lại");
                }
                else if (serverResult > 0) {
                    row.remove();
                    alert("Xóa thành công !!!");
                }
            });
        }
    });

    // click btn-restore-password
    $(".btn-restore-password").click(function () {
        var url = $(this).attr('data-url');
        var userResult = confirm("Bạn muốn khôi phục mật khẩu thành mặc định (ngày sinh của nhân vien)??");
        if (userResult === true) {
            sentAjax(url, 'post', 'text', {}, function (serverResult) {
                serverResult = parseInt(serverResult);
                if (serverResult === -2) {
                    alert("Có lỗi xãy ra!! Vui lòng thữ lại hoặc tải lại trang");
                }
                else if (serverResult === -1) {
                    alert("Nhân viên không tồn tại hoặc đã bị xóa!! Vui lòng thữ lại hoặc tải lại trang");
                }
                else if (serverResult > 0) {
                    alert("Khôi phục mật khẩu thành công!");
                }
            });
        }
    });
    // show detail one object
    $(".container-details").click(function () {
        $(".container-details").css("transform", "scale(0)");
        $(".section").css("opacity", "1");
        $(".container-show-details").html('');
    });
    $(".container-show-details").click(function (e) {
        e.stopPropagation();
    });
    $(".btn-details").click(function (event) {
        var url = $(this).attr("data-url");
        sentAjax(url, "get", "html", {}, function (result) {
            $(".section").css("opacity", "0.3");
            $(".container-details").css("transform", "scale(1)");
            $(".container-show-details").html(result);
        });
    });
});