//Read file 
function ReadFile(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
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

    $(".details").click(function (event) {
        event.preventDefault();
        var id = $(this).attr("at");
        console.log(id);
        sentAjax("/Employee/Details", "get", "html", {id: id}, function (result) {
            console.log(result);
        });
    });
});