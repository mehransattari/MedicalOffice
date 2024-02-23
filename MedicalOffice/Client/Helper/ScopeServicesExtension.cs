using MedicalOffice.Client.Repositories;
using MedicalOffice.Client.Repositories.Inteface;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MedicalOffice.Client.Helper;

public static class ScopeServicesExtension
{
    public static void AddScopeService(this WebAssemblyHostBuilder builder)
    {

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IRoleRepository, RoleRepository>();

        builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();

        builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();

        builder.Services.AddScoped<ISliderRepository, SliderRepository>();

        builder.Services.AddScoped<IProvidingServiceRepository, ProvidingServiceRepository>();

        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

        builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();

        builder.Services.AddScoped<IDaysReserveRepository, DaysReserveRepository>();

        builder.Services.AddScoped<ITimesRepository, TimesRepository>();

        builder.Services.AddScoped<IReserveRepository, ReserveRepository>();

    }
}
