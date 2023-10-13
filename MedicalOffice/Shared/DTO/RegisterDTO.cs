
using System.ComponentModel.DataAnnotations;


namespace MedicalOffice.Shared.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "وارد کردن  موبایل الزامی است")]
    [MaxLength(11)]
    public string Mobile { get; set; }

    [DataType(DataType.Password, ErrorMessage = "کلمه عبور را بدرستی وارد کنید")]
    [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی است")]
    public string Password { get; set; }
}
