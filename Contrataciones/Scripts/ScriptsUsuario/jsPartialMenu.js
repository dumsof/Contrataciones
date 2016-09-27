$(function () {
    console.log("ready!");
    var menu = localStorage.getItem('menu');
    if (menu != null && menu.length > 0) {
        $("#menuPrincipal").append(menu);
    } else {
        GenerarMenu();
    }

});

function GenerarMenu() {
    $.ajax({
        type: "GET",
        url: "MenuDinamico/ObtenerItemMenu",
        data: "",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (dataMenu) {
            var n = 0;
            var strMenuTodo = '';
            $.each(dataMenu, function (id, itemMenu) {
                console.log("Item Menu :" + itemMenu.DescripcionMenu);
                if (itemMenu.SubMenuOperacion != null && itemMenu.SubMenuOperacion.length > 0) {
                    strMenuTodo += CrearSubMenu(itemMenu.DescripcionMenu, itemMenu.SubMenuOperacion);
                } else {
                    strMenuTodo += Menu(itemMenu);
                }
            });
            //$("#menuPrincipal").empty();
            $("#menuPrincipal").append(strMenuTodo);
            localStorage.setItem('menu', strMenuTodo);
            location.reload();
            console.log(strMenuTodo);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    });
}
function Menu(itemMenu) {
    var url = window.location.href;
    var strMenu = '';
    strMenu = "<li><a href=\" " + url + " /" + itemMenu.Controlador + "/" + itemMenu.Accion + "\">" + itemMenu.DescripcionMenu + "</a></li>";
    return strMenu;
}

function CrearSubMenu(descripcionPrincipal, subMenu) {
    var strSubMenu = '';
    var strItemMenu = '';
    var url = window.location.href;
    strSubMenu = "<li class='dropdown'>";
    strSubMenu += "<a href= '#' class='dropdown-toggle' data-toggle='dropdown'>" + descripcionPrincipal + "<b class='caret'></b></a>";
    strSubMenu += "<ul class='dropdown-menu'>";
    $.each(subMenu, function (id, itemMenu) {
        strItemMenu += "<li><a href=\" " + url + " /" + itemMenu.Controlador + "/" + itemMenu.Accion + "\">" + itemMenu.DescripcionOperacion + "</a></li>";
        console.log("Item sub Menu :" + itemMenu.DescripcionOperacion);
    });
    strItemMenu += "<li><a href=\"" + url + "/elmah.axd\">Errores Aplicación</a></li>";
    strSubMenu += strItemMenu + "</ul></li>";
    return strSubMenu;
}
