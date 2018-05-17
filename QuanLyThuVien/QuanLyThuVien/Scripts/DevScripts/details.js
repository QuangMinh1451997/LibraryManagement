function clickDetails() {
    var id = $(this).attr("at");
    console.log(id);
    sentAjax("/Employee/Details", "get", "html", { id: id }, function (result) {
        console.log(result);
    });
}