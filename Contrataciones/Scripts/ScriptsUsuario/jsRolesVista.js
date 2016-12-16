$(function () {
    //evento que lo llama el boton check de la vista Usuarios/Create
    $("#tbAsignarRoles input[type='checkbox']").click(function () {       
        userID = $('#UserID').val();
        rolID = $(this).val();
        RegistrarRolUsuario(rolID, userID);       
    });   

    //evento que lo llama el boton check de la vista Usuarios//Index
    $("#tbAsignarUsuarios input[type='checkbox']").click(function () {  
         rolID= $('#RolID').val();
         userID = $(this).val();
         RegistrarRolUsuario(rolID,userID);       
    });
    
    RegistrarRolUsuario = function (rolID, userID) {
        var objUtilidad = new Utilidad();
        nomDirecVirtual = objUtilidad.ObtenerDirectorioVirtual();
        $.ajax({
            type: 'POST',
            url: nomDirecVirtual + 'Usuarios/AgregarRol',
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: "{'idRol':'" + rolID + "', 'idUsuario':'" + userID + "'}",
            success: function (data) {
                console.log(data.respuesta);
                if (data.respuesta == true) {
                    if (data.tipoProceso==1) {
                        alert('Usuario asignado al rol con éxito.');
                    } else {
                        alert('Usuario retirado del rol con éxito.');
                    }                   
                } else {
                    alert('El usuario no fue asignado.');
                }                
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert(xhr.status);
                alert(thrownError);
            }
        });
    }

});


