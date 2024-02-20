
using MedicalOffice.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class ReserveDto
{

    #region User Fields
    //==================================//
    [Required(ErrorMessage ="لطفا نام خود را وارد نمائید.")]
    public string FirstName { get; set; }
    //==================================//
    [Required(ErrorMessage = "لطفا نام خانوادگی خود را وارد نمائید.")]
    public string LastName { get; set; }
    //==================================//
    [Required(ErrorMessage = "لطفا موبایل خود را وارد نمائید.")]
    [RegularExpression(@"^\(?((\+98|0)?9\d{9})$", ErrorMessage = "لطفا شماره موبایل صحیح را وارد نمایید .")]
    public string Mobile { get; set; }
    //==================================//
    public string? Password { get; set; }
    //==================================//
    [Required(ErrorMessage = "لطفا کد ملی خود را وارد نمائید.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "کد ملی صحیح نمی باشد.")]
    public string NationalCode { get; set; }
    //==================================//
    public long RoleId { get; set; }
    //==================================//

    public int Code { get; set; }
    //==================================//

    [RegularExpression(@"^[0-9]+$", ErrorMessage = "کد یکبارمصرف باید عدد باشد.")]
    public string? SingleUseCode { get; set; }
    //==================================//

    #endregion

    #region Reserve Fields    
    //==================================//
    public long Id { get; set; }
    //==================================//
    public long TimesReserveId { get; set; }
    //==================================//
    public long UserId { get; set; }
    //==================================//
    public TimeSpan? FromTime { get; set; }
    //==================================//
    public TimeSpan? ToTime { get; set; }
    //==================================//
    public DateTime? Day { get; set; }
    //==================================//
    public StatusEnum Status { get; set; }
    //==================================//
    [Required(ErrorMessage = "لطفا  علت مراجعه را انتخاب نمائید.")]
    public ReserveTypeEnum ReserveType { get; set; } = ReserveTypeEnum.Visit;
    //==================================//
    public int Number { get; set; }
    //==================================//


    #endregion
}
