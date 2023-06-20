using TFHKA.Adjuntos.listado.Domain.Entidad;

namespace TFHKA.Adjuntos.listado.Domain.Interfaz
{
    public interface IAdjuntosListadoDomainInterfaz
    {
        IEnumerable<Invoice21File> ConsultaDocumentosAdjunto(int id_invoice);
        Invoice21 ConsultaDocumentos(string documento, int Id_enterprise);
    }
}
