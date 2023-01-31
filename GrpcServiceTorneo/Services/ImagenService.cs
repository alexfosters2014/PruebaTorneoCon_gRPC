using Azure.Core;
using Azure.Storage.Blobs;
using Grpc.Core;
using GrpcServiceTorneo.Protos;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;

namespace GrpcServiceTorneo.Services
{
    public class ImagenService : ImagenBP.ImagenBPBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly string _keyAzureStorage;
        private readonly string _storageNameAzure;

        public ImagenService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
            _keyAzureStorage = _configuration.GetValue<string>("KeyAzureStorage");
            _storageNameAzure = _configuration.GetValue<string>("AzureStorageName");
        }
        public async override Task<MensajeRequest> UploadImage(ImageRequest request, ServerCallContext context)
        {
            try
            {
                var imagen = request.Image.ToByteArray();

                var imagenStream = new MemoryStream();
                await imagenStream.WriteAsync(imagen);
                imagenStream.Position = 0;


                string extension = request.Extension;
                string imageName = Guid.NewGuid().ToString() + "." + extension;

                BlobContainerClient blobContainer = new BlobContainerClient(_keyAzureStorage, _storageNameAzure);
                var blob = blobContainer.GetBlobClient(imageName);

                await blob.UploadAsync(imagenStream);

                string urlImagen = blobContainer.Uri.ToString();

                MensajeRequest mREquest = new MensajeRequest()
                {
                    Ruta = imageName
                };

                return await Task.FromResult(mREquest);

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }

        public async override Task<MensajeRequest> UpdateImage(ImageUpdateRequest request, ServerCallContext context)
        {
            try
            {
                var imagen = request.Image.ToByteArray();

                var imagenStream = new MemoryStream();
                await imagenStream.WriteAsync(imagen);
                imagenStream.Position = 0;


                string extension = request.NuevaExtension;
                string imageName = Guid.NewGuid().ToString() + "." + extension;

                BlobContainerClient blobContainer = new BlobContainerClient(_keyAzureStorage, _storageNameAzure);
                

                var blobAEliminar = blobContainer.GetBlobClient(request.NombreArchivo);
                await EliminarImagen(blobAEliminar);


                var blob = blobContainer.GetBlobClient(imageName);
                await blob.UploadAsync(imagenStream);

                string urlImagen = blobContainer.Uri.ToString();

                MensajeRequest mREquest = new MensajeRequest()
                {
                    Ruta = imageName
                };

                return await Task.FromResult(mREquest);

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
            }
        }

        public async Task EliminarImagen(BlobClient blob)
        {
            try
            {
                await blob.DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
