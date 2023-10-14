

using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.Entities;

public class AboutUs 
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string? Image { get; set; }

    public string? Text { get; set; }
}
