$(function () {

    ValidarUsuarioLogueado();
    var swUsuarioLogueado =sessionStorage.getItem('swUsuarioLogueado');
    if (swUsuarioLogueado == "0") {
        //$("#menuPrincipal").append('');
        $("#menuPrincipal").empty();
        //location.reload();
        return;
    }
    var menu = sessionStorage.getItem('menu');
    if (menu != null && menu.length > 0) {
        $("#menuPrincipal").append(menu);
    } else {
        GenerarMenu();
        location.reload(false);
    }
});

function ValidarUsuarioLogueado()
{
    var usuarioLogueado = "0";
    $.ajax({
        type: "GET",
        url: "MenuDinamico/UsuarioAutentificado",
        data: "",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (dataUsuarioLogueado) {            
            sessionStorage.setItem('swUsuarioLogueado', dataUsuarioLogueado);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    });
    return usuarioLogueado;
}

function GenerarMenu() {
    $.ajax({
        type: "GET",
        url: "MenuDinamico/ObtenerItemMenu",
        data: "",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (dataMenu) {
            if (dataMenu == null)
            {
                sessionStorage.setItem('menu', '');
                //location.reload();
                return;
            }
            var n = 0;
            var strMenuTodo = '';
            var url = '';
            $.each(dataMenu, function (id, itemMenu) {
                if (url.length <= 0) {
                    url = ObtenerUrl(itemMenu.Url);
                }               
                if (itemMenu.SubMenuOperacion != null && itemMenu.SubMenuOperacion.length > 0) {
                    strMenuTodo += CrearSubMenu(itemMenu.DescripcionMenu, itemMenu.SubMenuOperacion,url);
                } else {
                    strMenuTodo += Menu(itemMenu,url);
                }
            });
            strMenuTodo += "<li><a href=\"" + url + "elmah.axd\" target=\"_blank\">Errores Aplicación</a></li>";
            $("#menuPrincipal").append(strMenuTodo);
            sessionStorage.setItem('menu', strMenuTodo);
            //location.reload(false);            
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status);
            console.log(thrownError);
        }
    });
}
function Menu(itemMenu,url) {    
    var strMenu = '';
    strMenu = "<li><a href=\"" + url + itemMenu.Controlador + "/" + itemMenu.Accion + "\">" + itemMenu.DescripcionMenu + "</a></li>";
    return strMenu;
}

function CrearSubMenu(descripcionPrincipal, subMenu,url) {
    var strSubMenu = '';
    var strItemMenu = '';    
    strSubMenu = "<li class='dropdown'>";
    strSubMenu += "<a href= '#' class='dropdown-toggle' data-toggle='dropdown'>" + descripcionPrincipal + "<b class='caret'></b></a>";
    strSubMenu += "<ul class='dropdown-menu'>";
    $.each(subMenu, function (id, itemMenu) {
        strItemMenu += "<li><a href=\"" + url + itemMenu.Controlador + "/" + itemMenu.Accion + "\">" + itemMenu.DescripcionOperacion + "</a></li>";
        console.log("Item sub Menu :" + itemMenu.DescripcionOperacion);
    });    
    strSubMenu += strItemMenu + "</ul></li>";
    return strSubMenu;
}

function ObtenerUrl(url) {
    return url.replace('MenuDinamico/ObtenerItemMenu', '');
}
