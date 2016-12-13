$(function () {
    $("input[type='checkbox']").click(function () {
        var objUtilidad = new Utilidad();
        var idRol = 0;
        userID = $('#UserID').val();
        rolID = $(this).val();
        nomDirecVirtual = objUtilidad.ObtenerDirectorioVirtual();
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

});


