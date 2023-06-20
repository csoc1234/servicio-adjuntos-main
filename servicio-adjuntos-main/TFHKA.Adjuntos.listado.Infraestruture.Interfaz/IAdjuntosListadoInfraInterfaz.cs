using TFHKA.Adjuntos.listado.Domain.Entidad;

namespace TFHKA.Adjuntos.listado.Infraestruture.Interfaz
{
    public interface IAdjuntosListadoInfraInterfaz
    {
        IEnumerable<Invoice21File> ConsultaDocumentosAdjunto(int id_invoice);
        Invoice21 ConsultaDocumentos(string documento, int Id_enterprise);
    }
}
