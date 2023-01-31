using Blazored.Modal;
using Grpc.Core;
using GrpcServiceTorneo.Protos;
using Microsoft.AspNetCore.Components.Forms;
using static GrpcServiceTorneo.Protos.JugadorBF;

namespace TorneusClient.ServicesgRPC
{
    public class JugadorService
    {
        private readonly JugadorBF.JugadorBFClient _jugadorBFClient;
        public JugadorService(JugadorBF.JugadorBFClient jugadorBPClient)
        {
            _jugadorBFClient = jugadorBPClient;

        }

        public async Task<JugadorReply> GetJugador(string cedula)
        {
            try
            {
                if (!Util.Util.EsCedulaValida(cedula)) throw new Exception("La cédula no es válida");


                JugadorCedulaRequest jugadorCedula = new JugadorCedulaRequest()
                {
                    Cedula = cedula
                };

                var jugador = await _jugadorBFClient.GetJugadorAsync(jugadorCedula);
                return jugador;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<JugadorIdRequest> AddNuevoJugador(JugadorReply jugador)
        {
            try
            {
                JugadorIdRequest reply = await _jugadorBFClient.NuevoJugadorAsync(jugador);
                return reply;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

   
    }
       
}
