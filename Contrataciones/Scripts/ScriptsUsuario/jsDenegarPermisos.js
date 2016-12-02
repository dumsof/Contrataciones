﻿$(function () {

    $("input[type='checkbox']").click(function () {
        var idRol = '0'
        //permite obtener el elemento cercano tr del check seleccionado.
        var $trCheckSeleccionado = $(this).closest('tr');
        var denegarPermisoId = $trCheckSeleccionado.find('td:nth-child(1)').text();       
        var descripcionMenu = $trCheckSeleccionado.find('td:nth-child(2)').text();        
        var controladorAccion = $trCheckSeleccionado.find('td:nth-child(3)').text();      
        var permiso = $(this).val();       
        
        $.ajax({
            type: 'POST',
            url: '/DenegarPermisos/IngresarPermisoDenegado',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{'denegarPermisoId':'" + denegarPermisoId + "', 'idRol':'" + idRol + "', 'descripcionMenu':'" + descripcionMenu + "', 'controladorAccion':'" + controladorAccion + "', 'permiso':'" + permiso + "'}",
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



