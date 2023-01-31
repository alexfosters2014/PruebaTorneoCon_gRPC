using Grpc.Core;
using GrpcServiceTorneo.Protos;
using Microsoft.AspNetCore.Components.Forms;
using static GrpcServiceTorneo.Protos.ImagenBP;

namespace TorneusClient.ServicesgRPC
{
    
    public class ImagenService
    {
        private readonly ImagenBP.ImagenBPClient _imagenBPClient;
        private readonly string _extension;
        private readonly string _format;
        public ImagenService(ImagenBP.ImagenBPClient imagenBPClient)
        {
            _imagenBPClient = imagenBPClient;
            _extension = "png";
            _format = $"image/{_extension}";

        }

        public async Task<string> SubirImagenFile(InputFileChangeEventArgs e)
        {
            try
            {
                MensajeRequest reply = new();

                foreach (var image in e.GetMultipleFiles(1))
                {
                    var imagenRedimensionado = await image.RequestImageFileAsync(_format, 200, 200);
                    //string extension = imagenRedimensionado.Name.Split(".").Last();
                    var buffer = new byte[imagenRedimensionado.Size];

                    await imagenRedimensionado.OpenReadStream().ReadAsync(buffer);

                    var request = new ImageRequest()
                    {
                        Image = Google.Protobuf.ByteString.CopyFrom(buffer),
                        Extension = _extension
                    };

                    reply = await _imagenBPClient.UploadImageAsync(request);

                    return reply.Ruta;

                }
                return reply.Ruta;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
        }

        public async Task<string> ActualizarImagenFile(InputFileChangeEventArgs e, string nombreArchivo)
        {
            try
            {
                MensajeRequest reply = new();

                foreach (var image in e.GetMultipleFiles(1))
                {
                    var imagenRedimensionado = await image.RequestImageFileAsync(_format, 200, 200);
                    var buffer = new byte[imagenRedimensionado.Size];

                    await imagenRedimensionado.OpenReadStream().ReadAsync(buffer);

                    var request = new ImageUpdateRequest()
                    {
                        Image = Google.Protobuf.ByteString.CopyFrom(buffer),
                        NuevaExtension = _extension,
                        NombreArchivo = nombreArchivo,
                    };

                    reply = await _imagenBPClient.UpdateImageAsync(request);

                    return reply.Ruta;

                }
                return reply.Ruta;

            }
            catch (RpcException ex)
            {
                throw new RpcException(Status.DefaultCancelled);
            }
        }



    }
}
