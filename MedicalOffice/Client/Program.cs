using MedicalOffice.Client;
using MedicalOffice.Client.Services.Helper;
using MedicalOffice.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Repositories;
using MedicalOffice.Shared.Helper;
using Tewr.Blazor.FileReader;
using MedicalOffice.Client.Services.Interface;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ProtectPassword>();
builder.Services.AddSingleton<UserStateService>();
builder.Services.AddScoped<GenerateNewToken>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAboutUsRepository, AboutUsRepository>();
builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<AuthenticationStateProvider, JWTService>(
    option => option.GetRequiredService<JWTService>()
);
builder.Services.AddScoped<IUserAuthService, JWTService>(
    option => option.GetRequiredService<JWTService>()
);

builder.Services.AddFileReaderService();
await builder.Build().RunAsync();
