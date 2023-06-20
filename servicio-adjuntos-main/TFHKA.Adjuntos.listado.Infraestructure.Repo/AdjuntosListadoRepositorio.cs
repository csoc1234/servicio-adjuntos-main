using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using TFHKA.Adjuntos.listado.Domain.Entidad;
using TFHKA.Adjuntos.listado.Infraestruture.Interfaz;
using TFHKA.Adjuntos.listado.Transversal.Comun;

namespace TFHKA.Adjuntos.listado.Infraestructure.Repo
{
    public class AdjuntosListadoRepositorio : IAdjuntosListadoInfraInterfaz
    {
        private readonly IConfiguration _configuracion;
        private readonly IFabricaConexion _fabricaConexion;
        public AdjuntosListadoRepositorio(IConfiguration configuracion, IFabricaConexion fabricaConexionSql)
        {
            _configuracion = configuracion;
            _fabricaConexion = fabricaConexionSql;
        }

        public Invoice21 ConsultaDocumentos(string documento, int id_enterprise)
        {
            string documento_id = documento;
            #region Consulta de registros
            using IDbConnection conexion = _fabricaConexion.Conexion();
            {
                string consultar = "ConsultaListadoDocumentoAdjunto";
                DynamicParameters parametros = new DynamicParameters();
                parametros.Add("@document_id", documento_id);
                parametros.Add("@id_enterprise", id_enterprise);

                Invoice21 document = conexion.QuerySingle<Invoice21>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);

                #endregion
                return document;
            }
        }

        public IEnumerable<Invoice21File> ConsultaDocumentosAdjunto(int documento)
        {
            // var result = new Invoice21File();

            using IDbConnection conexion = _fabricaConexion.Conexion();
            string consultar = "ConsultaDocumentoAdjuntoListado";
            DynamicParameters parametros = new();
            parametros.Add("@id_invoice", documento);

            IEnumerable<Invoice21File> registros = conexion.Query<Invoice21File>(sql: consultar, param: parametros, commandType: CommandType.StoredProcedure);

            return registros;
        }
    }
}
