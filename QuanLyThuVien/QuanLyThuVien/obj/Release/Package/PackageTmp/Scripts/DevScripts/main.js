
function sentAjax(url, method, dataType, data, func) {
    $.ajax({
        url: url,
        type: method,
        dataType: dataType,
        data: data,
        success: func,
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Vui lòng thử lại .");
        }
    });
}
function turnOnViewAjax() {
    $(".section").css("opacity", "0.3");
    $(".ajaxDetails").css("transform", "scale(1)");
}
function jQFormSerializeArrToJson(formSerializeArr, quantityListOfObj) {
    var jsonObj = {};
    for (var i = 0; i < quantityListOfObj; i++)
        jsonObj[i] = [];
    jQuery.map(formSerializeArr, function (n, i) {
        if (n.name === "0") {
            jsonObj[0].push(n.value);
        }
        else if (n.name === "1") {
            jsonObj[1].push(n.value);
        }
        else
            jsonObj[n.name] = n.value;
    });
    return jsonObj;
}
$(window).ready(function () {
    //
    $(".cbx-ma-mon-an").change(function () {
        var s = $(".tb-mon-an input[type='checkbox']:checked");
        console.log(s.length);
    });
    // Change info when click one row in tb mon an or dich vu
    $("input[type='checkbox']:checked").closest("tr").css("background", "#70a6ff");
    $(".tb-mon-an input[type='checkbox']:checked").closest("tr").find(".txt-so-luong").attr("name", "soLuong");
    $(".tb-mon-an input[type='checkbox']:checked").closest("tr").find(".txt-ghi-chu").attr("name", "ghiChu");
    $(".tb-mon-an tr, .tb-dich-vu").on('click', function () {
        var checkbox = $("input[type='checkbox']", this);

        var txtGiaBan = $(".txt-gia-ban");
        var giaMonAnVuaChon = checkbox.closest("tr").find(".gia-mon-an").text();

        if (checkbox.is(':checked')) {
            checkbox.prop('checked', false);
            checkbox.closest("tr").css("background", "transparent");
            checkbox.closest("tr").find(".txt-ghi-chu").attr("name", "");
            checkbox.closest("tr").find(".txt-so-luong").attr("name", "");
            // cap nhat giá bàn
            txtGiaBan.val(parseInt(txtGiaBan.val()) - parseInt(giaMonAnVuaChon));      
        }
        else {
            checkbox.prop('checked', true);
            checkbox.closest("tr").css("background", "#70a6ff");
            checkbox.closest("tr").find(".txt-ghi-chu").attr("name", "ghiChu");
            checkbox.closest("tr").find(".txt-so-luong").attr("name", "soLuong");
            // cập nhật giá bàn
            txtGiaBan.val(parseInt(txtGiaBan.val()) + parseInt(giaMonAnVuaChon)); 
        }

    });
    // set min height side = mid div
    $(".side-bar").css("min-height", $(".mid").height());
    // clear editor
    $(".btn-clear").on('click', function (e) {
        e.preventDefault();
        var kq = confirm("Bạn muốn làm mới thông tin đã nhập ???????????");
        if (kq === true) {
            var form = $(this).closest('form');
            $('.text-box', form).val('');
        }
    });
    // show detail one object
    $(".ajaxDetails").click(function () {
        $(".ajaxDetails").css("transform", "scale(0)");
        $(".section").css("opacity", "1");
        $(".div-details").html('');
    });
    $(".div-details").click(function (e) {
        e.stopPropagation();
    });
    $(".details").click(function (event) {
        event.preventDefault();
        turnOnViewAjax();
        var url = $(this).attr('href');
        $.ajax({
            url: url,
            type: 'get',
            dataType: 'html',
            success: function (result) {
                $(".div-details").html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert("Vui lòng thử lại .");
            }
        });
    });
    // click add one item
    $(".btn-save").on('click', function (e) {
        e.preventDefault();
        var save = confirm("Chắc chưa ? Suy nghĩ kỉ đi !!!!!!!");
        if (save === true) {
            $(this).closest("form").submit();
        }                 
    });
    // clikc delete one item
    $(".link-delete").on('click', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        var href = $(this).attr('at');
        var save = confirm("Bạn thật sự muốn xóa ?");
        if (save === true) {
            sentAjax(url, 'post', 'text', '', function (result) {
                alert(result);
                window.location.href = href;
            });
        }
    });
    $("input[type='number']").keypress(function (event) {
        if (event.which !== 8 && event.which !== 0 && (event.which < 48 || event.which > 57)) {
            $(".alert").html("Enter only digits!").show().fadeOut(2000);
            return false;
        }
    });
    $("input[type='number'], .txt-ghi-chu").click(function (event) {
        event.stopPropagation();
    });
    $("input[type='number']").focusout(function (event) {
        if ($(this).val() === "")
            $(this).val("0");
    });

    
});