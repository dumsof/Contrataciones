$(function () {


    console.log("ready!");
    console.log($('#menuPrincipal'))


    $("#menuPrincipal").load(function () {
        console.log("Load!");
        console.log($('#menuPrincipal'))
    });
});


//////$.ajax({
//    type: 'POST',
//    url: '/Menus/ObtenerMenu',
//    dataType: "json",
//    contentType: "application/json; charset=utf-8",
//    data: {},
//    success: function (data) {
//        alert(data);
//    },
//    error: function (xhr, ajaxOptions, thrownError) {
//        alert(xhr.status);
//        alert(thrownError);
//    }
//});