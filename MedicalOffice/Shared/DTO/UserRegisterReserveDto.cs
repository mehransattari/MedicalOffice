

using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO
{
    public class UserRegisterReserveDto
    {

        [Required(ErrorMessage = "شماره همراه الزامی است.")]
        [RegularExpression(@"^09[0-9]{9}$", ErrorMessage = "شماره همراه وارد شده معتبر نیست.")]
        public  string Mobile { get; set; }

        [Required(ErrorMessage = "نام الزامی است.")]
        public  string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی الزامی است.")]
        public  string LastName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی باید 10 رقمی باشد.")]
        [Required(ErrorMessage = " کد ملی الزامی است.")]
        public string NationalCode { get; set; } 

    }
}
