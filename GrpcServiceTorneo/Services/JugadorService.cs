using AutoMapper;
using Datos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceTorneo.Protos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using Util;

namespace GrpcServiceTorneo.Services
{
    public class JugadorService : JugadorBF.JugadorBFBase
    {
        private readonly TorneoBDContext _db;
        private readonly IMapper _mapper;

        public JugadorService(TorneoBDContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async override Task<JugadorReply> GetJugador(JugadorCedulaRequest request, ServerCallContext context)
        {
            try { 
            var jugadorBuscado = await _db.Jugadores.SingleOrDefaultAsync(s => s.Cedula == request.Cedula);

            JugadorReply jr = new JugadorReply();

            if (jugadorBuscado == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "No se encontró el jugador con la cedula especificada"));
            }
            else
            {
                jr = new JugadorReply()
                {
                    Id = jugadorBuscado.Id,
                    Cedula = jugadorBuscado.Cedula,
                    Nombres = jugadorBuscado.Nombres,
                    Apellidos = jugadorBuscado.Apellidos,
                    FechaNacimiento = Timestamp.FromDateTime(DateTime.SpecifyKind(jugadorBuscado.FechaNacimiento, DateTimeKind.Utc))
                };
            }
            //DateTime now = jr.FechaNacimiento.ToDateTime();
            return await Task.FromResult(jr);

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }

        public override async Task<JugadorIdRequest> NuevoJugador(JugadorReply request, ServerCallContext context)
        {
            try
            {
                if (request == null) throw new RpcException(new Status(StatusCode.NotFound, "El Jugador está vacio"));
                if (!Util.Util.EsCedulaValida(request.Cedula)) throw new RpcException(new Status(StatusCode.NotFound, "La cedula no es válida"));
                if (!Validar(request)) throw new RpcException(new Status(StatusCode.NotFound, "Hay campos vacios revise"));
                

                int jugadorExiste = await _db.Jugadores.CountAsync(s => s.Cedula == request.Cedula);

                if (jugadorExiste > 0) throw new RpcException(new Status(StatusCode.Aborted, "El jugador ya existe en la base de datos"));

                Jugador jugadorNuevo = _mapper.Map<JugadorReply, Jugador>(request);

                var nuevo = await _db.AddAsync(jugadorNuevo);

                await _db.SaveChangesAsync();

                JugadorIdRequest result = new()
                {
                    Id = nuevo.Entity.Id
                };

                return await Task.FromResult(result);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool Validar(JugadorReply jugador)
        {
            bool validado = !string.IsNullOrEmpty(jugador.Cedula) & !string.IsNullOrEmpty(jugador.Nombres) &
                             !string.IsNullOrEmpty(jugador.Apellidos) & jugador.FechaNacimiento.ToDateTime() > DateTime.MinValue;

            return validado;
        }

    }
}
