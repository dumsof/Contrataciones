$(function () {

    $("input[type='checkbox']").click(function () {
        var idRol = $('#RolID').val();
        //permite obtener el elemento cercano tr del check seleccionado.
        var $trCheckSeleccionado = $(this).closest('tr');
        var denegarPermisoId = $trCheckSeleccionado.find('td:nth-child(1)').text();       
        var descripcionMenu = $trCheckSeleccionado.find('td:nth-child(2)').text();        
        var controladorAccion = $trCheckSeleccionado.find('td:nth-child(3)').text();

        var datoPermiso = { denegarPermisoId: denegarPermisoId , idRol:idRol , descripcionMenu:descripcionMenu, controladorAccion: controladorAccion};
        //'@Url.Action("ControllerName", "ActionName")'
        //url: '@Url.Action("DenegarPermisos", "IngresarPermisoDenegado")',
        //'/DenegarPermisos/IngresarPermisoDenegado',
        //
        //data:JSON.stringify(datoPermiso),
        $.ajax({
            type: 'POST',
            url: '/DenegarPermisos/IngresarPermisoDenegado',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
             data: "{'denegarPermisoId':'" + denegarPermisoId + "', 'idRol':'" + idRol + "', 'descripcionMenu':'" + descripcionMenu + "', 'controladorAccion':'" + controladorAccion + "'}",
            success: function (data) {
                if (data == '1') {
                    alert('Permiso modificado con éxito');
                } else {
                    alert('El permiso no fue modificado.');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    });

});




