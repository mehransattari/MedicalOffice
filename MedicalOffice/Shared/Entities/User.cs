
using System.ComponentModel.DataAnnotations;


namespace MedicalOffice.Shared.Entities;

public class User
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
    [Required]
    public  string Password { get; set; }

    [Required]
    public  string NationalCode { get; set; }

    public long RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Status? Status { get; set; }

}
