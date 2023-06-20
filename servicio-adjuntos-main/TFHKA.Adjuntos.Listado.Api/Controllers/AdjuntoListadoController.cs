
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceRest.Models;
using System.Diagnostics;
using TFHKA.Adjuntos.listado.Application.Interfaz;
using TFHKA.Adjuntos.Listado.Api.Models;

namespace TFHKA.Adjuntos.Listado.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class AdjuntoListadoController : Controller
    {
        private IConfiguration _configuration;

        private readonly IAdjuntosListadoApplication _adjuntosListadoApplication;
        public AdjuntoListadoController(IConfiguration configuration, IAdjuntosListadoApplication adjuntosListadoInfraApplication)
        {
            _configuration = configuration;
            _adjuntosListadoApplication = adjuntosListadoInfraApplication;
        }


        [HttpGet("/api/adjuntolistado/{documento}")]
        public async Task<IActionResult> ActAdjuntoListado([FromRoute] string documento)
        {
            ResponseInternal response = new ResponseInternal();

            string contextstr = HttpContext.User.FindFirst("context").Value;
            CustomJwtTokenContext context = CustomJwtTokenContext.FromJson(contextstr);


            Stopwatch timeT = new Stopwatch();
            timeT.Start();

            string Id_enterprise1 = context.User.EnterpiseId;
            try
            {
                int Id_enterprise0 = int.Parse(context.User.EnterpiseId);

                listado.Transversal.Comun.Respuesta<listado.Application.Dto.Invoice21Dto> repuesta = _adjuntosListadoApplication.ConsultaDocumentos(documento, Id_enterprise0);
                string id_enterprise2 = repuesta.Datos.IdEnterprise.ToString();
                if (Id_enterprise1 == id_enterprise2)
                {

                    if (repuesta.Datos.Id != null)
                    {
                        response.Code = 200;
                        response.Message = "Con registros.";

                    }
                    else
                    {
                        response.Code = 413;
                        response.Message = "No hay registros.";
                        return Ok(response);
                    }
                }

                else
                {
                    response.Code = 166;
                    response.Message = " El documento indicado no se encuentra asociado a su cuenta; se recomienda validar la información definida.";

                    return Ok(response);
                }
                int id = repuesta.Datos.Id;
                listado.Transversal.Comun.Respuesta<IEnumerable<listado.Application.Dto.Invoice21FileDto>> respuesta1 = _adjuntosListadoApplication.ConsultaDocumentosAdjunto(id);
                if (respuesta1.Datos.Count() > 0)
                {
                    respuesta1.Mensaje = "Consulta de Adjunto no exitosa.";
                    respuesta1.TraeDatos = false;
                    respuesta1.EsExitosa = false;

                }

                return Ok(respuesta1.Datos);
            }
            catch (Exception en)
            {

                response.Code = 166;
                response.Message = "El documento indicado no se encuentra asociado a su cuenta; se recomienda validar la información definida";

                return Ok(response);
            }

        }
    }
}

