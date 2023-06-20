using AutoMapper;
using TFHKA.Adjuntos.listado.Application.Dto;
using TFHKA.Adjuntos.listado.Application.Interfaz;
using TFHKA.Adjuntos.listado.Domain.Entidad;
using TFHKA.Adjuntos.listado.Domain.Interfaz;
using TFHKA.Adjuntos.listado.Transversal.Comun;

namespace TFHKA.Adjuntos.listado.Application.Principal
{


    public class AdjuntosListadoApplication : IAdjuntosListadoApplication
    {
        private readonly IAdjuntosListadoDomainInterfaz _adjuntosListadoDomain;
        private readonly IMapper _mapeador;
        private Respuesta<IEnumerable<Invoice21FileDto>> lista;

        public AdjuntosListadoApplication(IAdjuntosListadoDomainInterfaz adjuntosListadoDomain, IMapper mapeador)
        {
            _adjuntosListadoDomain = adjuntosListadoDomain;
            _mapeador = mapeador;
        }

        public Respuesta<Invoice21Dto> ConsultaDocumentos(string documento, int Id_enterprise)
        {

            Respuesta<Invoice21Dto> respuesta = new Respuesta<Invoice21Dto>();
            try
            {
                Invoice21 consulta = _adjuntosListadoDomain.ConsultaDocumentos(documento, Id_enterprise);
                respuesta.Datos = _mapeador.Map<Invoice21Dto>(consulta);
                if (respuesta.Datos.Id != null)
                {
                    respuesta.Mensaje = "Consulta  exitosa";
                    respuesta.TraeDatos = true;
                    respuesta.EsExitosa = true;
                }
                if (respuesta.Datos == null)
                {
                    respuesta.Mensaje = "Consulta no exitosa";
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return (respuesta);
        }



        public Respuesta<IEnumerable<Invoice21FileDto>> ConsultaDocumentosAdjunto(int id_invoice)
        {


            Respuesta<IEnumerable<Invoice21FileDto>> respuestaDto = new Respuesta<IEnumerable<Invoice21FileDto>>();


            IEnumerable<Invoice21FileDto> listInvoiceFile = new List<Invoice21FileDto>();
            try
            {
                // IEnumerable<Invoice21File> respuesta = _adjuntosListadoDomain.ConsultaDocumentosAdjunto(id_invoice);


                IEnumerable<Invoice21File> consulta = _adjuntosListadoDomain.ConsultaDocumentosAdjunto(id_invoice);
                respuestaDto.Datos = _mapeador.Map<IEnumerable<Invoice21FileDto>>(consulta);

                if (respuestaDto.Datos.Count() > 0)
                {
                    respuestaDto.Mensaje = "Consulta  exitosa.";
                    respuestaDto.TraeDatos = true;
                    respuestaDto.EsExitosa = true;
                }
                else
                {
                    respuestaDto.Mensaje = "Consulta de Adjunto no exitosa.";
                    respuestaDto.TraeDatos = false;
                    respuestaDto.EsExitosa = false;
                    return respuestaDto;
                }
                if (respuestaDto.Datos.Count() == 0)
                {
                    respuestaDto.Mensaje = "Consulta no exitosa.";

                }
            }
            catch (Exception ex)
            {
                respuestaDto.Mensaje = ex.Message;
            }
            return respuestaDto;
        }
    }
}
