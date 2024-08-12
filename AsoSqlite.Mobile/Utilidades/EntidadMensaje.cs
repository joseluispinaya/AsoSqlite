using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoSqlite.Mobile.Utilidades
{
    public class EntidadMensaje<T>
    {
        public bool EsCrear { get; set; }
        public T? EntidadDto { get; set; }
    }
}
