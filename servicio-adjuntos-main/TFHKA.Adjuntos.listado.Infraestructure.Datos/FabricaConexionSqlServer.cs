using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using TFHKA.Adjuntos.listado.Transversal.Comun;

namespace TFHKA.Adjuntos.listado.Infraestructure.Datos
{
    public class FabricaConexionSqlServer : IFabricaConexion
    {
        private readonly IConfiguration _configuracion;

        public FabricaConexionSqlServer(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        public IDbConnection Conexion()
        {
            var conexionSql = new SqlConnection()
            {
                ConnectionString = _configuracion["DataBase:Facturacion"]
            };
            conexionSql.Open();
            return (conexionSql);
        }

    }
}