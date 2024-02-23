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
builder.Services.AddCustomServices(); // اضافه کردن سرویس‌های مخصوص


await builder.Build().RunAsync();

