using MedicalOffice.Ui.Repositories;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services;
using MedicalOffice.Ui.Services.Helper;
using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace MedicalOffice.Shared.Helper;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IHttpService, HttpService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IFileUpload, FileUpload>();

        services.AddScoped<ProtectPassword>();
        services.AddSingleton<UserStateService>();
        services.AddScoped<GenerateNewToken>();
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IAboutUsRepository, AboutUsRepository>();
        services.AddScoped<IContactUsRepository, ContactUsRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();
        services.AddScoped<IProvidingServiceRepository, ProvidingServiceRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddScoped<IDaysReserveRepository, DaysReserveRepository>();
        services.AddScoped<ITimesRepository, TimesRepository>();
        services.AddScoped<IReserveRepository, ReserveRepository>();
        services.AddScoped<ISmsService, SmsService>();

        services.AddScoped<JWTService>();
        services.AddScoped<AuthenticationStateProvider, JWTService>(option => option.GetRequiredService<JWTService>());
        services.AddScoped<IUserAuthService, JWTService>(option => option.GetRequiredService<JWTService>());
    }
}
