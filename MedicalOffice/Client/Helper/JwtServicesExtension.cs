using MedicalOffice.Client.Services.Helper;
using MedicalOffice.Client.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MedicalOffice.Client.Helper;

public static class JwtServicesExtension
{
    public static void AddJwtService(this WebAssemblyHostBuilder builder)
    {

        builder.Services.AddScoped<JWTService>();

        builder.Services.AddScoped<AuthenticationStateProvider, JWTService>(
            option => option.GetRequiredService<JWTService>()
        );

        builder.Services.AddScoped<IUserAuthService, JWTService>(
            option => option.GetRequiredService<JWTService>()
        );
    }
}
