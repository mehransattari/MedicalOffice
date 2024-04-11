using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;



var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

var typeSite = builder.Configuration["typeSite:adminsite"]; 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(typeSite) });

builder.Services.AddMudServices();

builder.Services.AddCustomServices(); // اضافه کردن سرویس‌های مخصوص

await builder.Build().RunAsync();

