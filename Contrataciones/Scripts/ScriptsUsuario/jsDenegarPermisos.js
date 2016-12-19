﻿$(function () {

    $("input[type='checkbox']").click(function () {
        var objUtilidad = new Utilidad();
        var idRol = $('#RolID').val();
        //permite obtener el elemento cercano tr del check seleccionado.
        var $trCheckSeleccionado = $(this).closest('tr');
        var denegarPermisoId = $trCheckSeleccionado.find('td:nth-child(1)').text();
        var descripcionMenu = $trCheckSeleccionado.find('td:nth-child(2)').text();
        var controladorAccion = $trCheckSeleccionado.find('td:nth-child(3)').text();        
        var nomDirecVirtual = objUtilidad.ObtenerDirectorioVirtual();       
        $.ajax({
            type: 'POST',
            url: nomDirecVirtual + 'DenegarPermisos/IngresarPermisoDenegado',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{'denegarPermisoId':'" + denegarPermisoId + "', 'idRol':'" + idRol + "', 'descripcionMenu':'" + descripcionMenu + "', 'controladorAccion':'" + controladorAccion + "'}",
            success: function (data) {
                if (data == '1') {
                    alert('Permiso modificado con éxito');
                } else {
                    alert('El permiso no fue modificado, por favor verifique.');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
       
    });

});




