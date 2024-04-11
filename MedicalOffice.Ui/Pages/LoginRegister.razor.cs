using MedicalOffice.Shared.Helper;
using MedicalOffice.Ui.Repositories.Inteface;
using MedicalOffice.Ui.Services;
using MedicalOffice.Ui.Services.Helper;
using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace MedicalOffice.Ui.Pages;

public class LoginRegisterBase:ComponentBase
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

    protected override void OnInitialized()
    {
        userData = new();
    }

    public async Task LoginUser()
    {
        generateNewToken.Dispose();
        var response =  await  authRepository.Login(userData);
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
            await Console.Out.WriteLineAsync(  response.Message );
            showError = true;
        }
    }
}
