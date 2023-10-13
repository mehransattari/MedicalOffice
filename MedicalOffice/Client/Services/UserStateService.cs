
using MedicalOffice.Shared.Helper;
using System.Security.Claims;

namespace MedicalOffice.Client.Services;

public class UserStateService
{
    #region Constructor
    public UserStateData user { get; set; }
    public UserStateData GetUserInfo()
    {
        return user;
    }
    #endregion

    #region Methods
    public void SetUserInfo(IEnumerable<Claim> claims)
    {
        user = new UserStateData
        {
            Mobile = claims.Where(p => p.Type == ClaimTypes.MobilePhone).Select(c => c.Value).FirstOrDefault(),
            RoleName = claims.Where(p => p.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault(),
            FirstName = claims.Where(p => p.Type == "FirstName").Select(c => c.Value).FirstOrDefault(),
            LastName = claims.Where(p => p.Type == "LastName").Select(c => c.Value).FirstOrDefault(),
            Id = claims.Where(p => p.Type == "UserId").Select(c => c.Value).FirstOrDefault()
        };
    }
    #endregion

}
