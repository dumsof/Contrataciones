using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Contrataciones.Controllers.Utilidades
{
    public enum SessionKey
    {
        ARCHIVOS,
        RETURN_URL
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

    }
}
