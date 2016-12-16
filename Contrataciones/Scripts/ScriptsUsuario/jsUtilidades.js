function Utilidad() {
    //funcion para poder obtener el nombre del directorio virtual de la aplicación.
    this.ObtenerDirectorioVirtual = function () {
        var urlRuta = window.location.pathname;
        var urlRutaSinPrimeraDiagonal = urlRuta.substring(1, urlRuta.length);
        var posicionDirectorio = urlRutaSinPrimeraDiagonal.indexOf('/');
        var respuesta = urlRuta.substring(0, posicionDirectorio + 1)+'/';
        return respuesta;
        //local 
        //return '/';
    }

    this.getRootWebSitePath = function () {
        var _location = document.location.toString();
        var applicationNameIndex = _location.indexOf('/', _location.indexOf('://') + 3);
        var applicationName = _location.substring(0, applicationNameIndex) + '/';
        var webFolderIndex = _location.indexOf('/', _location.indexOf(applicationName) + applicationName.length);
        var webFolderFullPath = _location.substring(0, webFolderIndex);
        return webFolderFullPath;
    }

}