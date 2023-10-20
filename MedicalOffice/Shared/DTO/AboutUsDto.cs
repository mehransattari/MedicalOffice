
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class AboutUsDto
{
    public long Id { get; set; }

    public string Title { get; set; }

    public IFormFile? Image { get; set; }
    public string?  ImageUrl { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Text { get; set; }

    public int Number { get; set; }
}


