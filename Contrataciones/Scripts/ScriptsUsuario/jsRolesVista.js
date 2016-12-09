$(function () {
    $("input[type='checkbox']").click(function () {
        var idRol = 0;
        userID = $('#UserID').val();
        rolID = $(this).val();
        nomDirecVirtual = ObtenerDirectorioVirtual();
        $.ajax({
            type: 'POST',
            url: nomDirecVirtual + '/Usuarios/AgregarRol',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{'idRol':'" + rolID + "', 'idUsuario':'" + userID + "'}",
            success: function (data) {
                alert('Rol modificado con éxito');
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    });

    function ObtenerDirectorioVirtual() {
        var urlRuta = window.location.pathname;
        var urlRutaSinPrimeraDiagonal = urlRuta.substring(1, urlRuta.length);
        var posicionDirectorio = urlRutaSinPrimeraDiagonal.indexOf('/');
        var respuesta = urlRuta.substring(0, posicionDirectorio + 1);
        return respuesta;
    }

});


