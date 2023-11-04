

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


