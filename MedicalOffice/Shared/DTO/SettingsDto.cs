

using Microsoft.AspNetCore.Http;

namespace MedicalOffice.Shared.DTO;

public class SettingsDto
{
    public long Id { get; set; }

    public string SiteName { get; set; }

    public string? ShortDescription { get; set; }
    
    public string? Logo { get; set; }

    public IFormFile? LogoFile { get; set; }

    public string? Instagram { get; set; }

    public string? Telegram { get; set; }

    public string? Whatsapp { get; set; }

}
