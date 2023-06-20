using AutoMapper;
using TFHKA.Adjuntos.listado.Application.Dto;
using TFHKA.Adjuntos.listado.Domain.Entidad;

namespace TFHKA.Adjuntos.listado.Transversal.Mapeo
{
    public class PerfilMapeo : Profile
    {
        public PerfilMapeo()
        {
            CreateMap<Invoice21File, Invoice21FileDto>().ReverseMap();
            CreateMap<Invoice21, Invoice21Dto>().ReverseMap();
        }
    }
}