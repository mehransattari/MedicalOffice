

using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class RoleDto
{
    public long Id { get; set; }

    [Required(ErrorMessage = "لطفا عنوان فارسی نقش را وارد نمائید.")]
    public string FaCaption { get; set; }

    [Required(ErrorMessage = "لطفا عنوان انگلیسی نقش را وارد نمائید.")]
    public string EnCaption { get; set; }

    public int Number { get; set; }
}
