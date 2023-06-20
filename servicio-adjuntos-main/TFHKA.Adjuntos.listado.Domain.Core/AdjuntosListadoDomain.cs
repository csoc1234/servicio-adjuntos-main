using TFHKA.Adjuntos.listado.Domain.Entidad;
using TFHKA.Adjuntos.listado.Domain.Interfaz;
using TFHKA.Adjuntos.listado.Infraestruture.Interfaz;

namespace TFHKA.Adjuntos.listado.Domain.Core
{
    public class AdjuntosListadoDomain : IAdjuntosListadoDomainInterfaz
    {

        private readonly IAdjuntosListadoInfraInterfaz _adjuntosListadoInfraInterfaz;

        public AdjuntosListadoDomain(IAdjuntosListadoInfraInterfaz adjuntosListadoInfraInterfaz)
        {
            _adjuntosListadoInfraInterfaz = adjuntosListadoInfraInterfaz;
        }

        public Invoice21 ConsultaDocumentos(string documento, int Id_enterprise1)
        {
            return _adjuntosListadoInfraInterfaz.ConsultaDocumentos(documento, Id_enterprise1);
        }

        public IEnumerable<Invoice21File> ConsultaDocumentosAdjunto(int documento)
        {
            return _adjuntosListadoInfraInterfaz.ConsultaDocumentosAdjunto(documento);
        }

    }
}
