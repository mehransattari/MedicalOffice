

namespace MedicalOffice.Shared.Entities;

/// <summary>
/// ساعات رزرو
/// </summary>
public class TimesReserve
{
    public long Id { get; set; }
    public long DaysReserveId { get; set; }
    public TimeSpan FromTime { get; set; }
    public TimeSpan ToTime { get; set; }
    public DaysReserve? DaysReserve { get; set; }
    public ICollection<Reservation>? Reservations { get; set; }
}
