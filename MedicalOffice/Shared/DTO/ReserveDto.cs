
using MedicalOffice.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO
{
    public class ReserveDto
    {


        //UserInfo
        [Required(ErrorMessage ="لطفا نام خود را وارد نمائید.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد نمائید.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "لطفا موبایل خود را وارد نمائید.")]
        public string Mobile { get; set; }

        public string? Password { get; set; }

        [Required(ErrorMessage = "لطفا کد ملی خود را وارد نمائید.")]
        public string NationalCode { get; set; }

        public long RoleId { get; set; }

        //Reserve
        public long Id { get; set; }
        public long TimesReserveId { get; set; }
        public long UserId { get; set; }

        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public DateTime? Day { get; set; }

        public StatusEnum Status { get; set; }

        [Required(ErrorMessage = "لطفا  علت مراجعه را انتخاب نمائید.")]
        public ReserveTypeEnum ReserveType { get; set; }

        public int Number { get; set; }
    }
}
