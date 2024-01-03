using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui;
using MedicalOffice.Ui.Repositories;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services;
using MedicalOffice.Ui.Services.Helper;
using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7117/") });
builder.Services.AddMudServices();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();

builder.Services.AddScoped<ProtectPassword>();
builder.Services.AddSingleton<UserStateService>();
builder.Services.AddScoped<GenerateNewToken>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
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


builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTService>(
    option => option.GetRequiredService<JWTService>()
);
builder.Services.AddScoped<IUserAuthService, JWTService>(
    option => option.GetRequiredService<JWTService>()
);


await builder.Build().RunAsync();

