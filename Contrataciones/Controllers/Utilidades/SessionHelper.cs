﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Contrataciones.Controllers.Utilidades
{
    public enum SessionKey
    {
        ARCHIVOS = 1,
        RETURN_URL = 2,
        ROLES_USUARIO = 3
    }
    public class SessionHelper
    {
        public static void Set(HttpSessionStateBase session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }
        public static T Get<T>(HttpSessionStateBase session, SessionKey key)
        {
            object dataValue = session[Enum.GetName(typeof(SessionKey), key)];
            if (dataValue != null && dataValue is T)
            {
                return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }

        public static void SetSession(SessionKey key, object value)
        {
            HttpContext.Current.Session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        public static T GetSession<T>(SessionKey key)
        {
            object dataValue = HttpContext.Current.Session[Enum.GetName(typeof(SessionKey), key)];         
            if (dataValue != null )
            {
                if (dataValue is T)
                {
                    return (T)dataValue;
                }
                else
                {
                    try
                    {
                        return (T)Convert.ChangeType(dataValue, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
                //return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }

    }
}
