using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcServiceTorneo.Protos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TorneusClient;
using TorneusClient.ServicesgRPC;
using MudBlazor.Services;
using Blazored.Modal;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

string backendUrlLocal = "https://localhost:7219"; 
string backendUrl = "https://some.external.url:12345";

builder.Services.AddScoped<EquipoService>();
builder.Services.AddScoped<JugadorService>();
builder.Services.AddScoped<ImagenService>();

builder.Services.AddSingleton(services =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress(backendUrlLocal, new GrpcChannelOptions { HttpClient = httpClient });
    return new ImagenBP.ImagenBPClient(channel);

});

builder.Services.AddSingleton(services =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress(backendUrlLocal, new GrpcChannelOptions { HttpClient = httpClient });
    return new EquipoBP.EquipoBPClient(channel);

});



builder.Services.AddSingleton(services =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress(backendUrlLocal, new GrpcChannelOptions { HttpClient = httpClient });
    return new JugadorBF.JugadorBFClient(channel);

});

await builder.Build().RunAsync();
