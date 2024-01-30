using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;



public class SmsDto
{

    [Display(Name = "گیرنده")]
    public string To { get; set; }

    [Display(Name = "متن")]
    public string Text { get; set; }


}

