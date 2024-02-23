using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace MedicalOffice.Client.Helper;

public static class BaseServicesExtension
{
    public static void AddBaseService(this WebAssemblyHostBuilder builder)
    {

        builder.RootComponents.Add<App>("#app");

        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        builder.Services.AddMudServices();

        builder.Services.AddOptions();

        builder.Services.AddAuthorizationCore();

    }
}
