using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tewr.Blazor.FileReader;
using MedicalOffice.Client.Helper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.AddBaseService();

builder.AddScopeService();

builder.AddJwtService();

builder.AddAnotherServices();

builder.Services.AddFileReaderService();

await builder.Build().RunAsync();
