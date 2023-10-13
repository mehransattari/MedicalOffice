
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class UserDto
{
    public long Id { get; set; }
    [Required(ErrorMessage ="لطفا نام را وارد نمائید.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمائید.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "لطفا موبایل را وارد نمائید.")]
    public string Mobile { get; set; }
 
    public string? Password { get; set; }

    public int Number { get; set; }

    public long RoleId { get; set; }

    public string? RoleName { get; set; }

}
