using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFHKA.Adjuntos.listado.Transversal.Comun
{
    public interface IFabricaConexion
    {
        public IDbConnection Conexion();
    }
}