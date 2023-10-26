

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class ProvidingServiceDto
{
    public long Id { get; set; }

    [Required(ErrorMessage ="عنوان را وارد نمائید.")]
    public string Title { get; set; }

    public IFormFile? Image { get; set; }
    
    public string? ImageUrl { get; set; }

    public string? ShortDesc { get; set; }

    public string? Desc { get; set; }
}
