$(function () {
    $("input[type='checkbox']").click(function () {
        var idRol = 0;       
        userID = $('#UserID').val();
        rolID = $(this).val();      
        $.ajax({
            type: 'POST',
            url: '/Usuarios/AgregarRol',
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


