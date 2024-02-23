using MedicalOffice.Client.Repositories;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Client.Services.Helper;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MedicalOffice.Client.Helper;

public static class AnotherServicesExtension
{
    public static void AddAnotherServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IHttpService, HttpService>();

        builder.Services.AddScoped<IAuthRepository, AuthRepository>();

        builder.Services.AddScoped<IFileUpload, FileUpload>();

        builder.Services.AddScoped<ProtectPassword>();

        builder.Services.AddSingleton<UserStateService>();

        builder.Services.AddScoped<GenerateNewToken>();
    }
}
