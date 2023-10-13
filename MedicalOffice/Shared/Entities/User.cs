
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalOffice.Shared.Entities;

public class User
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "وارد کردن  موبایل الزامی است")]
    [MaxLength(11)]
    public string Mobile { get; set; }

    [DataType(DataType.Password, ErrorMessage = "کلمه عبور را بدرستی وارد کنید")]
    [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی است")]
    public string Password { get; set; } 
    public long RoleId { get; set; }
    public virtual Role? Role { get; set; }
    public virtual Status? Status { get; set; }

}
