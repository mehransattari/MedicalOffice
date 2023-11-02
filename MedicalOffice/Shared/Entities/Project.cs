
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.Entities;

public class Project
{
    public long Id { get; set; }

    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string Image { get; set; }

    [MaxLength(400)]
    public string? Desc { get; set; }
}
