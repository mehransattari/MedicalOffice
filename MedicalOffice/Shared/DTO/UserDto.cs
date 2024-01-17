
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class UserDto
{
    public long Id { get; set; }

    [MaxLength(100)]
    [Required]
    public  string FirstName { get; set; }

    [MaxLength(100)]
    [Required]
    public  string LastName { get; set; }

    [Required]
    [MaxLength(11)]
    public  string Mobile { get; set; }

    [DataType(DataType.Password, ErrorMessage = "کلمه عبور را بدرستی وارد کنید")] 
    public  string? Password { get; set; }

    [Required]
    public  string NationalCode { get; set; }

    public long RoleId { get; set; }

    public string? RoleName { get; set; }

    public int Number { get; set; }  

}
