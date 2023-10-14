

using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.Entities;

public class ContactUs
{
    public long Id { get; set; }
    
    public string Title { get; set; }

    public string? Image { get; set; }

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
}
