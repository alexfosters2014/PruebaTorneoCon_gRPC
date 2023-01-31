using AutoMapper;
using Datos;
using Google.Protobuf.WellKnownTypes;
using GrpcServiceTorneo.Protos;

namespace GrpcServiceTorneo.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<DateTime,Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));
            CreateMap<Timestamp,DateTime>().ConvertUsing(x => x.ToDateTime());

            CreateMap<Jugador, JugadorReply>().ReverseMap();
            CreateMap<Equipo, EquipoReply>().ReverseMap();

        }
    }
}
