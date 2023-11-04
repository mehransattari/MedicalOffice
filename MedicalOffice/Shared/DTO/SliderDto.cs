
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace MedicalOffice.Shared.DTO;

public class SliderDto
{
    public long Id { get; set; }

    [Required(ErrorMessage ="عنوان را وارد کنید.")]
    public string Title { get; set; }

    public IFormFile? Image { get; set; }

    public string? ImageUrl { get; set; }

    public string? Desc { get; set; }

    [MaxLength(200)]
    public string? Property1 { get; set; }

    [MaxLength(500)]
    public string? ShortDesc1 { get; set; }

    [MaxLength(200)]
    public string? Property2 { get; set; }

    [MaxLength(500)]
    public string? ShortDesc2 { get; set; }

    [MaxLength(200)]
    public string? Property3 { get; set; }

    [MaxLength(500)]
    public string? ShortDesc3 { get; set; }
}
