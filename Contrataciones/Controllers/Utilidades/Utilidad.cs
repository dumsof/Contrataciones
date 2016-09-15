using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Contrataciones.Controllers.Utilidades
{
    public static class Utilidad
    {
        public enum TipoOrdenadoLista
        {
            SinOrden = 0,
            Descendente = 1,
            Ascendente = 2
        }
        //public static List<T> OrdenarLista<T>(List<T> listaOrdenar, string campoOrdenar, TipoOrdenadoLista orden = TipoOrdenadoLista.SinOrden)
        //{

        //    List<T> listaOrdenada = new List<T>();

        //    if (orden == TipoOrdenadoLista.Ascendente)
        //    {
        //        listaOrdenada = (from n in listaOrdenar
        //                         orderby ObtenerPropiedadOrdenar(n, campoOrdenar) ascending
        //                         select n).ToList();
        //    }
        //    else if (orden == TipoOrdenadoLista.Descendente)
        //    {
        //        listaOrdenada = (from n in listaOrdenar
        //                         orderby ObtenerPropiedadOrdenar(n, campoOrdenar) descending
        //                         select n).ToList();

        //    }
        //    return listaOrdenada;
        //}

        /// <summary>
        /// permite ordenar la lista segun el parametro enviado e ingresar la palabra seleccione.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datolista"></param>
        /// <param name="propiedadOrdenar"></param>
        /// <param name="orden"></param>
        /// <returns></returns>
        public static SelectList OrdenarLista<T>(List<T> datolista, string valor, string texto, object valorSeleccion = null, TipoOrdenadoLista orden = TipoOrdenadoLista.SinOrden) where T : class, new()
        {
            if (datolista != null && datolista.Count > 0)
            {
                //INICIO COLOCAR TEXTO POR DEFECTO SELECCIONE      
                //se declara una clase del tipo generico que se pasa por parametreo
                T elementoTemp = new T();
                //asigna el valor a la propiedad enviada
                elementoTemp.SetValueToPropertyXType(texto);
                //se agrega un elemento generico de la clase para definir el texto que debe presentar
                datolista.Add(elementoTemp);
                //FIN COLOCAR TEXTO POR DEFECTO

                //datolista.Add(new T { T.GetValueFromProperty(valor) = "0", datolista.GetValueFromProperty(texto) = "-Seleccione Tipo Documento-" });
                switch (orden)
                {
                    case TipoOrdenadoLista.Ascendente:
                        datolista = datolista.OrderBy(c => c.GetValueFromProperty(texto)).ToList();
                        break;
                    case TipoOrdenadoLista.Descendente:
                        datolista = datolista.OrderByDescending(c => c.GetValueFromProperty(texto)).ToList();
                        break;
                }
            }
            if (valorSeleccion == null)
            {
                return new SelectList(datolista, valor, texto);
            }
            else
            {
                //permite en la edicion presentar la lista con el item que esta almacenado.
                return new SelectList(datolista, valor, texto, valorSeleccion);
            }
        }


        /// <summary>
        /// metodo que permite asignar un valor a la propiedad que se le coloca el nombre
        /// </summary>
        /// <param name="objetoBase"></param>
        /// <param name="propiedad"></param>
        /// <param name="value"></param>
        /// <returns>retorna la entidad con el valor asignado a la propiedad enviada por parametro</returns>
        public static object SetValueToPropertyXType(this object objetoBase, string propiedad, object value = null)
        {
            PropertyInfo propiedadInfo = objetoBase.GetType().GetProperty(propiedad);

            if (propiedadInfo != null)
            {
                //se obtiene el tipo de dato de la propiedad
                //string nombrePropiedad = propiedadInfo.PropertyType.Name;
                propiedadInfo.SetValue(objetoBase, ".-Seleccione-");
            }
            return objetoBase;
        }


        public static object ObtenerPropiedadOrdenar(object elemento, string propName)
        {
            //Use reflection to get order type
            return elemento.GetType().GetProperty(propName).GetValue(elemento, null);
        }

        private static object GetValueFromProperty(this object objetoBase, string propiedad)
        {
            object resultado = null;
            if (objetoBase != null)
            {
                PropertyInfo propiedadInfo = objetoBase.GetType().GetProperty(propiedad);
                resultado = propiedadInfo.GetValue(objetoBase, null);
            }
            return resultado;
        }

        public static string ObtenerRutaArchivo(string nombreCarpeta, string nombreArchivo)
        {
            string rutaCompleta = string.Empty;
            if (!string.IsNullOrEmpty(nombreCarpeta))
            {
                rutaCompleta = string.Format("~/{0}/{1}", nombreCarpeta, nombreArchivo);
            }
            else
            {
                rutaCompleta = string.Format("~/{0}", nombreArchivo);
            }
            rutaCompleta = HttpContext.Current.Server.MapPath(rutaCompleta);
            return rutaCompleta;
        }

        /// <summary>
        /// Obteners the mensaje XML.
        /// </summary>
        /// <param name="codigo">The codigo.</param>
        /// <returns>retorna el mensaje que es igual al codigo</returns>
        public static string ObtenerMensajeXml(string codigo, string nombreArchivo, params string[] parametrosMensaje)
        {
            XElement XTemp = XElement.Load(ObtenerRutaArchivo("App_Data/Mensajes", nombreArchivo.Trim()));
            string mensaje = string.Empty;
            var resultadoMensaje = (from element in XTemp.DescendantNodes()
                                    where element.NodeType == System.Xml.XmlNodeType.CDATA
                                    && element.Parent.FirstAttribute.Value.Trim() == codigo.Trim()
                                    select element.Parent.Value.Trim()).FirstOrDefault<string>();

            if (resultadoMensaje != null)
            {
                mensaje = resultadoMensaje.ToString();
                if (parametrosMensaje != null && parametrosMensaje.Length > 0)
                {
                    mensaje = string.Format(mensaje, parametrosMensaje);
                }
            }
            return mensaje;
        }

        public static string ObtenerMensajeXml(string codigo, string nombreArchivo)
        {
            return ObtenerMensajeXml(codigo, nombreArchivo);

        }
    }
}
