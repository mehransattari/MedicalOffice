using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class UserMapper
{
    public static User Mapper(this UserDto userDto)
    {
        var user = new User()
        {
            Id = userDto.Id,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Mobile = userDto.Mobile,
            RoleId = userDto.RoleId,
        };
        return user;
    }
    public static UserDto Mapper(this User user)
    {
        var _user = new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Mobile = user.Mobile,
            RoleId = user.RoleId,
        };
        return _user;
    }
}
