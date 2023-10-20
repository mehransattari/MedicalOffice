
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.DTO;

public class ContactUsDto
{
    public long Id { get; set; }

    public string Title { get; set; } 

    public IFormFile? Image { get; set; }

    public string? ImageUrl { get; set; }

    public string? Text { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(11)]
    public string? Mobile { get; set; }

    [MaxLength(400)]
    public string? Address1 { get; set; }

    [MaxLength(400)]
    public string? Address2 { get; set; }

    public string? Map { get; set; }

    public int Number { get; set; }
}
