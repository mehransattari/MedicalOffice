

using System.ComponentModel.DataAnnotations;

namespace MedicalOffice.Shared.Entities;

public class Slider
{
    public long Id { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Image { get; set; }

    [MaxLength(1000)]
    public string? Desc { get; set; }

}


