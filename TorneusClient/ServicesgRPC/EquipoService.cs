using GrpcServiceTorneo.Protos;

namespace TorneusClient.ServicesgRPC
{
    public class EquipoService
    {
        private readonly EquipoBP.EquipoBPClient _equipoClient;

        public EquipoService(EquipoBP.EquipoBPClient equipoClient)
        {
            _equipoClient = equipoClient;
        }

        public async Task<bool> CrearEquipo(EquipoReply equipo)
        {
            try
            {
                var equipoReply = await _equipoClient.NewEquipoAsync(equipo);

                return equipoReply.Ok;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}
