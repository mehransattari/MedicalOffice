using MedicalOffice.Shared.DTO;
using MedicalOffice.Shared.Entities;

namespace MedicalOffice.Shared.Helper.Mapper;

public static class RoleMapper
{
    public static Role Mapper(this RoleDto roleDto)
    {
        var _role = new Role()
        {
            Id = roleDto.Id,
            FaCaption= roleDto.FaCaption,
            EnCaption= roleDto.EnCaption
        };
        return _role;
    }
    public static RoleDto Mapper(this Role roleDto)
    {
        var _role = new RoleDto()
        {
            Id = roleDto.Id,
            FaCaption = roleDto.FaCaption,
            EnCaption = roleDto.EnCaption
        };
        return _role;
    }
}