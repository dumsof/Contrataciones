$(function () {

    $("input[type='checkbox']").click(function () {
        var idRol = $('#RolID').val();
        //permite obtener el elemento cercano tr del check seleccionado.
        var $trCheckSeleccionado = $(this).closest('tr');
        var denegarPermisoId = $trCheckSeleccionado.find('td:nth-child(1)').text();
        var descripcionMenu = $trCheckSeleccionado.find('td:nth-child(2)').text();
        var controladorAccion = $trCheckSeleccionado.find('td:nth-child(3)').text();

        var datoPermiso = { denegarPermisoId: denegarPermisoId, idRol: idRol, descripcionMenu: descripcionMenu, controladorAccion: controladorAccion };
        var nomDirecVirtual = ObtenerDirectorioVirtual();
        //'@Url.Action("ControllerName", "ActionName")'
        //url: '@Url.Action("DenegarPermisos", "IngresarPermisoDenegado")',
        //'/DenegarPermisos/IngresarPermisoDenegado',
        //
        //data:JSON.stringify(datoPermiso),
        $.ajax({
            type: 'POST',
            url: nomDirecVirtual + '/DenegarPermisos/IngresarPermisoDenegado',
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

        function ObtenerDirectorioVirtual() {
            var urlRuta = window.location.pathname;
            var urlRutaSinPrimeraDiagonal = urlRuta.substring(1, urlRuta.length);
            var posicionDirectorio = urlRutaSinPrimeraDiagonal.indexOf('/');
            var respuesta = urlRuta.substring(0, posicionDirectorio + 1);
            return respuesta;
        }
    });

});




