using MedicalOffice.Client.Repositories.Inteface;
using MedicalOffice.Client.Services;
using MedicalOffice.Client.Services.Helper;
using MedicalOffice.Client.Services.Interface;
using MedicalOffice.Shared.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace MedicalOffice.Client.Pages;

public class LoginBase: ComponentBase
{
    #region Inject

    [Inject]
    public IAuthRepository authRepository { get; set; }

    [Inject]
    public IUserAuthService userAuthService { get; set; }

    [Inject]
    public UserStateService userStateService { get; set; }

    [Inject]
    public NavigationManager navigationManager { get; set; }

    [Inject]
    public GenerateNewToken generateNewToken { get; set; }

    [Inject]
    public IJSRuntime jSRuntime { get; set; }


    #endregion

    #region Data
    public UserData userData { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState> authenticationState { get; set; }

    public bool showError { get; set; }
    #endregion

    protected override async Task OnInitializedAsync()
    {
        var checkToken =  await userAuthService.CheckToken();

        if(checkToken)
        {
            navigationManager.NavigateTo("/");
        }

        userData = new();
    }

    public async Task LoginUser()
    {
        generateNewToken.Dispose();
        var response = await authRepository.Login(userData);
        var authentication = await authenticationState;

        if (response.Status)
        {
            generateNewToken.Initiate();

            await userAuthService.Login(response);
            userStateService.SetUserInfo(authentication.User.Claims.ToList());
            navigationManager.NavigateTo("/");
        }
        else
        {
            await Console.Out.WriteLineAsync(response.Message);
            showError = true;
        }
    }
}
