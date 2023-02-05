using AutoMapper;
using Datos;
using Grpc.Core;
using GrpcServiceTorneo.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceTorneo.Services
{
    public class EquipoService : EquipoBP.EquipoBPBase
    {
        private readonly TorneoBDContext _db;
        private readonly IMapper _mapper;

        public EquipoService(TorneoBDContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async override Task<EquipoReply> GetEquipo(EquipoIdRequest request, ServerCallContext context)
        {
           if (request == null) throw new RpcException(new Status(StatusCode.NotFound, "El Equipo está vacio"));

            var equipoBuscado = await _db.Equipos.SingleOrDefaultAsync(s => s.Id == request.Id);

            if (equipoBuscado == null) throw new RpcException(new Status(StatusCode.NotFound, "El Equipo buscado no existe"));

            EquipoReply result = _mapper.Map<Equipo,EquipoReply>(equipoBuscado);

            return await Task.FromResult(result);
        }

        public async override Task<NuevoEquipoConfirmReply> NewEquipo(EquipoReply request, ServerCallContext context)
        {
            try
            {
                if (request == null) throw new RpcException(new Status(StatusCode.NotFound, "El Equipo está vacio"));

                Equipo NuevoEquipo = _mapper.Map<EquipoReply, Equipo>(request);

                foreach (var jugador in NuevoEquipo.Jugadores)
                {
                    _db.Entry(jugador).State = EntityState.Modified;
                }

                var guardado = await _db.Equipos.AddAsync(NuevoEquipo);

                await _db.SaveChangesAsync();

                var result = new NuevoEquipoConfirmReply()
                {
                    Ok = true
                };

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


    }
}
