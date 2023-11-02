using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class ProjectDto
{
    public long Id { get; set; }

    [MaxLength(200)]
    public string? Title { get; set; }

    public string? ImageUrl { get; set; }

    public IFormFile? Image { get; set; }

    [MaxLength(400)]
    public string? Desc { get; set; }
}
