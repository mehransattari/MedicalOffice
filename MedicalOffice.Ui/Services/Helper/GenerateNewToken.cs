using MedicalOffice.Ui.Services.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Timers;


namespace MedicalOffice.Ui.Services.Helper;

public class GenerateNewToken
{
    #region Constructor
    [CascadingParameter]
    public Task<AuthenticationState> AuthenticationState { get; set; }

    private readonly IUserAuthService _loginService;

    System.Timers.Timer timer;

    public GenerateNewToken(IUserAuthService loginService)
    {
        _loginService = loginService;
    }
    #endregion

    #region Methods
    public void Initiate()
    {
        timer = new System.Timers.Timer();
        timer.Interval = 10000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        if (!await _loginService.CheckToken())
        {
            await _loginService.CleanUp();
            Dispose();
        }
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
    #endregion
}
