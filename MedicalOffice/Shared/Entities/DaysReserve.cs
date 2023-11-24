

namespace MedicalOffice.Shared.Entities;

/// <summary>
///  روز های حضور
/// </summary>
public class DaysReserve
{
    public long Id { get; set; }

    public DateTime Day { get; set; }

    public ICollection<TimesReserve>? TimesReserves { get; set; }

}


