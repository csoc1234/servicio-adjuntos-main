using TFHKA.Adjuntos.listado.Application.Dto;
using TFHKA.Adjuntos.listado.Transversal.Comun;

namespace TFHKA.Adjuntos.listado.Application.Interfaz
{
    public interface IAdjuntosListadoApplication
    {
        Respuesta<IEnumerable<Invoice21FileDto>> ConsultaDocumentosAdjunto(int id_invoice);
        Respuesta<Invoice21Dto> ConsultaDocumentos(string documento, int Id_enterprise);

    }
}
